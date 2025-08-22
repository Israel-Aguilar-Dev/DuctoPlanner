using Calculo_ductos_winUi_3.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Calculo_ductos.Utils;
using Calculo_ductos.Params;
using Microsoft.UI.Xaml.Hosting;
using System.Threading.Tasks;
using Windows.ApplicationModel.VoiceCommands;
using Microsoft.UI.Composition;
using System.Numerics;
using Calculo_ductos_winUi_3.ViewModels;
using Microsoft.UI.Xaml.Media.Animation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculo_ductos_winUi_3.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CalculateDuctsView : Page
    {
        
        
        public int floorCount = 0;
        public StateViewModel stateApp { get; set; }

        public CalculateDuctsView()
        {
            this.InitializeComponent();
            stateApp = ((App)Application.Current).ViewModel;
            this.DataContext = stateApp;
        }

        //private void CbxTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    cbxChimenea.Visibility = cbxTipo.SelectedIndex == 2 ? Visibility.Visible : Visibility.Collapsed;
        //    lblChimenea.Visibility = cbxTipo.SelectedIndex == 2 ? Visibility.Visible : Visibility.Collapsed;
        //    cbxCompuerta.Visibility = cbxTipo.SelectedIndex == 0 ? Visibility.Collapsed : Visibility.Visible;
        //    lblCompuerta.Visibility = cbxTipo.SelectedIndex == 0 ? Visibility.Collapsed : Visibility.Visible;
        //}

        private void SelectorSubPage_SelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
        {
            SelectorBarItem selectedItem = sender.SelectedItem;
            int currentSelectedIndex = sender.Items.IndexOf(selectedItem);
            System.Type pageType = null;

            switch (currentSelectedIndex)
            {
                case 0:
                    pageType = typeof(CalculateDuctsResumeDuctsView);
                    break;
                case 1:
                    pageType = typeof(CalculateDuctsBillOfMaterials);
                    break;
               
            }

            var slideNavigationTransitionEffect = currentSelectedIndex > 0 ? SlideNavigationTransitionEffect.FromRight : SlideNavigationTransitionEffect.FromLeft;

            contentsubPage.Navigate(pageType, null, new SlideNavigationTransitionInfo() { Effect = slideNavigationTransitionEffect });

            

        }
        private void SelectorLeftSubPage_SelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
        {
            SelectorBarItem selectedItem = sender.SelectedItem;
            int currentSelectedIndex = sender.Items.IndexOf(selectedItem);
            System.Type pageType = null;

            switch (currentSelectedIndex)
            {
                case 0:
                    pageType = typeof(DuctView);
                    break;
                case 1:
                    pageType = typeof(FloorView);
                    break;

            }

            var slideNavigationTransitionEffect = currentSelectedIndex > 0 ? SlideNavigationTransitionEffect.FromRight : SlideNavigationTransitionEffect.FromLeft;

            contentLeftsubPage.Navigate(pageType, null, new SlideNavigationTransitionInfo() { Effect = slideNavigationTransitionEffect });



        }


    }
}
