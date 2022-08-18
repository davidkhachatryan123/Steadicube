using Steadicube.Classes;
using Steadicube.Enums;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Steadicube.ViewModel
{
    public class ModeViewModel : INotifyPropertyChanged
    {
        public static ModeViewModel modeViewModel { get; set; }


        private RelayCommand loadedCommand;
        public ICommand LoadedCommand => loadedCommand ??= new RelayCommand(Loaded);

        private void Loaded(object commandParameter)
        {
            modeViewModel = this;
        }


        private M_Mode _m_Mode;
        public M_Mode m_Mode
        {
            get => _m_Mode;
            set
            {
                _m_Mode = value;

                OnPropertyChanged("m_Mode");
            }
        }

        private S_Mode _s_Mode;
        public string s_Mode
        {
            get => _s_Mode.ToString();
            set
            {
                Enum.TryParse<S_Mode>(value, out _s_Mode);

                OnPropertyChanged("s_Mode");
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
