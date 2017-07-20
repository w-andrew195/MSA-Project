using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Tabs.DataModels;

namespace Tabs
{
   public class AzureManager
    {
        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<SnapShopNZInformation> DataTable;

        private AzureManager()
        {
            this.client = new MobileServiceClient("http://snapshopnz.azurewebsites.net");
            this.DataTable = this.client.GetTable<SnapShopNZInformation>();
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

        public async Task<List<SnapShopNZInformation>> GetHotDogInformation()
        {
            return await this.DataTable.ToListAsync();
        }

        public async Task PostHotDogInformation(SnapShopNZInformation SnapShopModel)
        {
            await this.DataTable.InsertAsync(SnapShopModel);
        }

    }
}
