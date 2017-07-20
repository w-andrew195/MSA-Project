using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using SnapShop.DataModels;
using System.Net.Http;
using System.Net.Http.Headers;
namespace SnapShop
{
    public class AzureManager
    {
        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<SnapShopData> DataTable;

        private AzureManager()
        {
            this.client = new MobileServiceClient("http://snapshopnz.azurewebsites.net"); //Exception Here
            this.DataTable = this.client.GetTable<SnapShopData>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }

                return instance;
            }
        }
        public async Task<List<SnapShopData>> GetInformation()
        {
            return await this.DataTable.ToListAsync();
        }

        public async Task PostHotDogInformation(SnapShopData DataModel)
        {
            await this.DataTable.InsertAsync(DataModel);
        }

    }
}
