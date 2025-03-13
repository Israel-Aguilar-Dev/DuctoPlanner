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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculo_ductos_winUi_3.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CalculateDuctsView2 : Page
    {
        ObservableCollection<string> items = new ObservableCollection<string>();
        ObservableCollection<FloorDescription> floorDescriptions = new ObservableCollection<FloorDescription>();
        public int floorCount = 0;

        public CalculateDuctsView2()
        {
            this.InitializeComponent();
        }

        private void CbxTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxChimenea.Visibility = cbxTipo.SelectedIndex == 2 ? Visibility.Visible : Visibility.Collapsed;
            lblCompuerta.Visibility = cbxTipo.SelectedIndex == 2 ? Visibility.Visible : Visibility.Collapsed;

        }
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            //Agregamos un rowdefinition;
            int row = MyTable.RowDefinitions.Count;
            Guid uuid = Guid.NewGuid();
            FloorDescription floorDescription = new FloorDescription
            {
                Uuid = uuid,
                FloorCount = Convert.ToInt32(txtNumeroPisos.Text),
                FloorHeight = Convert.ToDecimal(txtAltura.Text),
                NeedGate = cbxCompuerta.SelectedIndex == 0,
                NeedChimney = cbxTipo.SelectedIndex == 2 ? cbxChimenea.SelectedIndex == 0: false
            };

            floorDescription.SetFloorType(cbxTipo.SelectedIndex);
            floorDescriptions.Add(floorDescription);
            MyTable.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Auto)
            });
            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure { StartPoint = new Windows.Foundation.Point(0, 0) };
            pathFigure.Segments.Add(new LineSegment { Point = new Windows.Foundation.Point(200, 0) });
            pathGeometry.Figures.Add(pathFigure);

            Path path = new Path
            {
                Tag = uuid.ToString(),
                Stroke = new SolidColorBrush(Microsoft.UI.Colors.Gray),
                StrokeThickness = 0.5,
                Data = pathGeometry,
                Margin = new Thickness(36, 0, 36, 0),
                Stretch = Stretch.Fill,
                VerticalAlignment = VerticalAlignment.Bottom
            };

            AddTextBlock(row,0,new TextBlock { Tag = uuid.ToString(), Text = floorDescription.GetDescription()});
            AddTextBlock(row,1,new TextBlock { Tag = uuid.ToString(), Text = floorDescription.FloorCount.ToString()});
            AddTextBlock(row,2,new TextBlock { Tag = uuid.ToString(), Text = floorDescription.FloorHeight.ToString()});
            AddTextBlock(row, 3, new TextBlock { Tag = uuid.ToString(), Text = floorDescription.NeedGate ? "Si" : "No"});
            AddTextBlock(row,4,new TextBlock { Tag = uuid.ToString(), Text = floorDescription.NeedChimney ? "Si" : "No"});
            AddDeleteButton(row, 5, new Button { 
                Background = new SolidColorBrush(Windows.UI.Color.FromArgb(1,255,0,0)), 
                Content = "Eliminar", 
                Tag = uuid.ToString()
            });
            Grid.SetRow(path, row);
            Grid.SetColumnSpan(path, 6);
            MyTable.Children.Add(path);
        }
        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string json = ExtractFloorResumeToJson();
                var lastLevel = floorDescriptions.FirstOrDefault(o => o.Type == Floor.TypeFloor.last);
                bool needChimmey = false;
                if (lastLevel != null) needChimmey = lastLevel.NeedChimney;
                Dictionary<Duct.TypeDuct, int> result = Calculo_ductos.Facade.CalculateDucts(json);
                //List<Floor> calculatedDescriptionFloors = Calculo_ductos.Facade.CalculateDuctsByFloor(json);
                //List<Floor> calculatedFloors = ReplicateFloors(calculatedDescriptionFloors);
                //Dictionary<Duct.TypeDuct, int> result = calculatedFloors.SumDucts();

                List<Component> components = Calculo_ductos.Facade.CalculateComponents(floorCount, result, needChimmey);

                ClearGrid(ref DuctTable);
                ClearGrid(ref ComponentTable);
                ShowDucts(result);
                ShowComponents(components);

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
        private void AddTextBlock(int row,int column, TextBlock textBlock) 
        {
            
            // Ubicar el borde en la celda del Grid
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.Margin = new Thickness(10,8,10,8);
            textBlock.Padding = new Thickness(3);
            Grid.SetRow(textBlock, row);
            Grid.SetColumn(textBlock, column);
            MyTable.Children.Add(textBlock);
        }
        private void AddDeleteButton(int row, int column, Button button)
        {
            button.Click += DynamicButton_Click;
            button.VerticalAlignment = VerticalAlignment.Center;
            button.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(button, row);
            Grid.SetColumn(button, column);
            MyTable.Children.Add(button);
        }
        private void DynamicButton_Click(object sender, RoutedEventArgs e)
        {
            // Obt�n el bot�n que dispar� el evento
            Button clickedButton = sender as Button;
            var elementsToRemove = new List<UIElement>();
            // Recupera la fila a eliminar desde la propiedad Tag
            string tag = clickedButton.Tag.ToString();

            RemoveFloorDescription(Guid.Parse(tag));

            foreach (FrameworkElement child in MyTable.Children)
            {
                var tagging = child.Tag;
                
                if (child.Tag.Equals(tag))
                {
                    elementsToRemove.Add(child);
                }
            }

            // Aqu� puedes agregar la l�gica para eliminar la fila correspondiente.
            // Eliminar los elementos de la colecci�n del Grid
            foreach (var element in elementsToRemove)
            {
                MyTable.Children.Remove(element);
            }

        }
        private void RemoveFloorDescription(Guid uuid)
        {
            var objetoAEliminar = floorDescriptions.FirstOrDefault(o => o.Uuid == uuid);
            if (objetoAEliminar != null)
            {
                floorDescriptions.Remove(objetoAEliminar);
            }
        }
        private string ExtractFloorResumeToJson()
        {
            List<object> floors = new List<object>();
            int counter = 0;
            foreach (FloorDescription floorDescription in floorDescriptions)
            {
                if (floorDescription.FloorCount == 1)
                {
                    floors.Add(new
                    {
                        Name = $"{floorDescription.Uuid}",
                        Height = floorDescription.FloorHeight,
                        NeedGate = floorDescription.NeedGate,
                        Type = floorDescription.Type,
                        NeedChimmey = floorDescription.NeedChimney
                    });
                    counter++;
                }
                else
                {
                    for (int i = 1; i <= floorDescription.FloorCount; i++)
                    {
                        floors.Add(new
                        {
                            Name = $"N{counter}",
                            Height = floorDescription.FloorHeight,
                            NeedGate = floorDescription.NeedGate,
                            Type = floorDescription.Type,
                            NeedChimmey = floorDescription.NeedChimney
                        });
                        counter++;
                    }
                }
            }
            floorCount = counter++;
            return JsonConvert.SerializeObject(floors);
        }
        private void ShowDucts(Dictionary<Duct.TypeDuct,int> ducts) 
        {
            foreach (var duct in ducts)
            {
                if (duct.Value > 0)
                {
                    int row = DuctTable.RowDefinitions.Count;
                    DuctTable.RowDefinitions.Add(new RowDefinition
                    {
                        Height = new GridLength(1, GridUnitType.Auto)
                    });
                    TextBlock name = new TextBlock
                    {
                        Tag = 1,
                        Text = duct.Key.ToString(),
                        TextAlignment = TextAlignment.Center,
                        Margin = new Thickness(10, 8, 13, 8),
                        Padding = new Thickness(3)
                    };
                    TextBlock count = new TextBlock
                    {
                        Tag = 1,
                        Text = duct.Value.ToString(),
                        TextAlignment = TextAlignment.Center,
                        Margin = new Thickness(10, 8, 13, 8),
                        Padding = new Thickness(3)
                    };
                    var pathGeometry = new PathGeometry();
                    var pathFigure = new PathFigure { StartPoint = new Windows.Foundation.Point(0, 0) };
                    pathFigure.Segments.Add(new LineSegment { Point = new Windows.Foundation.Point(200, 0) });
                    pathGeometry.Figures.Add(pathFigure);

                    Path path = new Path {
                        Tag = 1,
                        Stroke = new SolidColorBrush(Microsoft.UI.Colors.Gray),
                        StrokeThickness = 0.5,
                        Data = pathGeometry,
                        Margin = new Thickness(36, 0, 36, 0),
                        Stretch = Stretch.Fill,
                        VerticalAlignment = VerticalAlignment.Bottom
                    };

                    Grid.SetRow(name, row);
                    Grid.SetColumn(name, 0);
                    DuctTable.Children.Add(name);
                    Grid.SetRow(count, row);
                    Grid.SetColumn(count, 1);
                    DuctTable.Children.Add(count);
                    Grid.SetRow(path, row);
                    Grid.SetColumnSpan(path, 6);
                    DuctTable.Children.Add(path);
                }
            }
        }
        private void ShowComponents(List<Component> components)
        {
            foreach (var component in components)
            {
                int row = ComponentTable.RowDefinitions.Count;
                ComponentTable.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Auto)
                });
                TextBlock name = new TextBlock
                {
                    Tag = 1,
                    Text = component.GetTypeDescription(),
                    TextAlignment = TextAlignment.Center,
                    Margin = new Thickness(10, 8, 16, 8),
                    Padding = new Thickness(3)
                };
                TextBlock count = new TextBlock
                {
                    Tag = 1,
                    Text = component.Count.ToString(),
                    TextAlignment = TextAlignment.Center,
                    Margin = new Thickness(10, 8, 16, 8),
                    Padding = new Thickness(3)
                };
                var pathGeometry = new PathGeometry();
                var pathFigure = new PathFigure { StartPoint = new Windows.Foundation.Point(0, 0) };
                pathFigure.Segments.Add(new LineSegment { Point = new Windows.Foundation.Point(200, 0) });
                pathGeometry.Figures.Add(pathFigure);

                Path path = new Path
                {
                    Tag = 1,
                    Stroke = new SolidColorBrush(Microsoft.UI.Colors.Gray),
                    StrokeThickness = 0.5,
                    Data = pathGeometry,
                    Margin = new Thickness(36, 0, 36, 0),
                    Stretch = Stretch.Fill,
                    VerticalAlignment = VerticalAlignment.Bottom
                };
                Grid.SetRow(name, row);
                Grid.SetColumn(name, 0);
                ComponentTable.Children.Add(name);
                
                Grid.SetRow(count, row);
                Grid.SetColumn(count, 1);
                ComponentTable.Children.Add(count);

                Grid.SetRow(path, row);
                Grid.SetColumnSpan(path, 6);
                ComponentTable.Children.Add(path);
            }
        }
        private void ClearGrid(ref Grid grid)
        {
            
            var elementsToRemove = new List<UIElement>();
            foreach (FrameworkElement child in grid.Children)
            {
                var tagging = child.Tag.ToString();
                if (tagging.Equals("1"))
                {
                    elementsToRemove.Add(child);
                }
                
            }
            foreach (var element in elementsToRemove)
            {
                grid.Children.Remove(element);
            }
        }
        private List<Floor> ReplicateFloors(List<Floor> floors)
        {
            List <Floor > calculatedFloors = new List<Floor> ();
            foreach (FloorDescription description in floorDescriptions)
            {
                if (description.FloorCount == 1)
                    calculatedFloors.Add(floors.FirstOrDefault(x => x.Name.Equals(description.Uuid.ToString())));
                else 
                {
                    Floor floor = floors.FirstOrDefault(x => x.Name.Equals(description.Uuid.ToString()));
                    for (int i = 0; i <= description.FloorCount; i++)
                        calculatedFloors.Add(floor);
                }
            }
            return calculatedFloors;
        }
        private void FlipCard(object sender, RoutedEventArgs e)
        {
            var renderCard = leftCardContainer.RenderTransformOrigin;
            leftCardContainer.RenderTransformOrigin = new Windows.Foundation.Point(0.5, 0.5);
            var compositor = ElementCompositionPreview.GetElementVisual(leftCardContainer).Compositor;

            var rotateAnimation = compositor.CreateScalarKeyFrameAnimation();
            rotateAnimation.InsertKeyFrame(0f, 0f);
            rotateAnimation.InsertKeyFrame(0.5f, 90f);
            rotateAnimation.InsertKeyFrame(1f, 180f);
            rotateAnimation.Duration = TimeSpan.FromSeconds(0.5);

            var visual = ElementCompositionPreview.GetElementVisual(leftCardContainer);
            visual.AnchorPoint = new System.Numerics.Vector2(1f,0f);
            visual.RotationAxis = new System.Numerics.Vector3(0f, 1f, 0f);

            visual.StartAnimation("RotationAngleInDegrees", rotateAnimation);

            //await Task.Delay(250); // Mitad del giro, cambiar visibilidad
            FrontCard.Visibility = FrontCard.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            BackCard.Visibility = BackCard.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

    }
}
