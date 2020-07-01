using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagement
{
    public partial class EmployeePortalAzureStorage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #region "Common Functions"
        private void ProcessFile()
        {
            // Mock code to simulate processing of the file
            var result = 0;
            for (int i = 0; i < 10000; i++)
            {
                result++;
            } 
        }
        #endregion
             
     
        #region "Azure Storage"
        
        protected void fileUploadAzure_Click(object sender, EventArgs e)
        {
            if (fileUploadAzure.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(fileUploadAzure.FileName);
                    SaveFileToAzureStorage(fileUploadAzure.PostedFile, filename);

                    ProcessFile();
                    StatusLabelAzure.Text = "Upload status: File uploaded and processed.";
                    StatusLabelAzure.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    StatusLabelAzure.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                    StatusLabelAzure.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                StatusLabelAzure.Text = "You must select a file";
                StatusLabelAzure.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void SaveFileToAzureStorage(HttpPostedFile file, string filename)
        {
            // add nuget package Microsoft.Azure.Storage.Blob
            // https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet
            try
            {
                var storageAccName = ConfigurationManager.AppSettings["AzureStorageAccName"];
                var storageAccKey = ConfigurationManager.AppSettings["AzureStorageKey"];
                var containerName = ConfigurationManager.AppSettings["AzureContainerName"];
                var connectionString = String.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1};EndpointSuffix=core.windows.net", storageAccName, storageAccKey);

                // Create a BlobServiceClient object which will be used to create a container client
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

                // Create the container and return a container client object
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                containerClient.CreateIfNotExists(); 

                // Get a reference to a blob 
                BlobClient blobClient = containerClient.GetBlobClient(filename);              
                blobClient.Upload(file.InputStream);
                 
            }
            catch (Exception ex)
            {
                StatusLabelAzure.Text = ex.Message;
                StatusLabelAzure.ForeColor = System.Drawing.Color.Red;
            }
           

        }


        #endregion
    }
}