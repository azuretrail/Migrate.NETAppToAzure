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
    public partial class EmployeePortal : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #region "Common Functions"
        private void ProcessFile()
        {
            // Mock code to simulate some processing of the file, most likey save to data storage of some kind
            var result = 0;
            for (int i = 0; i < 10000; i++)
            {
                result++;
            } 
        }
        #endregion

        #region "Local Storage"

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                try
                { 
                    string filePath = ConfigurationManager.AppSettings["LocalFilePath"];
                    string filename = Path.GetFileName(FileUploadControl.FileName) ;
                    FileUploadControl.SaveAs(filePath + filename);

                    ProcessFile();

                    StatusLabel.Text = "Upload status: File uploaded !";
                    StatusLabel.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                    StatusLabel.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                StatusLabel.Text = "You must select a file";
                StatusLabel.ForeColor = System.Drawing.Color.Red;
            }
        }
        #endregion
      
    }
}