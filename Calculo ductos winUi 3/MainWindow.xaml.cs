using System;
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
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.SetIcon("Icono Vertical - VERDE.ico");
        }
        public StateViewModel _stateVieModel => ((App)Application.Current).ViewModel;
        public StateViewModel StateApp
        {
            get { return _stateVieModel; }
        }
        private ObservableCollection<NavLink> _navLinks = new ObservableCollection<NavLink>()
        {
            new NavLink() { Label = "Calcular ductos", Symbol = Symbol.Edit  },
            /*new NavLink() { Label = "People", Symbol = Symbol.People  },
            new NavLink() { Label = "Globe", Symbol = Symbol.Globe },
            new NavLink() { Label = "Message", Symbol = Symbol.Message },
            new NavLink() { Label = "Mail", Symbol = Symbol.Mail },*/
        };
        public ObservableCollection<NavLink> NavLinks
        {
            get { return _navLinks; }
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            //myButton.Content = "Clicked";
        }

        private void NavLinksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            //content.Text = (e.ClickedItem as NavLink).Label + " Page";
            contentPage.Navigate(typeof(CalculateDuctsView));
        }
        private async void ExportButton_Click(object sender, RoutedEventArgs e)
        {

            if (StateApp.ComponentsVM.ComponentList.Count == 0)
            {
                var dialog = new ContentDialog
                {
                    Title = "Sin datos",
                    Content = "No hay datos para exportar.",
                    CloseButtonText = "Aceptar",
                    XamlRoot = this.Content.XamlRoot
                };

                dialog.ShowAsync();
                return;
            }
            
            var path = await GetSaveFilePathAsync();
            if (path != null)
            {
                // Aquí ya tienes la ruta y nombre del archivo
                await StateApp.ExportToExcel(path);
            }
            
        }
        public async Task<string> GetSaveFilePathAsync()
        {
            var savePicker = new FileSavePicker();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hwnd);

            savePicker.FileTypeChoices.Add("Excel Workbook", new List<string>() { ".xlsx" });
            savePicker.SuggestedFileName = $"DESPIECE Y RENDIMIENTOS DUCTO DE BASURA 24 GALV {DateTime.Today.ToString()}";

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


    }
    public class NavLink
    {
        public string Label { get; set; }
        public Symbol Symbol { get; set; }
    }
    


    }
