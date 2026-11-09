namespace KalkulatorCenIT
{
    public partial class MainPage : ContentPage
    {
        private decimal cena = 0.00m;
        public MainPage()
        {
            InitializeComponent();
        }
        private void czyszczenie(object? sender, EventArgs e)
        {
            imie.Text = string.Empty;
            urzadzenie.Text = string.Empty;

            diagnostyka.IsChecked = true;
            konfig.IsChecked = false;
            naprawa.IsChecked = false;

            kopia.IsChecked = false;
            wymiana.IsChecked = false;

            ekspres.IsToggled = false;

            rabat.Text = string.Empty;

            cena = 0.00m;

            lblPodsumowanie.Text = "Wprowadź dane i kliknij Oblicz...";
        }
        private void oblicz(object? sender, EventArgs e)
        {
            if (Waliduj(sender, e))
            {
                cena = 0.00m;

                if (diagnostyka.IsChecked) { cena += 100.00m; }
                else if (konfig.IsChecked) { cena += 150.00m; }
                else if (naprawa.IsChecked) { cena += 250.00m; }


                if (kopia.IsChecked) { cena += 80.00m; }

                if (wymiana.IsChecked) { cena += 40.00m; }

                if (ekspres.IsToggled)
                {
                    cena += cena * 0.50m;
                }

                string wpisanyKod = Rabat.Text?.Trim().ToUpper() ?? "";

                if (wpisanyKod != null)
                {
                    cena -= cena * 0.10m;
                }
                lblPodsumowanie.TextColor = Colors.Green;

                lblPodsumowanie.Text = $"Klient: {imie.Text}\n" +
                                       $"Urządzenie: {urzadzenie.Text}\n" +
                                       $"Cena: {cena:F2} zł";
            }
            else
            {
                lblPodsumowanie.TextColor = Colors.Red;
            }
        }

        private async void zatwierdz(object sender, EventArgs e)
        {
            if (Waliduj(sender, e))
            {
                await DisplayAlertAsync("Zlecenie przyjęte",
                                        $"Klient: {imie.Text}\n" +
                                        $"Urządzenie: {urzadzenie.Text}\n" +
                                        $"Cena: {cena:F2} zł\n" +
                                         "Zlecenie przyjęte",
                                         "OK");
            }
            else
            {
                await DisplayAlertAsync("Błąd", "Wypełnij poprawnie formularz", "OK");
            }
        }
        private bool Waliduj(object? sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(imie.Text) ||
                !string.IsNullOrEmpty(urzadzenie.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
}
