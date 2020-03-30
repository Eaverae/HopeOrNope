using GuidFramework.Extensions;
using System;
using System.ComponentModel;
using System.Resources;

namespace GuidFramework.Classes
{
	/// <summary>
	/// LocalizedDescriptionAttribute
	/// </summary>
	/// <seealso cref="System.ComponentModel.DescriptionAttribute" />
	public class LocalizedDescriptionAttribute : DescriptionAttribute
	{
		private readonly string resourceKey;
		private readonly ResourceManager resourceManager;

		/// <summary>
		/// Initializes a new instance of the <see cref="LocalizedDescriptionAttribute"/> class.
		/// </summary>
		/// <param name="resourceKey">The resource key.</param>
		/// <param name="resourceType">Type of the resource.</param>
		public LocalizedDescriptionAttribute(string resourceKey, Type resourceType)
		{
			resourceManager = new ResourceManager(resourceType);
			this.resourceKey = resourceKey;
		}

		/// <summary>
		/// Gets the description stored in this attribute.
		/// </summary>
		public override string Description
		{
			get
			{
				string displayName = resourceManager.GetString(resourceKey);

				return displayName.IsNullOrWhiteSpace() ? $"[[{resourceKey}]]" : displayName;
			}
		}
	}
}
