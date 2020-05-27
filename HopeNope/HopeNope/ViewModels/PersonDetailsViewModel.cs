using Autofac;
using GuidFramework.Extensions;
using GuidFramework.Interfaces;
using GuidFramework.Services;
using HopeNope.Classes;
using HopeNope.Entities;
using HopeNope.Properties;
using HopeNope.ViewModels.Base;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Timers;
using System.Windows.Input;
using Xamarin.Essentials;
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
		private ILocalStorageHandler localStorageHandler;

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
		/// Gets the set reminder command.
		/// </summary>
		/// <value>
		/// The set reminder command.
		/// </value>
		public ICommand SetReminderCommand => new Command(SetReminderAsync, CanExecuteCommands);

		/// <summary>
		/// Gets the delete command.
		/// </summary>
		/// <value>
		/// The delete command.
		/// </value>
		public ICommand DeleteCommand => new Command(DeleteAsync, CanExecuteCommands);

		/// <summary>
		/// Initializes this instance.
		/// <para>Sets IsInitialized to true</para>
		/// </summary>
		public override void Init()
		{
			LoadProfilePicture();

			InitializeTimer();

			using (ILifetimeScope scope = App.Container.BeginLifetimeScope())
			{
				localStorageHandler = scope.Resolve<ILocalStorageHandler>();
			}

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

				string result = await AlertHandler.DisplayActionSheetAsync(Resources.FacialRecognition, Resources.Cancel, null, Resources.Camera, Resources.Gallery);

				// Error handling for the actionsheet
				if (!result.IsNullOrWhiteSpace() && !result.Equals(Resources.Cancel))
				{
					if (result.Equals(Resources.Camera))
					{
						PermissionStatus cameraStatus = await Permissions.CheckStatusAsync<Permissions.Camera>();

						if (cameraStatus != PermissionStatus.Granted)
							cameraStatus = await Permissions.RequestAsync<Permissions.Camera>();

						if (cameraStatus == PermissionStatus.Granted)
						{
							photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions()
							{
								Directory = ApplicationConstants.PictureFolder,
								Name = $"{DateTime.Now.ToFileTimeUtc()}.jpg"
							});

							// Rename the picture if it was taken
							if (photo != null && File.Exists(photo.Path))
							{
								byte[] existing = File.ReadAllBytes(photo.Path);

								IFileService fileService = DependencyService.Get<IFileService>();
								person.ProfilePicturePath = fileService.SaveFileToInternalStorage(existing, $"{person.Id}.jpg", ApplicationConstants.PictureFolder);

								// Save the person with the profile picture's path
								if (await localStorageHandler.SaveAsync(person))
									File.Delete(photo.Path);
							}
						}
						else
							await AlertHandler.DisplayAlertAsync(Resources.AlertTitleCameraPermissionNeeded, Resources.AlertMessageCameraPermissionNeeded, Resources.Ok);
					}
					else if (result.Equals(Resources.Gallery))
					{
						PermissionStatus photoStatus = await Permissions.CheckStatusAsync<Permissions.Photos>();

						if (photoStatus != PermissionStatus.Granted)
							photoStatus = await Permissions.RequestAsync<Permissions.Photos>();

						if (photoStatus == PermissionStatus.Granted)
						{
							photo = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions() { SaveMetaData = true });

							if (photo != null && File.Exists(photo.Path))
							{
								// Copy the picture
								byte[] existing = File.ReadAllBytes(photo.Path);

								IFileService fileService = DependencyService.Get<IFileService>();
								person.ProfilePicturePath = fileService.SaveFileToInternalStorage(existing, $"{person.Id}.jpg", ApplicationConstants.PictureFolder);

								// Save the person
								await localStorageHandler.SaveAsync(person);
							}
						}
						else
							await AlertHandler.DisplayAlertAsync(Resources.AlertTitleGalleryPermissionNeeded, Resources.AlertMessageGalleryPermissionNeeded, Resources.Ok);
					}

					// Set the profilepicture as imagedata
					if (photo != null)
						LoadProfilePicture();
				}
			}
		}

		/// <summary>
		/// Sets the reminder asynchronous.
		/// </summary>
		private async void SetReminderAsync()
		{
			PermissionStatus calenderStatus = await Permissions.CheckStatusAsync<Permissions.CalendarWrite>();

			if (calenderStatus != PermissionStatus.Granted)
				calenderStatus = await Permissions.RequestAsync<Permissions.CalendarWrite>();

			if (calenderStatus == PermissionStatus.Granted)
			{
				ICalendarService calendarService = DependencyService.Get<ICalendarService>();
				calendarService.CreateCalendarItem(Resources.CalendarItemTitleNopeIsHope.FormatInvariant(Person.DisplayName),
												   Resources.CalendarItemDescriptionNopeIsHope.FormatInvariant(Person.DisplayName),
												   Person.UnlockDate);
			}
		}

		/// <summary>
		/// Deletes the asynchronous.
		/// </summary>
		/// <exception cref="NotImplementedException"></exception>
		private async void DeleteAsync()
		{
			if (await AlertHandler.DisplayAlertAsync(Resources.AlertTitleAreYouSure, Resources.AlertMessageAreYouSure, Resources.Ok, Resources.Cancel))
			{
				if (!Person.ProfilePicturePath.IsNullOrWhiteSpace())
				{
					// Check storage permissions first
					PermissionStatus storageStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

					if (storageStatus != PermissionStatus.Granted)
						storageStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();

					if (storageStatus == PermissionStatus.Granted)
					{
						// Remove the picture

						IFileService fileService = DependencyService.Get<IFileService>();
						fileService.DeleteFileFromInternalStorage(Person.ProfilePicturePath);
					}
				}

				await localStorageHandler.DeleteAsync(Person);

				await NavigationService.CloseAsync();

				InitializeParent();
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
