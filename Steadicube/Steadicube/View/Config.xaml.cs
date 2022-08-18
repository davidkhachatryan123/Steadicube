using Steadicube.Model;
using Steadicube.ViewModel;
using System.Windows.Controls;

namespace Steadicube.View
{
    /// <summary>
    /// Interaction logic for Config.xaml
    /// </summary>
    public partial class Config : UserControl
    {
        public Config()
        {
            InitializeComponent();

            this.DataContext = new ConfigViewModel();
            this.Cube.DataContext = new Cube();
            this.Camera.DataContext = new Camera();
        }
    }
}
