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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MathGame.Display
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameOver : Page
    {
        private string highScore, localHighScore;

        public GameOver()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -= GameOver_BackRequested;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += GameOver_BackRequested;
            highScore = e.Parameter as string;
            localHighScore = Conditions.Score.LoadSett("HighScore");
            //Replaces new high score
            if (int.Parse(highScore) > int.Parse(localHighScore))
            {
                Conditions.Score.SaveSett("HighScore", highScore);

                Score.Text = highScore;
            }
        }

        private async void GameOver_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            e.Handled = true;
            var msg = new MessageDialog("App will be closed do you wish to continue?");
            var yes = new UICommand("Yes");
            var no = new UICommand("No");

            msg.Commands.Add(yes);
            msg.Commands.Add(no);
            IUICommand result = await msg.ShowAsync();

            if (result != null && result.Label.Equals("Yes"))
            {
                Application.Current.Exit();
            }//if
        }

        private void restart_Click(object sender, RoutedEventArgs e)
        {
            if(Conditions.Score.Mode == 1)
            {
                Frame.Navigate(typeof(HardMode));
            }
            else
            {
                //Frame.Navigate(typeof(EasyMode));

            }

        }

        private void home_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
