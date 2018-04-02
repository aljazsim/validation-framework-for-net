using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validatable base class.
    /// </summary>
    /// <seealso cref="ValidationFramework.IValidatable" />
    public abstract class Validatable : IValidatable
    {
        #region Public Methods

        /// <summary>
        /// Gets the list of currently active validation contexts.
        /// </summary>
        /// <returns>
        /// The list of currently active validation context.
        /// </returns>
        public virtual IEnumerable<string> GetActiveValidationContexts()
        {
            return Array.Empty<string>();
        }

        /// <summary>
        /// Validates the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        /// The list of validation messages.
        /// </returns>
        public virtual ValidationMessageCollection Validate(string propertyName, object propertyValue, string validationContext)
        {
            return this.ValidateAttributes(propertyName, propertyValue, validationContext);
        }

        #endregion Public Methods
    }
}
