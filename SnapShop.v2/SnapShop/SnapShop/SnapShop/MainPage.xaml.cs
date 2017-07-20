using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using SnapShop.Model;
using SnapShop.DataModels;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;

namespace SnapShop
{
    public partial class MainPage : TabbedPage
    {
        MobileServiceClient client = AzureManager.AzureManagerInstance.AzureClient;
        public MainPage()
        {
            InitializeComponent();
            image.Source = ImageSource.FromFile("logo.png");
                    }
        string placeName = "";

        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            List<SnapShopData> GetInfo = await AzureManager.AzureManagerInstance.GetInformation();

            HotDogList.ItemsSource = GetInfo;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {


            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Small,
                Directory = "Sample",
                Name = $"{DateTime.UtcNow}.jpg"
            });

            if (file == null)
                return;

            image.Source = ImageSource.FromStream(() =>
            {
                return file.GetStream();
            });


            await MakePredictionRequest(file);
            await postLocationAsync();
        }

        async Task postLocationAsync()
        {

            DateTime thisDay = DateTime.Today;

            SnapShopData model = new SnapShopData()
            {
                Date = thisDay.ToString("g"),
                Places = placeName

            };

            await AzureManager.AzureManagerInstance.PostHotDogInformation(model);
        }

        static byte[] GetImageAsByteArray(MediaFile file)
        {
            var stream = file.GetStream();
            BinaryReader binaryReader = new BinaryReader(stream);
            return binaryReader.ReadBytes((int)stream.Length);
        }

        async Task MakePredictionRequest(MediaFile file)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Prediction-Key", "60b7c5c219ec4d8395e592511ffab18b");

            string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v1.0/Prediction/1bf4f18a-cbf8-4af8-a5ab-53a5dfd8ad79/image?iterationId=821eab1f-0f81-47b5-964b-662879fba55c";

            HttpResponseMessage response;

            byte[] byteData = GetImageAsByteArray(file);

            using (var content = new ByteArrayContent(byteData))
            {

                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(url, content);


                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    EvaluationModel responseModel = JsonConvert.DeserializeObject<EvaluationModel>(responseString);

                    TagLabel.Text = responseModel.Predictions.ToList()[0].Tag;
                    placeName = responseModel.Predictions.ToList()[0].Tag;
                }

                
                file.Dispose();
            }
        }
    }
}