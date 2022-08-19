using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Steadicube.Model
{
    public class Cube : INotifyPropertyChanged
    {
        private double width;
        private double length;
        private double height;

        public double Width
        {
            get => width;
            set
            {
                this.width = value;

                OnPropertyChanged("Width");
            }
        }
        public double Length
        {
            get => length;
            set
            {
                this.length = value;

                OnPropertyChanged("Length");
            }
        }
        public double Height
        {
            get => height;
            set
            {
                this.height = value;

                OnPropertyChanged("Height");
            }
        }


        private double widthBind;
        private double lengthBind;
        private double heightBind;
        public double WidthBind
        {
            get => widthBind;
            set
            {
                this.widthBind = value;

                OnPropertyChanged("WidthBind");
            }
        }
        public double LengthBind
        {
            get => lengthBind;
            set
            {
                this.lengthBind = value;

                OnPropertyChanged("LengthBind");
            }
        }
        public double HeightBind
        {
            get => heightBind;
            set
            {
                this.heightBind = value;

                OnPropertyChanged("HeightBind");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
