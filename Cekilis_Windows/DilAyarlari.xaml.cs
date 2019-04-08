using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Settings Flyout item template is documented at http://go.microsoft.com/fwlink/?LinkId=273769

namespace Cekilis_Windows
{
    public sealed partial class DilAyarlari : SettingsFlyout
    {
        public DilAyarlari()
        {
            this.InitializeComponent();
        }

        private void SettingsFlyout_Loaded(object sender, RoutedEventArgs e)
        {
            MainPage1 main = new MainPage1();
            if (main.dilayar.Values["dil"].ToString() == "English")
            {
                dilComboBox.SelectedIndex = 1;
                varsayilanDilTextBlock.Text = "Your language selection:";
                dilAyariKaydetButton.Content = "Save";
                this.Title = "Language Settings";
            }
            else if (main.dilayar.Values["dil"].ToString() == "Türkçe")
            {
                dilComboBox.SelectedIndex = 0;
            }
        }

        async void MessageBoxKill(string metin, string baslik = "")
        {
            MessageDialog mesaj = new MessageDialog(metin, baslik);
            mesaj.Commands.Add(new UICommand("Tamam", kapathandler));
            await mesaj.ShowAsync();
        }

        private void kapathandler(IUICommand command)
        {
            Application.Current.Exit();
        }

        async void MessageBox(string metin, string baslik = "")
        {
            MessageDialog mesaj2 = new MessageDialog(metin, baslik);
            mesaj2.Commands.Add(new UICommand("Tamam", handler));
            await mesaj2.ShowAsync();
        }

        private void handler(IUICommand command)
        {
            
        }

        private void dilAyariKaydetButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)dilComboBox.SelectedItem;
            string value = typeItem.Content.ToString();
            MainPage1 main = new MainPage1();
            main.dilayar.Values["dil"] = value.ToString();
            if (main.dilayar.Values["dil"].ToString() == "Türkçe")
            {
                    
                MessageBoxKill("Dil seçiminiz değiştirilmiştir.", "Bilgi");
            }
            else if (main.dilayar.Values["dil"].ToString() == "English")
            {
                    
                MessageBoxKill("Language choice has changed.", "Information");
            }
        }

        private void SettingsFlyout_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
