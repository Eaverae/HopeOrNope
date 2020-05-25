using Autofac;
using ExifLib;
using GuidFramework;
using GuidFramework.Extensions;
using GuidFramework.Handlers;
using GuidFramework.Interfaces;
using GuidFramework.Services;
using GuidFramework.ValidationRules;
using HopeNope.Classes;
using HopeNope.Entities;
using HopeNope.Extensions;
using HopeNope.Handlers;
using HopeNope.Properties;
using HopeNope.ViewModels.Base;
using HopeNope.Views;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using Xamarin.Forms;

namespace HopeNope.ViewModels
{
	/// <summary>
	/// CalculatorViewModel
	/// </summary>
	/// <seealso cref="HopeNope.ViewModels.BaseViewModel" />
	public class CalculatorViewModel : HopeNopeViewModel, IValidatableViewModel
	{
		private readonly IValidationHandler validationHandler;
		private ValidatableObject<string> name = new ValidatableObject<string>();

		private int maxAds = new Random().Next(2, 5);

		private byte[] imageData;
		private MediaFile photo;
		private ImageSource profilePicture;
		private CalculatedResult calculatedResult;

		private string firstAgeInput;
		private string secondAgeInput;
		private bool hope;
		private bool isWizardInitialized;
		private ILocalStorageHandler localStorageHandler;
		private const double minimumWishListAge = 18;

		/// <summary>
		/// Gets the first age.
		/// </summary>
		/// <value>
		/// The first age.
		/// </value>
		private double FirstAge
		{
			get
			{
				double firstAge;

				if (!double.TryParse(firstAgeInput, out firstAge))
					firstAge = 0;

				return firstAge;
			}
		}

		/// <summary>
		/// Gets the second age.
		/// </summary>
		/// <value>
		/// The second age.
		/// </value>
		private double SecondAge
		{
			get
			{
				double secondAge;

				if (!double.TryParse(secondAgeInput, out secondAge))
					secondAge = 0;

				return secondAge;
			}
		}

