﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Calculo_ductos_winUi_3.Views;
using Calculo_ductos_winUi_3.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Windows.Storage.Pickers;
using WinRT.Interop;
using Calculo_ductos_winUi_3.Services;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System.Diagnostics;
using Calculo_ductos_winUi_3.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculo_ductos_winUi_3
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        
        public string AppTitleText
        {
            get
            {
                return "Ducto Planner ";
            }
        }
        public MainWindow()
        {
            this.InitializeComponent();
            
            this.Title = "Ducto Planner";

            _ = SetWindowIconAsync(); 

            StateApp.CompleteDuctVm.PropertyChanged += (s, e) => {
                if (e.PropertyName == nameof(StateApp.CompleteDuctVm.Quotes))
                {
                    RebuildFlyout();
                }
            };
        }
        public StateViewModel _stateVieModel => ((App)Application.Current).ViewModel;
        public StateViewModel StateApp
        {
            get { return _stateVieModel; }
        }
        private ObservableCollection<NavLink> _navLinks = new ObservableCollection<NavLink>()
        {
            new NavLink() { Type = NavLink.TypeMenu.duct , Label = "Calcular ductos", ImageSource = "ms-appx:///Assets/ducto.png"  },
            new NavLink() { Type = NavLink.TypeMenu.freight ,Label = "Calcular fletes", ImageSource  = "ms-appx:///Assets/camioneta2.png" },
            new NavLink() { Type = NavLink.TypeMenu.manpower ,Label = "Calcular mano de obra", ImageSource  = "ms-appx:///Assets/mo.png" },
            new NavLink() { Type = NavLink.TypeMenu.indirect ,Label = "Calcular indirectos", ImageSource  = "ms-appx:///Assets/maletin.png" },
            
        };
        public ObservableCollection<NavLink> NavLinks
        {
            get { return _navLinks; }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if(AppHasData())
                await StateApp.Save();
            else
                await ShowEmptyDataDialog(sender, "No hay datos para guardar.");
            
        }
        private void NavLinksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            //content.Text = (e.ClickedItem as NavLink).Label + " Page";
            
            switch ((e.ClickedItem as NavLink).Type ) 
            {
                case NavLink.TypeMenu.duct: contentPage.Navigate(typeof(CalculateDuctsView));break;
                case NavLink.TypeMenu.freight: contentPage.Navigate(typeof(CalculateFreightView));break;
                case NavLink.TypeMenu.manpower: contentPage.Navigate(typeof(CalculateManPower));break;
                case NavLink.TypeMenu.indirect: contentPage.Navigate(typeof(CalculateIndirects));break;
            }
            
        }
        private async void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            if (AppHasData())
            {
                var path = await GetSaveFilePathAsync();
                if (path != null)
                {
                    await StateApp.Export(path);
                }
            }
            else
                await ShowEmptyDataDialog(sender, "No hay datos para exportar.");

            
        }
        private async void NewFileButton_Click(object sender, RoutedEventArgs e)
        {
            
            var frameworkElement = sender as FrameworkElement;
            if (StateApp.FloorVM.FloorList.Count > 0)
            {
                var dialog = new ContentDialog
                {
                    Title = "Guardar datos",
                    Content = "Hay datos que no se han guardado. ¿Desea guardarlos?",
                    PrimaryButtonText = "Sí",
                    SecondaryButtonText = "No",
                    CloseButtonText = "Cancelar",
                    XamlRoot = frameworkElement.XamlRoot
                };

                var result = await dialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    // El usuario hizo clic en "Sí"
                    // Aquí puedes ejecutar el guardado
                    StateApp.Save(); // o lo que uses para guardar
                    StateApp.New();
                }
                else if (result == ContentDialogResult.Secondary)
                {
                    // El usuario hizo clic en "No"
                    // Puedes continuar sin guardar
                    StateApp.New();
                }
                else
                {
                    // Canceló
                    return;
                }
                return;
            }
            

            
        }
        public async Task<string> GetSaveFilePathAsync()
        {
            var savePicker = new FileSavePicker();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hwnd);
            
            savePicker.FileTypeChoices.Add("Excel Workbook", new List<string>() { ".xlsx" });
            savePicker.SuggestedFileName = $"DESPIECE Y RENDIMIENTOS DUCTO DE {GetPurposeString()} 24 {GetSheetString()} {DateTime.Today.ToString()}";

            var file = await savePicker.PickSaveFileAsync();

            if (file != null)
            {
                string path = file.Path;

                if (!path.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                    path += ".xlsx";

                return path;
            }

            return null;
        }
        private string GetPurposeString() 
        {
            return _stateVieModel.CompleteDuctVm.PurposeId == 0 ? "ROPA" : "BASURA";
        }
        private string GetSheetString()
        {
            return _stateVieModel.CompleteDuctVm.SheetTypeId == 0 ? "INOX" : "GALV";
        }
        private void RebuildFlyout()
        {
            var flyout = new MenuFlyout();

            foreach (var quote in StateApp.CompleteDuctVm.Quotes)
            {
                var item = new MenuFlyoutItem
                {
                    Text = quote.PT,
                    Tag = quote
                };
                item.Click += (s, e) =>
                {
                    var q = (QuoteModel)((MenuFlyoutItem)s).Tag;
                    // Acción: Abrir cotización, navegar, etc.
                    Trace.WriteLine($"Abriendo: {q.PT}");
                    _ = StateApp.LoadQuote(q.Id);
                    contentPage.Navigate(typeof(CalculateDuctsView));
                };

                flyout.Items.Add(item);
            }

            OpenButton.Flyout = flyout;
        }
        private async Task ShowEmptyDataDialog(object sender, string message)
        {
                var frameworkElement = sender as FrameworkElement;

                var dialog = new ContentDialog
                {
                    Title = "Sin datos",
                    Content = message,
                    CloseButtonText = "Aceptar",
                    XamlRoot = frameworkElement.XamlRoot // 👈 Esto sí es válido siempre
                };

                await dialog.ShowAsync();
        }
        private bool AppHasData()
        {
            return StateApp.ComponentsVM.ComponentList.Count > 0;
        }
        private async Task SetWindowIconAsync()
        {
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var iconFileName = "iconTemp.ico";
            var iconPath = System.IO.Path.Combine(localFolder.Path, iconFileName);

            if (!System.IO.File.Exists(iconPath))
            {
                var file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(
                    new Uri("ms-appx:///Assets/IconoVertical-VERDE.ico"));
                await file.CopyAsync(localFolder, iconFileName, Windows.Storage.NameCollisionOption.ReplaceExisting);
            }

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.SetIcon(iconPath);
        }
    }
    public class NavLink
    {
        public enum TypeMenu {duct, freight , manpower, indirect }
        public TypeMenu Type {  get; set; }
        public string Label { get; set; }
        public Symbol? Symbol { get; set; } // Puede seguir usándose
        public string ImageSource { get; set; } // Nueva propiedad para imágenes personalizadas
    }
   
}
