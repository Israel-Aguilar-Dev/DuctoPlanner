using Calculo_ductos.Params;
using Calculo_ductos_winUi_3.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace Calculo_ductos_winUi_3.ViewModels
{
    public class FloorDescriptionViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<FloorDescription> _floorList;
        private FloorDescription _floorDescription;
        private int _typeDuctIndex;
        private int _needChimmeyIndex;
        private int _needGateIndex;
        private int _typeDischargeIndex;
        private int _typeDoorIndex;
        //private CatalogRowModel _TypeDoorSelected;

        #endregion

        #region Constructor

        public FloorDescriptionViewModel()
        {
            _floorList = new ObservableCollection<FloorDescription>();
            _floorDescription = new FloorDescription
            {
                //Uuid = Guid.NewGuid(),
                Type = Floor.TypeFloor.discharge,
                Discharge = Floor.TypeDischarge.guilloutine,
                FloorCount = 0,
                NeedGate = false,
                NeedChimney = false,
                FloorHeight = 0m,
                DoorTypeId = 0
            };
            _typeDuctIndex = 0;
            _typeDoorIndex = 0;

            ChangeFloorTypeCommand = new RelayCommand<int>(SetFloorType);
            ChangeDischargeTypeCommand = new RelayCommand<int>(SetFloorTypeDischarge);
            //ChangeDoorTypeCommand = new RelayCommand<string>(SetFloorTypeDoor);
            AddFloorCommand = new RelayCommand(AddFloor);
            RemoveFloorCommand = new RelayCommand<Guid>(RemoveFloor);
        }

        #endregion

        #region Properties
        public ObservableCollection<CatalogRowModel> AllDoorType { get; set; }
        public ObservableCollection<CatalogRowModel> AvailableDoorTypes { get; set; } = new();
        public ObservableCollection<FloorDescription> FloorList
        {
            get => _floorList;
            set
            {
                _floorList = value;
                OnPropertyChanged();
            }
        }
        public CatalogRowModel TypeDoorSelected
        {
            get => _floorDescription.TypeDoor;
            set 
            {
                if (_floorDescription.TypeDoor != value)
                {
                    _floorDescription.TypeDoor = value;
                    OnPropertyChanged();
                }
            }
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

        public Floor.TypeDischarge Discharge

        {
            get => _floorDescription.Discharge;
            set
            {
                if (_floorDescription.Discharge != value)
                {
                    _floorDescription.Discharge = value;
                    OnPropertyChanged();
                }
            }
        }

        public Guid Uuid => _floorDescription.Uuid;

        public string FloorCountText
        {
            get => _floorDescription.FloorCount.ToString();
            set
            {
                if (int.TryParse(value, out int result))
                {
                    _floorDescription.FloorCount = result;
                    OnPropertyChanged();
                }
            }
        }

        public int TypeDuctIndex
        {
            get => _typeDuctIndex;
            set
            {
                _typeDuctIndex = value;
                _floorDescription.SetFloorType(value);
                OnPropertyChanged();
            }
        }
        public int TypeDischargeIndex
        {
            get => _typeDischargeIndex;
            set
            {
                _typeDischargeIndex = value;
                _floorDescription.SetFloorTypeDischarge(value);
                OnPropertyChanged();
            }
        }

        public int NeedGateIndex
        {
            get => _floorDescription.NeedGate ? 0 : 1;
            set
            {
                _needGateIndex = value;
                _floorDescription.NeedGate = value == 0;
                OnPropertyChanged();
            }
        }

        public int NeedChimmeyIndex
        {
            get => _floorDescription.NeedChimney ? 0 : 1;
            set
            {
                _needChimmeyIndex = value;
                _floorDescription.NeedChimney = TypeDuctIndex == 2 ? value == 0 : false;
                OnPropertyChanged();
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

        public string FloorHeightText
        {
            get => _floorDescription.FloorHeight.ToString();
            set
            {
                if (decimal.TryParse(value, out decimal result))
                {
                    _floorDescription.FloorHeight = result;
                    OnPropertyChanged();
                }
            }
        }

        public string Description => _floorDescription.Description;
        

        #endregion

        #region Commands

        public ICommand ChangeFloorTypeCommand { get; }
        public ICommand ChangeDischargeTypeCommand { get; }
        public ICommand ChangeDoorTypeCommand { get; }
        public ICommand AddFloorCommand { get; }
        public ICommand RemoveFloorCommand { get; }

        #endregion

        #region Methods

        public void AddFloor()
        {
            _floorList.Add(new FloorDescription
            {
                Uuid = Guid.NewGuid(),
                FloorCount = _floorDescription.FloorCount,
                NeedGate = _floorDescription.NeedGate,
                NeedChimney = _floorDescription.NeedChimney,
                FloorHeight = _floorDescription.FloorHeight,
                Type = _floorDescription.Type,
                Discharge = _floorDescription.Discharge,
                TypeDoor = _floorDescription.TypeDoor
            });
        }

        public void RemoveFloor(Guid floorUuid)
        {
            _floorList.Remove(_floorList.FirstOrDefault(x => x.Uuid == floorUuid));
        }

        private void SetFloorType(int selection)
        {
            _floorDescription.SetFloorType(selection);
            OnPropertyChanged(nameof(Type));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(NeedGateIndex));
        }
        private void SetFloorTypeDischarge(int selection) {
            _floorDescription.SetFloorTypeDischarge(selection);
            OnPropertyChanged(nameof(Discharge));
        }
        //private void SetFloorTypeDoor(string selection)
        //{
        //    _floorDescription.SetFloorTypeDoor(selection);
        //    OnPropertyChanged(nameof(Discharge));
        //}
        public void LoadCatalogs(List<CatalogRowModel> catalogo)
        {
            
            AllDoorType = new ObservableCollection<CatalogRowModel>(catalogo);
        }

        public void FilterDoorsTypes(string ductPurpose)
        {
            AvailableDoorTypes.Clear();
            var filtrados = AllDoorType
                .Where(p => p.Class.Equals(ductPurpose, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var item in filtrados)
                AvailableDoorTypes.Add(item);

            // Reset selección si no es válida
            if (!AvailableDoorTypes.Contains(TypeDoorSelected))
                TypeDoorSelected = AvailableDoorTypes.FirstOrDefault();
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

}
