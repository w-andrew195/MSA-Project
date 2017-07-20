using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tabs.DataModels;

namespace Tabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AzureTable : ContentPage
    {
        public AzureTable()
        {
            InitializeComponent();
        }

        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            List<SnapShopNZInformation> notHotDogInformation = await AzureManager.AzureManagerInstance.GetHotDogInformation();

            HotDogList.ItemsSource = notHotDogInformation;
        }


    }
}