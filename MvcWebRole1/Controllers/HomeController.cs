using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using MvcWebRole1.Models;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.ServiceRuntime;



namespace MvcWebRole1.Controllers
{
    public class HomeController : Controller
    {

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

        public ActionResult upLoad(string kategori, string receptnamn, string inloggningsnamn, string ingrediens1, string ingrediensmangd1, string mattenhet1, string antal, string image)
        {
            CloudBlobContainer blobContainer = GetCloudBlobContainer();
            List<string> blobs = new List<string>();
            foreach (var blobItem in blobContainer.ListBlobs())
            {
                blobs.Add(blobItem.Uri.ToString());
            }

            ViewBag.Message = kategori;

            category();

            var in1 = new List<string>();
            in1.Add("Mjölk");
            in1.Add("Salt");
            in1.Add("Svartpeppar");
            in1.Add("Curry");
            in1.Add("Ägg");
            in1.Add("Smör");
            in1.Add("Vetemjöl");
            in1.Add("Rågmjöl");
            in1.Add("Vetemjöl Special");
            in1.Add("Brödsirap");
            in1.Add("Vispgrädde");
            in1.Add("Choklad");
            in1.Add("Hallon");
            in1.Add("Matyogurt");
            in1.Add("Strösocker");
            in1.Add("Wokgrönskaer");
            in1.Add("Ris");
            in1.Add("Potatis");
            in1.Add("Kycklingfilé");
            in1.Add("Hönsbuljong");
            in1.Add("Matlagningsgrädde");
            in1.Add("Creme fraiche");
            in1.Add("Jäst");
            in1.Add("Honung");
            in1.Add("Vatten");
            in1.Add("Gräslök");
            in1.Add("Persilja");
            in1.Add("Krossade tomater");
            in1.Add("Olivolja");
            in1.Add("Valmofrön");
            in1.Add("Pasta");
            in1.Add("Ost");
            in1.Add("Ströbröd");
            in1.Add("Lammkotletter");
            in1.Add("Lammfärs");
            in1.Add("Rödlök");
            in1.Add("Gul lök");
            in1.Add("Vitlök");
            in1.Add("Rosmarin");
            in1.Add("Citron");
            in1.Add("Rödvin");
            in1.Add("Vitt vin");
            in1.Add("Schalottenlök");
            in1.Add("Oxfile");
            in1.Add("Lax");
            in1.Add("Spenat");
            in1.Add("Championer");
            in1.Add("Kantareller");
            ViewBag.ingrediens1 = new SelectList(in1);

            var matt1 = new List<string>();
            matt1.Add("dl");
            matt1.Add("cl");
            matt1.Add("krm");
            matt1.Add("tsk");
            matt1.Add("st");
            ViewBag.mattenhet1 = new SelectList(matt1);

            if(!String.IsNullOrEmpty(kategori)){

        }
           
            return View(blobs);
        }

        public void category()
        {
            var kat = new List<string>();
            kat.Add("Pannkakor");
            kat.Add("Soppor");
            kat.Add("Surdegsbröd");
            kat.Add("Kyckling");
            kat.Add("Bröd");
            kat.Add("Lamm");
            kat.Add("Oxfile");
            kat.Add("Lax");
            kat.Add("Glass");
            ViewBag.kategori = new SelectList(kat);
        }

        [HttpPost]
        public ActionResult upLoad(HttpPostedFileBase image, string antal, string kategori, string receptnamn, string inloggningsnamn, string ingrediens1, string ingrediensmangd1, string mattenhet1)
        {
            string blobName = image.FileName + inloggningsnamn;
            CloudBlobContainer blobContainer = GetCloudBlobContainer();
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(blobName);
            blob.UploadFromStream(image.InputStream);

           // CloudStorageAccount obj_Account = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            CloudStorageAccount obj_Account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("ReceptConnection"));
            CloudTableClient cloud_Table;
            CloudTable table;
            Recept obj_Entity = new Recept();
            TableOperation insertOperation;

            cloud_Table = obj_Account.CreateCloudTableClient();
            table = cloud_Table.GetTableReference("Recept");
            table.CreateIfNotExists();

            obj_Entity.ReceptNamn = receptnamn;
            obj_Entity.Inloggnamn = inloggningsnamn;
            if (ingrediens1.Equals("Mjölk"))
            {
                obj_Entity.mjolk = ingrediensmangd1;
                obj_Entity.mjolkmatt = mattenhet1;
            }
            else if (ingrediens1.Equals("Salt"))
            {
                obj_Entity.salt = ingrediensmangd1;
                obj_Entity.saltmatt = mattenhet1;
            }
            else if (ingrediens1.Equals("Ägg"))
            {
                obj_Entity.agg = ingrediensmangd1;
                obj_Entity.aggmatt = mattenhet1;
            }

            obj_Entity.antal = int.Parse(antal);
            obj_Entity.blobnamn = blobName;
            obj_Entity.PartitionKey = kategori;
            obj_Entity.RowKey = receptnamn;
            insertOperation = TableOperation.Insert(obj_Entity);
            table.Execute(insertOperation);

            
            return RedirectToAction("upLoad");
        }

        [HttpPost]
        public string DeleteImage(string name)
        {
            Uri uri = new Uri(name);
            string filename = System.IO.Path.GetFileName(uri.LocalPath);

            CloudBlobContainer blobContainer = GetCloudBlobContainer();
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(filename);

            blob.Delete();

            return "File Deleted";
        }

        public ActionResult Search(string category){
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
             CloudConfigurationManager.GetSetting("ReceptConnection"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Recept");
            TableQuery<Recept> query = new TableQuery<Recept>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, category));

            CloudBlobContainer blobContainer = GetCloudBlobContainer();
            List<string> blobs = new List<string>();

            ViewBag.Recipedata = table.ExecuteQuery(query);
           // foreach (Recept entity in table.ExecuteQuery(query))
           // {
            //    Console.WriteLine("{0}, {1}\t{2}\t{3}", entity.PartitionKey, entity.RowKey,
              //      entity, entity.PhoneNumber);
           // }
            foreach (var blobItem in blobContainer.ListBlobs())
            {
                blobs.Add(blobItem.Uri.ToString());
            }
            ViewBag.Blobs = blobs;
            return View();
        }

        public ActionResult DisplayRecipe(string RecipeName){
            ViewBag.In = RecipeName;


            return View();
        }

       
    }
}
