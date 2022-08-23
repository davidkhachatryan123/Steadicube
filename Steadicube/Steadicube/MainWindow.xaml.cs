using Steadicube.Tests;
using Steadicube.ViewModel;
using System.Windows;

namespace Steadicube
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static X_Y x_y { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();

            x_y = new X_Y();
            //x_y.Show();
        }
    }
}
