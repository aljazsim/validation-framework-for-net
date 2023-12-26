using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ValidationFramework.Examples.Wpf.Localization;

namespace ValidationFramework.Examples.Wpf
{
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
        #region Protected Methods

        /// <summary>
        /// Raises the <see cref="System.Windows.Application.Startup" /> event.
        /// </summary>
        /// <param name="e">A <see cref="System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow mainWindow;

            mainWindow = new MainWindow();
            mainWindow.ViewModel = new MainWindowModel();
            mainWindow.DataContext = mainWindow.ViewModel;

            // setup bindings (reactive UI binding does not support validation binding out of the box, that's why we use default WPF binding)
            mainWindow.comboValidationLanguage.SetBinding(ComboBox.ItemsSourceProperty, new Binding(nameof(mainWindow.ViewModel.Languages)));
            mainWindow.comboValidationLanguage.SetValue(ComboBox.SelectedValuePathProperty, "Key");
            mainWindow.comboValidationLanguage.SetValue(ComboBox.DisplayMemberPathProperty, "Value");
            mainWindow.comboValidationLanguage.SetBinding(ComboBox.SelectedValueProperty, new Binding(nameof(mainWindow.ViewModel.Language)));

            mainWindow.textFirstName.SetBinding(TextBox.TextProperty, new Binding(nameof(mainWindow.ViewModel.FirstName)) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, ValidatesOnDataErrors = true });
            mainWindow.textLastName.SetBinding(TextBox.TextProperty, new Binding(nameof(mainWindow.ViewModel.LastName)) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, ValidatesOnDataErrors = true });
            mainWindow.textPhoneNumber.SetBinding(TextBox.TextProperty, new Binding(nameof(mainWindow.ViewModel.PhoneNumber)) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, ValidatesOnDataErrors = true });
            mainWindow.textEmail.SetBinding(TextBox.TextProperty, new Binding(nameof(mainWindow.ViewModel.Email)) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, ValidatesOnDataErrors = true });

            mainWindow.comboUserType.SetBinding(ComboBox.ItemsSourceProperty, new Binding(nameof(mainWindow.ViewModel.UserTypes)));
            mainWindow.comboUserType.SetValue(ComboBox.SelectedValuePathProperty, "Key");
            mainWindow.comboUserType.SetValue(ComboBox.DisplayMemberPathProperty, "Value");
            mainWindow.comboUserType.SetBinding(ComboBox.SelectedValueProperty, new Binding(nameof(mainWindow.ViewModel.UserType)));

            mainWindow.textUsername.SetBinding(TextBox.TextProperty, new Binding(nameof(mainWindow.ViewModel.Username)) { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged, ValidatesOnDataErrors = true });

            mainWindow.buttonSubmit.SetBinding(Button.CommandProperty, new Binding(nameof(mainWindow.ViewModel.SubmitCommand)) { Mode = BindingMode.OneWay });
            mainWindow.buttonSubmit.SetBinding(Button.IsEnabledProperty, new Binding(nameof(mainWindow.ViewModel.SubmitEnabled)) { Mode = BindingMode.OneWay });

            // display window
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();

            // setup validation message localization
            ValidationMessage.GetLocalizedMessage = messageKey => ValidationMessageTranslations.ResourceManager.GetString(messageKey, Thread.CurrentThread.CurrentUICulture);
}

        #endregion Protected Methods
    }
}
