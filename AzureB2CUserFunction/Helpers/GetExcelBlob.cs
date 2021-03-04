using System;
using System.Data;
using System.IO;
using ExcelDataReader;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureB2CUserFunction.Helpers
{
    public class GetExcelBlob
    {
         public static DataSet GetExcelBlobData(string connectionString ,string filename, string containerName)
        {

            //--------enable me ---///////////
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // Retrieve reference to a blob named "test.xlsx"
            CloudBlockBlob blockBlobReference = container.GetBlockBlobReference(filename);

            DataSet ds;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);



            using (MemoryStream memoryStream = new MemoryStream())
            {
                //downloads blob's content to a stream
                blockBlobReference.DownloadToStreamAsync(memoryStream).Wait();
                var excelReader = ExcelReaderFactory.CreateOpenXmlReader(memoryStream);
                //var excelReader = ExcelReaderFactory.CreateBinaryReader
                ds = excelReader.AsDataSet();
                excelReader.Close();
            }

            return ds;



        }


    }
}
