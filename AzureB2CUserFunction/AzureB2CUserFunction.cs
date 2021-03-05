using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureB2CUserFunction.Models;
using AzureB2CUserFunction.Helpers;
using Microsoft.Azure.Cosmos.Table;
using System.Web.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.File;
using Microsoft.Azure;
using System.Configuration;
using System.Collections.Generic;

using Microsoft.WindowsAzure.Storage.Auth;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
//using static AzureB2CUserFunction.Helpers.TableRetriever;

namespace AzureB2CUserFunction
{
    public static class AzureB2CUserFunction
    {
        private const HttpResponseHeader eTag = HttpResponseHeader.ETag;
        
        [FunctionName("AzureB2CUserFunction")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, [Table("Users")] CloudTable UserTable,
            ILogger log)
        {
            
            log.LogInformation("C# HTTP trigger function processed a request.");


            //string valueToReturn;
            //var customresponse = new HttpRequestMessage();
            //  var codereturntoclient = new ObjectResult("");
            /*string signInName = req.Query["signInName"];
            string password = req.Query["password"]; //"Nelite1234";//req.Query["password"];    */


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = await req.Content.ReadAsAsync<User>();
             dynamic data = JsonConvert.DeserializeObject(requestBody);
            log.LogInformation($" body: {data}");
            string SignInName = data?.SignInName;
            log.LogInformation($"SignInName : {SignInName}");
            string password = data?.password;
            log.LogInformation($"password : {password}");

           /* string SignInName = data?.SignInName;
            string  password = data?.password;*/
            //emailAdress = emailAdress ?? data?.emailAdress;
            //password = password ?? data?.password;
            // string enc = Encryptor.MD5Hash(password);
            var userinfo = new User(SignInName, password)
            {
                SignInName = SignInName ?? data?.SignInName,
            //password =Encryptor.MD5Hash(password ?? data?.password)
            password = Encryptor.MD5Hash(password ?? data.password) //password ?? data?.password
            
            
        };
            /*string messagelog2 = $"user {userinfo.password} exists";
            var serializedUser2 = JsonConvert.SerializeObject(messagelog2);
            return new OkObjectResult(serializedUser2);*/

            string accountName = "oneeappstorage";
            string accountKey = "RdSHMEZ6ODYs8Q7srEiHoFeF3FUFf00qYoLxzCV1DKQ9iQB79QsYYF6l3YnVv0b1OJwhfT99BsAh6B3ApAemUw==";
            var creds = new Microsoft.Azure.Cosmos.Table.StorageCredentials(accountName, accountKey);
            var account = new Microsoft.Azure.Cosmos.Table.CloudStorageAccount(creds, useHttps: true);
            // Retrieve the role assignments table
            var client = account.CreateCloudTableClient();
            var table = client.GetTableReference("Users");
            var entities = table.ExecuteQuery(new TableQuery<UserEntity>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("Email", QueryComparisons.Equal,
                       userinfo.SignInName),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("UserPassword", QueryComparisons.Equal,
                        userinfo.password)))).ToList();
            var queryResult = table.ExecuteQuerySegmentedAsync(new TableQuery<UserEntity>(), null
                ).Result.ToList();
            //string messagelog;
            if (entities.Count()>0)
            {

                userinfo.UserExist = true;
               // codereturntoclient.Value = "User exists";
                //codereturntoclient.StatusCode = StatusCodes.Status200OK;
                //messagelog = $"user {userinfo.emailAdress} exists";
                //return (IActionResult)customresponse.CreateResponse(HttpStatusCode.OK);
                //customresponse.CreateResponse(HttpStatusCode.OK);



            }
            else
            {
                userinfo.UserExist = false;
               // codereturntoclient.Value = "User doesn't exist";
                //codereturntoclient.StatusCode = StatusCodes.Status404NotFound;
                //messagelog = $"User {userinfo.emailAdress} doesn't exist";
                //customresponse.CreateResponse(HttpStatusCode.NotFound);
            }



