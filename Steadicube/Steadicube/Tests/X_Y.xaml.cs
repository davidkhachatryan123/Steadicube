using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Steadicube.Tests
{
    /// <summary>
    /// Interaction logic for X_Y.xaml
    /// </summary>
    public partial class X_Y : Window
    {
        public X_Y()
        {
            InitializeComponent();
        }

        //int counter = 0;
        public void SetPoint(double x, double y)
        {
            //counter++;

            //if (counter == 1000)
            //{
            //canvas.Children.Clear();

            Ellipse ellipse = new Ellipse() { Width = 5, Height = 5, Stroke = new SolidColorBrush(Colors.Black), Fill = new SolidColorBrush(Colors.Black) };
            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);
            canvas.Children.Add(ellipse);

            //    counter = 0;
            //}
        }
    }
}
