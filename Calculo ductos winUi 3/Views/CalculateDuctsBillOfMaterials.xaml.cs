using Calculo_ductos_winUi_3.ViewModels;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Media;
using System;
using System.Numerics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculo_ductos_winUi_3.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CalculateDuctsBillOfMaterials : Page
    {
        
        
        public int floorCount = 0;
        public StateViewModel stateApp { get; set; }

        public CalculateDuctsBillOfMaterials()
        {
            this.InitializeComponent();
            stateApp = ((App)Application.Current).ViewModel;
            this.DataContext = stateApp;
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
