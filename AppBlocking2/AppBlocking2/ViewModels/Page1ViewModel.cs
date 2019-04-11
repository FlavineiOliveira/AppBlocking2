using AppBlocking2.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppBlocking2.ViewModels
{
    public class Page1ViewModel : ViewModelBase
    {
        private ICommand _navegarCommand;
        public ICommand NavegarCommand
        {
            get { return _navegarCommand; }
            set
            {
                _navegarCommand = value;
                OnPropertyChanged();
            }
        }

        public Page1ViewModel()
        {
            NavegarCommand = new Command(Navegar);
        }

        private async void Navegar()
        {
            await Navigation.PushAsync(new BlockingPage());
        }
    }
}
