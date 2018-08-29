using MathGame.Display;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MathGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            // Easy mode
            if(Conditions.Score.Mode == 0)
            {
               // Frame.Navigate(typeof(Display.EasyMode));
            }

            // Hard Mode
            else
            {
                Frame.Navigate(typeof(Display.HardMode));
            }
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -= MainPage_BackRequested;
        }
        // Back Request
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
            Conditions.Score.Mode = int.Parse(Conditions.Score.LoadSett("Mode").ToString());

            txtHighScore.Text = "High Score : " + Conditions.Score.LoadSett("HighScore").ToString();


        }

        private async void MainPage_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            e.Handled = true;
            var msg = new MessageDialog("App will be closed do you wish to continue?");
            var yes = new UICommand("Yes");
            var no = new UICommand("No");

            msg.Commands.Add(yes);
            msg.Commands.Add(no);
            IUICommand result = await msg.ShowAsync();

            if(result != null && result.Label.Equals("Yes"))
            {
                Application.Current.Exit();
            }//if
        }

        private void option_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Options));
        }
    }
}
