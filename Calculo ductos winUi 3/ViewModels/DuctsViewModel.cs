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
using Calculo_ductos.Utils;
using DocumentFormat.OpenXml.Drawing.Charts;
namespace Calculo_ductos_winUi_3.ViewModels
{
    
    public class DuctsViewModel : INotifyPropertyChanged
    {
        #region Fields
        private ObservableCollection<DuctModel> _ductList;
        private ObservableCollection<FloorDuctDetailModel> _ductDetailList;
        private Duct _completeDuct;
        #endregion
        public DuctsViewModel()
        {
            _ductList = new ObservableCollection<DuctModel>();
            _ductDetailList = new ObservableCollection<FloorDuctDetailModel>();
            _completeDuct = new Duct();
            CalculateDuctsCommand = new RelayCommand<string>(CalculateDucts);
        }
        public void New() {
            DucList.Clear();
            DuctDetailList.Clear();
            CompleteDuct = new Duct();
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
        public Duct CompleteDuct {
            get => _completeDuct;
            set {
                _completeDuct = value;
                OnPropertyChanged();
            }
        }
        private void CalculateDucts(string json)
        {
            //string json = state.ToJsonString();
            
            //Dictionary<DuctPiece.TypeDuct,int> ducts = DuctsLib.CalculateDucts(json);
            //List<Floor> ductsDetail = DuctsLib.CalculateDuctsByFloor(json);
            Duct completeDuct = DuctsLib.CalculateDuctsByFloor(json);
            CompleteDuct = completeDuct;
            DucList = completeDuct.floors.SumDuctPieces().MapDuctsFromList();
            var list = completeDuct.floors.MapToFloorDuctDetails();
            
            DuctDetailList = list;
            //DuctDetailList = completeDuct.floors.MapToFloorDuctDetails();
            //string? lastFloor = null;
            //foreach (var duct in DuctDetailList)
            //{
            //    duct.IsNewFloor = duct.FloorName != lastFloor;
            //    lastFloor = duct.FloorName;
            //}


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
