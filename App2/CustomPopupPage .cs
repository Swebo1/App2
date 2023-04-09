using System;
using Xamarin.Forms;

namespace App2
{
    public class CustomPopupPage : ContentPage
    {
        public CustomPopupPage()
        {
            BackgroundColor = Color.FromHex("#C0808080");

            var stackLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = Color.White,
                Padding = new Thickness(40, 20),
                WidthRequest = 300,
                HeightRequest = 250,
                Children =
                {
                    new Label { Text = "Введите данные", FontSize = 20 },
                    new Entry { Placeholder = "Ваше имя" },
                    new Entry { Placeholder = "Ваш возраст", Keyboard = Keyboard.Numeric },
                    new Button { Text = "OK", HorizontalOptions = LayoutOptions.Center, Margin = new Thickness(0, 20), Command = new Command(ClosePopup) }
                }
            };

            Content = stackLayout;
        }

        private async void ClosePopup()
        {
            await Navigation.PopAsync();
        }
    }
}