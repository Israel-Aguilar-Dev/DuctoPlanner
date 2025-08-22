using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using RBush;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_ductos_winUi_3.Services
{
    public class BusyService : ObservableObject
    {
        private bool _isBusy;
        private string _statusMessage;
        public BusyService() { 
            _isBusy = false;
            _statusMessage = string.Empty;
        }
        public Visibility IsVisible
        {
            get => IsBusy ? Visibility.Visible : Visibility.Collapsed;
        }
        public bool IsBusy
        {
            get => _isBusy;
            set 
            {
                if (SetProperty(ref _isBusy, value))
                {
                    OnPropertyChanged(nameof(IsVisible)); // <-- Notifica a la UI
                }
            }
        }
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }
    }
}
