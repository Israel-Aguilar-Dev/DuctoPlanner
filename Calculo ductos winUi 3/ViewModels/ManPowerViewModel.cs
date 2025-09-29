using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculo_ductos_winUi_3.ViewModels
{
    using Calculo_ductos.Params;
    using CommunityToolkit.Mvvm.Input;
    using DocumentFormat.OpenXml.Drawing;
    using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
    using Models;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class ManPowerViewModel : ObservableObject
    {
        #region Fields
        private CatalogResourceModel _resource;
        private CatalogResourceTypeModel _resourceType;
        private CatalogRentabilityModel _SelectedRentability;
        private EfectiveWorkDayModel _EfectiveWorkDays;
        private decimal _TotalPriceManPower;
        private decimal _SubTotalPriceManPower;
        #endregion
        public ManPowerViewModel()
        {
            _resource = new CatalogResourceModel();
            _resourceType = new CatalogResourceTypeModel();
            _EfectiveWorkDays = new EfectiveWorkDayModel();
            _TotalPriceManPower = 0m;
            _SubTotalPriceManPower = 0m;
            _SelectedRentability = new CatalogRentabilityModel();
            AddResourceCommand = new RelayCommand(AddResource);
            RemoveResourceCommand = new RelayCommand<Guid>(RemoveResource);
            ManPower = new ObservableCollection<HumanResourceModel>();
            Subtotals = new ObservableCollection<SubtotalHumaResource>();
        }
        public void New()
        {
            _resource = new CatalogResourceModel();
            _resourceType = new CatalogResourceTypeModel();
            _SelectedRentability = new CatalogRentabilityModel();
            _TotalPriceManPower = 0;
            _SubTotalPriceManPower = 0;
            ManPower.Clear();
            _EfectiveWorkDays = new EfectiveWorkDayModel();
            Subtotals.Clear();

        }
        #region Constructor
        #endregion
        #region Properties
        public CatalogResourceModel Resource
        {
            get => _resource;
            set {
                SetProperty(ref _resource, value);
            }
        }
        public CatalogResourceTypeModel ResourceType
        {
            get => _resourceType;
            set
            {
                SetProperty(ref _resourceType, value);
            }
        }
        public EfectiveWorkDayModel EfectiveWorkDays
        {
            get => _EfectiveWorkDays;
            set { SetProperty(ref _EfectiveWorkDays, value); }
        }
        public CatalogRentabilityModel SelectedRentability { 
            get => _SelectedRentability;
            set { 
                SetProperty(ref _SelectedRentability, value);
            }
        }
        public ObservableCollection<HumanResourceModel> ManPower { get; set; }
        public ObservableCollection<CatalogResourceModel> AvailableResources { get; set; }
        public ObservableCollection<CatalogResourceTypeModel> AvailableResourceTypes { get; set; }
        public ObservableCollection<SubtotalHumaResource> Subtotals { get; set; }
        public ObservableCollection<CatalogRentabilityModel> AvailableRentabilities { get; set; }
        public decimal TotalPriceManPower
        {
            get => _TotalPriceManPower;
            set {
                SetProperty(ref _TotalPriceManPower, value);
            }
        }
        public decimal SubTotalPriceManPower
        {
            get => _SubTotalPriceManPower;
            set
            {
                SetProperty(ref _SubTotalPriceManPower, value);
            }
        }
        #endregion
        #region Public Methods

        public async Task CalculateManPower()
        {
            Subtotals.Clear ();
            foreach (var model in ManPower) {
                Subtotals.Add(new SubtotalHumaResource {Descripcion = model.Recurso.Description, Subtotal = model.PrecioTotal });
            }
            Subtotals.Add(new SubtotalHumaResource { Descripcion = "Visita técnica", Subtotal = AvailableResources.Where(p => p.Id == 2).FirstOrDefault().SalaryPerWorkday});
            SubTotalPriceManPower = Subtotals.Select(p => p.Subtotal).Sum();
            TotalPriceManPower = SubTotalPriceManPower * SelectedRentability.Rentability;
        }
        public void LoadCatalogs(List<CatalogResourceModel> resources, List<CatalogResourceTypeModel> resourceTypes, List<CatalogRentabilityModel> rentabilities)
        {
            AvailableResources = new ObservableCollection<CatalogResourceModel>(resources);
            AvailableResourceTypes = new ObservableCollection<CatalogResourceTypeModel>(resourceTypes);
            AvailableRentabilities = new ObservableCollection<CatalogRentabilityModel>(rentabilities);
        }
        public void CalculateWorkDays(Duct duct,CatalogRowEntityModel entidad)
        {
            EfectiveWorkDays.WorkDaysBase = Convert.ToInt32(Math.Ceiling((duct.floors.Count ) / 2.5));
            EfectiveWorkDays.WorkDaysDobleFloors = duct.floors.Where(p => p.Height >= 4.5m ).ToList().Count * 0.5;
            EfectiveWorkDays.WorkDaysExtraFloors = duct.floors.Count > 10 ? 1 : 0;
            EfectiveWorkDays.WorkDayForeign = entidad.Name.Equals("CIUDAD DE MÉXICO") ? 0 : 1;
        }
        #endregion
        #region Commands
        public ICommand AddResourceCommand { get; }
        public ICommand RemoveResourceCommand { get; }
        #endregion
        #region Private Methods
        private void AddResource()
        {
            var human = new HumanResourceModel();
            human.Recurso = Resource;
            human.TipoRecurso = ResourceType;
            human.JornadasEfectivas = Resource.Id != 1 ? EfectiveWorkDays.TotalWorkDays + 3 : EfectiveWorkDays.TotalWorkDays;
            human.DiasNoLaborales = ResourceType.Id == 1 ? human.JornadasEfectivas / 7 : 0;
            ManPower.Add(human);
        }
        private void RemoveResource(Guid uuid)
        {
            ManPower.Remove(ManPower.FirstOrDefault(x => x.Uuid == uuid));
        }
        #endregion
    }
}
