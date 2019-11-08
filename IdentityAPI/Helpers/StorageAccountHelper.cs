using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Helpers
{
    public class StorageAccountHelper
    {
        private string storageConnectonString;
        private CloudStorageAccount storageAccount;
        private CloudQueueClient queueClient;
                        
        public string StorageConnectonString
        {
            get { return storageConnectonString; }
            set
            {
                this.storageConnectonString = value;
                storageAccount = CloudStorageAccount.Parse(this.storageConnectonString);
            }
        }

        public async Task SendMessageAsync(string messageText, string queueName)
        {
            queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference(queueName);
            await queue.CreateIfNotExistsAsync();
            CloudQueueMessage queueMessage = new CloudQueueMessage(messageText);
            await queue.AddMessageAsync(queueMessage);
        }
    }
}
