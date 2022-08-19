using SharpDX.DirectInput;
using Steadicube.Classes;
using Steadicube.Enums;
using Steadicube.ViewModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;
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

                JoystickCapture.Capture(JoystickOffset.Buttons99, -1, joystickMovement);

                if (datas != null)
                    foreach (var state in datas)
                        JoystickCapture.Capture(state.Offset, state.Value, joystickMovement);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //double counter = 0;
        public void Start(Settings settings)
        {
            JoystickMovement joystickMovement = new JoystickMovement();

            Task task = Task.Run(() =>
            {
                M_Mode m_Mode = M_Mode.M1;

                while (true)
                {
                    Update(joystickMovement);

                    if (ApplicationInit(joystickMovement))
                    {
                        ChangeMode(joystickMovement.Right_Btn_RIGHT, ref m_Mode);

                        //counter++;
                        //StatusBar3DViewModel.statusBar3DViewModel.vector3D = new System.Windows.Media.Media3D.Vector3D(counter, 0, 0);
                    }
                }
            });
        }


        private bool isInit = false;
        private bool ApplicationInit(JoystickMovement joystickMovement)
        {
            if (joystickMovement.Right_Stick_BTN)
                isInit = true;

            return isInit;
        }


        private M_Mode ChangeMode(bool btn, ref M_Mode mode)
        {


            return mode;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
