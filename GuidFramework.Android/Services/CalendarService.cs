using Android.Content;
using Android.Provider;
using GuidFramework.Android.Services;
using GuidFramework.Droid;
using GuidFramework.Extensions;
using GuidFramework.Interfaces;
using Java.Util;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(CalendarService))]
namespace GuidFramework.Android.Services
{
	public class CalendarService : ICalendarService
	{
		/// <summary>
		/// Creates the calendar item.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="description">The description.</param>
		/// <param name="startDate">The begin date.</param>
		/// <exception cref="ArgumentNullException">title</exception>
		public void CreateCalendarItem(string title, string description, DateTime startDate)
		{
			if (title.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(title));

			Intent intent = new Intent(Intent.ActionInsert);
			intent.SetType("vnd.android.cursor.item/event");

			intent.PutExtra(CalendarContract.EventsColumns.Title, title);

			if (!description.IsNullOrWhiteSpace())
				intent.PutExtra(CalendarContract.EventsColumns.Description, description);

			// We only need to set the startdate
			long dateaslong = CurrentTimeMillis(startDate);
			intent.PutExtra(CalendarContract.ExtraEventBeginTime, dateaslong);
			intent.PutExtra(CalendarContract.EventsColumns.AllDay, true);

			intent.SetFlags(ActivityFlags.NewTask);
			intent.SetData(CalendarContract.Events.ContentUri);

			GuidFrameworkActivity.CurrentActivity.StartActivity(intent);
		}

		/// <summary>
		/// Gets the right milliseconds of the date.
		/// </summary>
		/// <param name="date">The date and time</param>
		/// <returns>The date in total milli seconds</returns>
		private static long CurrentTimeMillis(DateTime date)
		{
			// Android's months are index based..
			int month = date.Month - 1;

			Calendar calendar = new GregorianCalendar(date.Year, month, date.Day)
			{
				TimeZone = Java.Util.TimeZone.GetTimeZone("UTC")
			};
			calendar.Set(CalendarField.Hour, 0);
			calendar.Set(CalendarField.Minute, 0);
			calendar.Set(CalendarField.Second, 0);
			calendar.Set(CalendarField.Millisecond, 0);

			long returnValue = calendar.TimeInMillis;

			return returnValue;
		}
	}
}