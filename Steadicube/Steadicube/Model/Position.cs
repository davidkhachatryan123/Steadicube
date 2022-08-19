using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Steadicube.Model
{
    public class Position : INotifyPropertyChanged
    {
        private double x { get; set; } = 0;
        private double y { get; set; } = 0;
        private double z { get; set; } = 0;

        public double X
        {
            get => x;
            set
            {
                this.x = value;

                OnPropertyChanged("X");
            }
        }
        public double Y
        {
            get => y;
            set
            {
                this.y = value;

                OnPropertyChanged("Y");
            }
        }
        public double Z
        {
            get => z;
            set
            {
                this.z = value;

                OnPropertyChanged("Z");
            }
        }


        public Position()
        {

        }
        public Position(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
