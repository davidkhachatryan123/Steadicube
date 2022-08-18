using Steadicube.ViewModel;
using System.Windows.Controls;

namespace Steadicube.View
{
    /// <summary>
    /// Interaction logic for StatusBar.xaml
    /// </summary>
    public partial class StatusBar3D : UserControl
    {
        public StatusBar3D()
        {
            InitializeComponent();

            this.DataContext = new StatusBar3DViewModel();
        }
    }
}
