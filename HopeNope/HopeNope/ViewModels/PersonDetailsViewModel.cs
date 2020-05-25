using GuidFramework.Extensions;
using GuidFramework.Services;
using HopeNope.Classes;
using HopeNope.Entities;
using HopeNope.Properties;
using HopeNope.ViewModels.Base;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.IO;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace HopeNope.ViewModels
{
	/// <summary>
	/// PersonDetails Viewmodel
	/// </summary>
	/// <seealso cref="HopeNope.ViewModels.Base.HopeNopeViewModel" />
	public class PersonDetailsViewModel : HopeNopeViewModel
	{
		private bool isDefaultPicture = true;
		private bool isLoadingProfilePicture = false;

		private byte[] imageData;
		private MediaFile photo;
		private ImageSource profilePicture;

		private Timer refreshTimer;

		private Person person;

		/// <summary>
		/// Gets or sets the person.
		/// </summary>
		/// <value>
		/// The person.
		/// </value>
		public Person Person
		{
			get
			{
				return person;
			}
			set
			{
				person = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is loading profile picture.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is loading profile picture; otherwise, <c>false</c>.
		/// </value>
		public bool IsLoadingProfilePicture
		{
			get { return isLoadingProfilePicture; }
			set
			{
				isLoadingProfilePicture = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance has profile picture.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has profile picture; otherwise, <c>false</c>.
		/// </value>
		public bool HasProfilePicture
		{
			get
			{
				return ProfilePicture != null && !isDefaultPicture;
			}
		}

		/// <summary>
		/// Gets or sets the current image.
		/// </summary>
		/// <value>
		/// The current image.
		/// </value>
		public ImageSource ProfilePicture
		{
			get
			{
				return profilePicture;
			}
			set
			{
				profilePicture = value;

				OnPropertyChanged();
				OnPropertyChanged(nameof(HasProfilePicture));
			}
		}

		/// <summary>
		/// Gets the edit profile picture command.
		/// </summary>
		/// <value>
		/// The edit profile picture command.
		/// </value>
		public ICommand EditProfilePictureCommand => new Command(EditProfilePictureAsync, CanExecuteCommands);

		/// <summary>
		/// Initializes this instance.
		/// <para>Sets IsInitialized to true</para>
		/// </summary>
		public override void Init()
		{
			LoadProfilePicture();

			InitializeTimer();

			base.Init();
		}

		/// <summary>
		/// Initializes the timer.
		/// </summary>
		private void InitializeTimer()
		{
			if (refreshTimer == null && Person != null && !Person.IsUnlocked)
			{
				refreshTimer = new Timer(1000);
				refreshTimer.Elapsed += (s, e) =>
				{
					Person.Refresh();

					if (Person.IsUnlocked)
						refreshTimer.Stop();
				};

				refreshTimer.Start();
			}
		}

		/// <summary>
		/// Edits the profile picture asynchronous.
		/// </summary>
		private async void EditProfilePictureAsync()
		{
			// Check if the device is compatible
			if (!CrossMedia.IsSupported)
				await AlertHandler.DisplayAlertAsync(Resources.AlertTitleActionNotSupported, Resources.AlertMessageActionNotSupported, Resources.Ok);
			else if (await CrossMedia.Current.Initialize())
			{
				photo = null;

				string result = await AlertHandler.DisplayActionSheetAsync(Resources.EditPicture, Resources.Cancel, null, Resources.Camera, Resources.Gallery);

				// Error handling for the actionsheet
				if (!result.IsNullOrWhiteSpace() && !result.Equals(Resources.Cancel))
				{
					if (result.Equals(Resources.Camera))
					{
						PermissionStatus cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>();

						if (cameraStatus != PermissionStatus.Granted)
							cameraStatus = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();

						if (cameraStatus == PermissionStatus.Granted)
						{
							photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
							{
								Directory = ApplicationConstants.PictureFolder,
								Name = $"{Person.Id}.jpg"
							}); ;
						}
						else
							await AlertHandler.DisplayAlertAsync(Resources.AlertTitleCameraPermissionNeeded, Resources.AlertMessageCameraPermissionNeeded, Resources.Ok);
					}
					else if (result.Equals(Resources.Gallery))
					{
						PermissionStatus photoStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<PhotosPermission>();

						if (photoStatus != PermissionStatus.Granted)
							photoStatus = await CrossPermissions.Current.RequestPermissionAsync<PhotosPermission>();

						if (photoStatus == PermissionStatus.Granted)
							photo = await CrossMedia.Current.PickPhotoAsync();
						else
							await AlertHandler.DisplayAlertAsync(Resources.AlertTitleGalleryPermissionNeeded, Resources.AlertMessageGalleryPermissionNeeded, Resources.Ok);
					}

					if (photo != null)
					{
						// Set the profilepicture as imagedata
						LoadProfilePicture();
					}
				}
			}
		}

		/// <summary>
		/// Loads the profile picture.
		/// </summary>
		private async void LoadProfilePicture()
		{
			IsLoadingProfilePicture = true;

			if (Person != null)
			{
				IFileService fileService = DependencyService.Get<IFileService>();
				imageData = await fileService.OpenFromInternalStorageAsync($"{Person.Id}.jpg", ApplicationConstants.PictureFolder);
				isDefaultPicture = false;

				if (imageData != null && imageData.Length > 0)
					ProfilePicture = ImageSource.FromStream(() => { return new MemoryStream(imageData); });
				else
					LoadDefaultPicture();
			}
			else
				LoadDefaultPicture();

			IsLoadingProfilePicture = false;

			// Loads the default picture
			void LoadDefaultPicture()
			{
				ProfilePicture = ImageSource.FromFile(ApplicationConstants.DefaultPictureFilename);
				isDefaultPicture = true;
			}
		}
	}
}
