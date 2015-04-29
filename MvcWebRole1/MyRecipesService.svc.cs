using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WebMatrix.WebData;
using MvcWebRole1.Models;

namespace MvcWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyRecipesService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MyRecipesService.svc or MyRecipesService.svc.cs at the Solution Explorer and start debugging.
    public class MyRecipesService : IMyRecipesService
    {

        public string Login(string username, string password)
        {
            CloudStorageAccount obj_Account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("ReceptConnection"));
            CloudTableClient cloud_Table;
            CloudTable table;
            CloudTable table2;



            cloud_Table = obj_Account.CreateCloudTableClient();
            table2 = cloud_Table.GetTableReference("SaveRecipe");
            table = cloud_Table.GetTableReference("Recept");
            CloudBlobContainer blobContainer = GetCloudBlobContainer();
            List<string> blobs = new List<string>();
            List<List<string>> reci = new List<List<string>>();
            TableQuery<SaveRecipe> queryinlogg = new TableQuery<SaveRecipe>().Where(TableQuery.GenerateFilterCondition("Inloggnamn", QueryComparisons.Equal,username));
            var myRec = table2.ExecuteQuery(queryinlogg);
            TableQuery<Recept> queryrecipe = new TableQuery<Recept>();
            var allRec = table.ExecuteQuery(queryrecipe);

            List<string> values = new List<string>();

            foreach (var rec in myRec)
            {
                foreach (var recItem in allRec)
                {

                   
                    if (rec.PartitionKey.Equals(recItem.ReceptNamn))
                    {
                        foreach (var blobItem in blobContainer.ListBlobs())
                        {

                            if (blobItem.Uri.ToString().Contains(recItem.blobnamn))
                            {
                                //List<Recept> ing = ()recItem;
                                foreach (var ingredients in (IEnumerable<Recept>) recItem)
                                {

                                if(ingredients != null){
                                    System.Diagnostics.Debug.WriteLine(ingredients);
                                }
                                }
                                //Tab.Where(TableQuery.GenerateFilterCondition("blobnamn", QueryComparisons.Equals,blobItem.Uri.ToString())));
                                // values.Add(recItem.blobnamn); //0
                                values.Add(rec.PartitionKey);//1
                                //values.Add(recItem.Instruktioner);//2
                                blobs.Add(blobItem.Uri.ToString());
                                //reci.Add(values);
                            }
                        }
                    }
                }
            }
            // reci.ElementAt(0).ElementAt(0);
            

            return "";
        }

        

        public CloudBlobContainer GetCloudBlobContainer()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("ReceptConnection"));
            CloudBlobClient blobclient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobclient.GetContainerReference("myimages");
            if (blobContainer.CreateIfNotExists())
            {
                blobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }
            return blobContainer;
        }
    }
}
