using SharpDX.DirectInput;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using DeviceList = System.Collections.Generic.Dictionary<System.Guid, string>;

namespace Steadicube.Model
{
    public class JoyStick : INotifyPropertyChanged
    {
        private DeviceList devices;

        public DeviceList Devices
        {
            get
            {
                DirectInput directInput = new DirectInput();

                devices = new DeviceList();

                foreach (var deviceInstance in directInput.GetDevices())
                    devices.Add(deviceInstance.InstanceGuid, deviceInstance.ProductName);

                return devices;
            }
            private set
            {
                OnPropertyChanged("Devices");
            }
        }


        /*
        public Dictionary<Guid, string>? devices;
        

        public void UpdateList()
        {
            devices?.Clear();

            DirectInput directInput = new DirectInput();

            devices = new Dictionary<Guid, string>();

            foreach (var deviceInstance in directInput.GetDevices())
                devices.Add(deviceInstance.InstanceGuid, deviceInstance.ProductName);
        }

        public void SetJoystick(int JoystickIndex)
        {
            DeleteJoystick();

            if (JoystickIndex != -1)
            {
                DirectInput directInput = new DirectInput();

                joystick = new Joystick(directInput, devices!.ElementAt(JoystickIndex).Key);

                joystick.Properties.BufferSize = 128;

                joystick.Acquire();
            }
        }

        public void DeleteJoystick()
        {
            if (joystick != null)
                joystick.Dispose();
        }

        public void Update(Velocity velocity)
        {
            try
            {
                joystick!.Poll();
                var datas = joystick!.GetBufferedData();

                if (datas != null)
                    foreach (var state in datas)
                        CaptureJoystick.Capture(state.Offset, state.Value, velocity);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void MoveCube(MainWindow main, TranslateTransform3D CubeObj, Structures.Vector3D cubeSize, double speed, double angle, Action<Structures.Vector4D> vector4D, CancellationToken ct)
        {
            Task task = Task.Run(() =>
            {
                Velocity velocity = new Velocity();
                Cube cube = new Cube(CubeObj);

                while (true)
                {
                    this.Update(velocity);

                    try
                    {
                        main.Dispatcher.Invoke(() =>
                        {
                            vector4D(cube.MoveCube(velocity, cubeSize, speed, angle));

                            main.X_L.Content = velocity.x;
                            main.Y_L.Content = velocity.y;
                            main.Z_L.Content = velocity.z;
                        });

                        if (ct.IsCancellationRequested)
                            return;
                    }
                    catch { }

                    Thread.Sleep(10);
                }
            });
        }*/

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
