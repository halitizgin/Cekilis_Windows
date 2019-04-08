using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Hakkinda : SettingsFlyout
    {
        public Hakkinda()
        {
            this.InitializeComponent();
        }

        private async void LaunchSite(string siteAddress)
        {
            try
            {
                Uri uri = new Uri(siteAddress);
                var launched = await Windows.System.Launcher.LaunchUriAsync(uri);
            }
            catch (Exception ex)
            {
                //handle the exception
            }
        }

        private void SettingsFlyout_Loaded(object sender, RoutedEventArgs e)
        {
            MainPage1 main = new MainPage1();
            if (main.dilayar.Values["dil"].ToString() == "Türkçe")
            {
                cekilisHakkindaBaslik.Text = "Çekiliş Hakkında";
                cekilisBilgiler.Text = "Çekiliş 2.0.0.0\n\nKodlama: Ready&Titan\n\nYazılım & Programlama & Webmaster Forumu ve Örnek Projeler için:\n\nhttp://www.kodevreni.com";
            }
            else if (main.dilayar.Values["dil"].ToString() == "English")
            {
                cekilisHakkindaBaslik.Text = "About Çekiliş";
                cekilisBilgiler.Text = "Çekiliş 2.0.0.0\n\nCoding: Ready&Titan\n\nEnglish Translation: Ready&Titan\n\nSoftware & Programming & Webmaster Forum and for the sample project:\n\nhttp://www.kodevreni.com";
            }
        }

        private void SettingsFlyout_Tapped(object sender, TappedRoutedEventArgs e)
        {
            LaunchSite("http://www.kodevreni.com");
        }
    }
}