		/// <summary>
		/// Returns a boolean value that indicates whether or not there is a profile picture
		/// </summary>
		public bool HasProfilePicture
		{
			get
			{
				return ProfilePicture != null;
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
		/// Gets a value indicating whether this instance has default age.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has default age; otherwise, <c>false</c>.
		/// </value>
		public bool HasDefaultAge
		{
			get
			{
				return Settings.HasDefaultAge;
			}
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public ValidatableObject<string> Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets a value indicating whether [wishlist enabled].
		/// </summary>
		/// <value>
		///   <c>true</c> if [wishlist enabled]; otherwise, <c>false</c>.
		/// </value>
		public bool WishlistEnabled
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the first age.
		/// </summary>
		/// <value>
		/// The first age.
		/// </value>
		public string FirstAgeInput
		{
			get
			{
				return firstAgeInput;
			}
			set
			{
				firstAgeInput = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the second age.
		/// </summary>
		/// <value>
		/// The second age.
		/// </value>
		public string SecondAgeInput
		{
			get
			{
				return secondAgeInput;
			}
			set
			{
				secondAgeInput = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="CalculatorViewModel"/> is hope.
		/// </summary>
		/// <value>
		///   <c>true</c> if hope; otherwise, <c>false</c>.
		/// </value>
		public bool Hope
		{
			get
			{
				return hope;
			}
			set
			{
				hope = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// The calculate command
		/// </summary>
		public ICommand CalculateCommand => new Command(DetermineHopeOrNope, CanExecuteCommands);

		/// <summary>
		/// Gets the reset command.
		/// </summary>
		/// <value>
		/// The reset command.
		/// </value>
		public ICommand ResetCommand => new Command(Reset, CanExecuteCommands);

		/// <summary>
		/// Gets the finish command.
		/// </summary>
		/// <value>
		/// The finish command.
		/// </value>
		public ICommand FinishCommand => new Command(Finish, CanExecuteCommands);

		/// <summary>
		/// Gets the add person command.
		/// </summary>
		/// <value>
		/// The add person command.
		/// </value>
		public ICommand AddPersonCommand => new Command(AddPersonAsync, () =>
		{
			return CanExecuteCommands() &&
				calculatedResult?.UserAge >= minimumWishListAge &&
				calculatedResult?.CompareAge >= minimumWishListAge;
		});

		/// <summary>
		/// Gets the determine age command.
		/// </summary>
		/// <value>
		/// The determine age command.
		/// </value>
		public ICommand DetermineAgeCommand => new Command(DetermineAge, CanExecuteCommands);

		/// <summary>
		/// Gets the select first tab command.
		/// </summary>
		/// <value>
		/// The select first tab command.
		/// </value>
		public ICommand SelectFirstTabCommand => new Command(SelectFirstTab, CanExecuteCommands);

		/// <summary>
		/// Gets the select second tab command.
		/// </summary>
		/// <value>
		/// The select second tab command.
		/// </value>
		public ICommand SelectSecondTabCommand => new Command(SelectSecondTab, CanExecuteCommands);

		/// <summary>
		/// Gets the statistics command.
		/// </summary>
		/// <value>
		/// The statistics command.
		/// </value>
		public ICommand StatisticsCommand => new Command(async () =>
		{
			await NavigationService.NavigateAsync<StatsViewModel>(animated: false);
		});

		/// <summary>
		/// Initializes a new instance of the <see cref="CalculatorViewModel"/> class.
		/// </summary>
		public CalculatorViewModel()
		{
			using (ILifetimeScope scope = App.Container.BeginLifetimeScope())
			{
				localStorageHandler = scope.Resolve<ILocalStorageHandler>();
				validationHandler = scope.ResolveValidationHandlerWithParameters();
			}
		}

		/// <summary>
		/// Initializes this instance.
		/// <para>Sets IsInitialized to true</para>
		/// </summary>
		public override void Init()
		{
			if (HasDefaultAge)
				FirstAgeInput = Settings.DefaultAge.ToString();

			AddValidationRules();

			base.Init();
		}

		/// <summary>
		/// Appearing method
		/// <para>This method will be called when the page appears</para>
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		public override void OnAppearing(object sender, EventArgs e)
		{
			base.OnAppearing(sender, e);

			// This code may only execute when the wizard first appears.
			if (HasDefaultAge && !isWizardInitialized)
			{
				GuidFramework.Services.NavigationService.MultipageSetSelectedItem<WizardPage2>();
				isWizardInitialized = true;
			}
		}

		/// <summary>
		/// Adds the person asynchronous.
		/// </summary>
		private async void AddPersonAsync()
		{
			if (await ValidateAsync() && calculatedResult != null && calculatedResult.UserAge > minimumWishListAge && WishlistEnabled)
			{
				PermissionStatus storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();

				if (storageStatus != PermissionStatus.Granted)
					storageStatus = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();

				if (storageStatus == PermissionStatus.Granted)
				{
					Person person = calculatedResult.ToPerson(Name.Value);

					bool result = await localStorageHandler.SaveAsync(person);

					// Rename the picture if it was taken
					if (photo != null && File.Exists(photo.Path))
					{
						byte[] existing = File.ReadAllBytes(photo.Path);

						IFileService fileService = DependencyService.Get<IFileService>();
						fileService.SaveFileToInternalStorage(existing, $"{person.Id}.jpg", ApplicationConstants.PictureFolder);

						File.Delete(photo.Path);
					}

					if (result)
						await ToastHandler.ShowSuccessMessageAsync(Resources.ToastMessageAddToWishlistSuccess);

					Name.Value = string.Empty;
				}
				else
					await AlertHandler.DisplayAlertAsync(Resources.AlertTitleStoragePermissionNeeded, Resources.AlertMessageStoragePermissionNeeded, Resources.Ok);

				WishlistEnabled = false;
				OnPropertyChanged(nameof(WishlistEnabled));
			}
		}

		/// <summary>
		/// Determines the age.
		/// </summary>
		private void DetermineAge()
		{
			EditProfilePictureAsync();
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
				DateTime? fileDateTime = null;

				string result = await AlertHandler.DisplayActionSheetAsync(Resources.FacialRecognition, Resources.Cancel, null, Resources.Camera, Resources.Gallery);

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
								Name = $"{DateTime.Now.ToFileTimeUtc()}.jpg"
							});
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
							photo = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions() { SaveMetaData = true });
						else
							await AlertHandler.DisplayAlertAsync(Resources.AlertTitleGalleryPermissionNeeded, Resources.AlertMessageGalleryPermissionNeeded, Resources.Ok);

						if (photo != null)
						{
							ExifReader exifReader = new ExifReader(photo.Path);
							DateTime exifDate;

							if (exifReader.GetTagValue(ExifTags.DateTimeOriginal, out exifDate))
								fileDateTime = exifDate;

							if (!fileDateTime.HasValue)
								fileDateTime = new FileInfo(photo.Path).CreationTime;
						}
					}

					SetProfilePictureAsync(fileDateTime);
				}
			}
		}

		/// <summary>
		/// Sets the profile picture asynchronous
		/// </summary>
		private async void SetProfilePictureAsync(DateTime? pictureDate = null)
		{
			bool showPictureDateMessage = false;

			if (photo != null)
			{
				IsLoading = true;

				// Set the profilepicture as imagedata
				using (MemoryStream photoStream = new MemoryStream())
				{
					// Copy the stream to cache the data
					await photo.GetStream().CopyToAsync(photoStream);
					imageData = photoStream.ToArray();
				}

				if (imageData != null)
				{
					string analysisResult = await FaceHandler.MakeAnalysisRequestAsync(imageData);

					if (!analysisResult.IsNullOrWhiteSpace())
					{
						try
						{
							List<FaceAnalysis> faceGroup = JsonConvert.DeserializeObject<List<FaceAnalysis>>(analysisResult);

							FaceAnalysis analysis = faceGroup.FirstOrDefault();

							if (analysis != null)
							{
								// Take the date of the picture into account when needed
								long age = analysis.FaceAttributes.Age;

								if (pictureDate.HasValue)
								{
									showPictureDateMessage = true;
									DateTime current = DateTime.Now;

									int year = new DateTime(current.Subtract(pictureDate.Value).Ticks).Year - 1;

									if (year > 0)
										age += year;
								}

								SecondAgeInput = age.ToString();
							}

						}
						catch (Exception ex)
						{
							LogHandler.LogException(ex);
						}
					}
				}

				ProfilePicture = ImageSource.FromStream(() =>
				{
					return new MemoryStream(imageData);
				});

				if (showPictureDateMessage)
					await AlertHandler.DisplayAlertAsync(Resources.AlertTitlePictureDateTakenIntoAccount, Resources.AlertMessagePictureDateTakenIntoAccount, Resources.Ok);

				IsLoading = false;
			}
		}

		/// <summary>
		/// Determines the hope or nope.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <exception cref="NotImplementedException"></exception>
		private async void DetermineHopeOrNope()
		{
			if (SecondAgeInput.IsNullOrWhiteSpace())
				await ToastHandler.ShowErrorMessageAsync(Resources.ToastMessageInputAgeEmpty);
			else if (SecondAge <= 0)
				await ToastHandler.ShowErrorMessageAsync(Resources.ToastMessageInputAgeInvalidInput);
			else if (SecondAge < Settings.MinimumAgeThreshold)
				await ToastHandler.ShowErrorMessageAsync(Resources.ToastMessageAgeTooLow);
			else
			{
				Hope = Calculator.DetermineHopeOrNope(FirstAge, SecondAge);
				calculatedResult = new CalculatedResult()
				{
					UserAge = FirstAge,
					CompareAge = SecondAge,
					DeterminedDate = DateTime.Now,
					Verdict = Hope
				};

				// Add the result as a statistic
				Settings.SaveStatistic(calculatedResult);

				WishlistEnabled = FirstAge >= minimumWishListAge && SecondAge >= minimumWishListAge;
				OnPropertyChanged(nameof(WishlistEnabled));

				if (AdsEnabled && maxAds > 0)
				{
					AdHandler.ShowFullScreenAd(BannerAdId, Resources.Loading, Resources.Continue, SecondBannerAdId, () =>
					{
						NavigateToResult();
						maxAds--;
					});
				}
				else
					NavigateToResult();

				// Local function for navigation
				void NavigateToResult()
				{
					// View the result
					GuidFramework.Services.NavigationService.MultipageSetSelectedItem<ResultWizardPage>();

					OnPropertyChanged(nameof(AddPersonCommand));
				}
			}
		}

		/// <summary>
		/// Selects the first tab.
		/// </summary>
		private void SelectFirstTab()
		{
			GuidFramework.Services.NavigationService.MultipageSetSelectedItem<WizardPage1>();
		}

		/// <summary>
		/// Selects the second tab.
		/// </summary>
		private async void SelectSecondTab()
		{
			if (FirstAgeInput.IsNullOrWhiteSpace())
				await ToastHandler.ShowErrorMessageAsync(Resources.ToastMessageInputAgeEmpty);
			else if (FirstAge <= 0)
				await ToastHandler.ShowErrorMessageAsync(Resources.ToastMessageInputAgeInvalidInput);
			else if (FirstAge < Settings.MinimumAgeThreshold)
				await ToastHandler.ShowErrorMessageAsync(Resources.ToastMessageAgeTooLow);
			else
			{
				if (AdsEnabled && maxAds > 0)
				{
					AdHandler.ShowFullScreenAd(BannerAdId, Resources.Loading, Resources.Continue, SecondBannerAdId, () =>
					{
						NavigateToSecondTab();
						maxAds--;
					});
				}
				else
					NavigateToSecondTab();

				// Local function for navigation
				void NavigateToSecondTab()
				{
					GuidFramework.Services.NavigationService.MultipageSetSelectedItem<WizardPage2>();
				}
			}
		}

		/// <summary>
		/// Resets this instance.
		/// </summary>
		private void Reset()
		{
			SecondAgeInput = string.Empty;
			ProfilePicture = null;
			WishlistEnabled = false;

			calculatedResult = null;

			SelectSecondTab();
		}

		/// <summary>
		/// Finishes this instance.
		/// </summary>
		private async void Finish()
		{
			bool exitWizard = !WishlistEnabled;

			if (!exitWizard && Name.HasValue && !Name.Value.IsNullOrWhiteSpace())
				exitWizard = await AlertHandler.DisplayAlertAsync(Resources.AlertTitleUnsavedChanges, Resources.AlertMessagePersonNotAddedToWishlist, Resources.Ok, Resources.Cancel);

			if (exitWizard)
			{
				SecondAgeInput = string.Empty;
				ProfilePicture = null;
				WishlistEnabled = false;

				calculatedResult = null;

				BackAsync();
			}
		}

		/// <summary>
		/// Adds the validation rules.
		/// </summary>
		public void AddValidationRules()
		{
			name.ValidationRules.Add(new IsNullOrWhiteSpaceRule<string>() { ValidationMessage = Resources.ValidationInvalidName });
		}

		/// <summary>
		/// Validates this instance.
		/// </summary>
		/// <returns>
		/// A boolean value
		/// </returns>
		public async Task<bool> ValidateAsync()
		{
			return await validationHandler.ValidateAsync(this);
		}
	}
}
