﻿using Android.App;
using Android.Views;
using HopeNope.Droid.Services;
using HopeNope.Services;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(StatusBarService))]
namespace HopeNope.Droid.Services
{

	/// <summary>
	/// Status bar service.
	/// </summary>
	/// <seealso cref="Rimek.Framework.App.Interfaces.IStatusBarService" />
	public class StatusBarService : IStatusBarService
	{
		private WindowManagerFlags originalFlags;

		/// <summary>
		/// Hides the status bar.
		/// </summary>
		public void HideStatusBar()
		{
			Activity activity = MainActivity.CurrentActivity;

			if (activity != null)
			{
				WindowManagerLayoutParams attributes = activity.Window.Attributes;
				originalFlags = attributes.Flags;
				attributes.Flags |= WindowManagerFlags.Fullscreen;
				activity.Window.Attributes = attributes;
			}
		}

		/// <summary>
		/// Shows the status bar.
		/// </summary>
		public void ShowStatusBar()
		{
			Activity activity = MainActivity.CurrentActivity;

			if (activity != null)
			{
				WindowManagerLayoutParams attributes = activity.Window.Attributes;
				attributes.Flags = originalFlags;
				activity.Window.Attributes = attributes;
			}
		}
	}
}