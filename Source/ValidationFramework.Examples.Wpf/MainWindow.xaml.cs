using System.Windows;
using ReactiveUI;

namespace ValidationFramework.Examples.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<MainWindowModel>
    {
        #region Public Fields

        /// <summary>
        /// The view model property dependency property.
        /// </summary>
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(MainWindowModel), typeof(MainWindow));

        #endregion Public Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The view model.
        /// </summary>
        object IViewFor.ViewModel
        {
            get
            {
                return this.ViewModel;
            }
            set
            {
                this.ViewModel = (MainWindowModel)value;
            }
        }

        /// <summary>
        /// The view model.
        /// </summary>
        public MainWindowModel ViewModel
        {
            get
            {
                return (MainWindowModel)this.GetValue(ViewModelProperty);
            }
            set
            {
                this.SetValue(ViewModelProperty, value);
            }
        }

        #endregion Public Properties
    }
}
