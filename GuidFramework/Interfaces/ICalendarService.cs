using System;

namespace GuidFramework.Interfaces
{
	/// <summary>
	/// Interface for the CalendarService
	/// </summary>
	public interface ICalendarService
	{
		/// <summary>
		/// Creates the calendar item.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="description">The description.</param>
		/// <param name="startDate">The start date.</param>
		void CreateCalendarItem(string title, string description, DateTime startDate);
	}
}
