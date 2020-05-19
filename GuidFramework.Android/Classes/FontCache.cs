using Android.Content;
using Android.Graphics;
using System;
using System.Collections.Generic;
using GuidFramework.Extensions;

namespace GuidFramework.Android.Classes
{
	/// <summary>
	/// Font cache class
	/// </summary>
	public class FontCache
	{
		/// <summary>
		/// Cache of type faces (fonts)
		/// </summary>
		private readonly IDictionary<string, Typeface> typeFaceCache;

		/// <summary>
		/// Instance of the FontCache
		/// </summary>
		private static FontCache instance;

		/// <summary>
		/// Current instance of the FontCache
		/// </summary>
		public static FontCache Instance => instance ?? (instance = new FontCache());

		/// <summary>
		/// Default constructor
		/// </summary>
		public FontCache()
		{
			typeFaceCache = new Dictionary<string, Typeface>();
		}

		/// <summary>
		/// Loads the font for named, if possible the load is from cache
		/// </summary>
		/// <param name="context">Current context</param>
		/// <param name="fontFamily">Font family name to load</param>
		/// <returns>Loaded font</returns>
		public Typeface FontForName(Context context, string fontFamily)
		{
			if (context == null)
				throw new ArgumentNullException(nameof(context));

			if (fontFamily.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(fontFamily));

			Typeface typeface;

			if (typeFaceCache.ContainsKey(fontFamily))
				typeface = typeFaceCache[fontFamily];
			else
			{
				typeface = Typeface.CreateFromAsset(context.Assets, fontFamily);
				typeFaceCache.Add(new KeyValuePair<string, Typeface>(fontFamily, typeface));
			}

			return typeface;
		}
	}
}