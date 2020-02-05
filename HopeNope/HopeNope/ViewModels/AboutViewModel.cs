using HopeNope.Properties;
using System;

namespace HopeNope.ViewModels
{
	public class AboutViewModel : BaseViewModel
	{
		public string CopyrightText
		{
			get
			{
				return String.Format(Properties.Resources.Copyright, DateTime.Now.Year);
			}
		}

		public string AboutText
		{
			get
			{
				return Resources.About;
			}
		}

		public AboutViewModel() : base()
		{

		}
	}
}