using GuidFramework.ViewModels;
using HopeNope.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HopeNope.ViewModels.Base
{
	/// <summary>
	/// HopeNopeViewModel
	/// </summary>
	/// <seealso cref="GuidFramework.ViewModels.BaseViewModel" />
	public class HopeNopeViewModel : BaseViewModel
	{
		/// <summary>
		/// Gets a value indicating whether [ads enabled].
		/// </summary>
		/// <value>
		///   <c>true</c> if [ads enabled]; otherwise, <c>false</c>.
		/// </value>
		public bool AdsEnabled
		{
			get
			{
				return Settings.AdsEnabled;
			}
		}

		/// <summary>
		/// Gets the banner ad identifier.
		/// </summary>
		/// <value>
		/// The banner ad identifier.
		/// </value>
		public string BannerAdId
		{
			get
			{
				return "YOURBANNERID";
			}
		}

		/// <summary>
		/// Gets the second banner ad identifier.
		/// </summary>
		/// <value>
		/// The second banner ad identifier.
		/// </value>
		public string SecondBannerAdId
		{
			get
			{
				return "YOURSECONDBANNERID";
			}
		}
	}
}
