using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventKit;
using EventKitUI;
using Foundation;
using GuidFramework.Extensions;
using GuidFramework.Interfaces;
using UIKit;
using Xamarin.Forms;

namespace GuidFramework.iOS.Services
{

	/// <summary>
	/// Class for Calendar service
	/// </summary>
	public class CalendarService : ICalendarService
	{
		/// <summary>
		/// EventStore
		/// </summary>
		private static EKEventStore eventStore;

		/// <summary>
		/// EventStore
		/// </summary>
		public static EKEventStore EventStore
		{
			get
			{
				if (eventStore == null)
					eventStore = new EKEventStore();

				return eventStore;
			}
		}

		/// <summary>
		/// Method to create an calendar item.
		/// </summary>
		/// <param name="title">Title of the item</param>
		/// <param name="description">Description of the item</param>
		/// <param name="startDate">Begindate of the item</param>
		public void CreateCalendarItem(string title, string description, DateTime startDate)
		{
			if (title.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(title));

			if (description.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(description));

			EventStore.RequestAccess(EKEntityType.Event, (granted, e) =>
			{
				if (granted)
				{
					EKEvent calendarItem = EKEvent.FromStore(EventStore);
					calendarItem.Title = title;

					if (!description.IsNullOrWhiteSpace())
						calendarItem.Notes = description;

					calendarItem.StartDate = DateTimeToNSDate(startDate);
					calendarItem.EndDate = DateTimeToNSDate(startDate);
					calendarItem.AllDay = true;

					Device.BeginInvokeOnMainThread(() =>
					{
						EKEventEditViewController eventController = new EKEventEditViewController()
						{
							EventStore = EventStore,
							Event = calendarItem
						};

						//// Set the navigation bar style
						//eventController.NavigationBar.Opaque = true;
						//eventController.NavigationBar.BarStyle = UIBarStyle.Black;
						//eventController.NavigationBar.Translucent = false;
						//eventController.NavigationBar.TintColor = UIColor.FromRGBA(183, 18, 52, 255);
						//eventController.NavigationBar.BarTintColor = UIColor.FromRGBA(183, 18, 52, 255);
						//eventController.NavigationBar.BackgroundColor = UIColor.FromRGBA(183, 18, 52, 255);

						eventController.EditViewDelegate = new CalendarViewDelegate(eventController);
						UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(eventController, true, null);
					});
				}
			});
		}

		/// <summary>
		/// Convert DateTime to NSDate
		/// </summary>
		/// <param name="date">The date</param>
		/// <returns>A NSDate value</returns>
		private static NSDate DateTimeToNSDate(DateTime date)
		{
			if (date.Kind == DateTimeKind.Unspecified)
				date = DateTime.SpecifyKind(date, DateTimeKind.Local);

			return (NSDate)date;
		}
	}
}