using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DefensiveProgrammingFramework;

namespace ValidationFramework
{
    /// <summary>
    /// The validation message list.
    /// </summary>
    public sealed class ValidationMessageCollection : Collection<ValidationMessage>
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationMessageCollection"/> class.
        /// </summary>
        public ValidationMessageCollection()
        {
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationMessageCollection"/> class.
        /// </summary>
        /// <param name="messages">The messages.</param>
        private ValidationMessageCollection(IEnumerable<ValidationMessage> messages)
        {
            if (messages != null &&
                messages.Count() > 0)
            {
                this.AddRange(messages);
            }
        }

        #endregion Private Constructors

        #region Public Indexers

        /// <summary>
        /// Gets the <see cref="ValidationFramework.ValidationMessageCollection" /> with the specified validation source.
        /// </summary>
        /// <value>
        /// Collection of validation messages for the specified validation source and property name.
        /// </value>
        /// <param name="validationSource">Validation source.</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        /// return value
        /// </returns>
        public ValidationMessageCollection this[IValidatable validationSource, string propertyName = null, string validationContext = ValidationContext.Default]
        {
            get
            {
                if (validationSource == null)
                {
                    return new ValidationMessageCollection();
                }
                else if (propertyName == null)
                {
                    return new ValidationMessageCollection(this.Where(message => message.ValidationSource == validationSource &&
                                                                                 message.ValidationContext == validationContext)
                                                               .ToList());
                }
                else
                {
                    return new ValidationMessageCollection(this.Where(message => message.ValidationSource == validationSource &&
                                                                                 message.PropertyName == propertyName &&
                                                                                 message.ValidationContext == validationContext)
                                                               .ToList());
                }
            }
        }

        #endregion Public Indexers

        #region Public Methods

        /// <summary>
        /// Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public new void Add(ValidationMessage message)
        {
            this.Insert(this.Count, message);
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="messages">The messages.</param>
        public void AddRange(IEnumerable<ValidationMessage> messages)
        {
            if (messages != null)
            {
                foreach (ValidationMessage message in messages)
                {
                    this.Add(message);
                }
            }
        }

        /// <summary>
        /// Determines whether an element is in the <see cref="System.Collections.ObjectModel.Collection`1"></see>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="System.Collections.ObjectModel.Collection`1"></see>. The value can be null for reference types.</param>
        /// <returns>
        /// true if <paramref name="item">item</paramref> is found in the <see cref="System.Collections.ObjectModel.Collection`1"></see>; otherwise, false.
        /// </returns>
        public new bool Contains(ValidationMessage item)
        {
            if (item == null)
            {
                return false;
            }
            else
            {
                return this.Contains(item, new ValidationMessageComparer());
            }
        }

        /// <summary>
        /// Inserts avalidation message at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="message">The message.</param>
        public new void Insert(int index, ValidationMessage message)
        {
            index.MustBeBetween(0, this.Count);
            message.CannotBeNull();

            ValidationMessageCollection messages = this[message.ValidationSource, message.PropertyName, message.ValidationContext];

            if (messages != null &&
                messages.Count > 0)
            {
                // insert only if there are no messages with higher validation level for this property
                if (!messages.Any(x => x.ValidationLevel > message.ValidationLevel))
                {
                    // remove messages with lower priority
                    foreach (var lowerPriorityMessage in messages.Where(x => x.ValidationLevel < message.ValidationLevel))
                    {
                        this.Remove(lowerPriorityMessage);
                    }

                    // insert message
                    base.Insert(Math.Min(this.Count, index), message);
                }
            }
            else
            {
                base.Insert(index, message);
            }
        }

        /// <summary>
        /// Merges the specified messages.
        /// </summary>
        /// <param name="validationSource">The validation source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// The merged message.
        /// </returns>
        public MergedValidationMessage Merge(IValidatable validationSource, string propertyName = null)
        {
            validationSource.CannotBeNull();

            ValidationMessageCollection messages = new ValidationMessageCollection();

            if (propertyName == null)
            {
                foreach (var validationMessage in this)
                {
                    if (validationMessage.ValidationSource == validationSource)
                    {
                        messages.Add(validationMessage);
                    }
                }
            }
            else
            {
                foreach (var validationMessage in this)
                {
                    if (validationMessage.ValidationSource == validationSource &&
                        validationMessage.PropertyName == propertyName)
                    {
                        messages.Add(validationMessage);
                    }
                }
            }

            return Merge(messages);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Merges the specified messages.
        /// </summary>
        /// <param name="list">The validation message list.</param>
        /// <returns>Merged message.</returns>
        private static MergedValidationMessage Merge(ValidationMessageCollection list)
        {
            ValidationLevel validationLevel;
            int validationPriortiy = int.MaxValue;
            HashSet<string> messages = new HashSet<string>();

            if (list == null ||
                list.Count == 0)
            {
                validationLevel = ValidationLevel.None;
            }
            else if (list.Count == 1)
            {
                messages.Add(list[0].Message);
                validationLevel = list[0].ValidationLevel;
            }
            else
            {
                validationLevel = ValidationLevel.None;

                // find max validation level
                foreach (ValidationMessage vm in list)
                {
                    validationLevel = (int)vm.ValidationLevel > (int)validationLevel ? vm.ValidationLevel : validationLevel;
                }

                // find max validation priority
                foreach (ValidationMessage vm in list.Where(x => x.ValidationLevel == validationLevel))
                {
                    validationPriortiy = vm.ValidationPriority < validationPriortiy ? vm.ValidationPriority : validationPriortiy;
                }

                // select only messages for that level
                foreach (ValidationMessage vm in list.Where(x => x.ValidationLevel == validationLevel &&
                                                                 x.ValidationPriority == validationPriortiy))
                {
                    if (!messages.Contains(vm.Message))
                    {
                        // prevent duplicates
                        messages.Add(vm.Message);
                    }
                }
            }

            return new MergedValidationMessage(validationLevel, messages);
        }

        #endregion Private Methods
    }
}
