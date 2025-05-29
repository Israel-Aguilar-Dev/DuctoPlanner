using Calculo_ductos_winUi_3.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_ductos_winUi_3.ViewModels
{
    public class CompleteDuctViewModel : INotifyPropertyChanged
    {
        private int _PurposeId;
        private string _ExecutiveName;
        private string _PT;
        private int _SheetTypeId;
        //private int _DischargeTypeId;
        
        
        public CompleteDuctViewModel() {
            PurposeId = 1;
            ExecutiveName = string.Empty;
            SheetTypeId = 0;
        }
        public int PurposeId 
        {
            get => _PurposeId;
            set {
                _PurposeId = value;
                OnPropertyChanged();
            }
        }
        public string ExecutiveName 
        {
            get => _ExecutiveName;
            set {
                _ExecutiveName = value;
                OnPropertyChanged();
            }
        }
        public string PT
        {
            get => _PT;
            set
            {
                _PT = value;
                OnPropertyChanged();
            }
        }
        public int SheetTypeId
        {
            get => _SheetTypeId;
            set {
                _SheetTypeId = value;
                OnPropertyChanged();
            }
        }
        //public int DischargeTypeId
        //{
        //    get => _DischargeTypeId;
        //    set
        //    {
        //        _DischargeTypeId = value;
        //        OnPropertyChanged();
        //    }
        //}
        

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
