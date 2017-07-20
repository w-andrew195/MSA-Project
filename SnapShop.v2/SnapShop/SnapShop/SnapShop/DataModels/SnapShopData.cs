using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace SnapShop.DataModels
{
    public class SnapShopData
    {
        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "Date")]
        public float Date { get; set; }

        [JsonProperty(PropertyName = "Time")]
        public float Time { get; set; }

        [JsonProperty(PropertyName = "Places")]
        public float Places { get; set; }

    }
}
