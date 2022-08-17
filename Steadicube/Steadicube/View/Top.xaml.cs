using Steadicube.ViewModel;
using System.Windows.Controls;

namespace Steadicube.View
{
    /// <summary>
    /// Interaction logic for Top.xaml
    /// </summary>
    public partial class Top : UserControl
    {
        public string Title { get; set; }

        public Top()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DataContext = new TopViewModel(Title);
        }
    }
}
