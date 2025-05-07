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

        private void CbxTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxChimenea.Visibility = cbxTipo.SelectedIndex == 2 ? Visibility.Visible : Visibility.Collapsed;
            lblChimenea.Visibility = cbxTipo.SelectedIndex == 2 ? Visibility.Visible : Visibility.Collapsed;
            cbxCompuerta.Visibility = cbxTipo.SelectedIndex == 0 ? Visibility.Collapsed : Visibility.Visible;
            lblCompuerta.Visibility = cbxTipo.SelectedIndex == 0 ? Visibility.Collapsed : Visibility.Visible;
        }
        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            stateApp.FloorVM.AddFloor();
            //Agregamos un rowdefinition;
            //int row = MyTable.RowDefinitions.Count;
            //Guid uuid = Guid.NewGuid();
            //FloorDescription floorDescription = new FloorDescription
            //{
            //    Uuid = uuid,
            //    FloorCount = Convert.ToInt32(txtNumeroPisos.Text),
            //    FloorHeight = Convert.ToDecimal(txtAltura.Text),
            //    NeedGate = cbxCompuerta.SelectedIndex == 0,
            //    NeedChimney = cbxTipo.SelectedIndex == 2 ? cbxChimenea.SelectedIndex == 0: false
            //};

            //floorDescription.SetFloorType(cbxTipo.SelectedIndex);
            //floorDescriptions.Add(floorDescription);
            //MyTable.RowDefinitions.Add(new RowDefinition
            //{
            //    Height = new GridLength(1, GridUnitType.Auto)
            //});
            //var pathGeometry = new PathGeometry();
            //var pathFigure = new PathFigure { StartPoint = new Windows.Foundation.Point(0, 0) };
            //pathFigure.Segments.Add(new LineSegment { Point = new Windows.Foundation.Point(200, 0) });
            //pathGeometry.Figures.Add(pathFigure);

            //Path path = new Path
            //{
            //    Tag = uuid.ToString(),
            //    Stroke = new SolidColorBrush(Microsoft.UI.Colors.Gray),
            //    StrokeThickness = 0.5,
            //    Data = pathGeometry,
            //    Margin = new Thickness(36, 0, 36, 0),
            //    Stretch = Stretch.Fill,
            //    VerticalAlignment = VerticalAlignment.Bottom
            //};

            //AddTextBlock(row,0,new TextBlock { Tag = uuid.ToString(), Text = floorDescription.GetDescription()});
            //AddTextBlock(row,1,new TextBlock { Tag = uuid.ToString(), Text = floorDescription.FloorCount.ToString()});
            //AddTextBlock(row,2,new TextBlock { Tag = uuid.ToString(), Text = floorDescription.FloorHeight.ToString()});
            //AddTextBlock(row, 3, new TextBlock { Tag = uuid.ToString(), Text = floorDescription.NeedGate ? "Si" : "No"});
            ////AddTextBlock(row,4,new TextBlock { Tag = uuid.ToString(), Text = floorDescription.NeedChimney ? "Si" : "No"});
            //string typeOfVentilation = floorDescription.Type == Floor.TypeFloor.last ? 
            //                            floorDescription.NeedChimney ?  "Chimenea" : "Cuello de ganso" : "-";
            //AddTextBlock(row,4,new TextBlock { Tag = uuid.ToString(), Text = typeOfVentilation});
            //AddDeleteButton(row, 5, new Button { 
            //    Background = new SolidColorBrush(Windows.UI.Color.FromArgb(1,255,0,0)), 
            //    Content = "Eliminar", 
            //    Tag = uuid.ToString()
            //});
            //Grid.SetRow(path, row);
            //Grid.SetColumnSpan(path, 6);
            //MyTable.Children.Add(path);
        }
        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //string json = ExtractFloorResumeToJson();
                //var lastLevel = floorDescriptions.FirstOrDefault(o => o.Type == Floor.TypeFloor.last);
                //bool needChimmey = false;
                //if (lastLevel != null) needChimmey = lastLevel.NeedChimney;
                ////Dictionary<Duct.TypeDuct, int> result = Calculo_ductos.Facade.CalculateDucts(json);
                //List<Floor> calculatedDescriptionFloors = Calculo_ductos.Facade.CalculateDuctsByFloor(json);
                ////List<Floor> calculatedFloors = ReplicateFloors(calculatedDescriptionFloors);
                //Dictionary<Duct.TypeDuct, int> result = calculatedDescriptionFloors.SumDucts();

                //List<Component> components = Calculo_ductos.Facade.CalculateComponents(floorCount, result, needChimmey);

                //ClearGrid(ref DuctTable);
                //ClearGrid(ref ComponentTable);
                //ClearGrid(ref DuctsByFloorTable);
                //ShowDucts(result);
                //ShowComponents(components);
                //ShowDuctsByFloor(calculatedDescriptionFloors);

            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
        private void AddTextBlock(int row,int column, TextBlock textBlock) 
        {
            
            // Ubicar el borde en la celda del Grid
            //textBlock.TextAlignment = TextAlignment.Center;
            //textBlock.Margin = new Thickness(10,8,10,8);
            //textBlock.Padding = new Thickness(3);
            //Grid.SetRow(textBlock, row);
            //Grid.SetColumn(textBlock, column);
            //MyTable.Children.Add(textBlock);
        }
        private void AddDeleteButton(int row, int column, Button button)
        {
            //button.Click += DynamicButton_Click;
            //button.VerticalAlignment = VerticalAlignment.Center;
            //button.HorizontalAlignment = HorizontalAlignment.Center;
            //Grid.SetRow(button, row);
            //Grid.SetColumn(button, column);
            //MyTable.Children.Add(button);
        }
        private void DynamicButton_Click(object sender, RoutedEventArgs e)
        {
            // Obtén el botón que disparó el evento
            Button clickedButton = sender as Button;
            var elementsToRemove = new List<UIElement>();
            // Recupera la fila a eliminar desde la propiedad Tag
            string tag = clickedButton.Tag.ToString();

            RemoveFloorDescription(Guid.Parse(tag));

            //foreach (FrameworkElement child in MyTable.Children)
            //{
            //    var tagging = child.Tag;
                
            //    if (child.Tag.Equals(tag))
            //    {
            //        elementsToRemove.Add(child);
            //    }
            //}

            //// Aquí puedes agregar la lógica para eliminar la fila correspondiente.
            //// Eliminar los elementos de la colección del Grid
            //foreach (var element in elementsToRemove)
            //{
            //    MyTable.Children.Remove(element);
            //}

        }
        private void RemoveFloorDescription(Guid uuid)
        {
            //var objetoAEliminar = floorDescriptions.FirstOrDefault(o => o.Uuid == uuid);
            //if (objetoAEliminar != null)
            //{
            //    floorDescriptions.Remove(objetoAEliminar);
            //}
        }
        private string ExtractFloorResumeToJson()
        {
            List<object> floors = new List<object>();
            int counter = 0;
            //foreach (FloorDescription floorDescription in floorDescriptions)
            //{
            //    if (floorDescription.FloorCount == 1)
            //    {
            //        floors.Add(new
            //        {
            //            Name = $"N{counter}",
            //            Height = floorDescription.FloorHeight,
            //            NeedGate = floorDescription.NeedGate,
            //            Type = floorDescription.Type,
            //            NeedChimmey = floorDescription.NeedChimney
            //        });
            //        counter++;
            //    }
            //    else
            //    {
            //        for (int i = 1; i <= floorDescription.FloorCount; i++)
            //        {
            //            floors.Add(new
            //            {
            //                Name = $"N{counter}",
            //                Height = floorDescription.FloorHeight,
            //                NeedGate = floorDescription.NeedGate,
            //                Type = floorDescription.Type,
            //                NeedChimmey = floorDescription.NeedChimney
            //            });
            //            counter++;
            //        }
            //    }
            //}
            //floorCount = counter++;
            return JsonConvert.SerializeObject(floors);
        }
        private void ShowDucts(Dictionary<Duct.TypeDuct,int> ducts) 
        {
            foreach (var duct in ducts)
            {
                if (duct.Value > 0)
                {
                    //int row = DuctTable.RowDefinitions.Count;
                    //DuctTable.RowDefinitions.Add(new RowDefinition
                    //{
                    //    Height = new GridLength(1, GridUnitType.Auto)
                    //});
                    //TextBlock name = new TextBlock
                    //{
                    //    Tag = 1,
                    //    Text = duct.Key.ToString(),
                    //    TextAlignment = TextAlignment.Center,
                    //    Margin = new Thickness(10, 8, 13, 8),
                    //    Padding = new Thickness(3)
                    //};
                    //TextBlock count = new TextBlock
                    //{
                    //    Tag = 1,
                    //    Text = duct.Value.ToString(),
                    //    TextAlignment = TextAlignment.Center,
                    //    Margin = new Thickness(10, 8, 13, 8),
                    //    Padding = new Thickness(3)
                    //};
                    //var pathGeometry = new PathGeometry();
                    //var pathFigure = new PathFigure { StartPoint = new Windows.Foundation.Point(0, 0) };
                    //pathFigure.Segments.Add(new LineSegment { Point = new Windows.Foundation.Point(200, 0) });
                    //pathGeometry.Figures.Add(pathFigure);

                    //Path path = new Path {
                    //    Tag = 1,
                    //    Stroke = new SolidColorBrush(Microsoft.UI.Colors.Gray),
                    //    StrokeThickness = 0.5,
                    //    Data = pathGeometry,
                    //    Margin = new Thickness(36, 0, 36, 0),
                    //    Stretch = Stretch.Fill,
                    //    VerticalAlignment = VerticalAlignment.Bottom
                    //};

                    //Grid.SetRow(name, row);
                    //Grid.SetColumn(name, 0);
                    //DuctTable.Children.Add(name);
                    //Grid.SetRow(count, row);
                    //Grid.SetColumn(count, 1);
                    //DuctTable.Children.Add(count);
                    //Grid.SetRow(path, row);
                    //Grid.SetColumnSpan(path, 6);
                    //DuctTable.Children.Add(path);
                }
            }
        }
        private void ShowDuctsByFloor(List<Floor> calculatedFloors)
        {
            //foreach (var floor in calculatedFloors)
            //{
            //    bool addFloor = false;
            //    int countSpan = 0;
            //    int startRow = DuctsByFloorTable.RowDefinitions.Count; // Guardar el índice inicial

            //    foreach (var duct in floor.Ducts)
            //    {
            //        if (duct.Value > 0)
            //        {
            //            addFloor = true;
            //            DuctsByFloorTable.RowDefinitions.Add(new RowDefinition
            //            {
            //                Height = new GridLength(1, GridUnitType.Auto)
            //            });

            //            int currentRow = DuctsByFloorTable.RowDefinitions.Count - 1;

            //            TextBlock name = new TextBlock
            //            {
            //                Tag = 1,
            //                Text = duct.Key.ToString(),
            //                TextAlignment = TextAlignment.Center,
            //                Margin = new Thickness(10, 8, 13, 8),
            //                Padding = new Thickness(3)
            //            };
            //            TextBlock count = new TextBlock
            //            {
            //                Tag = 1,
            //                Text = duct.Value.ToString(),
            //                TextAlignment = TextAlignment.Center,
            //                Margin = new Thickness(10, 8, 13, 8),
            //                Padding = new Thickness(3)
            //            };
            //            var pathGeometry = new PathGeometry();
            //            var pathFigure = new PathFigure { StartPoint = new Windows.Foundation.Point(0, 0) };
            //            pathFigure.Segments.Add(new LineSegment { Point = new Windows.Foundation.Point(200, 0) });
            //            pathGeometry.Figures.Add(pathFigure);

            //            Path path = new Path
            //            {
            //                Tag = 1,
            //                Stroke = new SolidColorBrush(Microsoft.UI.Colors.Gray),
            //                StrokeThickness = 0.5,
            //                Data = pathGeometry,
            //                Margin = new Thickness(36, 0, 36, 0),
            //                Stretch = Stretch.Fill,
            //                VerticalAlignment = VerticalAlignment.Bottom
            //            };

            //            Grid.SetRow(name, currentRow);
            //            Grid.SetColumn(name, 1);
            //            DuctsByFloorTable.Children.Add(name);

            //            Grid.SetRow(count, currentRow);
            //            Grid.SetColumn(count, 2);
            //            DuctsByFloorTable.Children.Add(count);

            //            Grid.SetRow(path, currentRow);
            //            Grid.SetColumnSpan(path, 6);
            //            DuctsByFloorTable.Children.Add(path);

            //            countSpan++;
            //        }
            //    }

            //    if (addFloor && countSpan > 0)
            //    {
            //        // Crear el TextBlock para el nombre del nivel
            //        TextBlock nameLevel = new TextBlock
            //        {
            //            Tag = 1,
            //            Text = floor.Name.ToString(),
            //            TextAlignment = TextAlignment.Center,
            //            Margin = new Thickness(10, 8, 13, 8),
            //            Padding = new Thickness(3)
            //        };

            //        // Colocar el nombre del nivel en la primera fila del grupo
            //        Grid.SetRow(nameLevel, startRow);
            //        Grid.SetColumn(nameLevel, 0);
            //        Grid.SetRowSpan(nameLevel, countSpan); // Abarca todas las filas del grupo

            //        DuctsByFloorTable.Children.Add(nameLevel);
            //    }
            //}
        }

        private void ShowDuctsByFloor2(List<Floor> calculatedFloors)
        {
            foreach (var floor in calculatedFloors)
            {
                bool addFloor = false;
                int countSpan = 0;
                int startRow = DuctsByFloorTable.RowDefinitions.Count; // Guardar el índice inicial

                foreach (var duct in floor.Ducts)
                {
                    if (duct.Value > 0)
                    {
                        addFloor = true;
                        DuctsByFloorTable.RowDefinitions.Add(new RowDefinition
                        {
                            Height = new GridLength(1, GridUnitType.Auto)
                        });

                        int currentRow = DuctsByFloorTable.RowDefinitions.Count - 1;

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

                        Grid.SetRow(name, currentRow);
                        Grid.SetColumn(name, 1);
                        DuctsByFloorTable.Children.Add(name);

                        Grid.SetRow(count, currentRow);
                        Grid.SetColumn(count, 2);
                        DuctsByFloorTable.Children.Add(count);

                        Grid.SetRow(path, currentRow);
                        Grid.SetColumnSpan(path, 6);
                        DuctsByFloorTable.Children.Add(path);

                        countSpan++;
                    }
                }

                if (addFloor && countSpan > 0)
                {
                    // Crear el TextBlock para el nombre del nivel
                    TextBlock nameLevel = new TextBlock
                    {
                        Tag = 1,
                        Text = floor.Name.ToString(),
                        TextAlignment = TextAlignment.Center,
                        Margin = new Thickness(10, 8, 13, 8),
                        Padding = new Thickness(3)
                    };

                    // Colocar el nombre del nivel en la primera fila del grupo
                    Grid.SetRow(nameLevel, startRow);
                    Grid.SetColumn(nameLevel, 0);
                    Grid.SetRowSpan(nameLevel, countSpan); // Abarca todas las filas del grupo

                    DuctsByFloorTable.Children.Add(nameLevel);
                }
            }
        }

      
        private void FlipCard(object sender, RoutedEventArgs e)
        {
            bool isFrontVisible = FrontCard.Visibility == Visibility.Visible;
            Compositor _compositor;
            Visual _frontVisual;
            Visual _backVisual;
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            _frontVisual = ElementCompositionPreview.GetElementVisual(FrontCard);
            _backVisual = ElementCompositionPreview.GetElementVisual(BackCard);

            if (isFrontVisible)
            {
                // Animar el frente para que gire 90 grados
                AnimateRotation(_frontVisual, 0, 90, _compositor, () =>
                {
                    // Ocultar el frente y mostrar el reverso
                    FrontCard.Visibility = Visibility.Collapsed;
                    BackCard.Visibility = Visibility.Visible;

                    // Animar el reverso desde -90 a 0 grados
                    AnimateRotation(_backVisual, -90, 0, _compositor);
                });
            }
            else
            {
                // Animar el reverso para que gire 90 grados
                AnimateRotation(_backVisual, 0, 90, _compositor, () =>
                {
                    // Ocultar el reverso y mostrar el frente
                    BackCard.Visibility = Visibility.Collapsed;
                    FrontCard.Visibility = Visibility.Visible;

                    // Animar el frente desde -90 a 0 grados
                    AnimateRotation(_frontVisual, -90, 0, _compositor);
                });
            }

            // Alternar el estado de visibilidad
            isFrontVisible = !isFrontVisible;
        }

        private void AnimateRotation(Visual target, float from, float to, Compositor _compositor, Action? completed = null)
        {
            var animation = _compositor.CreateScalarKeyFrameAnimation();
            animation.InsertKeyFrame(0f, from);
            animation.InsertKeyFrame(1f, to);
            animation.Duration = TimeSpan.FromMilliseconds(500);
            animation.Target = "RotationAngleInDegrees";

            target.CenterPoint = new Vector3((float)leftCardContainer.ActualWidth / 2, (float)leftCardContainer.ActualHeight / 2, 0);
            target.RotationAxis = new Vector3(0, 1, 0); // Eje Y para rotación horizontal

            // Usar CompositionScopedBatch para detectar cuando termine la animación
            var batch = _compositor.CreateScopedBatch(CompositionBatchTypes.Animation);
            target.StartAnimation("RotationAngleInDegrees", animation);

            if (completed != null)
            {
                batch.Completed += (s, e) => completed.Invoke();
            }

            batch.End();
        }
        

    }
}
