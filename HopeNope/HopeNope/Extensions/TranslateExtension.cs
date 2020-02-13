using HopeNope.Properties;
using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HopeNope.Extensions
{
	/// <summary>
	/// TranslateExtension
	/// </summary>
	/// <seealso cref="IMarkupExtension" />
	[ContentProperty("Text")]
	public class TranslateExtension : IMarkupExtension
	{
		/// <summary>
		/// The resource identifier
		/// </summary>
		private const string resourceId = "HopeNope.Properties.Resources";

		/// <summary>
		/// The Resource Manager
		/// </summary>
		private static readonly Lazy<ResourceManager> resourceManager = new Lazy<ResourceManager>(() => new ResourceManager(resourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public string Text { get; set; }

		/// <summary>
		/// Returns the object created from the markup extension.
		/// </summary>
		/// <param name="serviceProvider">The service that provides the value.</param>
		/// <returns>
		/// The object
		/// </returns>
		/// <remarks>
		/// To be added.
		/// </remarks>
		public object ProvideValue(IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
				throw new ArgumentNullException(nameof(serviceProvider));

			CultureInfo cultureInfo = CrossMultilingual.Current.CurrentCultureInfo;
			
			// Fallback to the default culture when no culture can be found
			string translation = resourceManager.Value.GetString(Text, cultureInfo) ?? resourceManager.Value.GetString(Text);

			return translation;
		}
	}
}
