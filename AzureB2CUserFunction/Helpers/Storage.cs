using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AzureB2CUserFunction.Helpers
{
    public class Storage
    {
        private static CloudTable AuthTable()
        {
            string accountName = "oneeappstorage";
            string accountKey = "RdSHMEZ6ODYs8Q7srEiHoFeF3FUFf00qYoLxzCV1DKQ9iQB79QsYYF6l3YnVv0b1OJwhfT99BsAh6B3ApAemUw==";
            try
            {
                StorageCredentials creds = new StorageCredentials(accountName, accountKey);
                CloudStorageAccount account = new CloudStorageAccount(creds, useHttps: true);

                CloudTableClient users = account.CreateCloudTableClient();

                CloudTable table = users.GetTableReference("Users");

                return table;
            }
            catch
            {
                return null;
            }
        }

        private static bool DoesUsernameExist(string emailAdress, CloudTable table)
        {
            TableOperation entity = TableOperation.Retrieve("EMAIL",emailAdress);

            var result = table.ExecuteAsync(entity);

            
            if (result.Result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


       public  static async Task<TableResult> GetAllMessages(CloudTable table, String emailAdress)
        {

            TableResult x = await table.ExecuteAsync(TableOperation.Retrieve("EMAIL",emailAdress));
            return x;
        }

        public static string ValidateUserV2(string emailAdress)
        {
            var table = AuthTable();

            var exists = GetAllMessages(table, emailAdress);

            if (exists !=null)
            {
                return "User exists " + emailAdress;
            }
            else
            {

                return "User deosn't exist";
            }



        }

        public static string ValidateUser(string emailAdress)
        {
            var table = AuthTable();

            var exists = DoesUsernameExist(emailAdress, table);

            if (exists)
            {
                return "User exists "+ emailAdress;
            }
            else
            {

                return "User deosn't exist";
            }

            

        }

    }
}
