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
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using AzureB2CUserFunction.Helpers;
using JWTDecoder.Algorithms;


namespace AzureB2CUserFunction
{/*
    public static class AzureB2CUserFunctionToken
    {

        
        [FunctionName("AzureB2CUserFunctionToken")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string emailAdress = req.Query["name"];
            string password = req.Query["password"];
           // string token = req.Query["token"];
            string token = "...";
            
            // receive a tupal with all three parts 
            var decodedToken = JWTDecoder.DecodeToken(token);

            JwtHeader header = decodedToken.Header; // contains Algorithm, Type
            string UserExist;
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var handler = new  
           //string output = requestBody.ToString();
            dynamic data = JsonConvert.DeserializeObject<User>(output);
             emailAdress = emailAdress ?? data?.emailAdress;
             password = password ?? data?.password;
            // name = name ?? data?.name;

            var userinfo = new User(emailAdress,password)
            {
                emailAdress = emailAdress ?? data?.emailAdress,
                password = password ?? data?.password

            };

            var serializedUser = JsonConvert.SerializeObject(userinfo);
            /* string responseMessage = string.IsNullOrEmpty()
                 ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                 : $"Hello, {}. This HTTP triggered function executed successfully.";*/

            //return new OkObjectResult(serializedUser);
       /* }
    }
*/}
