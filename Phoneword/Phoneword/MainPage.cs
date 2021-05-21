using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Phoneword
{
    public class MainPage : ContentPage
    {
        Entry phoneNumberText;
        Button translateButton;
        Button callButton;
        string translatedNumber;

        public MainPage()
        {
            // Sets padding between objects 
            this.Padding = new Thickness(20, 20, 20, 20);

            // Sets the spacing between objects on the panel or screen
            StackLayout panel = new StackLayout
            {
                Spacing = 15
            };

            // Adding labels, buttons, and the their default text thru children of the panel
            panel.Children.Add(new Label
            {
                Text = "Enter a Phoneword:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(phoneNumberText = new Entry
            {
                Text = "1-855-XAMARIN",
            });

            panel.Children.Add(translateButton = new Button
            {
                Text = "Translate"
            });

            panel.Children.Add(callButton = new Button
            {
                Text = "Call",
                IsEnabled = false,
            });

            translateButton.Clicked += OnTranslate;
            callButton.Clicked += Oncall;
            this.Content = panel;
        }

        private void OnTranslate(object sender, EventArgs e)
        {
            string enteredNumber = phoneNumberText.Text;
            // Assigns the translated number using the created domain's class verfication
            translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);

            // Enables or disables the functionality of Call the button on screen
            if (!string.IsNullOrEmpty(translatedNumber))
            {
                callButton.IsEnabled = true;
                callButton.Text = "Call: " + translatedNumber;
            }
            else
            {
                callButton.IsEnabled = false;
                callButton.Text = "Call";
            }
        }

        async void Oncall(object sender, System.EventArgs e)
        {
            if (await this.DisplayAlert(
                "Dial a Number",
                "Would you like to call: " + translatedNumber + "?",
                // String accept
                "Yes",
                // String cancel
                "No"))
            {
                // Using Xamarin.Essentials Phonedialer to dial the passed chars
                try
                {
                    PhoneDialer.Open(translatedNumber);
                }
                // Displayed if the entered numbers were null or not valid
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
                }
                catch (FeatureNotSupportedException)
                {
                    await DisplayAlert("Unable to dial", "Phone dialing not supported.", "OK");
                }   
                // "Catch All" if no other exception covers
                catch (Exception)
                {
                    await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
                }
            }
        }

    }
}
