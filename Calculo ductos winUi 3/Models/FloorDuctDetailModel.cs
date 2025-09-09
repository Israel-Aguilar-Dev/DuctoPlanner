using Calculo_ductos.Params;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_ductos_winUi_3.Models
{
    public class FloorDuctDetailModel : INotifyPropertyChanged
    {
        public string FloorName { get; set; }
        public DuctPiece.TypeDuct DuctType { get; set; }
        public int Count { get; set; }
        private bool _isNewFloor;
        public bool IsNewFloor
        {
            get => _isNewFloor;
            set
            {
                if (_isNewFloor != value)
                {
                    _isNewFloor = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(StrokeBrush));
                    OnPropertyChanged(nameof(StrokeThickness));
                }
            }
        }
        public SolidColorBrush StrokeBrush => GetBursh();
        public double StrokeThickness => GetStroke();
        public DoubleCollection StrokeDashArray => GetStrokeDashArray();
        public SolidColorBrush GetBursh() {
           return IsNewFloor ? 
                new SolidColorBrush(Microsoft.UI.Colors.LightCyan) : 
                new SolidColorBrush(Microsoft.UI.Colors.Gray);
        }
        public double GetStroke() {
            return IsNewFloor ? 1.0 : 0.5;
        }
        public DoubleCollection GetStrokeDashArray()
        {
            return IsNewFloor ? new DoubleCollection { 1, 0 } : new DoubleCollection { 8, 12 };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
