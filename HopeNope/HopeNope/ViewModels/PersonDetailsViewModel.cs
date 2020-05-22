using GuidFramework.Services;
using HopeNope.Classes;
using HopeNope.Entities;
using HopeNope.ViewModels.Base;
using Plugin.Media.Abstractions;
using System.IO;
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

		private ImageSource profilePicture;

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
		/// Initializes this instance.
		/// <para>Sets IsInitialized to true</para>
		/// </summary>
		public override void Init()
		{
			LoadProfilePicture();
			base.Init();
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
