using Steadicube.ViewModel;
using System.Windows.Controls;

namespace Steadicube.View
{
    /// <summary>
    /// Interaction logic for Mode.xaml
    /// </summary>
    public partial class Mode : UserControl
    {
        public Mode()
        {
            InitializeComponent();

            this.DataContext = new ModeViewModel();
        }
    }
}
