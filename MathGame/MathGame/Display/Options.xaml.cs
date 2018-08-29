using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MathGame.Display
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Options : Page
    {
        public Options()
        {
            this.InitializeComponent();
        }

        private void DiffSelectE_Checked(object sender, RoutedEventArgs e)
        {
            DiffSelectH.IsChecked = false;
            Conditions.Score.SaveSett("Play", "0");
        }


        private void DiffSelectH_Checked(object sender, RoutedEventArgs e)
        {
            DiffSelectE.IsChecked = false;
            Conditions.Score.SaveSett("Play", "1");
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -= Options_BackRequested;

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += Options_BackRequested;
            if(Conditions.Score.Mode == 0)
            {
                DiffSelectE.IsChecked = true;
                DiffSelectH.IsChecked = false;
            }
            else
            {
                DiffSelectE.IsChecked = false;
                DiffSelectH.IsChecked = true;
            }

            int sliderVal = Conditions.Score.Speed;
            slider.Value = sliderVal / 10; 
        }

        private void Options_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            e.Handled = true;
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Conditions.Score.Speed = int.Parse(slider.Value.ToString()) * 10;
            Conditions.Score.SaveSett("Speed", Conditions.Score.Speed.ToString());
        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
