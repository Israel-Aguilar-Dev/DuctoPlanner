using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Calculo_ductos_winUi_3.Views;
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
       

    }
    public class NavLink
    {
        public string Label { get; set; }
        public Symbol Symbol { get; set; }
    }
    public class CustomizeToolBaar 
    {
        public Image Icon { get; set; }
        public string Title { get; set; }
        public Symbol Close { get; set; }
        public Symbol Maximize { get; set; }
        public Symbol Minimize { get; set; }

    }
    public class MenuBar 
    {
        public string Label { get; set; }
    }

}