            /*Microsoft Doc -------------------------------------------------------------------------------
            TableQuery<UserEntity> rangeQuery = new TableQuery<UserEntity>().Where(
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("Email", QueryComparisons.Equal,
                       userinfo.emailAdress),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("UserPassword", QueryComparisons.Equal,
                        userinfo.password)));
            //var ExecutionQuery = UserTable.ExecuteQuerySegmentedAsync(rangeQuery,null);
            var ExecutionQuery = UserTable.ExecuteQuery(rangeQuery);
            UserEntity user = ExecutionQuery as UserEntity;
            var list = ExecutionQuery.ToList();
            
             if(ExecutionQuery.Count()>0)
            {
                foreach (UserEntity entity in
                ExecutionQuery)
                {
                    log.LogInformation(
                       $"{entity.Email}\t{entity.CIN}\t{entity.Timestamp}\t{entity.ID_Partenaire}");

                    

                }
            }
             */

            /*Microsoft Doc -------------------------------------------------------------------------------*/


            //var results = Storage.ValidateUser(userinfo.emailAdress);


            /*TableQuery code  ---------------------------------------------------------------------------------------------------------------


             var tableQuery = new TableQuery<UserEntity>();
             //var query = TableQuery.GenerateFilterCondition(nameof(UserEntity.EMAIL), QueryComparisons.Equal, userinfo.emailAdress);
             //var tableUserQuery = new TableQuery().Where("EMAIL == ?string? && USERPASSWORD == ?string?", userinfo.emailAdress, userinfo.password);

             tableQuery.SelectColumns = new List<string> { nameof(UserEntity.EMAIL)};
             tableQuery.FilterString = TableQuery.GenerateFilterCondition(nameof(UserEntity.EMAIL), QueryComparisons.Equal,userinfo.emailAdress);

              //var result = UserTable.ExecuteQuerySegmentedAsync(tableQuery, null);
             var res = UserTable.ExecuteQuery(tableQuery).ToList();


             /*TableQuery code  ---------------------------------------------------------------------------------------------------------------*/

            //  return new OkObjectResult(res);

            //var res = UserTable.ExecuteQuery(tableQuery);


            //string Uservalidation = Storage.ValidateUserV2(userinfo.emailAdress);

            /*------------------------------------------------------------------------------------------------------------
            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            string sourceContainerName = ConfigurationManager.AppSettings["azure-webjobs-hosts"];
            string sourceBlobFileName = "Users.xlsx";
            _ = new GetExcelBlob();
            _ = GetExcelBlob.GetExcelBlobData(connectionString, sourceBlobFileName, sourceContainerName); */


            /*static async Task<IEnumerable<T>> GetAll<T>(string tableName) where T : class
           {

               var table = this.GetCloudTable(tableName);
               TableContinuationToken token = null;
               do
               {
                   var q = new TableQuery<T>();
                   var queryResult = await table.ExecuteQuerySegmentedAsync(q, token);
                   foreach (var item in queryResult.Results)
                   {
                       yield return item;
                   }
                   token = queryResult.ContinuationToken;
               }
               while (token != null);
           }*/
            /*Azure Storage --------------------------------------------------------------------------------------




            string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            string sourceContainerName = ConfigurationManager.AppSettings["azure-webjobs-hosts"];
            string sourceBlobFileName = "Users.xlsx";
            _ = new ExcelReader();
            _ = ExcelReader.GetExcelBlobData(sourceBlobFileName, connectionString, sourceContainerName);



            string connectionString = CloudConfigurationManager.GetSetting("StorageConnectionString"); //blob connection string
            string sourceContainerName = ConfigurationManager.AppSettings["sourcecontainerName"]; //source blob container name
            string sourceBlobFileName = "test.xlsx"; //source blob name
            _ = new ExcelReader();
            _ = ExcelReader.GetExcelBlobData(sourceBlobFileName, connectionString, sourceContainerName);

            /*Azure Storage ------------------------------*/
             var serializedUser = JsonConvert.SerializeObject(userinfo, Formatting.Indented);

            /* string responseMessage = string.IsNullOrEmpty(serializedUser)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {serializedUser}. This HTTP triggered function executed successfully.";*/
            //return codereturntoclient;
            //return customresponse.
           // return new ObjectResult(serializedUser);
            log.LogInformation("C# SEND HTTP RESPONSE");
            log.LogInformation($"serializedUser: {serializedUser}");
           //return new ObjectResult(serializedUser);
            //log.LogInformation("C# END HTTP REQUEST");
             return new HttpResponseMessage(HttpStatusCode.OK)
             {
                 Content = new StringContent(serializedUser,Encoding.UTF8,"application/json")
             };


        }
    }
}
