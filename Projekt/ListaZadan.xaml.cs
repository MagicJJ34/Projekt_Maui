using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public partial class ListaZadan : ContentPage
    {
        public ObservableCollection<Zadanie> Zadania { get; set; } = new ObservableCollection<Zadanie>();
        public ListaZadan()

        {
            InitializeComponent();
            BindingContext = this;
        }
        private void DodajZadanie_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PoleZadania.Text))
            {
                Zadania.Add(new Zadanie { Opis = PoleZadania.Text, Wykonane = false });
                PoleZadania.Text = string.Empty;
            }
        }
        private void UsunZadanie_Clicked(object sender, EventArgs e)
        {
            var przycisk = sender as Button;
            var zadanie = przycisk?.CommandParameter as Zadanie;
            if (zadanie != null)
            {
                Zadania.Remove(zadanie);
            }
        }
    }
}

