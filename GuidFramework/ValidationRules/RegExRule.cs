using GuidFramework.Extensions;
using GuidFramework.Interfaces;
using System.Text.RegularExpressions;

namespace GuidFramework.ValidationRules
{
    /// <summary>
    /// Checks if value mathes the RegEx pattern
    /// </summary>
    /// <seealso cref="GuidFramework.Interfaces.IValidationRule{System.String}" />
    public class RegExRule : IValidationRule<string>
    {
        /// <summary>
        /// Gets or sets the validation message.
        /// </summary>
        /// <value>
        /// The validation message.
        /// </value>
        public string ValidationMessage { get; set; }

        /// <summary>
        /// Gets or sets the pattern.
        /// </summary>
        /// <value>
        /// The pattern.
        /// </value>
        public string Pattern { get; set; }

        /// <summary>
        /// Checks the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>True when the value is a match</returns>
        public bool Check(string value)
        {
            bool result = false;

            if (!value.IsNullOrWhiteSpace() && !Pattern.IsNullOrWhiteSpace())
                result = Regex.IsMatch(value, Pattern, RegexOptions.None);

            return result;
        }
    }
}
