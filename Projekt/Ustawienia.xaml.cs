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
            SwitchMotyw.IsToggled = Preferences.Get("Motyw", false);
            SwitchDzwieki.IsToggled = Preferences.Get("dzwieki", true); 
            SwitchAutozapis.IsToggled = Preferences.Get("autozapis", false);
        }
        private void SwitchDzwieki_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("dzwieki", e.Value);
        }
        private void SwitchAutozapis_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("autozapis", e.Value);
        }
        private async void OnZapiszClicked(object sender, System.EventArgs e)
        {
            Preferences.Set("Motyw", SwitchMotyw.IsToggled);
            await DisplayAlert("Ustawienia", "Ustawienia zapisane!", "Ok");

            Application.Current.UserAppTheme = SwitchMotyw.IsToggled ? AppTheme.Dark : AppTheme.Light;
        }
    }
}
