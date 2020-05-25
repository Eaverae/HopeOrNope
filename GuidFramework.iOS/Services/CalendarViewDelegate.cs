using EventKitUI;

namespace GuidFramework.iOS.Services
{
	/// <summary>
	/// CalendarViewDelegate
	/// </summary>
	/// <seealso cref="EventKitUI.EKEventEditViewDelegate" />
	public class CalendarViewDelegate : EKEventEditViewDelegate
	{
		/// <summary>
		/// The event controller
		/// </summary>
		private readonly EKEventEditViewController eventController;

		/// <summary>
		/// Initializes a new instance of the <see cref="CalendarViewDelegate"/> class.
		/// </summary>
		/// <param name="eventController">The event controller.</param>
		public CalendarViewDelegate(EKEventEditViewController eventController)
		{
			this.eventController = eventController;
		}

		/// <summary>
		/// Called when a user cancels, deletes or saves.
		/// </summary>
		/// <param name="controller">The controller</param>
		/// <param name="action">The action to perform</param>
		public override void Completed(EKEventEditViewController controller, EKEventEditViewAction action)
		{
			eventController.DismissViewController(true, null);
		}
	}
}