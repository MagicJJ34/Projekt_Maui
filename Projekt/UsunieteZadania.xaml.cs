using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Projekt
{
    public partial class UsunieteZadania : ContentPage
    {
        // Kolekcja usuniętych zadań
        public ObservableCollection<Zadanie> Usuniete { get; set; }

        // Odwołanie do głównej listy zadań
        public ObservableCollection<Zadanie> ZadaniaGlowne { get; set; }

        // Konstruktor do trzymania dwóch list
        public UsunieteZadania(ObservableCollection<Zadanie> usuniete, ObservableCollection<Zadanie> zadaniaGlowne)
        {
            InitializeComponent();
            Usuniete = usuniete;
            ZadaniaGlowne = zadaniaGlowne;
            BindingContext = this;
        }
        // Metoda do przywrócenia zadania na liste
        private async void Przywroc_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var zadanie = btn?.CommandParameter as Zadanie;

            if (zadanie != null)
            {
                ZadaniaGlowne.Add(zadanie);
                Usuniete.Remove(zadanie);
            }
        }
    }
}

