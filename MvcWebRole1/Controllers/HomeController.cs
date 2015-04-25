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



namespace MvcWebRole1.Controllers
{
    public class HomeController : Controller
    {
        BlobStorageServices _blobStorageServices = new BlobStorageServices();

        public ActionResult upLoad(string kategori, string receptnamn, string inloggningsnamn, string ingrediens1, string ingrediensmangd1, string mattenhet1, string antal, string image)
        {
            CloudBlobContainer blobContainer = _blobStorageServices.GetCloudBlobContainer();
            List<string> blobs = new List<string>();
            foreach (var blobItem in blobContainer.ListBlobs())
            {
                blobs.Add(blobItem.Uri.ToString());
            }

            ViewBag.Message = kategori;

            var kat = new List<string>();
            kat.Add("Pannkaka");
            kat.Add("Soppa");
            kat.Add("Glass");
            ViewBag.kategori = new SelectList(kat);

            var in1 = new List<string>();
            in1.Add("Mjölk");
            in1.Add("Salt");
            in1.Add("Ägg");
            ViewBag.ingrediens1 = new SelectList(in1);

            var matt1 = new List<string>();
            matt1.Add("dl");
            matt1.Add("cl");
            matt1.Add("krm");
            matt1.Add("st");
            ViewBag.mattenhet1 = new SelectList(matt1);

            if(!String.IsNullOrEmpty(kategori)){
            /*CloudStorageAccount obj_Account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("ConnectionString"));
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
            obj_Entity.blobnamn = image;
            obj_Entity.PartitionKey = kategori;
            obj_Entity.RowKey = receptnamn;
            insertOperation = TableOperation.Insert(obj_Entity);
            table.Execute(insertOperation);*/
        }
           
            return View(blobs);
        }

        

        [HttpPost]
        public ActionResult upLoad(HttpPostedFileBase image, string antal, string kategori, string receptnamn, string inloggningsnamn, string ingrediens1, string ingrediensmangd1, string mattenhet1)
        {
            string blobName = image.FileName + inloggningsnamn;
            CloudBlobContainer blobContainer = _blobStorageServices.GetCloudBlobContainer();
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(blobName);
            blob.UploadFromStream(image.InputStream);

            CloudStorageAccount obj_Account = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
           //CloudStorageAccount obj_Account = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("ReceptConnection"));
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

            CloudBlobContainer blobContainer = _blobStorageServices.GetCloudBlobContainer();
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(filename);

            blob.Delete();

            return "File Deleted";
        }

       
    }
}
