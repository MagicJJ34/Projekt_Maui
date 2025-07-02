using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace Projekt
{
    public partial class Ustawienia : ContentPage
    {
        public Ustawienia()
        {
            InitializeComponent();
            // Ustawienie wartości dla switchów
            SwitchMotyw.IsToggled = Preferences.Get("Motyw", false);
            SwitchDzwieki.IsToggled = Preferences.Get("dzwieki", true); 
            SwitchAutozapis.IsToggled = Preferences.Get("autozapis", false);
        }
        // Zapisuje ustawienia
        private async void OnZapiszClicked(object sender, System.EventArgs e)
        {
            Preferences.Set("Motyw", SwitchMotyw.IsToggled);
            Preferences.Set("autozapis", SwitchAutozapis.IsToggled);
            Preferences.Set("dzwieki", SwitchDzwieki.IsToggled);
            await DisplayAlert("Ustawienia", "Ustawienia zapisane!", "Ok");

            Application.Current.UserAppTheme = SwitchMotyw.IsToggled ? AppTheme.Dark : AppTheme.Light;
        }
    }
}
