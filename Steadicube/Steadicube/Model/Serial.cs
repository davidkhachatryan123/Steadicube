using Steadicube.Classes;
using System.ComponentModel;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace Steadicube.Model
{
    public class Serial : INotifyPropertyChanged
    {
        private SerialPort? serialPort;

        private DispatcherTimer dispatcherTimer_Vectors = new DispatcherTimer();
        private bool isSend_Vectors = false;

        private DispatcherTimer dispatcherTimer_Rotate = new DispatcherTimer();
        private bool isSend_Rotate = false;


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


        public void OpenPort(string port, int baud)
        {
            serialPort = new SerialPort(port, baud, Parity.None, 8, StopBits.One);

            if (!serialPort.IsOpen)
                serialPort.Open();

            dispatcherTimer_Vectors.Tick += new EventHandler(dispatcherTimer_Vectors_Tick);
            dispatcherTimer_Vectors.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer_Vectors.Start();

            dispatcherTimer_Rotate.Tick += new EventHandler(dispatcherTimer_Rotate_Tick);
            dispatcherTimer_Rotate.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer_Rotate.Start();
        }

        public void ClosePort()
        {
            if (serialPort != null)
            {
                if (serialPort!.IsOpen)
                    serialPort!.Close();

                serialPort!.Dispose();
            }

            dispatcherTimer_Vectors.Stop();
            isSend_Vectors = false;
        }

        public void SendSerial_Vectors(Vector4D vector4D)
        {
            if (isSend_Vectors)
            {
                if (serialPort!.IsOpen)
                {
                    serialPort!.WriteLine("A: " + Math.Round(vector4D.A)
                            + "\nB: " + Math.Round(vector4D.B)
                            + "\nC: " + Math.Round(vector4D.C)
                            + "\nD: " + Math.Round(vector4D.D));

                    isSend_Vectors = false;
                }
            }
        }
        private void dispatcherTimer_Vectors_Tick(object? sender, EventArgs e)
        {
            isSend_Vectors = true;
        }


        public void SendSerial_ServoValue(string servo, string value)
        {
            if (serialPort!.IsOpen)
                serialPort!.WriteLine(string.Format("Servo: {0}, {1}", servo, value));
        }

        public void SendSerial_Rotate(string z, string x1)
        {
            if (isSend_Rotate)
            {
                if (serialPort!.IsOpen)
                {
                    serialPort!.WriteLine(string.Format("r: {0}, {1}", z, x1));

                    isSend_Rotate = false;
                }
            }
        }
        private void dispatcherTimer_Rotate_Tick(object? sender, EventArgs e)
        {
            isSend_Rotate = true;
        }


        ~Serial()
        {
            ClosePort();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
