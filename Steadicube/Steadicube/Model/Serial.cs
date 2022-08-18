using System.ComponentModel;
using System.IO.Ports;
using System.Runtime.CompilerServices;

namespace Steadicube.Model
{
    public class Serial : INotifyPropertyChanged
    {
        private List<string> ports;

        public List<string> Ports
        {
            get
            {
                ports = new List<string>();

                foreach (string port in SerialPort.GetPortNames())
                    ports.Add(port);

                return ports;
            }
            private set
            {
                OnPropertyChanged("Ports");
            }
        }


        private List<string> bauds;

        public List<string> Bauds
        {
            get
            {
                bauds = new List<string>();

                bauds.Add("300");
                bauds.Add("600");
                bauds.Add("1200");
                bauds.Add("2400");
                bauds.Add("4800");
                bauds.Add("9600");
                bauds.Add("14400");
                bauds.Add("19200");
                bauds.Add("28800");
                bauds.Add("31250");
                bauds.Add("38400");
                bauds.Add("57600");
                bauds.Add("115200");

                return bauds;
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
