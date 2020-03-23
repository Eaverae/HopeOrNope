using HopeNope.Properties;
using System;
using System.Timers;

namespace HopeNope.ViewModels
{
	/// <summary>
	/// FullscreenAdPopupViewModel
	/// </summary>
	/// <seealso cref="HopeNope.ViewModels.BaseViewModel" />
	public class FullscreenAdPopupViewModel : BaseViewModel
	{
		private static Timer timer;

		/// <summary>
		/// Gets the seconds left.
		/// </summary>
		/// <value>
		/// The seconds left.
		/// </value>
		public int SecondsLeft
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets a value indicating whether [allow dismissal].
		/// </summary>
		/// <value>
		///   <c>true</c> if [allow dismissal]; otherwise, <c>false</c>.
		/// </value>
		public bool AllowDismissal
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the loading label.
		/// </summary>
		/// <value>
		/// The loading label.
		/// </value>
		public string LoadingLabel
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the ad identifier.
		/// </summary>
		/// <value>
		/// The ad identifier.
		/// </value>
		public string AdId
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the secondary ad identifier.
		/// </summary>
		/// <value>
		/// The secondary ad identifier.
		/// </value>
		public string SecondaryAdId
		{
			get;
			private set;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FullscreenAdPopupViewModel"/> class.
		/// </summary>
		/// <param name="adId">The ad identifier.</param>
		/// <param name="secondaryAdId">[Optional] The secondary ad identifier</param>
		/// <exception cref="System.ArgumentNullException">adId</exception>
		public FullscreenAdPopupViewModel(string adId, string secondaryAdId = null)
		{
			if (adId.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(adId));

			AdId = adId;
			SecondaryAdId = secondaryAdId;
		}

		/// <summary>
		/// Navigates the viewmodel back asynchronous.
		/// </summary>
		public override async void BackAsync()
		{
			await NavigationService.PopModalAsync();
		}

		/// <summary>
		/// Appearing method
		/// <para>This method will be called when the page appears</para>
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		public override void OnAppearing(object sender, EventArgs e)
		{
			LoadingLabel = Resources.Loading;
			AllowDismissal = false;
			SecondsLeft = new Random().Next(3, 5);

			OnPropertyChanged(nameof(SecondsLeft));
			OnPropertyChanged(nameof(AllowDismissal));
			OnPropertyChanged(nameof(LoadingLabel));

			timer = new Timer(1000);
			timer.Elapsed += Timer_Elapsed;
			timer.Start();

			base.OnAppearing(sender, e);
		}

		/// <summary>
		/// Handles the Elapsed event of the Timer control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (SecondsLeft > 0)
			{
				SecondsLeft--;
				OnPropertyChanged(nameof(SecondsLeft));
			}
			else
			{
				timer.Stop();
				AllowDismissal = true;
				LoadingLabel = Resources.Continue;

				OnPropertyChanged(nameof(SecondsLeft));
				OnPropertyChanged(nameof(AllowDismissal));
				OnPropertyChanged(nameof(LoadingLabel));
			}
		}

		/// <summary>
		/// Disappearing method
		/// <para>This method will be called when the page disappears</para>
		/// <para>Sets IsInitialized to false</para>
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		public override void OnDisappearing(object sender, EventArgs e)
		{
			timer.Dispose();
			timer = null;

			base.OnDisappearing(sender, e);
		}
	}
}