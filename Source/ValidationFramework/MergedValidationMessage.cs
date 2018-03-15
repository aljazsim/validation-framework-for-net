using System;
using System.Collections.Generic;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The result of merging multiple validation messages.
    /// </summary>
    public class MergedValidationMessage
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MergedValidationMessage" /> class.
        /// </summary>
        /// <param name="validationLevel">The validation level.</param>
        /// <param name="messages">The messages.</param>
        public MergedValidationMessage(ValidationLevel validationLevel, IEnumerable<string> messages)
            : this()
        {
            messages.CannotBeNull();

            this.ValidationLevel = validationLevel;
            this.Message = GetMergedMessages == null ? DefaultMergeMessages(messages) : GetMergedMessages(messages) ?? DefaultMergeMessages(messages);
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="MergedValidationMessage"/> class from being created.
        /// </summary>
        private MergedValidationMessage()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the get merge messages delegate.
        /// This delegate can be set in order create custom merging of messages.
        /// <example>MergedValidationMessage.MergeMessages = (messages) => string.Join(Environment.NewLine, messages);</example>
        /// </summary>
        /// <value>The get message from resource file delegate.</value>
        public static Func<IEnumerable<string>, string> GetMergedMessages
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the validation level.
        /// </summary>
        /// <value>The validation level.</value>
        public ValidationLevel ValidationLevel
        {
            get;
            private set;
        }

        #endregion Public Properties

        #region Private Methods

        /// <summary>
        /// Default message merging function.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <returns>The merged messages.</returns>
        private static string DefaultMergeMessages(IEnumerable<string> messages)
        {
            messages.CannotBeNull();

            return string.Join(Environment.NewLine, messages);
        }

        #endregion Private Methods
    }
}
