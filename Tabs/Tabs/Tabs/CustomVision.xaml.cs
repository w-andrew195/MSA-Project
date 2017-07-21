using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Tabs.Model;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;
using Tabs.DataModels;
using Plugin.Geolocator;
using Android.Net;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Util;
namespace Tabs
{
    public partial class CustomVision : ContentPage
    {
        public CustomVision()
        {
            InitializeComponent();
            image.Source = ImageSource.FromFile("logo.png");
        }
        string placeName = "";
        


        private async void loadCamera(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "No camera available.", "OK");
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

            SnapShopNZInformation model = new SnapShopNZInformation()
            {
                Date = thisDay.ToString("d"),
                Place = placeName

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

            string url = "https://southcentralus.api.cognitive.microsoft.com/customvision/v1.0/Prediction/1bf4f18a-cbf8-4af8-a5ab-53a5dfd8ad79/image?iterationId=4ee881d3-3750-4966-98a9-c143d1f3c1eb";

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

                    
                    placeName = responseModel.Predictions.ToList()[0].Tag;
                    TagLabel.Text = responseModel.Predictions.ToList()[0].Tag;
                    DataEntry DataOut = new DataEntry();
                    string website = DataOut.checker(placeName);
                    DataOutLabel.Text = website; 
                    //DataOutLabel.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnLabelClicked()));

                }

                file.Dispose();
            }
        }
    }
}