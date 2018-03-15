using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using ReactiveUI;

namespace ValidationFramework.Examples.Wpf
{
    /// <summary>
    /// The main window model.
    /// </summary>
    /// <seealso cref="ValidationFramework.Examples.Wpf.ModelBase" />
    public sealed class MainWindowModel : ModelBase
    {
        #region Private Fields

        /// <summary>
        /// The email.
        /// </summary>
        private string email;

        /// <summary>
        /// The first name
        /// </summary>
        private string firstName;

        /// <summary>
        /// The is username enabled.
        /// </summary>
        private bool isUsernameEnabled;

        /// <summary>
        /// The language.
        /// </summary>
        private string language;

        /// <summary>
        /// The last name.
        /// </summary>
        private string lastName;

        /// <summary>
        /// The phone number.
        /// </summary>
        private string phoneNumber;

        /// <summary>
        /// The submit enabled.
        /// </summary>
        private bool submitEnabled;

        /// <summary>
        /// The username.
        /// </summary>
        private string username;

        /// <summary>
        /// The user type.
        /// </summary>
        private string userType;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowModel"/> class.
        /// </summary>
        public MainWindowModel()
        {
            this.SupressValidation = true;

            this.SubmitCommand = ReactiveCommand.Create(() => this.Submit());

            this.Languages = new Dictionary<string, string>();
            this.Languages.Add("en", "English");
            this.Languages.Add("sl", "Slovenian");

            // set default validation language to English
            this.Language = "en";

            this.UserTypes = new Dictionary<string, string>();
            this.UserTypes.Add("new", "New user");
            this.UserTypes.Add("existing", "Existing user");

            // set default user type to new
            this.UserType = "new";

            // enable submit button only if validation is enabled and all values are valid
            this.WhenAnyValue(x => x.FirstName, x => x.LastName, x => x.PhoneNumber, x => x.Email, x => x.UserType, x => x.Username).Subscribe(_ => this.SubmitEnabled = this.SupressValidation || this.IsValid());

            // re-validate username when user type is changed (username validation depends on user type)
            this.WhenAnyValue(x => x.UserType).Subscribe(_ => this.RaisePropertyChanged(nameof(this.Username)));

            // handle changing the validation culture (see App.xaml.cs)
            this.WhenAnyValue(x => x.Language).Subscribe(x =>
            {
                // change current UI culture
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(x);

                // refresh items
                this.RefreshProperties();
            });
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [MustMatch(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", MessageKey = "InvalidEmail", ValidationPriority = 2)]
        [CannotBeLongerThan(20, MessageKey = "ValueMaxLength", ValidationPriority = 1)]
        [CannotBeNullOrWhitespace(MessageKey = "ValueIsMandatory", ValidationPriority = 0)]
        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                this.RaiseAndSetIfChanged(ref this.email, value);
            }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [MustMatch(@"^[\w ]+$", MessageKey = "InvalidFirstName", ValidationPriority = 2)]
        [CannotBeLongerThan(20, MessageKey = "ValueMaxLength", ValidationPriority = 1)]
        [CannotBeNullOrWhitespace(MessageKey = "ValueIsMandatory", ValidationPriority = 0)]
        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                this.RaiseAndSetIfChanged(ref this.firstName, value);
            }
        }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string Language
        {
            get
            {
                return this.language;
            }

            set
            {
                this.RaiseAndSetIfChanged(ref this.language, value);
            }
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <value>
        /// The languages.
        /// </value>
        public Dictionary<string, string> Languages
        {
            get;
        }

        /// <summary>
        /// Gets or sets the lastname.
        /// </summary>
        /// <value>
        /// The lastname.
        /// </value
        [MustMatch(@"^[\w ]+$", MessageKey = "InvalidLastName", ValidationPriority = 2)]
        [CannotBeLongerThan(20, MessageKey = "ValueMaxLength", ValidationPriority = 1)]
        [CannotBeNullOrWhitespace(MessageKey = "ValueIsMandatory", ValidationPriority = 0)]
        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                this.RaiseAndSetIfChanged(ref this.lastName, value);
            }
        }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        [MustMatch("^[0-9]{3} [0-9]{3} [0-9]{4}$", MessageKey = "InvalidPhoneNumber", ValidationPriority = 1)]
        [CannotBeNullOrWhitespace(MessageKey = "ValueIsMandatory", ValidationPriority = 0)]
        public string PhoneNumber
        {
            get
            {
                return this.phoneNumber;
            }

            set
            {
                this.RaiseAndSetIfChanged(ref this.phoneNumber, value);
            }
        }

        /// <summary>
        /// Gets the submit command.
        /// </summary>
        /// <value>
        /// The submit command.
        /// </value>
        public ICommand SubmitCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the submit enabled.
        /// </summary>
        /// <value>
        /// The submit enabled.
        /// </value>
        public bool SubmitEnabled
        {
            get
            {
                return this.submitEnabled;
            }

            private set
            {
                this.RaiseAndSetIfChanged(ref this.submitEnabled, value);
            }
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [CannotBeNullOrEmpty(MessageKey = "ValueIsMandatory", ValidationContext = "existing")]
        [MustBeNullOrEmpty(MessageKey = "ValueMustBeEmpty", ValidationContext = "new")]
        public string Username
        {
            get
            {
                return this.username;
            }

            set
            {
                this.RaiseAndSetIfChanged(ref this.username, value);
            }
        }

        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>
        /// The type of the user.
        /// </value>
        public string UserType
        {
            get
            {
                return this.userType;
            }

            set
            {
                this.RaiseAndSetIfChanged(ref this.userType, value);
            }
        }

        /// <summary>
        /// Gets the user types.
        /// </summary>
        /// <value>
        /// The user types.
        /// </value>
        public Dictionary<string, string> UserTypes
        {
            get;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Gets the list of currently active validation contexts.
        /// </summary>
        /// <returns>
        /// The list of currently active validation contexts.
        /// </returns>
        public override IEnumerable<string> GetActiveValidationContexts()
        {
            return new string[] { this.UserType };
        }

        #endregion Public Methods

        #region Private Methods

        private void RefreshProperties()
        {
            // trigger property changed in order to show error messages
            this.RaisePropertyChanged(nameof(this.FirstName));
            this.RaisePropertyChanged(nameof(this.LastName));
            this.RaisePropertyChanged(nameof(this.PhoneNumber));
            this.RaisePropertyChanged(nameof(this.Email));
            this.RaisePropertyChanged(nameof(this.Username));
        }

        /// <summary>
        /// Submits the data.
        /// </summary>
        private void Submit()
        {
            if (this.IsValid())
            {
                this.SupressValidation = true;

                MessageBox.Show("Data successfully validated.", "Validation success");

                // clear form
                this.FirstName = null;
                this.LastName = null;
                this.PhoneNumber = null;
                this.Email = null;
                this.Username = null;
            }
            else
            {
                // enable showing validation errors
                this.SupressValidation = false;

                // prevent submission until input data is corrected
                this.SubmitEnabled = false;

                this.RefreshProperties();
            }
        }

        #endregion Private Methods
    }
}
