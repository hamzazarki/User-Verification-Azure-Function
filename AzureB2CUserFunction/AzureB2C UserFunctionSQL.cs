using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

using AzureB2CUserFunction.Models;
using AzureB2CUserFunction.Helpers;
using System.Configuration;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;

namespace AzureB2CUserFunction
{
    public static class AzureB2C_UserFunctionSQL
    {
        [FunctionName("AzureB2C_UserFunctionSQL")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();         
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            log.LogInformation($" body: {data}");
            string SignInName = data?.SignInName;
            log.LogInformation($"SignInName : {SignInName}");
            string password = data?.password;
            log.LogInformation($"password : {password}");

            var userinfo = new User(SignInName, password)
            {
                SignInName = SignInName ?? data?.SignInName,
               
                password = Encryptor.MD5Hash(password ?? data.password) 


            };


            //Connect to SQL
            //Connection String 
            var cnnString = "Server=tcp:autorelevedb.database.windows.net,1433;Initial Catalog=User_Services_PréProd;Persist Security Info=False;User ID=sqladmin;Password=P@ssw0rd2019;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            
            using (SqlConnection conn = new SqlConnection(cnnString))
            {
                
               

                using (SqlCommand cmd = new SqlCommand("[dbo].UserCredentialsV2", conn))
                {
                    DataTable dataTable = new DataTable();
                    conn.Open();
                    // Execute the stored Procedure -- 
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", userinfo.SignInName);
                    cmd.Parameters.AddWithValue("@UserPassword", userinfo.password);
                    
                    var rows = await cmd.ExecuteNonQueryAsync();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    dataAdapter.Fill(dataTable);
                    

                    log.LogInformation($"{rows} rows were retreived");
                    if (dataTable.Rows.Count>0)
                    {

                        userinfo.UserExist = true;
                       
                    }
                    else
                    {
                        userinfo.UserExist = false;
                       
                    }

                    
                }

            }

            //serialize the object and return JSON reponse and HTTPS Code 
            var serializedUser = JsonConvert.SerializeObject(userinfo, Formatting.Indented);
            log.LogInformation("C# SEND HTTP RESPONSE");
            log.LogInformation($"serializedUser: {serializedUser}");
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(serializedUser, System.Text.Encoding.UTF8, "application/json")
            };
        }
    }
}
