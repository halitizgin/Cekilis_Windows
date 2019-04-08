using Cekilis_Windows.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Cekilis_Windows
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MainPage1 : Page
    {

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public MainPage1()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            this.Loaded += MainPage_Loaded;
            this.SizeChanged += Page_SizeChanged;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 500)
            {
                VisualStateManager.GoToState(this, "MinimalLayout", true);
            }
            else if (e.NewSize.Width < e.NewSize.Height)
            {
                VisualStateManager.GoToState(this, "PortraitLayout", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "DefaultLayout", true);
            }
        }

        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        public Windows.Storage.ApplicationDataContainer dilayar = Windows.Storage.ApplicationData.Current.LocalSettings;

        public void deneme()
        {
            katilimcilarBaslikTextBlocks.Text = "Değişti!";
        }

        public void yenile()
        {
            if (dilayar.Values["dil"] == null)
            {
                dilayar.Values["dil"] = "Türkçe";
            }
            else
            {
                textBox1.TextChanged += textBox1_TextChanged;
                katilimcilar.SelectionMode = SelectionMode.Extended;
                var timer = new DispatcherTimer();
                timer.Tick += DispatcherTimerEventHandler;
                timer.Interval = new TimeSpan(0, 0, 0, 1);
                timer.Start();
                kaydetButton.Click += kaydetButton_Click;
                yukleButton.Click += yukleButton_Click;
                onaylaButton.Click += onaylaButton_Click;
                iptalButton.Click += iptalButton_Click;
                if (dilayar.Values["dil"].ToString() == "Türkçe")
                {
                    ekleButton.Content = "Ekle";
                    kayitYoneticisiButtonApp.Content = "Kayıt Yöneticisi";
                    silButton.Content = "Sil";
                    duzenleButton.Content = "Düzenle";
                    hizliCheckBox.OffContent = "Hızlı Düzenleme";
                    hizliCheckBox.OnContent = "Hızlı Düzenleme";
                    adet.Text = "0 katılımcı,";
                    tekliRadioButton.Content = "Tekli";
                    takimliRadioButton.Content = "Takımlı";
                    kazanacakLabel.Text = "Kazanacak Sayısı";
                    cekilisYapButton.Content = "Çekiliş Yap";
                    kazananlarLabel.Text = "Kazananlar:";
                    katilimcilarBaslikTextBlocks.Text = "Katılımcılar:";
                    aciklamaLabel.Text = "(*) Tüm çıkma ihtimalleri aynı.";
                }
                else if (dilayar.Values["dil"].ToString() == "English")
                {
                    ekleButton.Content = "Add";
                    kayitYoneticisiButtonApp.Content = "Record Manager";
                    silButton.Content = "Delete";
                    duzenleButton.Content = "Edit";
                    hizliCheckBox.OffContent = "Fast Editing";
                    hizliCheckBox.OnContent = "Fast Editing";
                    adet.Text = "Participiants 0,";
                    tekliRadioButton.Content = "Single";
                    takimliRadioButton.Content = "Dual";
                    kazanacakLabel.Text = "Winner Count";
                    cekilisYapButton.Content = "Do";
                    kazananlarLabel.Text = "Winners:";
                    katilimcilarBaslikTextBlocks.Text = "Participants:";
                    aciklamaLabel.Text = "(*) All posibilities are same";
                }
            }
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            yenile();
        }


        bool gAktif = false;
        bool gGosterildi = false;
        Windows.Storage.ApplicationDataContainer value = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        string gIslem = "";
        MessageDialog ileti = new MessageDialog("", "");
        int gIndex = -1;
        string gVeriler = "";
        string gIsim = "";
        string katilimci = "";

        public async void MessageBox(string baslik, string metin)
        {
            ileti.Title = baslik;
            ileti.Content = metin;
            await ileti.ShowAsync();
        }

        private void EvetCommandHandler(IUICommand command)
        {
            value.Values[gIsim] = gVeriler;
        }

        private void HayirCommandHandler(IUICommand command)
        {

        }

        public void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void timer2Event(object sender, object e)
        {

        }

        private void yukleButton_Click(object sender, RoutedEventArgs e)
        {
            if (kayilarListBox.SelectedIndex != -1)
            {
                int index = kayilarListBox.SelectedIndex;
                string secilen = Convert.ToString(kayilarListBox.Items[index]);
                string secilenvalue = Convert.ToString(value.Values[secilen]);
                string[] secilenitems = secilenvalue.Split('|');
                katilimcilar.Items.Clear();
                foreach (string item in secilenitems)
                {
                    katilimcilar.Items.Add(item);
                }
                kayitYonetimi.IsOpen = false;
                if (dilayar.Values["dil"].ToString() == "Türkçe")
                {
                    adet.Text = katilimcilar.Items.Count + " katılımcı, seçilenin çıkma ihtimali %? (Tüm katılımcı çıkma ihtimalleri aynı)";
                }
                else if (dilayar.Values["dil"].ToString() == "English")
                {
                    adet.Text = "Participants " + katilimcilar.Items.Count + ", possibility of selected ?% (All participants likely to go the same)";
                }

                List<string> veriler = new List<string>();
                for (int i = 0; i < katilimcilar.Items.Count; i++)
                {
                    veriler.Add(katilimcilar.Items[i].ToString());
                }

                string sVeriler = "";

                foreach (string item in veriler)
                {
                    if (sVeriler == "")
                    {
                        sVeriler = item;
                    }
                    else
                    {
                        sVeriler += "\n" + item;
                    }
                }
                hizliRichEditBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, sVeriler);
            }
        }

        private void kaydetButton_Click(object sender, RoutedEventArgs e)
        {
            if (katilimcilar.Items.Count > 0)
            {
                katilimci = "";
                for (int i = 0; i < katilimcilar.Items.Count; i++)
                {
                    if (katilimci != "")
                    {
                        katilimci += "|" + Convert.ToString(katilimcilar.Items[i]);
                    }
                    else
                    {
                        katilimci = Convert.ToString(katilimcilar.Items[i]);
                    }
                }

                textBox1.Text = "";
                customDialog.IsOpen = true;
                if (dilayar.Values["dil"].ToString() == "Türkçe")
                {
                    customDialog.Title = "Kayıt ismini giriniz:";
                    onaylaButton.Content = "Onayla";
                    iptalButton.Content = "İptal";
                }
                else if (dilayar.Values["dil"].ToString() == "English")
                {
                    customDialog.Title = "Enter a name for your records:";
                    onaylaButton.Content = "Confirm";
                    iptalButton.Content = "Cancel";
                }
                gIslem = "Kayıt";
                gVeriler = katilimci;
            }
        }

        private void DispatcherTimerEventHandler(object sender, object e)
        {
            if (takimliRadioButton.IsChecked == true || tekliRadioButton.IsChecked == true)
            {
                if (takimliRadioButton.IsChecked == true)
                {
                    if (katilimcilar.Items.Count % 2 == 0)
                    {
                        kazanacakSayisi.Minimum = 1;
                        kazanacakSayisi.Maximum = katilimcilar.Items.Count / 2;
                    }
                    else
                    {
                        kazanacakSayisi.Minimum = 1;
                        kazanacakSayisi.Maximum = (katilimcilar.Items.Count + 1) / 2;
                    }
                }
                else if (tekliRadioButton.IsChecked == true)
                {
                    kazanacakSayisi.Minimum = 1;
                    kazanacakSayisi.Maximum = katilimcilar.Items.Count - 1;
                }

                if (Convert.ToInt32(kazanacakSayisi.Value) > 0 && takimliRadioButton.IsChecked == true || tekliRadioButton.IsChecked == true && katilimcilar.Items.Count > 0)
                {
                    cekilisYapButton.IsEnabled = true;
                }
                else
                {
                    cekilisYapButton.IsEnabled = false;
                }
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                onaylaButton.IsEnabled = false;
            }
            else
            {
                onaylaButton.IsEnabled = true;
            }
        }

        private void ekleDugme_Click(object sender, RoutedEventArgs e)
        {

        }

        private void iptalButton_Click(object sender, RoutedEventArgs e)
        {
            customDialog.IsOpen = false;
        }

        private async void onaylaButton_Click(object sender, RoutedEventArgs e)
        {
            bool esit = false;
            if (textBox1.Text.Trim() != "" && textBox1.Text.Trim() != "dil")
            {
                customDialog.IsOpen = false;
                if (gIslem == "Ekle")
                {
                    katilimcilar.Items.Add(textBox1.Text.Trim());
                    textBox1.Text = "";
                    if (dilayar.Values["dil"].ToString() == "Türkçe")
                    {
                        adet.Text = katilimcilar.Items.Count + " katılımcı, seçilenin çıkma ihtimali %? (Tüm katılımcı çıkma ihtimalleri aynı)";
                    }
                    else if (dilayar.Values["dil"].ToString() == "English")
                    {
                        adet.Text = "Participants " + katilimcilar.Items.Count + ", possibility of selected ?% (All participants likely to go the same)";
                    }

                }
                else if (gIslem == "Düzenle")
                {
                    if (gIndex != -1)
                    {
                        katilimcilar.Items[gIndex] = textBox1.Text.Trim();
                        textBox1.Text = "";
                    }
                }
                else if (gIslem == "Kayıt")
                {
                    textBox1.Text.Replace('|', '-');
                    foreach (string key in value.Values.Keys)
                    {
                        if (textBox1.Text.Trim() == key)
                        {
                            esit = true;
                        }
                    }

                    if (esit == true)
                    {
                        gIsim = textBox1.Text.Trim();
                        if (dilayar.Values["dil"].ToString() == "Türkçe")
                        {
                            ileti.Title = "Soru";
                            ileti.Content = textBox1.Text.Trim() + " adlı kayıt zaten mevcuttur. Üzerine yazmak istediğinize emin misiniz?";
                        }
                        else if (dilayar.Values["dil"].ToString() == "English")
                        {
                            ileti.Title = "Question";
                            ileti.Content = "I " + textBox1.Text.Trim() + " records are already available. Are you sure you want to overwrite?";
                        }
                        ileti.CancelCommandIndex = 1;
                        if (gGosterildi == false)
                        {
                            if (dilayar.Values["dil"].ToString() == "Türkçe")
                            {
                                ileti.Commands.Add(new UICommand("Evet", new UICommandInvokedHandler(EvetCommandHandler)));
                                ileti.Commands.Add(new UICommand("Hayır", new UICommandInvokedHandler(HayirCommandHandler)));
                            }
                            else if (dilayar.Values["dil"].ToString() == "English")
                            {
                                ileti.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(EvetCommandHandler)));
                                ileti.Commands.Add(new UICommand("No", new UICommandInvokedHandler(HayirCommandHandler)));
                            }
                            gGosterildi = true;
                            await ileti.ShowAsync();
                        }
                        else
                        {
                            await ileti.ShowAsync();
                        }
                    }
                    else
                    {
                        value.Values[textBox1.Text.Trim()] = gVeriler;
                    }

                    kayilarListBox.Items.Clear();
                    foreach (string veri in value.Values.Keys)
                    {
                        if (veri != "dil")
                        {
                            kayilarListBox.Items.Add(veri);
                        }
                    }
                }

                List<string> veriler = new List<string>();
                for (int i = 0; i < katilimcilar.Items.Count; i++)
                {
                    veriler.Add(katilimcilar.Items[i].ToString());
                }

                string sVeriler = "";

                foreach (string item in veriler)
                {
                    if (sVeriler == "")
                    {
                        sVeriler = item;
                    }
                    else
                    {
                        sVeriler += "\n" + item;
                    }
                }
                hizliRichEditBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, sVeriler);
            }
        }

        private void silDugme_Click(object sender, RoutedEventArgs e)
        {

        }

        private void duzenleDugme_Click(object sender, RoutedEventArgs e)
        {

        }

        public double YuzdeHesapla(int index)
        {
            int sayisi = 0;
            List<string> tum = new List<string>();
            List<string> hesap = new List<string>();
            int seciliindex = index;
            string seciliveri = Convert.ToString(katilimcilar.Items[seciliindex]);
            for (int i = 0; i < katilimcilar.Items.Count; i++)
            {
                tum.Add(Convert.ToString(katilimcilar.Items[i]));
            }

            foreach (string ileti in tum)
            {
                if (ileti == seciliveri)
                {
                    sayisi++;
                }
            }
            double yuzde = ((double)sayisi / katilimcilar.Items.Count) * 100;
            return yuzde;
        }

        public double YuzdeHesaplaMetin(string metin)
        {
            int sayisi = 0;
            List<string> tum = new List<string>();
            List<string> hesap = new List<string>();
            string seciliveri = metin;
            for (int i = 0; i < katilimcilar.Items.Count; i++)
            {
                tum.Add(Convert.ToString(katilimcilar.Items[i]));
            }

            foreach (string ileti in tum)
            {
                if (ileti == metin)
                {
                    sayisi++;
                }
            }
            double yuzde = ((double)sayisi / katilimcilar.Items.Count) * 100;
            return yuzde;
        }

        private void katilimcilar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                List<double> items = new List<double>();
                int sayi = 0;
                double yuzde = YuzdeHesapla(katilimcilar.SelectedIndex);
                for (int i = 0; i < katilimcilar.Items.Count; i++)
                {
                    items.Add(YuzdeHesapla(i));
                }

                foreach (double oran in items)
                {
                    if (yuzde == oran)
                    {
                        sayi++;
                    }
                }
                int tam = Convert.ToInt32(yuzde);
                int virgul = -1;
                string bYuzde = yuzde.ToString();
                for (int b = 0; b < bYuzde.Length; b++)
                {
                    char karakter = Convert.ToChar(bYuzde.Substring(b, 1));
                    if (karakter == ',')
                    {
                        virgul = b;
                    }
                }
                
                if (virgul == -1)
                {
                    if (katilimcilar.Items.Count == sayi)
                    {

                        if (dilayar.Values["dil"].ToString() == "Türkçe")
                        {
                            adet.Text = katilimcilar.Items.Count + " katılımcı,";
                            yuzdeLabel.Text = "%" + yuzde + " (*)";
                        }
                        else if (dilayar.Values["dil"].ToString() == "English")
                        {
                            adet.Text = "Participants " + katilimcilar.Items.Count + ",";
                            yuzdeLabel.Text = yuzde + "% (*)";
                        }
                    }
                    else
                    {
                        if (dilayar.Values["dil"].ToString() == "Türkçe")
                        {
                            adet.Text = katilimcilar.Items.Count + " katılımcı,";
                            yuzdeLabel.Text = "%" + yuzde + " (*)";
                        }
                        else if (dilayar.Values["dil"].ToString() == "English")
                        {
                            adet.Text = "Participants " + katilimcilar.Items.Count + ",";
                            yuzdeLabel.Text = yuzde + "% (*)";
                        }
                    }
                }

                string ilkveri = bYuzde.Substring(0, virgul);
                string ikinciveri = bYuzde.Substring(virgul + 1, 1);
                string birles = ilkveri + "," + ikinciveri;

                if (katilimcilar.Items.Count == sayi)
                {
                    if (dilayar.Values["dil"].ToString() == "Türkçe")
                    {
                        adet.Text = katilimcilar.Items.Count + " katılımcı,";
                        yuzdeLabel.Text = "%" + birles + " (*)";
                    }
                    else if (dilayar.Values["dil"].ToString() == "English")
                    {
                        adet.Text = "Participants " + katilimcilar.Items.Count + ",";
                        yuzdeLabel.Text = birles + "% (*)";
                    }
                }
                else
                {
                    if (dilayar.Values["dil"].ToString() == "Türkçe")
                    {
                        adet.Text = katilimcilar.Items.Count + " katılımcı";
                        yuzdeLabel.Text = "%" + birles;
                    }
                    else if (dilayar.Values["dil"].ToString() == "English")
                    {
                        adet.Text = "Participants " + katilimcilar.Items.Count + ",";
                        yuzdeLabel.Text = birles + "%";
                    }
                }
            }
            catch (Exception hata)
            {

            }
        }

        private void cekilisYapButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string kazananlarmetin = "";
                List<string> kayitlar = new List<string>();
                int ilkKazanan = 0;
                int ikinciKazanan = 0;
                Random rastgele = new Random();
                int son = 0;
                int uretilensayi = 0;
                int count = katilimcilar.Items.Count;
                List<string> veriler = new List<string>(katilimcilar.Items.Count);
                List<string> veriler2 = new List<string>(katilimcilar.Items.Count);
                List<string> uretilenler = new List<string>(Convert.ToInt32(kazanacakSayisi.Value));
                if (tekliRadioButton.IsChecked == true)
                {
                    kazananlarmetin = "";
                    for (int i = 0; i < count; i++)
                    {
                        veriler.Add(Convert.ToString(katilimcilar.Items[i]));
                    }

                    for (int i = 0; i < Convert.ToInt32(kazanacakSayisi.Value); i++)
                    {
                        uretilensayi = rastgele.Next(0, veriler.Count);
                        uretilenler.Add(veriler[uretilensayi]);
                        veriler.RemoveAt(uretilensayi);
                    }
                    kazananlarmetin = "";
                    foreach (string kazananveri in uretilenler)
                    {
                        if (kazananlarmetin == "")
                        {
                            kazananlarmetin = kazananveri;
                        }
                        else
                        {
                            kazananlarmetin += "\n" + kazananveri;
                        }
                    }
                }
                else if (takimliRadioButton.IsChecked == true)
                {
                    kazananlarmetin = "";
                    int uretilen = rastgele.Next(0, veriler2.Count);

                    for (int i = 0; i < katilimcilar.Items.Count; i++)
                    {
                        veriler2.Add(Convert.ToString(katilimcilar.Items[i]));
                    }
                    for (int i = 0; i < Convert.ToInt32(kazanacakSayisi.Value); i++)
                    {

                        if (kazananlarmetin != "")
                        {
                            kazananlarmetin += "\n" + veriler2[uretilen];
                            veriler2.RemoveAt(uretilen);
                        }
                        else
                        {
                            kazananlarmetin += veriler2[uretilen];
                            veriler2.RemoveAt(uretilen);
                        }

                        if (veriler2.Count > 0)
                        {
                            int uretilen2 = rastgele.Next(0, veriler2.Count);
                            if (kazananlarmetin != "")
                            {
                                kazananlarmetin = kazananlarmetin + "   -   " + veriler2[uretilen2];
                                veriler2.RemoveAt(uretilen2);
                            }
                            else
                            {
                                kazananlarmetin = kazananlarmetin + "   -   " + veriler2[uretilen2];
                                veriler2.RemoveAt(uretilen2);
                            }
                        }
                        else
                        {
                            kazananlarmetin = kazananlarmetin + "  (BAY)";
                        }
                    }
                }

                if (kazananlarmetin != "")
                {
                    kazananlarRichBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, kazananlarmetin);
                }
            }
            catch (Exception hata)
            {
                MessageBox("Hata", hata.Message);
            }
        }

        private void NumericUpDown_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void kayitYoneticisiButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void kaydet2Button_Click(object sender, RoutedEventArgs e)
        {
            if (katilimcilar.Items.Count > 0)
            {
                katilimci = "";
                for (int i = 0; i < katilimcilar.Items.Count; i++)
                {
                    if (katilimci != "")
                    {
                        katilimci += "|" + Convert.ToString(katilimcilar.Items[i]);
                    }
                    else
                    {
                        katilimci = Convert.ToString(katilimcilar.Items[i]);
                    }
                }

                customDialog.IsOpen = true;
                gIslem = "Kayıt";
                if (dilayar.Values["dil"].ToString() == "Türkçe")
                {
                    customDialog.Title = "Kayıt ismini giriniz:";
                    onaylaButton.Content = "Onayla";
                    iptalButton.Content = "İptal";
                }
                else if (dilayar.Values["dil"].ToString() == "English")
                {
                    customDialog.Title = "Enter a name for your records:";
                    onaylaButton.Content = "Confirm";
                    iptalButton.Content = "Cancel";
                }
                gVeriler = katilimci;
            }
            else
            {
                if (dilayar.Values["dil"].ToString() == "Türkçe")
                {
                    MessageBox("Hata", "Katılımcıları kayıt edebilmeniz için en az 1 veri bulunması gerekmektedir!");
                }
                else if (dilayar.Values["dil"].ToString() == "English")
                {
                    MessageBox("Error", "If i can record the participants, it must exist minimum 1 data!");
                }
            }
        }

        private void kayitSilButton_Click(object sender, RoutedEventArgs e)
        {
            if (kayilarListBox.SelectedIndex != -1)
            {
                int index = kayilarListBox.SelectedIndex;
                string secilen = Convert.ToString(kayilarListBox.Items[index]);
                value.Values.Remove(secilen);
                kayilarListBox.Items.Clear();
                foreach (string veri in value.Values.Keys)
                {
                    if (veri != "dil")
                    {
                        kayilarListBox.Items.Add(veri);
                    }
                }
            }
        }

        private void textBox1_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void RichEditBox_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void kayilarListBox_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (kayilarListBox.SelectedIndex != -1)
            {
                int index = kayilarListBox.SelectedIndex;
                string secilen = Convert.ToString(kayilarListBox.Items[index]);
                string secilenvalue = Convert.ToString(value.Values[secilen]);
                string[] secilenitems = secilenvalue.Split('|');
                katilimcilar.Items.Clear();
                foreach (string item in secilenitems)
                {
                    katilimcilar.Items.Add(item);
                }
                kayitYonetimi.IsOpen = false;
                if (dilayar.Values["dil"].ToString() == "Türkçe")
                {
                    adet.Text = katilimcilar.Items.Count + " katılımcı, seçilenin çıkma ihtimali %? (Tüm katılımcı çıkma ihtimalleri aynı)";
                }
                else if (dilayar.Values["dil"].ToString() == "English")
                {
                    adet.Text = "Participants " + katilimcilar.Items.Count + ", possibility of selected ?% (All participants likely to go the same)";
                }
                hizliRichEditBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                katilimcilar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        private void hizliCheckBox_Click(object sender, RoutedEventArgs e)
        {
            string deger = String.Empty;
            if (hizliCheckBox.IsOn == true)
            {
                hizliRichEditBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
                katilimcilar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                if (katilimcilar.Items.Count > 0)
                {
                    string satirlar = "";
                    for (int i = 0; i < katilimcilar.Items.Count; i++)
                    {
                        hizliRichEditBox.Document.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out deger);
                        if (satirlar == "")
                        {
                            satirlar = katilimcilar.Items[i].ToString();
                        }
                        else
                        {
                            satirlar += "\n" + katilimcilar.Items[i].ToString();
                        }
                    }
                    hizliRichEditBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, satirlar);
                }
            }
            else
            {
                hizliRichEditBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                katilimcilar.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        private void hizliRichEditBox_TextChanged(object sender, RoutedEventArgs e)
        {
            /*string satir = String.Empty;
            hizliRichEditBox.Document.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out satir);
            if (satir != "")
            {
                List<double> items = new List<double>();
                int sayi = 0;
                double yuzde = YuzdeHesapla(katilimcilar.SelectedIndex);
                for (int i = 0; i < katilimcilar.Items.Count; i++)
                {
                    items.Add(YuzdeHesapla(i));
                }

                foreach (double oran in items)
                {
                    if (yuzde == oran)
                    {
                        sayi++;
                    }
                }
                katilimcilar.Items.Clear();
                string[] satirlar = satir.Split('\r');
                foreach (string veri in satirlar)
                {
                    if (veri.Trim() != "")
                    {
                        katilimcilar.Items.Add(veri);
                    }
                }

                if (sayi == katilimcilar.Items.Count)
                {
                    if (dilayar.Values["dil"].ToString() == "Türkçe")
                    {
                        adet.Text = katilimcilar.Items.Count + " katılımcı, seçilenin çıkma ihtimali %" + yuzde + " (Tüm katılımcı çıkma ihtimalleri aynı)";
                    }
                    else if (dilayar.Values["dil"].ToString() == "English")
                    {
                        adet.Text = "Participants " + katilimcilar.Items.Count + ", possibility of selected " + yuzde + "% (All participants likely to go the same)";
                    }
                }
                else
                {
                    if (dilayar.Values["dil"].ToString() == "Türkçe")
                    {
                        adet.Text = katilimcilar.Items.Count + " katılımcı, seçilenin çıkma ihtimali %" + yuzde;
                    }
                    else if (dilayar.Values["dil"].ToString() == "English")
                    {
                        adet.Text = "Participants " + katilimcilar.Items.Count + ", possibility of selected " + yuzde + "%";
                    }
                }
            }
            else
            {
                katilimcilar.Items.Clear();
            }*/
        }

        private void hizliRichEditBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (hizliRichEditBox.Document.Selection.Text != "")
                {
                    List<double> items = new List<double>();
                    int sayi = 0;
                    double yuzde = YuzdeHesaplaMetin(hizliRichEditBox.Document.Selection.Text);
                    for (int i = 0; i < katilimcilar.Items.Count; i++)
                    {
                        items.Add(YuzdeHesapla(i));
                    }

                    foreach (double oran in items)
                    {
                        if (yuzde == oran)
                        {
                            sayi++;
                        }
                    }
                    int tam = Convert.ToInt32(yuzde);
                    int virgul = -1;
                    string bYuzde = yuzde.ToString();
                    for (int b = 0; b < bYuzde.Length; b++)
                    {
                        char karakter = Convert.ToChar(bYuzde.Substring(b, 1));
                        if (karakter == ',')
                        {
                            virgul = b;
                        }
                    }

                    if (virgul == -1)
                    {
                        if (katilimcilar.Items.Count == sayi)
                        {
                            if (dilayar.Values["dil"].ToString() == "Türkçe")
                            {
                                adet.Text = katilimcilar.Items.Count + " katılımcı, seçilenin çıkma ihtimali %" + yuzde + " (Tüm katılımcı çıkma ihtimalleri aynı)";

                            }
                            else if (dilayar.Values["dil"].ToString() == "English")
                            {
                                adet.Text = "Participants " + katilimcilar.Items.Count + ", possibility of selected " + yuzde + "% (All participants likely to go the same)";
                            }
                        }
                        else
                        {
                            if (dilayar.Values["dil"].ToString() == "Türkçe")
                            {
                                adet.Text = katilimcilar.Items.Count + " katılımcı, seçilenin çıkma ihtimali %" + yuzde;
                            }
                            else if (dilayar.Values["dil"].ToString() == "English")
                            {
                                adet.Text = "Participants " + katilimcilar.Items.Count + ", possibility of selected " + yuzde + "%";
                            }
                        }
                    }

                    string ilkveri = bYuzde.Substring(0, virgul);
                    string ikinciveri = bYuzde.Substring(virgul + 1, 1);
                    string birles = ilkveri + "," + ikinciveri;

                    if (katilimcilar.Items.Count == sayi)
                    {
                        if (dilayar.Values["dil"].ToString() == "Türkçe")
                        {
                            adet.Text = katilimcilar.Items.Count + " katılımcı, seçilenin çıkma ihtimali %" + birles + " (Tüm katılımcı çıkma ihtimalleri aynı)";

                        }
                        else if (dilayar.Values["dil"].ToString() == "English")
                        {
                            adet.Text = "Participants " + katilimcilar.Items.Count + ", possibility of selected " + birles + "% (All participants likely to go the same)";
                        }
                    }
                    else
                    {
                        if (dilayar.Values["dil"].ToString() == "Türkçe")
                        {
                            adet.Text = katilimcilar.Items.Count + " katılımcı, seçilenin çıkma ihtimali %" + birles;
                        }
                        else if (dilayar.Values["dil"].ToString() == "English")
                        {
                            adet.Text = "Participants " + katilimcilar.Items.Count + ", possibility of selected " + birles + "%";
                        }
                    }
                }
            }
            catch (Exception hata)
            {

            }
        }

        private void hizliRichEditBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {

        }

        private async void textBox1_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                gAktif = true;
                bool esit = false;
                if (textBox1.Text.Trim() != "")
                {
                    customDialog.IsOpen = false;
                    if (gIslem == "Ekle")
                    {
                        katilimcilar.Items.Add(textBox1.Text.Trim());
                        textBox1.Text = "";
                        if (dilayar.Values["dil"].ToString() == "Türkçe")
                        {
                            adet.Text = katilimcilar.Items.Count + " katılımcı, seçilenin çıkma ihtimali %? (Tüm katılımcı çıkma ihtimalleri aynı)";
                        }
                        else if (dilayar.Values["dil"].ToString() == "English")
                        {
                            adet.Text = "Participants " + katilimcilar.Items.Count + ", possibility of selected ?% (All participants likely to go the same)";
                        }

                    }
                    else if (gIslem == "Düzenle")
                    {
                        katilimcilar.Items[gIndex] = textBox1.Text.Trim();
                        textBox1.Text = "";
                    }
                    else if (gIslem == "Kayıt")
                    {
                        textBox1.Text.Replace('|', '-');
                        foreach (string key in value.Values.Keys)
                        {
                            if (textBox1.Text.Trim() == key)
                            {
                                esit = true;
                            }
                        }

                        if (esit == true)
                        {
                            gIsim = textBox1.Text.Trim();
                            if (dilayar.Values["dil"].ToString() == "Türkçe")
                            {
                                ileti.Title = "Soru";
                                ileti.Content = textBox1.Text.Trim() + " adlı kayıt zaten mevcuttur. Üzerine yazmak istediğinize emin misiniz?";
                            }
                            else if (dilayar.Values["dil"].ToString() == "English")
                            {
                                ileti.Title = "Question";
                                ileti.Content = textBox1.Text.Trim() + "'s records are already available. Are you sure you want to overwrite?";
                            }
                            ileti.CancelCommandIndex = 1;
                            if (gGosterildi == false)
                            {
                                if (dilayar.Values["dil"].ToString() == "Türkçe")
                                {
                                    ileti.Commands.Add(new UICommand("Evet", new UICommandInvokedHandler(EvetCommandHandler)));
                                    ileti.Commands.Add(new UICommand("Hayır", new UICommandInvokedHandler(HayirCommandHandler)));
                                }
                                else if (dilayar.Values["dil"].ToString() == "English")
                                {
                                    ileti.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(EvetCommandHandler)));
                                    ileti.Commands.Add(new UICommand("No", new UICommandInvokedHandler(HayirCommandHandler)));
                                }
                                gGosterildi = true;
                                await ileti.ShowAsync();
                            }
                            else
                            {
                                await ileti.ShowAsync();
                            }
                        }
                        else
                        {
                            value.Values[textBox1.Text.Trim()] = gVeriler;
                        }

                        kayilarListBox.Items.Clear();
                        foreach (string veri in value.Values.Keys)
                        {
                            if (veri != "dil")
                            {
                                kayilarListBox.Items.Add(veri);
                            }
                        }
                    }
                }
            }
        }

        private void katilimcilar_KeyUp(object sender, KeyRoutedEventArgs e)
        {

        }

        private void hizliRichEditBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {

            string satir = String.Empty;
            hizliRichEditBox.Document.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out satir);
            katilimcilar.Items.Clear();
            string[] satirlar = satir.Split('\r');
            foreach (string veri in satirlar)
            {
                if (veri.Trim() != "")
                {
                    katilimcilar.Items.Add(veri);
                }
            }
            if (dilayar.Values["dil"].ToString() == "Türkçe")
            {
                adet.Text = katilimcilar.Items.Count + " katılımcı,";
            }
            else if (dilayar.Values["dil"].ToString() == "English")
            {
                adet.Text = "Participants " + katilimcilar.Items.Count + ",";
            }
        }

        private void hizliCheckBox_Toggled(object sender, RoutedEventArgs e)
        {
            string deger = String.Empty;
            string anki = String.Empty;
            if (hizliCheckBox.IsOn == true)
            {
                hizliRichEditBox.Visibility = Windows.UI.Xaml.Visibility.Visible;
                katilimcilar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                if (katilimcilar.Items.Count > 0)
                {
                    string satirlar = "";
                    for (int i = 0; i < katilimcilar.Items.Count; i++)
                    {
                        hizliRichEditBox.Document.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out deger);
                        if (satirlar == "")
                        {
                            satirlar = katilimcilar.Items[i].ToString();
                        }
                        else
                        {
                            satirlar += "\n" + katilimcilar.Items[i].ToString();
                        }
                    }
                    hizliRichEditBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, satirlar);
                }
            }
            else
            {
                hizliRichEditBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                katilimcilar.Visibility = Windows.UI.Xaml.Visibility.Visible;
                hizliRichEditBox.Document.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out deger);
                if (katilimcilar.Items.Count > 0)
                {
                    hizliRichEditBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, "");
                    foreach (string item in katilimcilar.SelectedItems)
                    {
                        hizliRichEditBox.Document.GetText(Windows.UI.Text.TextGetOptions.AdjustCrlf, out anki);
                        if (anki == "")
                        {
                            hizliRichEditBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, item);
                        }
                        else
                        {
                            hizliRichEditBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, anki + "\n" + item);
                        }
                    }
                }
                else
                {
                    hizliRichEditBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, "");
                }
            }
        }

        private void ekleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ekleItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            customDialog.IsOpen = true;
            gIslem = "Ekle";
            if (dilayar.Values["dil"].ToString() == "Türkçe")
            {
                customDialog.Title = "Eklenecek veriyi giriniz:";
                onaylaButton.Content = "Onayla";
                iptalButton.Content = "İptal";
            }
            else if (dilayar.Values["dil"].ToString() == "English")
            {
                customDialog.Title = "Enter will be add data:";
                onaylaButton.Content = "Confirm";
                iptalButton.Content = "Cancel";
            }
        }

        private void gelismisItem_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void menu_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void menu_PointerExited(object sender, PointerRoutedEventArgs e)
        {

        }

        private void acilirMenu_PointerExited(object sender, PointerRoutedEventArgs e)
        {

        }

        private void acilirDugme_Click(object sender, RoutedEventArgs e)
        {

        }

        private void silDugme_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            katilimcilar.Items.Clear();
            hizliRichEditBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, "");
        }

        private void kaydetButton1_Click(object sender, RoutedEventArgs e)
        {
            if (katilimcilar.Items.Count > 0)
            {
                katilimci = "";
                for (int i = 0; i < katilimcilar.Items.Count; i++)
                {
                    if (katilimci != "")
                    {
                        katilimci += "|" + Convert.ToString(katilimcilar.Items[i]);
                    }
                    else
                    {
                        katilimci = Convert.ToString(katilimcilar.Items[i]);
                    }
                }

                textBox1.Text = "";
                customDialog.IsOpen = true;
                if (dilayar.Values["dil"].ToString() == "Türkçe")
                {
                    customDialog.Title = "Kayıt ismini giriniz:";
                    onaylaButton.Content = "Onayla";
                    iptalButton.Content = "İptal";
                }
                else if (dilayar.Values["dil"].ToString() == "English")
                {
                    customDialog.Title = "Enter a name for your records:";
                    onaylaButton.Content = "Confirm";
                    iptalButton.Content = "Cancel";
                }
                gIslem = "Kayıt";
                gVeriler = katilimci;
            }
        }

        private void ekleButton_Click_1(object sender, RoutedEventArgs e)
        {
            customDialog.IsOpen = true;
            gIslem = "Ekle";
            if (dilayar.Values["dil"].ToString() == "Türkçe")
            {
                customDialog.Title = "Eklenecek veriyi giriniz:";
                onaylaButton.Content = "Onayla";
                iptalButton.Content = "İptal";
            }
            else if (dilayar.Values["dil"].ToString() == "English")
            {
                customDialog.Title = "Enter will be add data:";
                onaylaButton.Content = "Confirm";
                iptalButton.Content = "Cancel";
            }
        }

        private void silButton_Click(object sender, RoutedEventArgs e)
        {
            List<int> indises = new List<int>();
            if (katilimcilar.SelectedItems.Count > 0)
            {
                string veriler = "";
                for (int i = 0; i < katilimcilar.SelectedItems.Count; i++)
                {
                    indises.Add(katilimcilar.Items.IndexOf(katilimcilar.SelectedItems[i]));
                }
                indises.Sort();
                indises.Reverse();
                for (int i = 0; i < indises.Count; i++)
                {
                    katilimcilar.Items.RemoveAt(indises[i]);
                }

                if (dilayar.Values["dil"].ToString() == "Türkçe")
                {
                    adet.Text = katilimcilar.Items.Count + " katılımcı, seçilenin çıkma ihtimali %? (Tüm katılımcı çıkma ihtimalleri aynı)";
                }
                else if (dilayar.Values["dil"].ToString() == "English")
                {
                    adet.Text = "Participants " + katilimcilar.Items.Count + ", possibility of selected ?% (All participants likely to go the same)";
                }

                List<string> xVeriler = new List<string>();
                for (int i = 0; i < katilimcilar.Items.Count; i++)
                {
                    xVeriler.Add(katilimcilar.Items[i].ToString());
                }

                string sVeriler = "";

                foreach (string item in xVeriler)
                {
                    if (sVeriler == "")
                    {
                        sVeriler = item;
                    }
                    else
                    {
                        sVeriler += "\n" + item;
                    }
                }
                hizliRichEditBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, sVeriler);
            }
        }

        private void duzenleButton_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = Convert.ToString(katilimcilar.SelectedItem);
            customDialog.IsOpen = true;
            gIslem = "Düzenle";
            if (dilayar.Values["dil"].ToString() == "Türkçe")
            {
                customDialog.Title = "Yeni veriyi giriniz:";
                onaylaButton.Content = "Onayla";
                iptalButton.Content = "İptal";
            }
            else if (dilayar.Values["dil"].ToString() == "English")
            {
                customDialog.Title = "Please enter new data:";
                onaylaButton.Content = "Confirm";
                iptalButton.Content = "Cancel";
            }
            gIndex = katilimcilar.SelectedIndex;
        }

        private void kayitYoneticisiButtonApp_Click(object sender, RoutedEventArgs e)
        {
            if (gAktif == true)
            {
                gAktif = false;
            }
            else
            {
                kayitYonetimi.IsOpen = true;
                if (dilayar.Values["dil"].ToString() == "Türkçe")
                {
                    kayitlarBaslikTextBlock.Text = "Kayıtlar";
                    kaydetButton.Content = "Kaydet";
                    kayitSilButton.Content = "Sil";
                    yukleButton.Content = "Yükle";
                    katilimcilarBaslikTextBlock.Text = "Katılımcılar";
                    kayitYonetimi.Title = "Kayıt Yönetimi";
                }
                else if (dilayar.Values["dil"].ToString() == "English")
                {
                    kayitlarBaslikTextBlock.Text = "Records";
                    kaydetButton.Content = "Save";
                    kayitSilButton.Content = "Delete";
                    yukleButton.Content = "Load";
                    katilimcilarBaslikTextBlock.Text = "Participants";
                    kayitYonetimi.Title = "Records Management";
                }
                if (katilimcilar.Items.Count > 0)
                {
                    suankiListBox.Items.Clear();
                    kaydetButton.IsEnabled = true;
                    for (int i = 0; i < katilimcilar.Items.Count; i++)
                    {
                        suankiListBox.Items.Add(katilimcilar.Items[i]);
                    }
                }
                else
                {
                    kaydetButton.IsEnabled = false;
                }
                kayilarListBox.Items.Clear();
                foreach (string veri in value.Values.Keys)
                {
                    if (veri != "dil")
                    {
                        kayilarListBox.Items.Add(veri);
                    }
                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

            if (dilayar.Values["dil"].ToString() == "Türkçe")
            {
                kayitYoneticisiButtonApp.Label = "Kayıt Yöneticisi";
                ekleButton.Label = "Ekle";
                silButton.Label = "Sil";
                duzenleButton.Label = "Düzenle";
                kaydetButton1.Label = "Kaydet";
                dilAppBarButton.Label = "Dil";
                hakkindaAppBarButton.Label = "Hakkında";
            }
            else if (dilayar.Values["dil"].ToString() == "English")
            {
                kayitYoneticisiButtonApp.Label = "Records Manager";
                ekleButton.Label = "Add";
                silButton.Label = "Remove";
                duzenleButton.Label = "Edit";
                kaydetButton1.Label = "Save";
                uygulamayiKapatAppBarButton.Label = "Exit Application";
                hakkindaAppBarButton.Label = "About";
                dilAppBarButton.Label = "Language";
            }

            if (katilimcilar.SelectedItems.Count == 0)
            {
                silButton.IsEnabled = false;
                duzenleButton.IsEnabled = false;
            }
            else
            {
                silButton.IsEnabled = true;
                duzenleButton.IsEnabled = true;
            }

            if (katilimcilar.Items.Count == 0)
            {
                kaydetButton1.IsEnabled = false;
            }
            else
            {
                kaydetButton1.IsEnabled = true;
            }
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void katilimcilar_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            List<string> veriler = new List<string>();
            for (int i = 0; i < katilimcilar.Items.Count; i++)
            {
                veriler.Add(katilimcilar.Items[i].ToString());
            }

            string sVeriler = "";

            foreach (string item in veriler)
            {
                if (sVeriler == "")
                {
                    sVeriler = item;
                }
                else
                {
                    sVeriler += "\n" + item;
                }
            }
            hizliRichEditBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, sVeriler);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void uygulamayiKapatAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (dilayar.Values["dil"].ToString() == "Türkçe")
            {
                MessageDialog mesaj = new MessageDialog("Uygulamadan çıkmak istediğinize emin misiniz?", "Soru");
                mesaj.Commands.Add(new UICommand("Evet", new UICommandInvokedHandler(evetbutton)));
                mesaj.Commands.Add(new UICommand("Hayır", new UICommandInvokedHandler(hayirbutton)));
                await mesaj.ShowAsync();
            }
            else if (dilayar.Values["dil"].ToString() == "English")
            {
                MessageDialog mesaj = new MessageDialog("Are you sure you want to exit the application?", "Question");
                mesaj.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(evetbutton)));
                mesaj.Commands.Add(new UICommand("No", new UICommandInvokedHandler(hayirbutton)));
                await mesaj.ShowAsync();
            }
        }

        private void hayirbutton(IUICommand command)
        {
            
        }

        private void evetbutton(IUICommand command)
        {
            Application.Current.Exit();
        }

        private void araButton_Click(object sender, RoutedEventArgs e)
        {
            /*string veri = verigirisi.Text.Trim();
            int ara = katilimcilar.Items.IndexOf(katilimcilar.Items.IndexOf(veri));
            indexcikisi.Text = ara.ToString();*/
        }

        private async void onaylaButton_Click_1(object sender, RoutedEventArgs e)
        {
            gAktif = true;
            bool esit = false;
            if (textBox1.Text.Trim() != "")
            {
                customDialog.IsOpen = false;
                if (gIslem == "Ekle")
                {
                    katilimcilar.Items.Add(textBox1.Text.Trim());
                    textBox1.Text = "";
                    if (dilayar.Values["dil"].ToString() == "Türkçe")
                    {
                        adet.Text = katilimcilar.Items.Count + " katılımcı, seçilenin çıkma ihtimali %? (Tüm katılımcı çıkma ihtimalleri aynı)";
                    }
                    else if (dilayar.Values["dil"].ToString() == "English")
                    {
                        adet.Text = "Participants " + katilimcilar.Items.Count + ", possibility of selected ?% (All participants likely to go the same)";
                    }

                }
                else if (gIslem == "Düzenle")
                {
                    katilimcilar.Items[gIndex] = textBox1.Text.Trim();
                    textBox1.Text = "";
                }
                else if (gIslem == "Kayıt")
                {
                    textBox1.Text.Replace('|', '-');
                    foreach (string key in value.Values.Keys)
                    {
                        if (textBox1.Text.Trim() == key)
                        {
                            esit = true;
                        }
                    }

                    if (esit == true)
                    {
                        gIsim = textBox1.Text.Trim();
                        if (dilayar.Values["dil"].ToString() == "Türkçe")
                        {
                            ileti.Title = "Soru";
                            ileti.Content = textBox1.Text.Trim() + " adlı kayıt zaten mevcuttur. Üzerine yazmak istediğinize emin misiniz?";
                        }
                        else if (dilayar.Values["dil"].ToString() == "English")
                        {
                            ileti.Title = "Question";
                            ileti.Content = textBox1.Text.Trim() + "'s records are already available. Are you sure you want to overwrite?";
                        }
                        ileti.CancelCommandIndex = 1;
                        if (gGosterildi == false)
                        {
                            if (dilayar.Values["dil"].ToString() == "Türkçe")
                            {
                                ileti.Commands.Add(new UICommand("Evet", new UICommandInvokedHandler(EvetCommandHandler)));
                                ileti.Commands.Add(new UICommand("Hayır", new UICommandInvokedHandler(HayirCommandHandler)));
                            }
                            else if (dilayar.Values["dil"].ToString() == "English")
                            {
                                ileti.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(EvetCommandHandler)));
                                ileti.Commands.Add(new UICommand("No", new UICommandInvokedHandler(HayirCommandHandler)));
                            }
                            gGosterildi = true;
                            await ileti.ShowAsync();
                        }
                        else
                        {
                            await ileti.ShowAsync();
                        }
                    }
                    else
                    {
                        value.Values[textBox1.Text.Trim()] = gVeriler;
                    }

                    kayilarListBox.Items.Clear();
                    foreach (string veri in value.Values.Keys)
                    {
                        if (veri != "dil")
                        {
                            kayilarListBox.Items.Add(veri);
                        }
                    }
                }
            }
            hizliRichEditBox.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            katilimcilar.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void iptalButton_Click_1(object sender, RoutedEventArgs e)
        {
            customDialog.IsOpen = false;
        }

        private void dilAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            string hakkindatitle = "Hakkında";
            string dilayarititle = "Dil Ayarları";
            MainPage1 main = new MainPage1();
            if (main.dilayar.Values["dil"].ToString() == "Türkçe")
            {
                hakkindatitle = "Hakkında";
                dilayarititle = "Dil Ayarları";
            }
            else if (main.dilayar.Values["dil"].ToString() == "English")
            {
                hakkindatitle = "About";
                dilayarititle = "Language Settings";
            }
            DilAyarlari dilayari = new DilAyarlari();
            dilayari.Show();
        }

        private void hakkindaAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            string hakkindatitle = "Hakkında";
            string dilayarititle = "Dil Ayarları";
            MainPage1 main = new MainPage1();
            if (main.dilayar.Values["dil"].ToString() == "Türkçe")
            {
                hakkindatitle = "Hakkında";
                dilayarititle = "Dil Ayarları";
            }
            else if (main.dilayar.Values["dil"].ToString() == "English")
            {
                hakkindatitle = "About";
                dilayarititle = "Language Settings";
            }
            Hakkinda hakkinda = new Hakkinda();
            hakkinda.Show();
        }
    }
}
