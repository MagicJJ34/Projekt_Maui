using System;
using Microsoft.Maui.Controls;

namespace Projekt
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void PrzejdzDoListy_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaZadan());
        }
    }
}
