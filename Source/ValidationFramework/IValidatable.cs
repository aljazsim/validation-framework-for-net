using System.Collections.Generic;

namespace ValidationFramework
{
    /// <summary>
    /// The interface that a class must implement in order to enable property validation.
    /// </summary>
    public interface IValidatable
    {
        #region Public Methods

        /// <summary>
        /// Gets the list of currently active validation contexts.
        /// </summary>
        /// <returns>The list of currently active validation contexts.</returns>
        IEnumerable<string> GetActiveValidationContexts();

        /// <summary>
        /// Validates the specified property name for the specified validation context.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        /// The list of validation messages.
        /// </returns>
        ValidationMessageCollection Validate(string propertyName, object propertyValue, string validationContext);

        #endregion Public Methods
    }
}
