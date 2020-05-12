using HopeNope.Entities;
using HopeNope.ViewModels.Base;

namespace HopeNope.ViewModels
{
	/// <summary>
	/// PersonDetails Viewmodel
	/// </summary>
	/// <seealso cref="HopeNope.ViewModels.Base.HopeNopeViewModel" />
	public class PersonDetailsViewModel : HopeNopeViewModel
	{
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
	}
}
