using GuidFramework.Enums;
using GuidFramework.Interfaces;
using System;

namespace GuidFramework.ValidationRules
{
    /// <summary>
    /// DateCompareRule
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="GuidFramework.Interfaces.IValidationRule{System.DateTime}" />
    public class DateCompareRule<T> : IValidationRule<DateTime>
    {
        /// <summary>
        /// Gets or sets the type of the compare.
        /// </summary>
        /// <value>
        /// The type of the compare.
        /// </value>
        public CompareType CompareType { get; set; }

        /// <summary>
        /// Gets or sets the validation message.
        /// </summary>
        /// <value>
        /// The validation message.
        /// </value>
        public string ValidationMessage { get; set; }

        /// <summary>
        /// Gets or sets the validatable object.
        /// </summary>
        /// <value>
        /// The validatable object.
        /// </value>
        public DateTime ValidatableObject { get; set; }

        /// <summary>
        /// Checks the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A boolean value</returns>
        public bool Check(DateTime value)
        {
            bool result;

            switch (CompareType)
            {
                case CompareType.AreEqual:
                    result = ValidatableObject.Equals(value);
                    break;
                case CompareType.Greater:
                    result = value.Ticks > ValidatableObject.Ticks;
                    break;
                case CompareType.GreaterOrEqual:
                    result = value.Ticks >= ValidatableObject.Ticks;
                    break;
                case CompareType.Less:
                    result = value.Ticks < ValidatableObject.Ticks;
                    break;
                case CompareType.LessOrEqual:
                    result = value.Ticks <= ValidatableObject.Ticks;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }
    }
}
