using System;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.File;
using Microsoft.Azure;
using System.Data;
using ExcelDataReader;
using System.IO;

namespace AzureB2CUserFunction.Helpers
{
    public class ExcelReader
    {
        public static DataSet GetExcelBlobData(string filename, string connectionString, string containerName)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // Retrieve reference to a blob named "imex.xlsx".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(filename);

            DataSet ds;

            using (var memoryStream = new MemoryStream())
            {
                //downloads blob's content to a stream
                //blockBlobReference.DownloadToStream(memoryStream);
                blockBlob.DownloadToStreamAsync(memoryStream);

                var excelReader = ExcelReaderFactory.CreateOpenXmlReader(memoryStream);
                ds = excelReader.AsDataSet();
                excelReader.Close();
            }

            return ds;
            // Save blob contents to a file.
            /*using (var fileStream = File.OpenWrite(@"path\myfile"))
            {
                blockBlob.DownloadToStreamAsync(fileStream);
            }*/
        }
    }
}
