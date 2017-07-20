using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;
namespace SnapShop.DataModels
{
    public class SnapShopData
    {
        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "Date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "Places")]
        public string Places { get; set; }

    }
}
