using Calculo_ductos_winUi_3.Models;
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
using DuctsLib = Calculo_ductos.Facade;
using Calculo_ductos_winUi_3.Services;
using CommunityToolkit.Mvvm.Input;
using Calculo_ductos_winUi_3.Params;

namespace Calculo_ductos_winUi_3.ViewModels
{
    public class ComponentsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ComponentModel> _componentList;

        public ComponentsViewModel()
        {
            _componentList = new ObservableCollection<ComponentModel>();
            CalculateComponentsCommand = new RelayCommand<Duct>(CalculateComponents);

        }
        public ObservableCollection<ComponentModel> ComponentList
        {
            get => _componentList;
            set 
            {
                _componentList = value;
                OnPropertyChanged();
            }
        }
        public void New() { 
            ComponentList.Clear();
        }
        private void CalculateComponents(Duct args)
        {
            List<Calculo_ductos.Params.Component> components = args.Components;
            ComponentList = components.MapComponents();
        }
        public ICommand CalculateComponentsCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private bool NeedChimmey(ObservableCollection<FloorDescription> floors)
        {
            bool needChimmey = false;
            try
            {
                FloorDescription lastFloor = floors.Where(floor => floor.Type == Floor.TypeFloor.last).FirstOrDefault();
                if (lastFloor != null)
                {
                    needChimmey = lastFloor.NeedChimney;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return needChimmey;
        }
    }
}
