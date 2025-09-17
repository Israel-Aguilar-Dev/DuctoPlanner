using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Media.Animation;
using Calculo_ductos_winUi_3.ViewModels;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculo_ductos_winUi_3.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CalculateManPower : Page
    {
        public StateViewModel stateApp { get; set; }
        
        public CalculateManPower()
        {
            this.InitializeComponent();
            stateApp = ((App)Application.Current).ViewModel;
            this.DataContext = stateApp;
        }
        private void SelectorSubPage_SelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
        {
            SelectorBarItem selectedItem = sender.SelectedItem;
            int currentSelectedIndex = sender.Items.IndexOf(selectedItem);
            System.Type pageType = null;

            switch (currentSelectedIndex)
            {
                case 0:
                    pageType = typeof(CalculateManPowerSubPageMO);
                    break;
                case 1:
                    pageType = typeof(CalculateManPowerSubPageDetail);
                    break;

            }

            var slideNavigationTransitionEffect = currentSelectedIndex > 0 ? SlideNavigationTransitionEffect.FromRight : SlideNavigationTransitionEffect.FromLeft;

            contentsubPage.Navigate(pageType, null, new SlideNavigationTransitionInfo() { Effect = slideNavigationTransitionEffect });



        }
        private async void CalculateManPower_Click(object sender, RoutedEventArgs e)
        {
            if (AppHasData())
            {
                await stateApp.CalculateManPower(sender,e);
            }
            else
                await ShowEmptyDataDialog(sender, "Aun no se tiene un despiece.");
        }
        private async Task ShowEmptyDataDialog(object sender, string message)
        {
            var frameworkElement = sender as FrameworkElement;

            var dialog = new ContentDialog
            {
                Title = "Sin datos",
                Content = message,
                CloseButtonText = "Aceptar",
                XamlRoot = frameworkElement.XamlRoot // ?? Esto sí es válido siempre
            };

            await dialog.ShowAsync();
        }
        private bool AppHasData()
        {
            return stateApp.ComponentsVM.ComponentList.Count > 0;
        }
    }
}
