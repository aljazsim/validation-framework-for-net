using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation message as a result of a failed validation.
    /// </summary>
    public class ValidationMessage : IComparable<ValidationMessage>
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationMessage" /> class.
        /// </summary>
        /// <param name="messageKey">The message key.</param>
        /// <param name="defaultMessage">The default message.</param>
        /// <param name="messageParameters">The message parameters.</param>
        /// <param name="validationSource">The validation source.</param>
        /// <param name="propertyName">The validated property name.</param>
        /// <param name="validationLevel">The level of validation.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <param name="validationPriority">The validation priority.</param>
        public ValidationMessage(string messageKey, string defaultMessage, IEnumerable<object> messageParameters, IValidatable validationSource, string propertyName, ValidationLevel validationLevel = ValidationLevel.Error, string validationContext = ValidationFramework.ValidationContext.Default, int validationPriority = 0)
          : this()
        {
            messageKey.CannotBeNullOrEmpty();
            defaultMessage.CannotBeNullOrEmpty();
            validationSource.CannotBeNull();
            propertyName.CannotBeNullOrEmpty();

            this.Message = FormatMessage(messageKey, defaultMessage, messageParameters);
            this.ValidationLevel = validationLevel;
            this.ValidationSource = validationSource;
            this.PropertyName = propertyName;
            this.ValidationContext = validationContext;
            this.ValidationPriority = validationPriority;
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="ValidationMessage"/> class from being created.
        /// </summary>
        private ValidationMessage()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the get localized message delegate.
        /// This delegate has to be set in order for the class to be
        /// able to get the localized message from a resoruce file
        /// (or perhaps any other source). For getting the localized
        /// description from a resource file the event handler would look like this:
        /// <example>ValidationMessage.GetLocalizedMessage = (k) => { return Resources.ResourceManager.GetString(k); };</example>
        /// </summary>
        /// <value>The get message from resource file delegate.</value>
        public static Func<string, string> GetLocalizedMessage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the validation message.
        /// </summary>
        /// <value>The validation message.</value>
        public string Message
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the validated property.
        /// </summary>
        /// <value>The name of the validated property.</value>
        public string PropertyName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the validation context.
        /// </summary>
        /// <value>The validation context.</value>
        public string ValidationContext
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the level of validation.
        /// </summary>
        /// <value>The level of validation.</value>
        public ValidationLevel ValidationLevel
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the message validation priority.
        /// </summary>
        /// <value>The validation priority.</value>
        public int ValidationPriority
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the validation source.
        /// </summary>
        /// <value>The validation source.</value>
        public IValidatable ValidationSource
        {
            get;
            private set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="a">Left side item.</param>
        /// <param name="b">Right side item.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(ValidationMessage a, ValidationMessage b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="a">Left side item.</param>
        /// <param name="b">Right side item.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(ValidationMessage a, ValidationMessage b)
        {
            if (object.ReferenceEquals(a, b))
            {
                // if both are null, or both are same instance, return true
                return true;
            }
            else if (a as object == null &&
                     b as object != null)
            {
                return false;
            }
            else if (a as object != null &&
                     b as object == null)
            {
                return false;
            }
            else
            {
                // compare properties
                return a.ValidationSource == b.ValidationSource &&
                       a.PropertyName == b.PropertyName &&
                       a.ValidationLevel == b.ValidationLevel &&
                       a.ValidationPriority == b.ValidationPriority &&
                       a.ValidationContext == b.ValidationContext &&
                       a.Message == b.Message;
            }
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings:
        /// Value
        /// Meaning
        /// Less than zero
        /// This object is less than the <paramref name="other"/> parameter.
        /// Zero
        /// This object is equal to <paramref name="other"/>.
        /// Greater than zero
        /// This object is greater than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(ValidationMessage other)
        {
            return new ValidationMessageComparer().Compare(this, other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return obj is ValidationMessage && this == (ValidationMessage)obj;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            int hashCode;

            hashCode = this.ValidationSource.GetHashCode() ^
                       this.PropertyName.GetHashCode() ^
                       this.ValidationLevel.GetHashCode() ^
                       this.ValidationPriority.GetHashCode() ^
                       this.Message.GetHashCode();

            if (this.ValidationContext != null)
            {
                hashCode ^= this.ValidationContext.GetHashCode();
            }

            return hashCode;
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{this.ValidationSource.GetType().Name}.{this.PropertyName} (message: \"{this.Message}\", level: {this.ValidationLevel}, context: {this.ValidationContext ?? "default"}, priority: {this.ValidationPriority})";
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Formats the message.
        /// </summary>
        /// <param name="messageKey">The message key.</param>
        /// <param name="defaultMessage">The default message.</param>
        /// <param name="messageParameters">The message parameters.</param>
        /// <returns>The formatted message.</returns>
        private static string FormatMessage(string messageKey, string defaultMessage, IEnumerable<object> messageParameters)
        {
            messageKey.CannotBeNullOrEmpty();
            defaultMessage.CannotBeNullOrEmpty();

            string message;

            message = GetLocalizedMessage == null ? defaultMessage : GetLocalizedMessage(messageKey) ?? defaultMessage;

            try
            {
                if (messageParameters != null &&
                    messageParameters.Any())
                {
                    message = string.Format(CultureInfo.CurrentCulture, message, messageParameters.ToArray());
                }
            }
            catch (FormatException)
            {
                // unable to format -> keep unformatted message
            }

            return message;
        }

        #endregion Private Methods
    }
}
