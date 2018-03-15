using System.Collections.Generic;

namespace ValidationFramework.Test
{
    /// <summary>
    /// The example.
    /// </summary>
    /// <seealso cref="ValidationFramework.Validatable" />
    public sealed class Example : Validatable
    {
        #region Public Properties

        public int Id
        {
            get;
            set;
        }

        [CannotBeNull(ValidationContext = "existing")]
        public string Name
        {
            get;
            set;
        }

        public override IEnumerable<string> GetActiveValidationContexts()
        {
            if (this.Id == -1)
            {
                return new string[] { "new" };
            }
            else
            {
                return new string[] { "existing" };
            }
        }

        #endregion Public Properties
    }
}