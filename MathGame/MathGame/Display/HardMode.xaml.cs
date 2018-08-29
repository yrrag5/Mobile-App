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
    public sealed partial class HardMode : Page
    {

        private Random val = new Random();
        private int Score = 0, state = 1, highScore = 0, num1, num2, result;
        private DispatcherTimer disTimer;

        void setupProgressBar()
        {
            disTimer = new DispatcherTimer();
            disTimer.Tick += DisTimer_Tick;
            disTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            disTimer.Start();

            bar.Value = 999;
        }

        private void DisTimer_Tick(object sender, object e)
        {
            bar.Value -= Conditions.Score.Speed;
            if(bar.Value <= 0)
            {
                disTimer.Stop();
                disTimer = null;
                Frame.Navigate(typeof(GameOver), score.ToString());
            }
        }

        public HardMode()
        {
            this.InitializeComponent();
        }

        private int randomNum()
        {
            return val.Next(1, 9);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += HardMode_BackRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += HardMode_BackRequested;
            highScore = int.Parse(Conditions.Score.LoadSett("HighScore"));
            HighScore.Text = String.Format("Highest:(0)", highScore);
            disTimer = null;
            Play();
        }
        private int randNumValue()
        {
            return val.Next(0, 3);

        }
        private void Play()
        {
            int val1 = randomNum();
            int val2 = val.Next(0, num1 - 1);
            int mathVal = randNumValue();
            int res = -1;

            if(mathVal == 0)
            {
                res = val1 + val2;
            }

            else if(mathVal == 1)
            {
                res = val1 - val2;
            }

            else if(mathVal == 2)
            {
                res = val1 * val2;
            }
        
            else
            {
                res = val1 / val2;
            }

            num1 = val1;
            num2 = val2;
            result = res;
            question.Text = String.Format("(0) ... (1) = (2)", num1, num2, result);

            setupProgressBar();
    }

        private async void HardMode_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
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
            }
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            if(num1+num2 == result)
            {
                score.Text = String.Format("Score:(0)".ToUpper(), ++Score);
                State.Text = String.Format("(0)", ++state);
                disTimer.Stop();
                disTimer = null;
                Play();
            }

            else
            {
                disTimer.Stop();
                disTimer = null;
                Frame.Navigate(typeof(GameOver), score.ToString());
            }
        }

        private void minus_Click(object sender, RoutedEventArgs e)
        {
            if (num1 - num2 == result)
            {
                score.Text = String.Format("Score:(0)".ToUpper(), ++Score);
                State.Text = String.Format("(0)", ++state);
                disTimer.Stop();
                disTimer = null;
                Play();
            }

            else
            {
                disTimer.Stop();
                disTimer = null;
                Frame.Navigate(typeof(GameOver), score.ToString());
            }

        }

        private void muiltiply_Click(object sender, RoutedEventArgs e)
        {
            if (num1 * num2 == result)
            {
                score.Text = String.Format("Score:(0)".ToUpper(), ++Score);
                State.Text = String.Format("(0)", ++state);
                disTimer.Stop();
                disTimer = null;
                Play();
            }

            else
            {
                disTimer.Stop();
                disTimer = null;
                Frame.Navigate(typeof(GameOver), score.ToString());
            }

        }

        private void Divide_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (num1 / num2 == result)
                {
                    score.Text = String.Format("Score:(0)".ToUpper(), ++Score);
                    State.Text = String.Format("(0)", ++state);
                    disTimer.Stop();
                    disTimer = null;
                    Play();
                }

                else
                {
                    disTimer.Stop();
                    disTimer = null;
                    Frame.Navigate(typeof(GameOver), score.ToString());
                }
            }//try
            catch (DivideByZeroException)
            {
                Frame.Navigate(typeof(GameOver), score.ToString());
            }//catch

        }
    }
}
