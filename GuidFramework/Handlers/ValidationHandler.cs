using GuidFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GuidFramework.Handlers
{
    /// <summary>
    /// ValidationHandler
    /// </summary>
    /// <seealso cref="GuidFramework.Interfaces.IValidationHandler" />
    public class ValidationHandler : IValidationHandler
    {
        /// <summary>
        /// The validation message when multiple fields are invalid
        /// </summary>
        private readonly string validationMessageMultipleFieldsAreInvalid;

        /// <summary>
        /// The toast handler
        /// </summary>
        private readonly IToastHandler toastHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationHandler"/> class.
        /// </summary>
        /// <param name="toastHandler">The toast handler.</param>
        /// <param name="validationMessageMultipleFieldsAreInvalid">The default validationmessage for when multiple fields are invalid</param>
        public ValidationHandler(IToastHandler toastHandler, string validationMessageMultipleFieldsAreInvalid)
        {
            this.toastHandler = toastHandler;
            this.validationMessageMultipleFieldsAreInvalid = validationMessageMultipleFieldsAreInvalid;
        }

        /// <summary>
        /// Validates the given validatable object in the given validationIndex.
        /// </summary>
        /// <typeparam name="T">The type of enum</typeparam>
        /// <param name="validatable">The validatable view model.</param>
        /// <param name="validationEnum">The validation enum.</param>
        /// <returns>
        /// True when valid
        /// </returns>
        public async Task<bool> ValidateAsync<T>(IValidatable validatable, T validationEnum)
            where T : struct, IConvertible
        {
            bool result;

            if (typeof(T).IsEnum)
            {
                int index = Convert.ToInt32(validationEnum);
                result = await ValidateAsync(validatable, validationIndex: index);
            }
            else
                result = false;

            return result;
        }

        /// <summary>
        /// Validates the given validatable object in the given validationIndex.
        /// </summary>
        /// <param name="validatable">The object to validate.</param>
        /// <param name="validationIndex">[Optional] ValidationIndex to validate groups</param>
        /// <returns>A boolean value</returns>
        /// <exception cref="InvalidOperationException">No validatable properties found on type.</exception>
        public async Task<bool> ValidateAsync(IValidatable validatable, int? validationIndex = null)
        {
            if (validatable == null)
                throw new ArgumentNullException(nameof(validatable));

            if (toastHandler == null)
                throw new InvalidOperationException("The toastHandler cannot be found.");

            // Default is valid
            bool isValid = true;

            List<string> validationErrors = new List<string>();

            List<PropertyInfo> properties = validatable.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(property => typeof(IValidity).IsAssignableFrom(property.PropertyType)).ToList();

            if (!properties.Any())
                throw new InvalidOperationException("No validatable properties found on type.");

            foreach (PropertyInfo propertyInfo in properties)
            {
                IValidity validProp = (propertyInfo.GetValue(validatable) as IValidity);

                if (validProp != null && ((validationIndex.HasValue && validProp.ValidationIndex == validationIndex) || !validationIndex.HasValue))
                {
                    validProp.Validate();

                    if (!validProp.IsValid)
                        validationErrors.AddRange(validProp.ValidationErrors);

                    isValid &= validProp.IsValid;
                }
            }

            if (toastHandler != null)
            {
                if (validationErrors.Count > 1)
                    await toastHandler.ShowErrorMessageAsync(validationMessageMultipleFieldsAreInvalid);
                else if (validationErrors.Any())
                    await toastHandler.ShowErrorMessageAsync(validationErrors.First());
            }

            return isValid;
        }

        /// <summary>
        /// Clears the validationerrors
        /// </summary>
        /// <param name="validatableViewModel">The viewmodel to clear the errors for</param>
        public void Clear(IValidatableViewModel validatableViewModel)
        {
            if (validatableViewModel == null)
                throw new ArgumentNullException(nameof(validatableViewModel));

            List<PropertyInfo> properties = validatableViewModel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(property => typeof(IValidity).IsAssignableFrom(property.PropertyType)).ToList();

            foreach (PropertyInfo propertyInfo in properties)
            {
                IValidity validProp = (propertyInfo.GetValue(validatableViewModel) as IValidity);

                if (validProp != null)
                {
                    validProp.ValidationErrors?.Clear();
                    validProp.IsValid = true;
                }
            }
        }
    }
}