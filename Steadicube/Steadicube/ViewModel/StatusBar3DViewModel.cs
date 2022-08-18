using Steadicube.Classes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Steadicube.ViewModel
{
    public class StatusBar3DViewModel : INotifyPropertyChanged
    {
        public static StatusBar3DViewModel statusBar3DViewModel { get; set; }


        private RelayCommand loadedCommand;
        public ICommand LoadedCommand => loadedCommand ??= new RelayCommand(Loaded);

        private void Loaded(object commandParameter)
        {
            statusBar3DViewModel = this;
        }


        private Vector3D _vector3D;

        public Vector3D vector3D
        {
            get => _vector3D;
            set
            {
                _vector3D = value;

                OnPropertyChanged("Vector3D");
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
