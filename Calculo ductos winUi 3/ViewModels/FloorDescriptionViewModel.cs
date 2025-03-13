using Calculo_ductos.Params;
using Calculo_ductos_winUi_3.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Calculo_ductos_winUi_3.ViewModels
{
    public class FloorDescriptionViewModel : INotifyPropertyChanged
    {
        private FloorDescription _floorDescription;

        public FloorDescriptionViewModel()
        {
            _floorDescription = new FloorDescription
            {
                Uuid = Guid.NewGuid(),
                Type = Floor.TypeFloor.common,
                FloorCount = 1,
                NeedGate = false,
                NeedChimney = false,
                FloorHeight = 2.5m
            };

            ChangeFloorTypeCommand = new RelayCommand<int>(SetFloorType);
        }

        public Floor.TypeFloor Type
        {
            get => _floorDescription.Type;
            set
            {
                if (_floorDescription.Type != value)
                {
                    _floorDescription.Type = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        public int FloorCount
        {
            get => _floorDescription.FloorCount;
            set
            {
                if (_floorDescription.FloorCount != value)
                {
                    _floorDescription.FloorCount = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool NeedGate
        {
            get => _floorDescription.NeedGate;
            set
            {
                if (_floorDescription.NeedGate != value)
                {
                    _floorDescription.NeedGate = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool NeedChimney
        {
            get => _floorDescription.NeedChimney;
            set
            {
                if (_floorDescription.NeedChimney != value)
                {
                    _floorDescription.NeedChimney = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal FloorHeight
        {
            get => _floorDescription.FloorHeight;
            set
            {
                if (_floorDescription.FloorHeight != value)
                {
                    _floorDescription.FloorHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description => _floorDescription.GetDescription();

        // Comando para cambiar el tipo de piso
        public ICommand ChangeFloorTypeCommand { get; }

        private void SetFloorType(int selection)
        {
            _floorDescription.SetFloorType(selection);
            OnPropertyChanged(nameof(Type));
            OnPropertyChanged(nameof(Description));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
