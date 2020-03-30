using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace GuidFramework.ViewModels
{
	/// <summary>
	/// NotifiyPropertyChanged
	/// </summary>
	public class NotifyPropertyChanged : INotifyPropertyChanged
	{
		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		/// <returns></returns>
		public event PropertyChangedEventHandler PropertyChanged;


		/// <summary>
		/// Occurs when a property value changes
		/// </summary>
		/// <param name="propertyName">PropertyName</param>
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler propertyChanged = PropertyChanged;

			if (propertyChanged != null)
				Device.BeginInvokeOnMainThread(() => propertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName)));
		}
	}
}
