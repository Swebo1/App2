using System;
using Xamarin.Forms;

namespace App2.Page
{
    public class UserInputDialog : ContentPage
    {
        private string Placeholder;

        public Keyboard Keyboard { get; }

        public UserInputDialog(string placeholder, Keyboard keyboard)
        {
            Title = "Введите данные";
            Padding = new Thickness(10);

            var ageEntry = new Entry
            {
                Placeholder = "Возраст",
                Keyboard = Keyboard.Numeric,
                AutomationId = "AgeEntry"
            };

            var genderPicker = new Picker
            {
                Title = "Пол",
                Items = { "Мужской", "Женский" },
                AutomationId = "GenderPicker"
            };

            var weightEntry = new Entry
            {
                Placeholder = "Вес в килограммах",
                Keyboard = Keyboard.Numeric,
                AutomationId = "WeightEntry"
            };

            var heightEntry = new Entry
            {
                Placeholder = "Рост в сантиметрах",
                Keyboard = Keyboard.Numeric,
                AutomationId = "HeightEntry"
            };

            var okButton = new Button
            {
                Text = "OK"
            };
            okButton.Clicked += async (sender, e) =>
            {
                if (int.TryParse(ageEntry.Text, out int age) &&
                    double.TryParse(weightEntry.Text, out double weight) &&
                    double.TryParse(heightEntry.Text, out double height) &&
                    genderPicker.SelectedItem != null)
                {
                    // Получаем выбранный пол из элемента Picker
                    string gender = (string)genderPicker.SelectedItem;

                    // Вычисляем норму калорий по формуле Харриса-Бенедикта
                    double bmr = CalculateBMR(age, weight, height, gender);

                    // Отображаем результат на экране
                    await DisplayAlert("Результат", $"{bmr} ккал", "OK");

                    // Закрываем всплывающее окно
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Ошибка", "Пожалуйста, заполните все поля", "OK");
                }
            };


            var cancelButton = new Button
            {
                Text = "Отмена"
            };
            cancelButton.Clicked += async (sender, e) =>
            {
                // Закрываем всплывающее окно
                await Navigation.PopModalAsync();
            };

            var stackLayout = new StackLayout
            {
                Children = {
                ageEntry,
                genderPicker,
                weightEntry,
                heightEntry,
                new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = { okButton, cancelButton }
                }
            }
            };


            Content = stackLayout;

            Placeholder = placeholder;
            Keyboard = keyboard;
        }


        // Обработчик событий для кнопки "Рассчитать"
        public async void OnCalculateClicked (object sender, EventArgs e)
        {
            var ageEntry = Content.FindByName<Entry>("AgeEntry");
            var genderPicker = Content.FindByName<Picker>("GenderPicker");
            var weightEntry = Content.FindByName<Entry>("WeightEntry");
            var heightEntry = Content.FindByName<Entry>("HeightEntry");
            if (int.TryParse(ageEntry.Text, out int age) &&
            double.TryParse(weightEntry.Text, out double weight) &&
            double.TryParse(heightEntry.Text, out double height) &&
            genderPicker.SelectedItem != null)
            {
                string gender = (string)genderPicker.SelectedItem;
                double bmr = CalculateBMR(age, weight, height, gender);
                await DisplayAlert("Результат", $"{bmr} ккал", "OK");
            }
        }

        // Метод для вычисления нормы калорий по формуле Харриса-Бенедикта
        private double CalculateBMR(int age, double weight, double height, string gender)
        {
            double bmr;

            if (gender == "Мужской")
            {
                bmr = 88.36 + (13.4 * weight) + (4.8 * height) - (5.7 * age);
            }
            else
            {
                bmr = 447.6 + (9.2 * weight) + (3.1 * height) - (4.3 * age);
            }

            return bmr;
        }
    }
}
