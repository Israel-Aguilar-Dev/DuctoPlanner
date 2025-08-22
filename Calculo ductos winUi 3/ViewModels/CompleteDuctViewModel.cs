using Calculo_ductos_winUi_3.Models;
using DocumentFormat.OpenXml.Drawing.Charts;
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
        private int _QuoteVersion;
        private bool _NeedSprinkler;
        private bool _NeedDesinfectionSystem;
        private ObservableCollection<QuoteModel> _Quotes { get; set; }
        //private int _DischargeTypeId;
        
        
        public CompleteDuctViewModel() {
            PurposeId = 1;
            ExecutiveName = string.Empty;
            SheetTypeId = 0;
            NeedSprinklerValue = 1;
            NeedDesinfetionSystemValue = 1;
        }
        public void New() {
            PurposeId = 1;
            ExecutiveName = string.Empty;
            PT = string.Empty;
            SheetTypeId = 0;
            QuoteVersion = 0;
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
        public int QuoteVersion 
        {
            get => _QuoteVersion;
            set {
                _QuoteVersion = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(QuoteVersionDescription));
            }
        }
        public string QuoteVersionDescription
        {
            get => QuoteVersion > 0 ? $"Versión: {QuoteVersion.ToString()}" : string.Empty;
        }
        public int NeedSprinklerValue
        {
            get => _NeedSprinkler ? 0 : 1;
            set {
                _NeedSprinkler = value == 0 ? true : false;
                OnPropertyChanged(nameof(NeedSprinklerValue));
            }
        }
        public string NeedSprinklerString
        {
            get => _NeedSprinkler ? "Si" : "No";
        }
        public bool NeedSprinkler
        {
            get => _NeedSprinkler;
            set
            {
                if (_NeedSprinkler != value)
                {
                    _NeedSprinkler = value;
                    OnPropertyChanged(nameof(NeedSprinkler));
                    OnPropertyChanged(nameof(NeedSprinklerString));
                    OnPropertyChanged(nameof(NeedSprinklerValue));
                }
            }
        }
        public int NeedDesinfetionSystemValue
        {
            get => _NeedDesinfectionSystem ? 0 : 1;
            set
            {
                _NeedDesinfectionSystem = value == 0 ? true : false;
                OnPropertyChanged(nameof(NeedDesinfetionSystemValue));
            }
        }
        public string NeedDesinfetionSystemString
        {
            get => _NeedDesinfectionSystem ? "Si" : "No";
        }
        public bool NeedDesinfectionSystem
        {
            get => _NeedDesinfectionSystem;
            set 
            {
                if (_NeedDesinfectionSystem != value)
                {
                    _NeedDesinfectionSystem = value;
                    OnPropertyChanged(nameof(NeedDesinfectionSystem));
                    OnPropertyChanged(nameof(NeedDesinfetionSystemString));
                    OnPropertyChanged(nameof(NeedDesinfetionSystemValue));
                }
            }
        }
        public ObservableCollection<QuoteModel> Quotes
        {
            get => _Quotes;
            set 
            {
                _Quotes = value;
                OnPropertyChanged();
            }
        }
        

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
