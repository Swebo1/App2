using System;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using App2.Page;
using Rg.Plugins.Popup.Pages;

namespace App2
{
    public partial class MainPage : ContentPage
    {
        private double _dailyCalorieIntake = 2000;
        // здесь мы указываем общее количество калорий,
        // которое мы можем потреблять за день

        public MainPage()
        {
            InitializeComponent();
            UpdateCalorieDisplay();
                                    // обновляем отображение количества калорий на экране
            ShowUserDataPopup(); // добавлен вызов метода показа всплывающего окна
        }


        public partial class UserPopupPage : PopupPage
        {
            public UserPopupPage()
            {

            }

            public object UserData { get; internal set; }
        }


        private async void ShowUserDataPopup()
        {
            // создаем новое всплывающее окно
            var popup = new UserPopupPage();

            // открываем всплывающее окно и ждем, пока пользователь заполнит данные
            await App.Current.MainPage.Navigation.PushPopupAsync(popup);

            // здесь можно обработать данные, которые ввел пользователь, например:
            var userData = popup.UserData;

            // после того, как пользователь закрыл всплывающее окно,
            // можно обновить отображение калорий на экране
            UpdateCalorieDisplay();
        }





        private void UpdateCalorieDisplay()
        {
            // Обновляем метки на экране с учетом новых значений
            DailyCalorieIntakeLabel.Text = $"Ваша норма — {_dailyCalorieIntake} ккал";
            CaloriesLeftLabel.Text = $"У вас осталось — {_dailyCalorieIntake} ккал";
            CaloriesConsumedLabel.Text = $"Вы потребили — {(_dailyCalorieIntake + 2000)} ккал";
            CaloriesEntry.Text = ""; // очищаем окно для ввода
        }

        private void OnCaloriesEntered(object sender, EventArgs e)
        {
            if (double.TryParse(CaloriesEntry.Text, out double caloriesConsumed))
            {
                // Если пользователь ввел корректное число,
                // обновляем количество потребленных калорий и обновляем отображение на экране
                _dailyCalorieIntake -= caloriesConsumed;
                UpdateCalorieDisplay();
            }
        }
    }
}