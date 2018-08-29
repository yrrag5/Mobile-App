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
    public sealed partial class EasyMode : Page
    {
        private int num1, num2, result, randomResult, score=0, state=1, highScore=0, Mode;
        private DispatcherTimer disTimer;

        public void ProgressBar()
        {
            disTimer = new DispatcherTimer();
            disTimer.Tick += DispatcherTimer_Tick;
            disTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            disTimer.Start();

            bar.Value = 999;
        }// Progress Bar

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -= EasyMode_BackRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += EasyMode_BackRequested;
            disTimer = null;
        }

        private async void EasyMode_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            e.Handled = true;
            disTimer.Stop();
            disTimer = null;

            var msg = new MessageDialog("Do you want to quit?");
            var yes = new UICommand("Yes");
            var no = new UICommand("No");

            msg.Commands.Add(yes);
            msg.Commands.Add(no);
            IUICommand result = await msg.ShowAsync();

            if (result != null && result.Label.Equals("Yes"))
            {
                Frame.Navigate(typeof(GameOver), Score.ToString());
            }
        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            bar.Value -= Conditions.Score.Speed;
            if(bar.Value <= 0)
            {
                disTimer.Stop();
                disTimer = null;

                Frame.Navigate(typeof(GameOver), Score.ToString());
            }
        }

        public EasyMode()
        {
            this.InitializeComponent();
        }

        private void true_Click(object sender, RoutedEventArgs e)
        {
            //Answer if right 
            if(Mode == 1)
            {
                Score.Text = String.Format("Score:(0)".ToUpper(), ++score);
                State.Text = String.Format("(0)", ++state);
                disTimer.Stop();
                disTimer = null;
                Play();
            }//if

            else
            {
                disTimer.Stop();
                disTimer = null;

                Frame.Navigate(typeof(GameOver), Score.ToString());
            }
        }

        private void Play()
        {
            Random val = new Random();
            int value = val.Next(1, 4);
            if(value == 1)
            {
                num1 = val.Next(1, 9);
                num2 = val.Next(0, num1 - 1);
                result = num1 + num2;
                randomResult = val.Next(0, 99);

                Mode = val.Next(0, 10);
                if(Mode == 0)
                {
                    Ques.Text = String.Format("(0) + (1) =  (2)", num1, num2, randomResult);
                }//inner if
                else
                {
                    Ques.Text = String.Format("(0) + (1) =  (2)", num1, num2, result);
                }//else
            }//if

            if (value == 2)
            {
                num1 = val.Next(1, 9);
                num2 = val.Next(0, num1 - 1);
                result = num1 - num2;
                randomResult = val.Next(0, 99);

                Mode = val.Next(0, 10);
                if (Mode == 0)
                {
                    Ques.Text = String.Format("(0) - (1) =  (2)", num1, num2, randomResult);
                }//inner if
                else
                {
                    Ques.Text = String.Format("(0) - (1) =  (2)", num1, num2, result);
                }//else
            }//if

            if (value == 3)
            {
                num1 = val.Next(1, 9);
                num2 = val.Next(0, num2 - 1);
                result = num1 * num2;
                randomResult = val.Next(0, 99);

                Mode = val.Next(0, 10);
                if (Mode == 0)
                {
                    Ques.Text = String.Format("(0) * (1) =  (2)", num1, num2, randomResult);
                }//inner if
                else
                {
                    Ques.Text = String.Format("(0) * (1) =  (2)", num1, num2, result);
                }//else
            }//if

            if (value == 3)
            {
                num1 = val.Next(1, 9);
                num2 = val.Next(1, num2);
                result = num1 / num2;
                randomResult = val.Next(0, 99);

                Mode = val.Next(0, 10);
                if (Mode == 0)
                {
                    Ques.Text = String.Format("(0) / (1) =  (2)", num1, num2, randomResult);
                }//inner if
                else
                {
                    Ques.Text = String.Format("(0) / (1) =  (2)", num1, num2, result);
                }//else
            }//if
        }

        private void false_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
