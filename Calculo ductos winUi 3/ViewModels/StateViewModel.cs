using Calculo_ductos.Params;
using Calculo_ductos_winUi_3.Models;
using Calculo_ductos_winUi_3.Services;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;


namespace Calculo_ductos_winUi_3.ViewModels
{
    public class StateViewModel 
    {
        #region Fields
        private readonly string _baseUrl = "http://192.168.10.228:8092/CotizadorApiVertical/Api/";
        private List<CatalogRowModel> PurposeCatalog;
        private List<CatalogRowModel> DoorTypeCatalog;
        private List<CatalogRowModel> SheetTypeCatalog;
        private List<CatalogRowModel> FloorTypeCatalog;
        private List<CatalogRowEntityModel> StateCatalog;
        private List<CatalogRowEntityModel> MunicipalityCatalog;
        private List<CatalogRowEntityModel> LocalityCatalog;
        private List<CatalogRowTruckTypeModel> TruckTypeCatalog;

        private WebApi Client;
        private readonly BusyService _Busy;
        #endregion
        #region Constructor
        public StateViewModel()
        {
            _Busy = new BusyService();
            Client = new WebApi(_baseUrl);
            _ = InitializeAsync();
            FloorVM = new FloorDescriptionViewModel();
            DuctsVM = new DuctsViewModel();
            ComponentsVM = new ComponentsViewModel();
            CompleteDuctVm = new CompleteDuctViewModel();
            CompleteDuctVm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(CompleteDuctVm.PurposeId))
                {
                    FloorVM.FilterDoorsTypes(CompleteDuctVm.PurposeId == 0 ? "ropa" : "basura");
                }
            };
            FreightVM = new CalculateFreightViewModel(Client);
        }

        #endregion
        #region Properties
        public BusyService Busy
        {
            get => _Busy;
        }
        public FloorDescriptionViewModel FloorVM { get; }
        public DuctsViewModel DuctsVM { get; }
        public ComponentsViewModel ComponentsVM { get; }
        public CompleteDuctViewModel CompleteDuctVm { get; }
        public CalculateFreightViewModel FreightVM { get; }
        public string AppVersion => GetAppVersion();
        

        #endregion


        public async Task CalculateDucts(object sender, RoutedEventArgs e)
        {
            await ShowLoader("Calculando despiece...");

            DuctsVM.CalculateDuctsCommand.Execute(this.ToJsonString());
            ComponentsVM.CalculateComponentsCommand.Execute(DuctsVM.CompleteDuct);

            RemoveEmptyPieces();
            DiferenceFloors();

            await HideLoader("Calculo terminado.");
        }
        private void RemoveEmptyPieces() 
        {
            var floorList = DuctsVM.DuctDetailList.GroupBy(duct => duct.FloorName).Select(g => new { Name = g.Key, Count = g.Count() }).ToList();
            var itemsToRemove = DuctsVM.DuctDetailList.Where(duct => duct.Count == 0).ToList();
            foreach (var item in itemsToRemove)
            {
                DuctsVM.DuctDetailList.Remove(item);
            }

            foreach (var floorName in floorList)
            {
                if (floorName.Count == itemsToRemove.Where(d => d.FloorName.Equals(floorName.Name)).Count())
                    DuctsVM.DuctDetailList.Insert(0, new FloorDuctDetailModel { FloorName = floorName.Name, DuctType = DuctPiece.TypeDuct.SinDucto });
            }

            var ductsToRemove = DuctsVM.DucList.Where(duct => duct.Count == 0).ToList();
            foreach (var item in ductsToRemove)
            {
                DuctsVM.DucList.Remove(item);
            }

            var componentsToRemove = ComponentsVM.ComponentList.Where(component => component.Count == 0).ToList();
            foreach (var item in componentsToRemove)
            {
                ComponentsVM.ComponentList.Remove(item);
            }
        }
        private void DiferenceFloors()
        {
            //string? lastFloor = null;
            var count = DuctsVM.DuctDetailList.Count - 2;
            for ( int i = 0;  i <= count; i++)
            {
                var duct = DuctsVM.DuctDetailList[i];
                var ductNext = DuctsVM.DuctDetailList[i+1];
                duct.IsNewFloor = duct.FloorName != ductNext.FloorName;
                if (i == count)
                    ductNext.IsNewFloor = true;
                //lastFloor = duct.FloorName;
            }
        }
        public async Task CalculateFreigth(object sender, RoutedEventArgs e) 
        {
            await ShowLoader("Cargando información...");
            await FreightVM.CalculateFreight(DuctsVM.DucList.ToList());
            await HideLoader("Informacion cargada", 500);
        }
        public void CalculateManPower() { }
        public void CalculateIndirects() { }
        private async Task InitializeAsync()
        {
            await ShowLoader("Cargando datos...");
            await LoadCatalogsAsync();
            await LoadQuotesAsync();
            await HideLoader("Datos cargados.");
            
        }

        #region Api
        private async Task LoadCatalogsAsync() 
        {
            PurposeCatalog = new List<CatalogRowModel>();
            DoorTypeCatalog = new List<CatalogRowModel>();
            SheetTypeCatalog = new List<CatalogRowModel>();
            FloorTypeCatalog = new List<CatalogRowModel>();
            
            try
            {
                var catalogListWrapper = await Client.GetAsync<CatalogModelList>("Catalog");

                if (catalogListWrapper != null)
                {
                    PurposeCatalog = catalogListWrapper.PurposeCatalog ?? new();
                    DoorTypeCatalog = catalogListWrapper.DoorTypeCatalog ?? new();
                    SheetTypeCatalog = catalogListWrapper.SheetTypeCatalog ?? new();
                    FloorTypeCatalog = catalogListWrapper.FloorTypeCatalog ?? new();
                    StateCatalog = catalogListWrapper.Entities ?? new();
                    MunicipalityCatalog = catalogListWrapper.Municipalities ?? new();
                    LocalityCatalog = catalogListWrapper.Localities ?? new();
                    TruckTypeCatalog = catalogListWrapper.TruckTypeCatalog ?? new();

                    FloorVM.LoadCatalogs(DoorTypeCatalog);
                    FloorVM.FilterDoorsTypes("basura");//ya que esta seleccionado por automatico basura

                    FreightVM.LoadCatalogs(StateCatalog, MunicipalityCatalog, LocalityCatalog, TruckTypeCatalog);

                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"[Catalog Load Error]: {ex.Message}");
               
            }
        }
        private async Task LoadQuotesAsync() {
            try
            {
                var quotes = await Client.GetAsync<ObservableCollection<QuoteModel>>("Quoter");

                if (quotes != null)
                {
                    //CompleteDuctVm.Quotes.Clear();
                    CompleteDuctVm.Quotes = quotes;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"[Quotes Load Error]: {ex.Message}");

            }
        }
        private async Task LoadQuoteDetail(int id)
        {
            try
            {
                var quote = await Client.GetAsync<QuoteDetailModel>($"Quoter/{id}");

                if (quote != null)
                {
                    this.New();
                    CompleteDuctVm.QuoteVersion = quote.NumeroVersion;
                    CompleteDuctVm.ExecutiveName = quote.NombreEjecutivo;
                    CompleteDuctVm.PT = quote.PT;
                    CompleteDuctVm.PurposeId = quote.PropositoId;
                    CompleteDuctVm.SheetTypeId = quote.TipoLaminaId;
                    CompleteDuctVm.NeedSprinkler = quote.NecesitaAspersor;
                    CompleteDuctVm.NeedDesinfectionSystem = quote.NecesitaSistemaDD;
                    FloorVM.FloorList = quote.MapQuoteDetailToFloorList(FloorVM);

                    DuctsVM.CalculateDuctsCommand.Execute(this.ToJsonString());
                    ComponentsVM.CalculateComponentsCommand.Execute(DuctsVM.CompleteDuct);
                    RemoveEmptyPieces();
                    DiferenceFloors();
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"[Quotes Load Error]: {ex.Message}");

            }
        }
       
        private async Task SaveState() 
        {
            await ShowLoader("Guardando datos...");
            try
            {
                var quote = this.MapStateAppToQuoteDetail();
                var response = await Client.PostAsync<QuoteDetailModel, QuoteInsertionResultModel>("Quoter", quote);
                if (response != null)
                {
                    CompleteDuctVm.QuoteVersion = response.Version;
                    await LoadQuotesAsync();
                    await HideLoader("Datos guardados con exito.");
                }
                else await HideLoader("No se pudo guardar los datos.");
            }
            catch (Exception ex)
            {

                await HideLoader("Ocurrio un erro al guardar los datos: "+ex.Message,18000);
            }
            

        }
        #endregion


        #region UI
        private string GetAppVersion()
        {
            PackageVersion v = Package.Current.Id.Version;
            return $"Version: {v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
        }
        public void New() {
            FloorVM.New();
            DuctsVM.New();
            ComponentsVM.New();
            CompleteDuctVm.New();
            FreightVM.New();
        }
        public async Task LoadQuote(int id)
        {
            await ShowLoader("Cargando información...");
            await LoadQuoteDetail(id);
            await HideLoader("Informacion cargada",500);
        }
        public async Task Save() {
            await SaveState();
        }
        public async Task Export(string filePath)
        {
            await ShowLoader("Exportando...");
            try
            {
                await this.ExportToExcel(filePath);
                Trace.WriteLine("Se terminó de crear con Aspose");
                await this.FinishExport(filePath);
                Trace.WriteLine("Se terminó de crear con ClosedXML");
                await HideLoader("Se termino de exportar.");
            }
            catch (Exception ex)
            {
                Trace.WriteLine("ERROR AL GUARDAR: " + ex.ToString());
                await HideLoader("Ocurrio un error, contacte al administrador.");
            }
        }
        public async Task ShowLoader(string message, int seconds = 1000) {
            Busy.IsBusy = true;
            Busy.StatusMessage = message;
            await Task.Delay(seconds);
        }
        public async Task HideLoader(string message, int seconds = 1000)
        {
            Busy.StatusMessage = message;
            await Task.Delay(seconds);
            Busy.IsBusy = false;
        }

        #endregion

    }
}
