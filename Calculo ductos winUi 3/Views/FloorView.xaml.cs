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
    public sealed partial class FloorView : Page
    {
        
        
        public int floorCount = 0;
        public StateViewModel stateApp { get; set; }

        public FloorView()
        {
            this.InitializeComponent();
            stateApp = ((App)Application.Current).ViewModel;
            this.DataContext = stateApp;
        }

        private void CbxTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxChimenea.Visibility = cbxTipo.SelectedIndex == 2 ? Visibility.Visible : Visibility.Collapsed;
            lblChimenea.Visibility = cbxTipo.SelectedIndex == 2 ? Visibility.Visible : Visibility.Collapsed;

            cbxCompuerta.Visibility = cbxTipo.SelectedIndex == 0 ? Visibility.Collapsed : Visibility.Visible;
            lblCompuerta.Visibility = cbxTipo.SelectedIndex == 0 ? Visibility.Collapsed : Visibility.Visible;

            cbxTipoPuerta.Visibility = cbxTipo.SelectedIndex == 0 || cbxCompuerta.SelectedIndex == 1 ? Visibility.Collapsed : Visibility.Visible;
            lblTipoPuerta.Visibility = cbxTipo.SelectedIndex == 0 || cbxCompuerta.SelectedIndex == 1 ? Visibility.Collapsed : Visibility.Visible;
        }
        private void CbxCompuerta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxTipoPuerta.Visibility = cbxCompuerta.SelectedIndex == 1 ? Visibility.Collapsed : Visibility.Visible;
            lblTipoPuerta.Visibility = cbxCompuerta.SelectedIndex == 1 ? Visibility.Collapsed : Visibility.Visible;
        }



    }
}
