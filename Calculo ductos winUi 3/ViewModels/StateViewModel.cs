using Aspose.Cells;
using Calculo_ductos_winUi_3.Models;
using Calculo_ductos_winUi_3.Params;
using Calculo_ductos_winUi_3.Services;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Style = Aspose.Cells.Style;
using Calculo_ductos_winUi_3.Services;


namespace Calculo_ductos_winUi_3.ViewModels
{
    public class StateViewModel 
    {
        private readonly string _baseUrl = "http://192.168.10.228:8092/CotizadorApiVertical/Api/";
        private List<CatalogRowModel> PurposeCatalog;
        private List<CatalogRowModel> DoorTypeCatalog;
        private List<CatalogRowModel> SheetTypeCatalog;
        private List<CatalogRowModel> FloorTypeCatalog;
        private WebApi Client;
        public FloorDescriptionViewModel FloorVM { get; }
        public DuctsViewModel DuctsVM { get; }
        public ComponentsViewModel ComponentsVM { get; }
        public CompleteDuctViewModel CompleteDuctVm { get; }
        public StateViewModel()
        {
            Client = new WebApi(_baseUrl);
            // 🔄 Desencadena la carga sin esperar directamente
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
        }

        public void CalculateDucts(object sender, RoutedEventArgs e)
        {
            DuctsVM.CalculateDuctsCommand.Execute(FloorVM.FloorList);

            ComponentCalculationParams args = new ComponentCalculationParams() { 
                Floors = FloorVM.FloorList,
                Ducts = DuctsVM.DucList.MapDuctsToDictionary()
            };

            ComponentsVM.CalculateComponentsCommand.Execute(DuctsVM.CompleteDuct);
            var itemsToRemove = DuctsVM.DuctDetailList.Where(duct => duct.Count == 0).ToList();
            foreach (var item in itemsToRemove)
            {
                DuctsVM.DuctDetailList.Remove(item);
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

        public async Task Export(string filePath)
        {


            
            try
            {
                this.ExportToExcel(filePath);
                Trace.WriteLine("Se terminó de crear con Aspose");
                this.FinishExport(filePath);
                Trace.WriteLine("Se terminó de crear con ClosedXML");
            }
            catch (Exception ex)
            {
                Trace.WriteLine("ERROR AL GUARDAR: " + ex.ToString());
            }
            


            // Aquí usamos un MemoryStream para hacer la escritura realmente async
            //using var memoryStream = new MemoryStream();

            //memoryStream.Seek(0, SeekOrigin.Begin);

            //using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true);
            //await memoryStream.CopyToAsync(fileStream);
        }
        private async Task InitializeAsync()
        {
            await LoadCatalogsAsync();
            
        }
        private async Task LoadCatalogsAsync() 
        {
            PurposeCatalog = new List<CatalogRowModel>();
            DoorTypeCatalog = new List<CatalogRowModel>();
            SheetTypeCatalog = new List<CatalogRowModel>();
            FloorTypeCatalog = new List<CatalogRowModel>();
            //var result = Client.GetAsync<Response<List<CatalogModel>>>("Catalog");
            //var catalogList = result.Result;
            //PurposeCatalog = catalogList.Result.Where(catalog=>catalog.Name.Equals("PurposeCatalog")).FirstOrDefault().Data;
            //DoorTypeCatalog = catalogList.Result.Where(catalog=>catalog.Name.Equals("DoorTypeCatalog")).FirstOrDefault().Data;
            //SheetTypeCatalog = catalogList.Result.Where(catalog=>catalog.Name.Equals("SheetTypeCatalog")).FirstOrDefault().Data;
            //FloorTypeCatalog = catalogList.Result.Where(catalog=>catalog.Name.Equals("FloorTypeCatalog")).FirstOrDefault().Data;
            try
            {
                var catalogListWrapper = await Client.GetAsync<List<CatalogModel>>("Catalog");

                if (catalogListWrapper != null)
                {
                    PurposeCatalog = catalogListWrapper.FirstOrDefault(c => c.Name.Equals("PurposeCatalog"))?.Data ?? new();
                    DoorTypeCatalog = catalogListWrapper.FirstOrDefault(c => c.Name.Equals("DoorTypeCatalog"))?.Data ?? new();
                    SheetTypeCatalog = catalogListWrapper.FirstOrDefault(c => c.Name.Equals("SheetTypeCatalog"))?.Data ?? new();
                    FloorTypeCatalog = catalogListWrapper.FirstOrDefault(c => c.Name.Equals("FloorTypeCatalog"))?.Data ?? new();
                    FloorVM.LoadCatalogs(DoorTypeCatalog);
                    FloorVM.FilterDoorsTypes("basura");//ya que esta seleccionado por automatico basura
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Catalog Load Error]: {ex.Message}");
                // Aquí puedes mostrar algún mensaje en UI si lo necesitas
            }
        }
       
      
    }
}
