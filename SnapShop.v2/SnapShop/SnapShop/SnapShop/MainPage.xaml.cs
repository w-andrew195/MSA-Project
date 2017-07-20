using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Diagnostics;
using System.IO;
//using System.Net.Http;
//using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SnapShop
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            image.Source = ImageSource.FromFile("logo.png");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}
