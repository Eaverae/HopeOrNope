using GuidFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GuidFramework
{
    /// <summary>
    /// Object wrapper which allows for validationrules to be applied to the object
    /// </summary>
    /// <typeparam name="T">The type of the object</typeparam>
    /// <seealso cref="Rimek.Framework.App.Interfaces.IValidity" />
    public class ValidatableObject<T> : NotifyPropertyChanged, IValidity
    {
        private List<string> errors;
        private T value;
        private bool isValid;

        /// <summary>
        /// Gets the index of the validation.
        /// </summary>
        /// <value>
        /// The index of the validation.
        /// </value>
        public int? ValidationIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the validationrules.
        /// </summary>
        /// <value>
        /// The validations.
        /// </value>
        public List<IValidationRule<T>> ValidationRules { get; } = new List<IValidationRule<T>>();

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public List<string> ValidationErrors
        {
            get => errors;
            private set
            {
                errors = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has value; otherwise, <c>false</c>.
        /// </value>
        public bool HasValue
        {
            get
            {
                return value != null;
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T Value
        {
            get => value;
            set
            {
                ValidationErrors.Clear();
                this.value = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(HasValue));

                if (ValueChanged != null)
                    ValueChanged.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Occurs when [is valid changed].
        /// </summary>
        public event EventHandler IsValidChanged;

        /// <summary>
        /// Occurs when [value changed].
        /// </summary>
        public event EventHandler ValueChanged;

        /// <summary>
        /// Returns true if the instance is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get => isValid;
            set
            {
                isValid = value;
                OnPropertyChanged();

                if (IsValidChanged != null)
                    IsValidChanged.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatableObject{T}"/> class.
        /// </summary>
        /// <param name="validationIndex">ValidationIndex to group results</param>
        public ValidatableObject(int? validationIndex = null)
        {
            ValidationIndex = validationIndex;
            isValid = true;
            errors = new List<string>();
            ValidationRules = new List<IValidationRule<T>>();
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns>A boolean value</returns>
        public void Validate()
        {
            ValidationErrors.Clear();

            IEnumerable<string> errors = ValidationRules
                .Where(item => !item.Check(Value))
                .Select(item => item.ValidationMessage)
                .Distinct();

            ValidationErrors = errors.ToList();
            IsValid = !ValidationErrors.Any();
        }
    }
}
