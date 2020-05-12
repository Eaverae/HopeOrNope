using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace GuidFramework.Controls
{
	/// <summary>
	/// ValidationEntry baseclass
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Entry" />
	public class ValidationEntry : Entry
	{
		/// <summary>
		/// Bindable property for UnderlineColorProperty
		/// </summary>
		public static readonly BindableProperty UnderlineColorProperty = BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(ValidationEntry), Color.Transparent);

		/// <summary>
		/// Gets or sets the UnderlineColor
		/// </summary>
		public Color UnderlineColor
		{
			get { return (Color)GetValue(UnderlineColorProperty); }
			set { SetValue(UnderlineColorProperty, value); }
		}

		/// <summary>
		/// The validatable object bindable property
		/// </summary>
		public static readonly BindableProperty IsValidProperty = BindableProperty.CreateAttached(nameof(IsValid), typeof(bool), typeof(ValidationEntry), true);

		/// <summary>
		/// Returns true if the control is valid.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
		/// </value>
		public bool IsValid
		{
			get => (bool)GetValue(IsValidProperty);
			set => SetValue(IsValidProperty, value);
		}

		/// <summary>
		/// Bindable property for ValidationErrorColor
		/// </summary>
		public static readonly BindableProperty ValidationErrorColorProperty = BindableProperty.Create(nameof(ValidationErrorColor), typeof(Color), typeof(ValidationEntry), Color.Red);

		/// <summary>
		/// Gets or sets the ValidationErrorColor
		/// </summary>
		public Color ValidationErrorColor
		{
			get { return (Color)GetValue(ValidationErrorColorProperty); }
			set { SetValue(ValidationErrorColorProperty, value); }
		}

		/// <summary>
		/// The validatable object bindable property
		/// </summary>
		public static readonly BindableProperty EnableValidationUnderlineProperty = BindableProperty.CreateAttached(nameof(EnableValidationUnderline), typeof(bool), typeof(ValidationEntry), true);

		/// <summary>
		/// Gets or sets a value indicating whether [enable validation underline].
		/// </summary>
		/// <value>
		///   <c>true</c> if [enable validation underline]; otherwise, <c>false</c>.
		/// </value>
		public bool EnableValidationUnderline
		{
			get => (bool)GetValue(EnableValidationUnderlineProperty);
			set => SetValue(EnableValidationUnderlineProperty, value);
		}
	}

	/// <summary>
	/// Custom validation entry
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Entry" />
	public class ValidationEntry<T> : ValidationEntry
	{
		/// <summary>
		/// The validatable object bindable property
		/// </summary>
		public static readonly BindableProperty ValidatableObjectProperty = BindableProperty.CreateAttached(nameof(ValidatableObject), typeof(ValidatableObject<T>), typeof(ValidationEntry), null);

		/// <summary>
		/// Gets or sets the validatable object.
		/// </summary>
		/// <value>
		/// The validatable object.
		/// </value>
		public ValidatableObject<T> ValidatableObject
		{
			get => (ValidatableObject<T>)GetValue(ValidatableObjectProperty);
			set => SetValue(ValidatableObjectProperty, value);
		}

		/// <summary>
		/// Method that is called when a bound property is changed.
		/// </summary>
		/// <param name="propertyName">The name of the bound property that changed.</param>
		/// <remarks>
		/// To be added.
		/// </remarks>
		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName.Equals(nameof(ValidatableObject)) && ValidatableObject != null)
			{
				Text = ValidatableObject.Value?.ToString();

				ValidatableObject.ValueChanged -= ValidationEntry_ValueChanged;
				ValidatableObject.ValueChanged += ValidationEntry_ValueChanged;

				ValidatableObject.IsValidChanged -= ValidatableObject_IsValidChanged;
				ValidatableObject.IsValidChanged += ValidatableObject_IsValidChanged;
			}
			else if (propertyName.Equals(nameof(Text)) && ValidatableObject != null)
			{
				bool isNullable = Nullable.GetUnderlyingType(typeof(T)) != null;

				// Convert the type to a nullable type if needed
				if (!isNullable)
					ValidatableObject.Value = string.IsNullOrWhiteSpace(Text) ? default(T) : (T)Convert.ChangeType(Text, typeof(T));
				else
					ValidatableObject.Value = string.IsNullOrWhiteSpace(Text) ? default : (T)Convert.ChangeType(Text, Nullable.GetUnderlyingType(typeof(T)));
			}
		}

		/// <summary>
		/// Handles the IsValidChanged event of the ValidatableObject control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void ValidatableObject_IsValidChanged(object sender, EventArgs e)
		{
			// Manually set the IsValid value
			if (ValidatableObject != null)
				IsValid = ValidatableObject.IsValid;
		}

		/// <summary>
		/// Handles the ValueChanged event of the ValidatableObject.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void ValidationEntry_ValueChanged(object sender, EventArgs e)
		{
			if (ValidatableObject != null)
				Text = ValidatableObject.Value?.ToString();
		}
	}
}
