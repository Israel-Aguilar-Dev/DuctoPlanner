using Calculo_ductos_winUi_3.Models;
using Calculo_ductos_winUi_3.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Calculo_ductos.Params;    
using CommunityToolkit.Mvvm.Input;
using DuctsLib = Calculo_ductos.Facade;
namespace Calculo_ductos_winUi_3.ViewModels
{
    
    public class DuctsViewModel : INotifyPropertyChanged
    {
        #region Fields
        private ObservableCollection<DuctModel> _ductList;
        private ObservableCollection<FloorDuctDetailModel> _ductDetailList;
        #endregion
        public DuctsViewModel()
        {
            _ductList = new ObservableCollection<DuctModel>();
            _ductDetailList = new ObservableCollection<FloorDuctDetailModel>();
            CalculateDuctsCommand = new RelayCommand<ObservableCollection<FloorDescription>>(CalculateDucts);
        }

        public ObservableCollection<DuctModel> DucList
        {
            get => _ductList;
            set 
            {
                _ductList = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<FloorDuctDetailModel> DuctDetailList
        {
            get => _ductDetailList;
            set
            {
                _ductDetailList = value;
                OnPropertyChanged();
            }
        }
        private void CalculateDucts(ObservableCollection<FloorDescription> floors)
        {
            string json = floors.ToJsonString();
            
            Dictionary<DuctPiece.TypeDuct,int> ducts = DuctsLib.CalculateDucts(json);
            List<Floor> ductsDetail = DuctsLib.CalculateDuctsByFloor(json);

            DucList = ducts.MapDuctsFromDictionary();
            DuctDetailList = ductsDetail.MapToFloorDuctDetails();


        }
        public ICommand CalculateDuctsCommand { get; }
        public ICommand CalculateDuctsByFloorCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }

}
