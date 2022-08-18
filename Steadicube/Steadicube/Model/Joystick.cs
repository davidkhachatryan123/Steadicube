using SharpDX.DirectInput;
using Steadicube.Classes;
using Steadicube.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

using DeviceList = System.Collections.Generic.Dictionary<System.Guid, string>;

namespace Steadicube.Model
{
    public class JoyStick : INotifyPropertyChanged
    {
        private DeviceList devices;
        private Joystick joystick;

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

        public void SetJoystick(int JoystickIndex)
        {
            DeleteJoystick();

            DirectInput directInput = new DirectInput();
            joystick = new Joystick(directInput, devices!.ElementAt(JoystickIndex).Key);
            joystick.Properties.BufferSize = 128;
            joystick.Acquire();
        }

        public void DeleteJoystick()
        {
            if (joystick != null)
                joystick.Dispose();
        }

        private void Update(JoystickMovement joystickMovement)
        {
            try
            {
                joystick!.Poll();
                var datas = joystick!.GetBufferedData();

                if (datas != null)
                    foreach (var state in datas)
                        JoystickCapture.Capture(state.Offset, state.Value, joystickMovement);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        public void Start(StatusBar3DViewModel statusBar3D)
        {
            JoystickMovement joystickMovement = new JoystickMovement();

            Task task = Task.Run(() =>
            {
                while (true)
                {
                    Update(joystickMovement);

                    statusBar3D.vector3D.X = joystickMovement.Left_Stick_X;
                }
            });
        }




















        /*
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
