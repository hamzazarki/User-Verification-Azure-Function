using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage;

namespace AzureB2CUserFunction.Helpers
{/*
    class TableRetriever
    {
        public class MyTableEntity : TableEntity
        {
            public string emailAdress { get; set; }
            public string password { get; set; }
        }

        private CloudTable mytable = null;

        public  AzConnection()
        {
            var storageAccount = CloudStorageAccount.Parse("your azure storage key goes here..");
            var cloudTableClient = storageAccount.CreateCloudTableClient();
            mytable = cloudTableClient.GetTableReference("Users");
            mytable.CreateIfNotExistsAsync();
        }

        public IEnumerable<MyTableEntity> GetAll()
        {
            var query = new TableQuery<MyTableEntity>();
            var entties = mytable.ExecuteQuerySegmentedAsync(query);
            return entties;
        }

        public void CreateOrUpdate(MyTableEntity myTableOperation)
        {
            var operation = TableOperation.InsertOrReplace(myTableOperation);
            mytable.ExecuteAsync(operation);
        }
        public void Delete(MyTableEntity myTableOperation)
        {
            var operation = TableOperation.Delete(myTableOperation);
            mytable.ExecuteAsync(operation);
        }


        public MyTableEntity Get(string partitionKey, string RowId)
        {
            var operation = TableOperation.Retrieve<MyTableEntity>(partitionKey, RowId);
            var result = mytable.ExecuteAsync(operation);
            return result.Result as MyTableEntity;
        }
    }
        */
}
