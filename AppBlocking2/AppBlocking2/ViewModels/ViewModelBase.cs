using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace AppBlocking2.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Initialize()
        {
        }
    }
}