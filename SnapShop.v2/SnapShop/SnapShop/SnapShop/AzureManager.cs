using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using SnapShop.DataModels;
namespace SnapShop
{
    public class AzureManager
    {
        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<SnapShopData> DataTable;

        private AzureManager()
        {
            this.client = new MobileServiceClient("http://snapshopnz.azurewebsites.net");
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
    }
}
