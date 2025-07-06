using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Projekt
{
    public partial class ListaZadan : ContentPage
    {
        public ObservableCollection<Zadanie> Zadania { get; set; } = new ObservableCollection<Zadanie>(); // Kolekcja zadań
        private readonly string sciezkaPliku = "/data/user/0/com.companyname.projekt/files/zadania.json"; // Ścieżka do pliku
        public ObservableCollection<Zadanie> UsunieteZadaniaLista { get; set; } = 
        new ObservableCollection<Zadanie>(); // Lista usuniętych zadań
        public ListaZadan()
        {
            InitializeComponent();
            BindingContext = this;
        }
        // Metoda do dodania zadania na listę
        private async void DodajZadanie_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PoleZadania.Text))
            {
                Zadania.Add(new Zadanie { Opis = PoleZadania.Text, Wykonane = false });
                PoleZadania.Text = string.Empty;

                if (Preferences.Get("autozapis", false))
                {
                    await ZapiszZadaniaAsync();
                }
            }
        }
        // Metoda do asynchronicznego zapisu zadań do pliku 
        private async Task ZapiszZadaniaAsync()
        {
            var json = JsonSerializer.Serialize(Zadania, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(sciezkaPliku, json);
        }

        // Metoda do usunięcie zadania z listy
        private void UsunZadanie_Clicked(object sender, EventArgs e)
        {
            var przycisk = sender as Button;
            var zadanie = przycisk?.CommandParameter as Zadanie;
            if (zadanie != null)
            {
                Zadania.Remove(zadanie);
                UsunieteZadaniaLista.Add(zadanie);
            }
        }

        // Metoda do zapisania listy w pliku
        private async void ZapiszZadania_Clicked(object sender, EventArgs e)
        {
            try
            {
                var json = JsonSerializer.Serialize(Zadania, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(sciezkaPliku, json);

                await DisplayAlert("Sukces", "Zadania zostały zapisane do pliku.", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", $"Nie udało się zapisać: {ex.Message}", "Ok");
            }
        }
        // Metoda do wczytania pliku
        private async void WczytajZadania_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(sciezkaPliku))
                {
                    var json = File.ReadAllText(sciezkaPliku);
                    var wczytane = JsonSerializer.Deserialize<ObservableCollection<Zadanie>>(json);

                    if (wczytane != null)
                    {
                        Zadania.Clear();
                        foreach (var zadanie in wczytane)
                            Zadania.Add(zadanie);

                        await DisplayAlert("Wczytano", "Zadania zostały wczytane z pliku.", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Błąd", "Nie udało się wczytać danych.", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Brak pliku", "Nie znaleziono pliku", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", $"Błąd podczas wczytywania: {ex.Message}", "Ok");
            }
        }
        // Metoda do pokazania usuniętych zadań
        private async void PokazUsuniete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UsunieteZadania(UsunieteZadaniaLista, Zadania));
        }
        // Metoda wyczyszczenia listy zadań
        private async void WyczyscZadania_Clicked(object sender, EventArgs e)
        {
            bool potwierdzenie = await DisplayAlert("Potwierdzenie", "Czy na pewno chcesz usunąć wszystkie zadania?", 
                "Tak", "Nie");
            if (potwierdzenie)
            {
                Zadania.Clear();
                UsunieteZadaniaLista.Clear();

                await DisplayAlert("Wyczyszczono", "Wszystkie zadania zostały usunięte.", "Ok");
            }
        }

    }
}

