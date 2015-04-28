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
            in1.Add("Wokgrönsaker");
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
            ViewBag.ingrediens2 = new SelectList(in1);
            ViewBag.ingrediens3 = new SelectList(in1);
            ViewBag.ingrediens4 = new SelectList(in1);
            ViewBag.ingrediens5 = new SelectList(in1);
            ViewBag.ingrediens6 = new SelectList(in1);
            ViewBag.ingrediens7 = new SelectList(in1);

            var matt1 = new List<string>();
            matt1.Add("dl");
            matt1.Add("cl");
            matt1.Add("krm");
            matt1.Add("tsk");
            matt1.Add("st");
            ViewBag.mattenhet1 = new SelectList(matt1);
            ViewBag.mattenhet2 = new SelectList(matt1);
            ViewBag.mattenhet3 = new SelectList(matt1);
            ViewBag.mattenhet4 = new SelectList(matt1);
            ViewBag.mattenhet5 = new SelectList(matt1);
            ViewBag.mattenhet6 = new SelectList(matt1);
            ViewBag.mattenhet7 = new SelectList(matt1);

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
        public ActionResult upLoad(HttpPostedFileBase image, string instruktioner, string antal, string kategori, string receptnamn, string ingrediens1, string ingrediensmangd1, string mattenhet1
            , string ingrediens2, string ingrediensmangd2, string mattenhet2, string ingrediens3, string ingrediensmangd3, string mattenhet3
            , string ingrediens4, string ingrediensmangd4, string mattenhet4, string ingrediens5, string ingrediensmangd5, string mattenhet5
            , string ingrediens6, string ingrediensmangd6, string mattenhet6, string ingrediens7, string ingrediensmangd7, string mattenhet7)
        {
            string blobName = Guid.NewGuid().ToString();
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
            obj_Entity.Inloggnamn = User.Identity.Name;

            /*------------------Ingrediens 1-------------------*/
            if (ingrediens1.Equals("Mjölk")){obj_Entity.mjolk = ingrediensmangd1;obj_Entity.mjolkmatt = mattenhet1;}
            else if (ingrediens1.Equals("Salt")){obj_Entity.salt = ingrediensmangd1;obj_Entity.saltmatt = mattenhet1;}
            else if (ingrediens1.Equals("Ägg")){obj_Entity.agg = ingrediensmangd1;obj_Entity.aggmatt = mattenhet1;}
            else if (ingrediens1.Equals("Svartpeppar")){obj_Entity.svartpeppar = ingrediensmangd1;obj_Entity.svartpepparmatt = mattenhet1;}
            else if (ingrediens1.Equals("Curry")){obj_Entity.curry = ingrediensmangd1;obj_Entity.currymatt = mattenhet1;}
            else if (ingrediens1.Equals("Smör")){obj_Entity.smor = ingrediensmangd1;obj_Entity.smormatt = mattenhet1;}
            else if (ingrediens1.Equals("Vetemjöl")){obj_Entity.vetemjol = ingrediensmangd1;obj_Entity.vetemjolmatt = mattenhet1;}
            else if (ingrediens1.Equals("Rågmjöl")){obj_Entity.rogmjol = ingrediensmangd1;obj_Entity.rogmjolmatt = mattenhet1;}
            else if (ingrediens1.Equals("Vetemjöl Special")){obj_Entity.vetemjolspecial = ingrediensmangd1;obj_Entity.vetemjolspecialmatt = mattenhet1;}
            else if (ingrediens1.Equals("Brödsirap")){obj_Entity.brodsirap = ingrediensmangd1;obj_Entity.brodsirapmatt = mattenhet1;}
            else if (ingrediens1.Equals("Vispgrädde")){obj_Entity.vispgradde = ingrediensmangd1;obj_Entity.vispgraddematt = mattenhet1;}
            else if (ingrediens1.Equals("Choklad")){obj_Entity.choklad = ingrediensmangd1;obj_Entity.chokladmatt = mattenhet1;}
            else if (ingrediens1.Equals("Hallon")){obj_Entity.hallon = ingrediensmangd1;obj_Entity.hallonmatt = mattenhet1;}
            else if (ingrediens1.Equals("Matyogurt")){obj_Entity.matyogurt = ingrediensmangd1;obj_Entity.matyogurtmatt = mattenhet1;}
            else if (ingrediens1.Equals("Strösocker")){obj_Entity.strosocker = ingrediensmangd1;obj_Entity.strosockermatt = mattenhet1;}
            else if (ingrediens1.Equals("Wokgrönsaker")){obj_Entity.wokgronsaker = ingrediensmangd1;obj_Entity.wokgronsakermatt = mattenhet1;}
            else if (ingrediens1.Equals("Ris")){obj_Entity.ris = ingrediensmangd1;obj_Entity.rismatt = mattenhet1;}
            else if (ingrediens1.Equals("Potatis")){obj_Entity.potatis = ingrediensmangd1;obj_Entity.potatismatt = mattenhet1;}
            else if (ingrediens1.Equals("Kycklingfile")){obj_Entity.kycklingfile = ingrediensmangd1;obj_Entity.kycklingfilematt = mattenhet1;}
            else if (ingrediens1.Equals("Hönsbuljong")) { obj_Entity.honsbuljong = ingrediensmangd1; obj_Entity.honsbuljongmatt = mattenhet1; }
            else if (ingrediens1.Equals("Matlagningsgrädde")){obj_Entity.matlagningsgradde = ingrediensmangd1;obj_Entity.matlagningsgraddematt = mattenhet1;}
            else if (ingrediens1.Equals("Creme fraiche")){obj_Entity.cremefraiche = ingrediensmangd1;obj_Entity.cremefraichematt = mattenhet1;}
            else if (ingrediens1.Equals("Jäst")){obj_Entity.jast = ingrediensmangd1;obj_Entity.jastmatt = mattenhet1;}
            else if (ingrediens1.Equals("Honung")){obj_Entity.honung = ingrediensmangd1;obj_Entity.honungmatt = mattenhet1;}
            else if (ingrediens1.Equals("Vatten")){obj_Entity.vatten = ingrediensmangd1;obj_Entity.vattenmatt = mattenhet1;}
            else if (ingrediens1.Equals("Gräslök")){obj_Entity.graslok = ingrediensmangd1;obj_Entity.graslokmatt = mattenhet1;}
            else if (ingrediens1.Equals("Persilja")){obj_Entity.persilja = ingrediensmangd1;obj_Entity.persiljamatt = mattenhet1;}
            else if (ingrediens1.Equals("Krossade tomater")){obj_Entity.krossadetomater = ingrediensmangd1;obj_Entity.krossadetomatermatt = mattenhet1;}
            else if (ingrediens1.Equals("Olivolja")){obj_Entity.olivolja = ingrediensmangd1;obj_Entity.olivoljamatt = mattenhet1;}
            else if (ingrediens1.Equals("Valmofrön")){obj_Entity.valmofron = ingrediensmangd1;obj_Entity.valmofronmatt = mattenhet1;}
            else if (ingrediens1.Equals("Pasta")){obj_Entity.pasta = ingrediensmangd1;obj_Entity.pastamatt = mattenhet1;}
            else if (ingrediens1.Equals("Ost")){obj_Entity.ost = ingrediensmangd1;obj_Entity.ostmatt = mattenhet1;}
            else if (ingrediens1.Equals("Ströbröd")){obj_Entity.strobrod = ingrediensmangd1;obj_Entity.strobrodmatt = mattenhet1;}
            else if (ingrediens1.Equals("Lammkotletter")){obj_Entity.lammkotletter = ingrediensmangd1;obj_Entity.lammkotlettermatt = mattenhet1;}
            else if (ingrediens1.Equals("Lammfärs")){obj_Entity.lammfars = ingrediensmangd1;obj_Entity.lammfarsmatt = mattenhet1;}
            else if (ingrediens1.Equals("Rödlök")){obj_Entity.rodlok = ingrediensmangd1;obj_Entity.rodlokmatt = mattenhet1;}
            else if (ingrediens1.Equals("Gul lök")){obj_Entity.gullok = ingrediensmangd1;obj_Entity.gullokmatt = mattenhet1;}
            else if (ingrediens1.Equals("Vitlök")){obj_Entity.vitlok = ingrediensmangd1;obj_Entity.vitlokmatt = mattenhet1;}
            else if (ingrediens1.Equals("Rosmarin")){obj_Entity.rosmarin = ingrediensmangd1;obj_Entity.rosmarinmatt = mattenhet1;}
            else if (ingrediens1.Equals("Citron")){obj_Entity.citron = ingrediensmangd1;obj_Entity.citronmatt = mattenhet1;}
            else if (ingrediens1.Equals("Rödvin")){obj_Entity.rodvin = ingrediensmangd1;obj_Entity.rodvinmatt = mattenhet1;}
            else if (ingrediens1.Equals("Vitt vin")){obj_Entity.vittvin = ingrediensmangd1;obj_Entity.vittvinmatt = mattenhet1;}
            else if (ingrediens1.Equals("Schalottenlök")){obj_Entity.schalottenlok = ingrediensmangd1;obj_Entity.schalottenlokmatt = mattenhet1;}
            else if (ingrediens1.Equals("Oxfile")){obj_Entity.oxfile = ingrediensmangd1;obj_Entity.oxfilematt = mattenhet1;}
            else if (ingrediens1.Equals("Lax")){obj_Entity.lax = ingrediensmangd1;obj_Entity.laxmatt = mattenhet1;}
            else if (ingrediens1.Equals("Spenat")){obj_Entity.spenat = ingrediensmangd1;obj_Entity.spenatmatt = mattenhet1;}
            else if (ingrediens1.Equals("Champinjoner")){obj_Entity.champinjoner = ingrediensmangd1;obj_Entity.champinjonermatt = mattenhet1;}
            else if (ingrediens1.Equals("Kantareller")){obj_Entity.kantareller = ingrediensmangd1;obj_Entity.kantarellermatt = mattenhet1;}

            /*---------------Ingrediens 2---------------*/
            if (ingrediens2.Equals("Mjölk")) { obj_Entity.mjolk = ingrediensmangd2; obj_Entity.mjolkmatt = mattenhet2; }
            else if (ingrediens2.Equals("Salt")) { obj_Entity.salt = ingrediensmangd2; obj_Entity.saltmatt = mattenhet2; }
            else if (ingrediens2.Equals("Ägg")) { obj_Entity.agg = ingrediensmangd2; obj_Entity.aggmatt = mattenhet2; }
            else if (ingrediens2.Equals("Svartpeppar")) { obj_Entity.svartpeppar = ingrediensmangd2; obj_Entity.svartpepparmatt = mattenhet2; }
            else if (ingrediens2.Equals("Curry")) { obj_Entity.curry = ingrediensmangd2; obj_Entity.currymatt = mattenhet2; }
            else if (ingrediens2.Equals("Smör")) { obj_Entity.smor = ingrediensmangd2; obj_Entity.smormatt = mattenhet2; }
            else if (ingrediens2.Equals("Vetemjöl")) { obj_Entity.vetemjol = ingrediensmangd2; obj_Entity.vetemjolmatt = mattenhet2; }
            else if (ingrediens2.Equals("Rågmjöl")) { obj_Entity.rogmjol = ingrediensmangd2; obj_Entity.rogmjolmatt = mattenhet2; }
            else if (ingrediens2.Equals("Vetemjöl Special")) { obj_Entity.vetemjolspecial = ingrediensmangd2; obj_Entity.vetemjolspecialmatt = mattenhet2; }
            else if (ingrediens2.Equals("Brödsirap")) { obj_Entity.brodsirap = ingrediensmangd2; obj_Entity.brodsirapmatt = mattenhet2; }
            else if (ingrediens2.Equals("Vispgrädde")) { obj_Entity.vispgradde = ingrediensmangd2; obj_Entity.vispgraddematt = mattenhet2; }
            else if (ingrediens2.Equals("Choklad")) { obj_Entity.choklad = ingrediensmangd2; obj_Entity.chokladmatt = mattenhet2; }
            else if (ingrediens2.Equals("Hallon")) { obj_Entity.hallon = ingrediensmangd2; obj_Entity.hallonmatt = mattenhet2; }
            else if (ingrediens2.Equals("Matyogurt")) { obj_Entity.matyogurt = ingrediensmangd2; obj_Entity.matyogurtmatt = mattenhet2; }
            else if (ingrediens2.Equals("Strösocker")) { obj_Entity.strosocker = ingrediensmangd2; obj_Entity.strosockermatt = mattenhet2; }
            else if (ingrediens2.Equals("Wokgrönsaker")) { obj_Entity.wokgronsaker = ingrediensmangd2; obj_Entity.wokgronsakermatt = mattenhet2; }
            else if (ingrediens2.Equals("Ris")) { obj_Entity.ris = ingrediensmangd2; obj_Entity.rismatt = mattenhet2; }
            else if (ingrediens2.Equals("Potatis")) { obj_Entity.potatis = ingrediensmangd2; obj_Entity.potatismatt = mattenhet2; }
            else if (ingrediens2.Equals("Kycklingfile")) { obj_Entity.kycklingfile = ingrediensmangd2; obj_Entity.kycklingfilematt = mattenhet2; }
            else if (ingrediens2.Equals("Hönsbuljong")) { obj_Entity.honsbuljong = ingrediensmangd2; obj_Entity.honsbuljongmatt = mattenhet2; }
            else if (ingrediens2.Equals("Matlagningsgrädde")) { obj_Entity.matlagningsgradde = ingrediensmangd2; obj_Entity.matlagningsgraddematt = mattenhet2; }
            else if (ingrediens2.Equals("Creme fraiche")) { obj_Entity.cremefraiche = ingrediensmangd2; obj_Entity.cremefraichematt = mattenhet2; }
            else if (ingrediens2.Equals("Jäst")) { obj_Entity.jast = ingrediensmangd2; obj_Entity.jastmatt = mattenhet2; }
            else if (ingrediens2.Equals("Honung")) { obj_Entity.honung = ingrediensmangd2; obj_Entity.honungmatt = mattenhet2; }
            else if (ingrediens2.Equals("Vatten")) { obj_Entity.vatten = ingrediensmangd2; obj_Entity.vattenmatt = mattenhet2; }
            else if (ingrediens2.Equals("Gräslök")) { obj_Entity.graslok = ingrediensmangd2; obj_Entity.graslokmatt = mattenhet2; }
            else if (ingrediens2.Equals("Persilja")) { obj_Entity.persilja = ingrediensmangd2; obj_Entity.persiljamatt = mattenhet2; }
            else if (ingrediens2.Equals("Krossade tomater")) { obj_Entity.krossadetomater = ingrediensmangd2; obj_Entity.krossadetomatermatt = mattenhet2; }
            else if (ingrediens2.Equals("Olivolja")) { obj_Entity.olivolja = ingrediensmangd2; obj_Entity.olivoljamatt = mattenhet2; }
            else if (ingrediens2.Equals("Valmofrön")) { obj_Entity.valmofron = ingrediensmangd2; obj_Entity.valmofronmatt = mattenhet2; }
            else if (ingrediens2.Equals("Pasta")) { obj_Entity.pasta = ingrediensmangd2; obj_Entity.pastamatt = mattenhet2; }
            else if (ingrediens2.Equals("Ost")) { obj_Entity.ost = ingrediensmangd2; obj_Entity.ostmatt = mattenhet2; }
            else if (ingrediens2.Equals("Ströbröd")) { obj_Entity.strobrod = ingrediensmangd2; obj_Entity.strobrodmatt = mattenhet2; }
            else if (ingrediens2.Equals("Lammkotletter")) { obj_Entity.lammkotletter = ingrediensmangd2; obj_Entity.lammkotlettermatt = mattenhet2; }
            else if (ingrediens2.Equals("Lammfärs")) { obj_Entity.lammfars = ingrediensmangd2; obj_Entity.lammfarsmatt = mattenhet2; }
            else if (ingrediens2.Equals("Rödlök")) { obj_Entity.rodlok = ingrediensmangd2; obj_Entity.rodlokmatt = mattenhet2; }
            else if (ingrediens2.Equals("Gul lök")) { obj_Entity.gullok = ingrediensmangd2; obj_Entity.gullokmatt = mattenhet2; }
            else if (ingrediens2.Equals("Vitlök")) { obj_Entity.vitlok = ingrediensmangd2; obj_Entity.vitlokmatt = mattenhet2; }
            else if (ingrediens2.Equals("Rosmarin")) { obj_Entity.rosmarin = ingrediensmangd2; obj_Entity.rosmarinmatt = mattenhet2; }
            else if (ingrediens2.Equals("Citron")) { obj_Entity.citron = ingrediensmangd2; obj_Entity.citronmatt = mattenhet2; }
            else if (ingrediens2.Equals("Rödvin")) { obj_Entity.rodvin = ingrediensmangd2; obj_Entity.rodvinmatt = mattenhet2; }
            else if (ingrediens2.Equals("Vitt vin")) { obj_Entity.vittvin = ingrediensmangd2; obj_Entity.vittvinmatt = mattenhet2; }
            else if (ingrediens2.Equals("Schalottenlök")) { obj_Entity.schalottenlok = ingrediensmangd2; obj_Entity.schalottenlokmatt = mattenhet2; }
            else if (ingrediens2.Equals("Oxfile")) { obj_Entity.oxfile = ingrediensmangd2; obj_Entity.oxfilematt = mattenhet2; }
            else if (ingrediens2.Equals("Lax")) { obj_Entity.lax = ingrediensmangd2; obj_Entity.laxmatt = mattenhet2; }
            else if (ingrediens2.Equals("Spenat")) { obj_Entity.spenat = ingrediensmangd2; obj_Entity.spenatmatt = mattenhet2; }
            else if (ingrediens2.Equals("Champinjoner")) { obj_Entity.champinjoner = ingrediensmangd2; obj_Entity.champinjonermatt = mattenhet2; }
            else if (ingrediens2.Equals("Kantareller")) { obj_Entity.kantareller = ingrediensmangd2; obj_Entity.kantarellermatt = mattenhet2; }

            /*----------------------Ingrediens 3------------------*/
            if (ingrediens3.Equals("Mjölk")) { obj_Entity.mjolk = ingrediensmangd3; obj_Entity.mjolkmatt = mattenhet3; }
            else if (ingrediens3.Equals("Salt")) { obj_Entity.salt = ingrediensmangd3; obj_Entity.saltmatt = mattenhet3; }
            else if (ingrediens3.Equals("Ägg")) { obj_Entity.agg = ingrediensmangd3; obj_Entity.aggmatt = mattenhet3; }
            else if (ingrediens3.Equals("Svartpeppar")) { obj_Entity.svartpeppar = ingrediensmangd3; obj_Entity.svartpepparmatt = mattenhet3; }
            else if (ingrediens3.Equals("Curry")) { obj_Entity.curry = ingrediensmangd3; obj_Entity.currymatt = mattenhet3; }
            else if (ingrediens3.Equals("Smör")) { obj_Entity.smor = ingrediensmangd3; obj_Entity.smormatt = mattenhet3; }
            else if (ingrediens3.Equals("Vetemjöl")) { obj_Entity.vetemjol = ingrediensmangd3; obj_Entity.vetemjolmatt = mattenhet3; }
            else if (ingrediens3.Equals("Rågmjöl")) { obj_Entity.rogmjol = ingrediensmangd3; obj_Entity.rogmjolmatt = mattenhet3; }
            else if (ingrediens3.Equals("Vetemjöl Special")) { obj_Entity.vetemjolspecial = ingrediensmangd3; obj_Entity.vetemjolspecialmatt = mattenhet3; }
            else if (ingrediens3.Equals("Brödsirap")) { obj_Entity.brodsirap = ingrediensmangd3; obj_Entity.brodsirapmatt = mattenhet3; }
            else if (ingrediens3.Equals("Vispgrädde")) { obj_Entity.vispgradde = ingrediensmangd3; obj_Entity.vispgraddematt = mattenhet3; }
            else if (ingrediens3.Equals("Choklad")) { obj_Entity.choklad = ingrediensmangd3; obj_Entity.chokladmatt = mattenhet3; }
            else if (ingrediens3.Equals("Hallon")) { obj_Entity.hallon = ingrediensmangd3; obj_Entity.hallonmatt = mattenhet3; }
            else if (ingrediens3.Equals("Matyogurt")) { obj_Entity.matyogurt = ingrediensmangd3; obj_Entity.matyogurtmatt = mattenhet3; }
            else if (ingrediens3.Equals("Strösocker")) { obj_Entity.strosocker = ingrediensmangd3; obj_Entity.strosockermatt = mattenhet3; }
            else if (ingrediens3.Equals("Wokgrönsaker")) { obj_Entity.wokgronsaker = ingrediensmangd3; obj_Entity.wokgronsakermatt = mattenhet3; }
            else if (ingrediens3.Equals("Ris")) { obj_Entity.ris = ingrediensmangd3; obj_Entity.rismatt = mattenhet3; }
            else if (ingrediens3.Equals("Potatis")) { obj_Entity.potatis = ingrediensmangd3; obj_Entity.potatismatt = mattenhet3; }
            else if (ingrediens3.Equals("Kycklingfile")) { obj_Entity.kycklingfile = ingrediensmangd3; obj_Entity.kycklingfilematt = mattenhet3; }
            else if (ingrediens3.Equals("Hönsbuljong")) { obj_Entity.honsbuljong = ingrediensmangd3; obj_Entity.honsbuljongmatt = mattenhet3; }
            else if (ingrediens3.Equals("Matlagningsgrädde")) { obj_Entity.matlagningsgradde = ingrediensmangd3; obj_Entity.matlagningsgraddematt = mattenhet3; }
            else if (ingrediens3.Equals("Creme fraiche")) { obj_Entity.cremefraiche = ingrediensmangd3; obj_Entity.cremefraichematt = mattenhet3; }
            else if (ingrediens3.Equals("Jäst")) { obj_Entity.jast = ingrediensmangd3; obj_Entity.jastmatt = mattenhet3; }
            else if (ingrediens3.Equals("Honung")) { obj_Entity.honung = ingrediensmangd3; obj_Entity.honungmatt = mattenhet3; }
            else if (ingrediens3.Equals("Vatten")) { obj_Entity.vatten = ingrediensmangd3; obj_Entity.vattenmatt = mattenhet3; }
            else if (ingrediens3.Equals("Gräslök")) { obj_Entity.graslok = ingrediensmangd3; obj_Entity.graslokmatt = mattenhet3; }
            else if (ingrediens3.Equals("Persilja")) { obj_Entity.persilja = ingrediensmangd3; obj_Entity.persiljamatt = mattenhet3; }
            else if (ingrediens3.Equals("Krossade tomater")) { obj_Entity.krossadetomater = ingrediensmangd3; obj_Entity.krossadetomatermatt = mattenhet3; }
            else if (ingrediens3.Equals("Olivolja")) { obj_Entity.olivolja = ingrediensmangd3; obj_Entity.olivoljamatt = mattenhet3; }
            else if (ingrediens3.Equals("Valmofrön")) { obj_Entity.valmofron = ingrediensmangd3; obj_Entity.valmofronmatt = mattenhet3; }
            else if (ingrediens3.Equals("Pasta")) { obj_Entity.pasta = ingrediensmangd3; obj_Entity.pastamatt = mattenhet3; }
            else if (ingrediens3.Equals("Ost")) { obj_Entity.ost = ingrediensmangd3; obj_Entity.ostmatt = mattenhet3; }
            else if (ingrediens3.Equals("Ströbröd")) { obj_Entity.strobrod = ingrediensmangd3; obj_Entity.strobrodmatt = mattenhet3; }
            else if (ingrediens3.Equals("Lammkotletter")) { obj_Entity.lammkotletter = ingrediensmangd3; obj_Entity.lammkotlettermatt = mattenhet3; }
            else if (ingrediens3.Equals("Lammfärs")) { obj_Entity.lammfars = ingrediensmangd3; obj_Entity.lammfarsmatt = mattenhet3; }
            else if (ingrediens3.Equals("Rödlök")) { obj_Entity.rodlok = ingrediensmangd3; obj_Entity.rodlokmatt = mattenhet3; }
            else if (ingrediens3.Equals("Gul lök")) { obj_Entity.gullok = ingrediensmangd3; obj_Entity.gullokmatt = mattenhet3; }
            else if (ingrediens3.Equals("Vitlök")) { obj_Entity.vitlok = ingrediensmangd3; obj_Entity.vitlokmatt = mattenhet3; }
            else if (ingrediens3.Equals("Rosmarin")) { obj_Entity.rosmarin = ingrediensmangd3; obj_Entity.rosmarinmatt = mattenhet3; }
            else if (ingrediens3.Equals("Citron")) { obj_Entity.citron = ingrediensmangd3; obj_Entity.citronmatt = mattenhet3; }
            else if (ingrediens3.Equals("Rödvin")) { obj_Entity.rodvin = ingrediensmangd3; obj_Entity.rodvinmatt = mattenhet3; }
            else if (ingrediens3.Equals("Vitt vin")) { obj_Entity.vittvin = ingrediensmangd3; obj_Entity.vittvinmatt = mattenhet3; }
            else if (ingrediens3.Equals("Schalottenlök")) { obj_Entity.schalottenlok = ingrediensmangd3; obj_Entity.schalottenlokmatt = mattenhet3; }
            else if (ingrediens3.Equals("Oxfile")) { obj_Entity.oxfile = ingrediensmangd3; obj_Entity.oxfilematt = mattenhet3; }
            else if (ingrediens3.Equals("Lax")) { obj_Entity.lax = ingrediensmangd3; obj_Entity.laxmatt = mattenhet3; }
            else if (ingrediens3.Equals("Spenat")) { obj_Entity.spenat = ingrediensmangd3; obj_Entity.spenatmatt = mattenhet3; }
            else if (ingrediens3.Equals("Champinjoner")) { obj_Entity.champinjoner = ingrediensmangd3; obj_Entity.champinjonermatt = mattenhet3; }
            else if (ingrediens3.Equals("Kantareller")) { obj_Entity.kantareller = ingrediensmangd3; obj_Entity.kantarellermatt = mattenhet3; }

            /*------------------Ingrediens 4---------------*/
            if (ingrediens4.Equals("Mjölk")) { obj_Entity.mjolk = ingrediensmangd4; obj_Entity.mjolkmatt = mattenhet4; }
            else if (ingrediens4.Equals("Salt")) { obj_Entity.salt = ingrediensmangd4; obj_Entity.saltmatt = mattenhet4; }
            else if (ingrediens4.Equals("Ägg")) { obj_Entity.agg = ingrediensmangd4; obj_Entity.aggmatt = mattenhet4; }
            else if (ingrediens4.Equals("Svartpeppar")) { obj_Entity.svartpeppar = ingrediensmangd4; obj_Entity.svartpepparmatt = mattenhet4; }
            else if (ingrediens4.Equals("Curry")) { obj_Entity.curry = ingrediensmangd4; obj_Entity.currymatt = mattenhet4; }
            else if (ingrediens4.Equals("Smör")) { obj_Entity.smor = ingrediensmangd4; obj_Entity.smormatt = mattenhet4; }
            else if (ingrediens4.Equals("Vetemjöl")) { obj_Entity.vetemjol = ingrediensmangd4; obj_Entity.vetemjolmatt = mattenhet4; }
            else if (ingrediens4.Equals("Rågmjöl")) { obj_Entity.rogmjol = ingrediensmangd4; obj_Entity.rogmjolmatt = mattenhet4; }
            else if (ingrediens4.Equals("Vetemjöl Special")) { obj_Entity.vetemjolspecial = ingrediensmangd4; obj_Entity.vetemjolspecialmatt = mattenhet4; }
            else if (ingrediens4.Equals("Brödsirap")) { obj_Entity.brodsirap = ingrediensmangd4; obj_Entity.brodsirapmatt = mattenhet4; }
            else if (ingrediens4.Equals("Vispgrädde")) { obj_Entity.vispgradde = ingrediensmangd4; obj_Entity.vispgraddematt = mattenhet4; }
            else if (ingrediens4.Equals("Choklad")) { obj_Entity.choklad = ingrediensmangd4; obj_Entity.chokladmatt = mattenhet4; }
            else if (ingrediens4.Equals("Hallon")) { obj_Entity.hallon = ingrediensmangd4; obj_Entity.hallonmatt = mattenhet4; }
            else if (ingrediens4.Equals("Matyogurt")) { obj_Entity.matyogurt = ingrediensmangd4; obj_Entity.matyogurtmatt = mattenhet4; }
            else if (ingrediens4.Equals("Strösocker")) { obj_Entity.strosocker = ingrediensmangd4; obj_Entity.strosockermatt = mattenhet4; }
            else if (ingrediens4.Equals("Wokgrönsaker")) { obj_Entity.wokgronsaker = ingrediensmangd4; obj_Entity.wokgronsakermatt = mattenhet4; }
            else if (ingrediens4.Equals("Ris")) { obj_Entity.ris = ingrediensmangd4; obj_Entity.rismatt = mattenhet4; }
            else if (ingrediens4.Equals("Potatis")) { obj_Entity.potatis = ingrediensmangd4; obj_Entity.potatismatt = mattenhet4; }
            else if (ingrediens4.Equals("Kycklingfile")) { obj_Entity.kycklingfile = ingrediensmangd4; obj_Entity.kycklingfilematt = mattenhet4; }
            else if (ingrediens4.Equals("Hönsbuljong")) { obj_Entity.honsbuljong = ingrediensmangd4; obj_Entity.honsbuljongmatt = mattenhet4; }
            else if (ingrediens4.Equals("Matlagningsgrädde")) { obj_Entity.matlagningsgradde = ingrediensmangd4; obj_Entity.matlagningsgraddematt = mattenhet4; }
            else if (ingrediens4.Equals("Creme fraiche")) { obj_Entity.cremefraiche = ingrediensmangd4; obj_Entity.cremefraichematt = mattenhet4; }
            else if (ingrediens4.Equals("Jäst")) { obj_Entity.jast = ingrediensmangd4; obj_Entity.jastmatt = mattenhet4; }
            else if (ingrediens4.Equals("Honung")) { obj_Entity.honung = ingrediensmangd4; obj_Entity.honungmatt = mattenhet4; }
            else if (ingrediens4.Equals("Vatten")) { obj_Entity.vatten = ingrediensmangd4; obj_Entity.vattenmatt = mattenhet4; }
            else if (ingrediens4.Equals("Gräslök")) { obj_Entity.graslok = ingrediensmangd4; obj_Entity.graslokmatt = mattenhet4; }
            else if (ingrediens4.Equals("Persilja")) { obj_Entity.persilja = ingrediensmangd4; obj_Entity.persiljamatt = mattenhet4; }
            else if (ingrediens4.Equals("Krossade tomater")) { obj_Entity.krossadetomater = ingrediensmangd4; obj_Entity.krossadetomatermatt = mattenhet4; }
            else if (ingrediens4.Equals("Olivolja")) { obj_Entity.olivolja = ingrediensmangd4; obj_Entity.olivoljamatt = mattenhet4; }
            else if (ingrediens4.Equals("Valmofrön")) { obj_Entity.valmofron = ingrediensmangd4; obj_Entity.valmofronmatt = mattenhet4; }
            else if (ingrediens4.Equals("Pasta")) { obj_Entity.pasta = ingrediensmangd4; obj_Entity.pastamatt = mattenhet4; }
            else if (ingrediens4.Equals("Ost")) { obj_Entity.ost = ingrediensmangd4; obj_Entity.ostmatt = mattenhet4; }
            else if (ingrediens4.Equals("Ströbröd")) { obj_Entity.strobrod = ingrediensmangd4; obj_Entity.strobrodmatt = mattenhet4; }
            else if (ingrediens4.Equals("Lammkotletter")) { obj_Entity.lammkotletter = ingrediensmangd4; obj_Entity.lammkotlettermatt = mattenhet4; }
            else if (ingrediens4.Equals("Lammfärs")) { obj_Entity.lammfars = ingrediensmangd4; obj_Entity.lammfarsmatt = mattenhet4; }
            else if (ingrediens4.Equals("Rödlök")) { obj_Entity.rodlok = ingrediensmangd4; obj_Entity.rodlokmatt = mattenhet4; }
            else if (ingrediens4.Equals("Gul lök")) { obj_Entity.gullok = ingrediensmangd4; obj_Entity.gullokmatt = mattenhet4; }
            else if (ingrediens4.Equals("Vitlök")) { obj_Entity.vitlok = ingrediensmangd4; obj_Entity.vitlokmatt = mattenhet4; }
            else if (ingrediens4.Equals("Rosmarin")) { obj_Entity.rosmarin = ingrediensmangd4; obj_Entity.rosmarinmatt = mattenhet4; }
            else if (ingrediens4.Equals("Citron")) { obj_Entity.citron = ingrediensmangd4; obj_Entity.citronmatt = mattenhet4; }
            else if (ingrediens4.Equals("Rödvin")) { obj_Entity.rodvin = ingrediensmangd4; obj_Entity.rodvinmatt = mattenhet4; }
            else if (ingrediens4.Equals("Vitt vin")) { obj_Entity.vittvin = ingrediensmangd4; obj_Entity.vittvinmatt = mattenhet4; }
            else if (ingrediens4.Equals("Schalottenlök")) { obj_Entity.schalottenlok = ingrediensmangd4; obj_Entity.schalottenlokmatt = mattenhet4; }
            else if (ingrediens4.Equals("Oxfile")) { obj_Entity.oxfile = ingrediensmangd4; obj_Entity.oxfilematt = mattenhet4; }
            else if (ingrediens4.Equals("Lax")) { obj_Entity.lax = ingrediensmangd4; obj_Entity.laxmatt = mattenhet4; }
            else if (ingrediens4.Equals("Spenat")) { obj_Entity.spenat = ingrediensmangd4; obj_Entity.spenatmatt = mattenhet4; }
            else if (ingrediens4.Equals("Champinjoner")) { obj_Entity.champinjoner = ingrediensmangd4; obj_Entity.champinjonermatt = mattenhet4; }
            else if (ingrediens4.Equals("Kantareller")) { obj_Entity.kantareller = ingrediensmangd4; obj_Entity.kantarellermatt = mattenhet4; }

            /*----------Ingrediens 5-------------------*/

            if (ingrediens5.Equals("Mjölk")) { obj_Entity.mjolk = ingrediensmangd5; obj_Entity.mjolkmatt = mattenhet5; }
            else if (ingrediens5.Equals("Salt")) { obj_Entity.salt = ingrediensmangd5; obj_Entity.saltmatt = mattenhet5; }
            else if (ingrediens5.Equals("Ägg")) { obj_Entity.agg = ingrediensmangd5; obj_Entity.aggmatt = mattenhet5; }
            else if (ingrediens5.Equals("Svartpeppar")) { obj_Entity.svartpeppar = ingrediensmangd5; obj_Entity.svartpepparmatt = mattenhet5; }
            else if (ingrediens5.Equals("Curry")) { obj_Entity.curry = ingrediensmangd5; obj_Entity.currymatt = mattenhet5; }
            else if (ingrediens5.Equals("Smör")) { obj_Entity.smor = ingrediensmangd5; obj_Entity.smormatt = mattenhet5; }
            else if (ingrediens5.Equals("Vetemjöl")) { obj_Entity.vetemjol = ingrediensmangd5; obj_Entity.vetemjolmatt = mattenhet5; }
            else if (ingrediens5.Equals("Rågmjöl")) { obj_Entity.rogmjol = ingrediensmangd5; obj_Entity.rogmjolmatt = mattenhet5; }
            else if (ingrediens5.Equals("Vetemjöl Special")) { obj_Entity.vetemjolspecial = ingrediensmangd5; obj_Entity.vetemjolspecialmatt = mattenhet5; }
            else if (ingrediens5.Equals("Brödsirap")) { obj_Entity.brodsirap = ingrediensmangd5; obj_Entity.brodsirapmatt = mattenhet5; }
            else if (ingrediens5.Equals("Vispgrädde")) { obj_Entity.vispgradde = ingrediensmangd5; obj_Entity.vispgraddematt = mattenhet5; }
            else if (ingrediens5.Equals("Choklad")) { obj_Entity.choklad = ingrediensmangd5; obj_Entity.chokladmatt = mattenhet5; }
            else if (ingrediens5.Equals("Hallon")) { obj_Entity.hallon = ingrediensmangd5; obj_Entity.hallonmatt = mattenhet5; }
            else if (ingrediens5.Equals("Matyogurt")) { obj_Entity.matyogurt = ingrediensmangd5; obj_Entity.matyogurtmatt = mattenhet5; }
            else if (ingrediens5.Equals("Strösocker")) { obj_Entity.strosocker = ingrediensmangd5; obj_Entity.strosockermatt = mattenhet5; }
            else if (ingrediens5.Equals("Wokgrönsaker")) { obj_Entity.wokgronsaker = ingrediensmangd5; obj_Entity.wokgronsakermatt = mattenhet5; }
            else if (ingrediens5.Equals("Ris")) { obj_Entity.ris = ingrediensmangd5; obj_Entity.rismatt = mattenhet5; }
            else if (ingrediens5.Equals("Potatis")) { obj_Entity.potatis = ingrediensmangd5; obj_Entity.potatismatt = mattenhet5; }
            else if (ingrediens5.Equals("Kycklingfile")) { obj_Entity.kycklingfile = ingrediensmangd5; obj_Entity.kycklingfilematt = mattenhet5; }
            else if (ingrediens5.Equals("Hönsbuljong")) { obj_Entity.honsbuljong = ingrediensmangd5; obj_Entity.honsbuljongmatt = mattenhet5; }
            else if (ingrediens5.Equals("Matlagningsgrädde")) { obj_Entity.matlagningsgradde = ingrediensmangd5; obj_Entity.matlagningsgraddematt = mattenhet5; }
            else if (ingrediens5.Equals("Creme fraiche")) { obj_Entity.cremefraiche = ingrediensmangd5; obj_Entity.cremefraichematt = mattenhet5; }
            else if (ingrediens5.Equals("Jäst")) { obj_Entity.jast = ingrediensmangd5; obj_Entity.jastmatt = mattenhet5; }
            else if (ingrediens5.Equals("Honung")) { obj_Entity.honung = ingrediensmangd5; obj_Entity.honungmatt = mattenhet5; }
            else if (ingrediens5.Equals("Vatten")) { obj_Entity.vatten = ingrediensmangd5; obj_Entity.vattenmatt = mattenhet5; }
            else if (ingrediens5.Equals("Gräslök")) { obj_Entity.graslok = ingrediensmangd5; obj_Entity.graslokmatt = mattenhet5; }
            else if (ingrediens5.Equals("Persilja")) { obj_Entity.persilja = ingrediensmangd5; obj_Entity.persiljamatt = mattenhet5; }
            else if (ingrediens5.Equals("Krossade tomater")) { obj_Entity.krossadetomater = ingrediensmangd5; obj_Entity.krossadetomatermatt = mattenhet5; }
            else if (ingrediens5.Equals("Olivolja")) { obj_Entity.olivolja = ingrediensmangd5; obj_Entity.olivoljamatt = mattenhet5; }
            else if (ingrediens5.Equals("Valmofrön")) { obj_Entity.valmofron = ingrediensmangd5; obj_Entity.valmofronmatt = mattenhet5; }
            else if (ingrediens5.Equals("Pasta")) { obj_Entity.pasta = ingrediensmangd5; obj_Entity.pastamatt = mattenhet5; }
            else if (ingrediens5.Equals("Ost")) { obj_Entity.ost = ingrediensmangd5; obj_Entity.ostmatt = mattenhet5; }
            else if (ingrediens5.Equals("Ströbröd")) { obj_Entity.strobrod = ingrediensmangd5; obj_Entity.strobrodmatt = mattenhet5; }
            else if (ingrediens5.Equals("Lammkotletter")) { obj_Entity.lammkotletter = ingrediensmangd5; obj_Entity.lammkotlettermatt = mattenhet5; }
            else if (ingrediens5.Equals("Lammfärs")) { obj_Entity.lammfars = ingrediensmangd5; obj_Entity.lammfarsmatt = mattenhet5; }
            else if (ingrediens5.Equals("Rödlök")) { obj_Entity.rodlok = ingrediensmangd5; obj_Entity.rodlokmatt = mattenhet5; }
            else if (ingrediens5.Equals("Gul lök")) { obj_Entity.gullok = ingrediensmangd5; obj_Entity.gullokmatt = mattenhet5; }
            else if (ingrediens5.Equals("Vitlök")) { obj_Entity.vitlok = ingrediensmangd5; obj_Entity.vitlokmatt = mattenhet5; }
            else if (ingrediens5.Equals("Rosmarin")) { obj_Entity.rosmarin = ingrediensmangd5; obj_Entity.rosmarinmatt = mattenhet5; }
            else if (ingrediens5.Equals("Citron")) { obj_Entity.citron = ingrediensmangd5; obj_Entity.citronmatt = mattenhet5; }
            else if (ingrediens5.Equals("Rödvin")) { obj_Entity.rodvin = ingrediensmangd5; obj_Entity.rodvinmatt = mattenhet5; }
            else if (ingrediens5.Equals("Vitt vin")) { obj_Entity.vittvin = ingrediensmangd5; obj_Entity.vittvinmatt = mattenhet5; }
            else if (ingrediens5.Equals("Schalottenlök")) { obj_Entity.schalottenlok = ingrediensmangd5; obj_Entity.schalottenlokmatt = mattenhet5; }
            else if (ingrediens5.Equals("Oxfile")) { obj_Entity.oxfile = ingrediensmangd5; obj_Entity.oxfilematt = mattenhet5; }
            else if (ingrediens5.Equals("Lax")) { obj_Entity.lax = ingrediensmangd5; obj_Entity.laxmatt = mattenhet5; }
            else if (ingrediens5.Equals("Spenat")) { obj_Entity.spenat = ingrediensmangd5; obj_Entity.spenatmatt = mattenhet5; }
            else if (ingrediens5.Equals("Champinjoner")) { obj_Entity.champinjoner = ingrediensmangd5; obj_Entity.champinjonermatt = mattenhet5; }
            else if (ingrediens5.Equals("Kantareller")) { obj_Entity.kantareller = ingrediensmangd5; obj_Entity.kantarellermatt = mattenhet5; }

            /*------------Ingrediens 6--------------*/

            if (ingrediens6.Equals("Mjölk")) { obj_Entity.mjolk = ingrediensmangd6; obj_Entity.mjolkmatt = mattenhet6; }
            else if (ingrediens6.Equals("Salt")) { obj_Entity.salt = ingrediensmangd6; obj_Entity.saltmatt = mattenhet6; }
            else if (ingrediens6.Equals("Ägg")) { obj_Entity.agg = ingrediensmangd6; obj_Entity.aggmatt = mattenhet6; }
            else if (ingrediens6.Equals("Svartpeppar")) { obj_Entity.svartpeppar = ingrediensmangd6; obj_Entity.svartpepparmatt = mattenhet6; }
            else if (ingrediens6.Equals("Curry")) { obj_Entity.curry = ingrediensmangd6; obj_Entity.currymatt = mattenhet6; }
            else if (ingrediens6.Equals("Smör")) { obj_Entity.smor = ingrediensmangd6; obj_Entity.smormatt = mattenhet6; }
            else if (ingrediens6.Equals("Vetemjöl")) { obj_Entity.vetemjol = ingrediensmangd6; obj_Entity.vetemjolmatt = mattenhet6; }
            else if (ingrediens6.Equals("Rågmjöl")) { obj_Entity.rogmjol = ingrediensmangd6; obj_Entity.rogmjolmatt = mattenhet6; }
            else if (ingrediens6.Equals("Vetemjöl Special")) { obj_Entity.vetemjolspecial = ingrediensmangd6; obj_Entity.vetemjolspecialmatt = mattenhet6; }
            else if (ingrediens6.Equals("Brödsirap")) { obj_Entity.brodsirap = ingrediensmangd6; obj_Entity.brodsirapmatt = mattenhet6; }
            else if (ingrediens6.Equals("Vispgrädde")) { obj_Entity.vispgradde = ingrediensmangd6; obj_Entity.vispgraddematt = mattenhet6; }
            else if (ingrediens6.Equals("Choklad")) { obj_Entity.choklad = ingrediensmangd6; obj_Entity.chokladmatt = mattenhet6; }
            else if (ingrediens6.Equals("Hallon")) { obj_Entity.hallon = ingrediensmangd6; obj_Entity.hallonmatt = mattenhet6; }
            else if (ingrediens6.Equals("Matyogurt")) { obj_Entity.matyogurt = ingrediensmangd6; obj_Entity.matyogurtmatt = mattenhet6; }
            else if (ingrediens6.Equals("Strösocker")) { obj_Entity.strosocker = ingrediensmangd6; obj_Entity.strosockermatt = mattenhet6; }
            else if (ingrediens6.Equals("Wokgrönsaker")) { obj_Entity.wokgronsaker = ingrediensmangd6; obj_Entity.wokgronsakermatt = mattenhet6; }
            else if (ingrediens6.Equals("Ris")) { obj_Entity.ris = ingrediensmangd6; obj_Entity.rismatt = mattenhet6; }
            else if (ingrediens6.Equals("Potatis")) { obj_Entity.potatis = ingrediensmangd6; obj_Entity.potatismatt = mattenhet6; }
            else if (ingrediens6.Equals("Kycklingfile")) { obj_Entity.kycklingfile = ingrediensmangd6; obj_Entity.kycklingfilematt = mattenhet6; }
            else if (ingrediens6.Equals("Hönsbuljong")) { obj_Entity.honsbuljong = ingrediensmangd6; obj_Entity.honsbuljongmatt = mattenhet6; }
            else if (ingrediens6.Equals("Matlagningsgrädde")) { obj_Entity.matlagningsgradde = ingrediensmangd6; obj_Entity.matlagningsgraddematt = mattenhet6; }
            else if (ingrediens6.Equals("Creme fraiche")) { obj_Entity.cremefraiche = ingrediensmangd6; obj_Entity.cremefraichematt = mattenhet6; }
            else if (ingrediens6.Equals("Jäst")) { obj_Entity.jast = ingrediensmangd6; obj_Entity.jastmatt = mattenhet6; }
            else if (ingrediens6.Equals("Honung")) { obj_Entity.honung = ingrediensmangd6; obj_Entity.honungmatt = mattenhet6; }
            else if (ingrediens6.Equals("Vatten")) { obj_Entity.vatten = ingrediensmangd6; obj_Entity.vattenmatt = mattenhet6; }
            else if (ingrediens6.Equals("Gräslök")) { obj_Entity.graslok = ingrediensmangd6; obj_Entity.graslokmatt = mattenhet6; }
            else if (ingrediens6.Equals("Persilja")) { obj_Entity.persilja = ingrediensmangd6; obj_Entity.persiljamatt = mattenhet6; }
            else if (ingrediens6.Equals("Krossade tomater")) { obj_Entity.krossadetomater = ingrediensmangd6; obj_Entity.krossadetomatermatt = mattenhet6; }
            else if (ingrediens6.Equals("Olivolja")) { obj_Entity.olivolja = ingrediensmangd6; obj_Entity.olivoljamatt = mattenhet6; }
            else if (ingrediens6.Equals("Valmofrön")) { obj_Entity.valmofron = ingrediensmangd6; obj_Entity.valmofronmatt = mattenhet6; }
            else if (ingrediens6.Equals("Pasta")) { obj_Entity.pasta = ingrediensmangd6; obj_Entity.pastamatt = mattenhet6; }
            else if (ingrediens6.Equals("Ost")) { obj_Entity.ost = ingrediensmangd6; obj_Entity.ostmatt = mattenhet6; }
            else if (ingrediens6.Equals("Ströbröd")) { obj_Entity.strobrod = ingrediensmangd6; obj_Entity.strobrodmatt = mattenhet6; }
            else if (ingrediens6.Equals("Lammkotletter")) { obj_Entity.lammkotletter = ingrediensmangd6; obj_Entity.lammkotlettermatt = mattenhet6; }
            else if (ingrediens6.Equals("Lammfärs")) { obj_Entity.lammfars = ingrediensmangd6; obj_Entity.lammfarsmatt = mattenhet6; }
            else if (ingrediens6.Equals("Rödlök")) { obj_Entity.rodlok = ingrediensmangd6; obj_Entity.rodlokmatt = mattenhet6; }
            else if (ingrediens6.Equals("Gul lök")) { obj_Entity.gullok = ingrediensmangd6; obj_Entity.gullokmatt = mattenhet6; }
            else if (ingrediens6.Equals("Vitlök")) { obj_Entity.vitlok = ingrediensmangd6; obj_Entity.vitlokmatt = mattenhet6; }
            else if (ingrediens6.Equals("Rosmarin")) { obj_Entity.rosmarin = ingrediensmangd6; obj_Entity.rosmarinmatt = mattenhet6; }
            else if (ingrediens6.Equals("Citron")) { obj_Entity.citron = ingrediensmangd6; obj_Entity.citronmatt = mattenhet6; }
            else if (ingrediens6.Equals("Rödvin")) { obj_Entity.rodvin = ingrediensmangd6; obj_Entity.rodvinmatt = mattenhet6; }
            else if (ingrediens6.Equals("Vitt vin")) { obj_Entity.vittvin = ingrediensmangd6; obj_Entity.vittvinmatt = mattenhet6; }
            else if (ingrediens6.Equals("Schalottenlök")) { obj_Entity.schalottenlok = ingrediensmangd6; obj_Entity.schalottenlokmatt = mattenhet6; }
            else if (ingrediens6.Equals("Oxfile")) { obj_Entity.oxfile = ingrediensmangd6; obj_Entity.oxfilematt = mattenhet6; }
            else if (ingrediens6.Equals("Lax")) { obj_Entity.lax = ingrediensmangd6; obj_Entity.laxmatt = mattenhet6; }
            else if (ingrediens6.Equals("Spenat")) { obj_Entity.spenat = ingrediensmangd6; obj_Entity.spenatmatt = mattenhet6; }
            else if (ingrediens6.Equals("Champinjoner")) { obj_Entity.champinjoner = ingrediensmangd6; obj_Entity.champinjonermatt = mattenhet6; }
            else if (ingrediens6.Equals("Kantareller")) { obj_Entity.kantareller = ingrediensmangd6; obj_Entity.kantarellermatt = mattenhet6; }

            /*-----------Ingrediens 7-------------*/
            if (ingrediens7.Equals("Mjölk")) { obj_Entity.mjolk = ingrediensmangd7; obj_Entity.mjolkmatt = mattenhet7; }
            else if (ingrediens7.Equals("Salt")) { obj_Entity.salt = ingrediensmangd7; obj_Entity.saltmatt = mattenhet7; }
            else if (ingrediens7.Equals("Ägg")) { obj_Entity.agg = ingrediensmangd7; obj_Entity.aggmatt = mattenhet7; }
            else if (ingrediens7.Equals("Svartpeppar")) { obj_Entity.svartpeppar = ingrediensmangd7; obj_Entity.svartpepparmatt = mattenhet7; }
            else if (ingrediens7.Equals("Curry")) { obj_Entity.curry = ingrediensmangd7; obj_Entity.currymatt = mattenhet7; }
            else if (ingrediens7.Equals("Smör")) { obj_Entity.smor = ingrediensmangd7; obj_Entity.smormatt = mattenhet7; }
            else if (ingrediens7.Equals("Vetemjöl")) { obj_Entity.vetemjol = ingrediensmangd7; obj_Entity.vetemjolmatt = mattenhet7; }
            else if (ingrediens7.Equals("Rågmjöl")) { obj_Entity.rogmjol = ingrediensmangd7; obj_Entity.rogmjolmatt = mattenhet7; }
            else if (ingrediens7.Equals("Vetemjöl Special")) { obj_Entity.vetemjolspecial = ingrediensmangd7; obj_Entity.vetemjolspecialmatt = mattenhet7; }
            else if (ingrediens7.Equals("Brödsirap")) { obj_Entity.brodsirap = ingrediensmangd7; obj_Entity.brodsirapmatt = mattenhet7; }
            else if (ingrediens7.Equals("Vispgrädde")) { obj_Entity.vispgradde = ingrediensmangd7; obj_Entity.vispgraddematt = mattenhet7; }
            else if (ingrediens7.Equals("Choklad")) { obj_Entity.choklad = ingrediensmangd7; obj_Entity.chokladmatt = mattenhet7; }
            else if (ingrediens7.Equals("Hallon")) { obj_Entity.hallon = ingrediensmangd7; obj_Entity.hallonmatt = mattenhet7; }
            else if (ingrediens7.Equals("Matyogurt")) { obj_Entity.matyogurt = ingrediensmangd7; obj_Entity.matyogurtmatt = mattenhet7; }
            else if (ingrediens7.Equals("Strösocker")) { obj_Entity.strosocker = ingrediensmangd7; obj_Entity.strosockermatt = mattenhet7; }
            else if (ingrediens7.Equals("Wokgrönsaker")) { obj_Entity.wokgronsaker = ingrediensmangd7; obj_Entity.wokgronsakermatt = mattenhet7; }
            else if (ingrediens7.Equals("Ris")) { obj_Entity.ris = ingrediensmangd7; obj_Entity.rismatt = mattenhet7; }
            else if (ingrediens7.Equals("Potatis")) { obj_Entity.potatis = ingrediensmangd7; obj_Entity.potatismatt = mattenhet7; }
            else if (ingrediens7.Equals("Kycklingfile")) { obj_Entity.kycklingfile = ingrediensmangd7; obj_Entity.kycklingfilematt = mattenhet7; }
            else if (ingrediens7.Equals("Hönsbuljong")) { obj_Entity.honsbuljong = ingrediensmangd7; obj_Entity.honsbuljongmatt = mattenhet7; }
            else if (ingrediens7.Equals("Matlagningsgrädde")) { obj_Entity.matlagningsgradde = ingrediensmangd7; obj_Entity.matlagningsgraddematt = mattenhet7; }
            else if (ingrediens7.Equals("Creme fraiche")) { obj_Entity.cremefraiche = ingrediensmangd7; obj_Entity.cremefraichematt = mattenhet7; }
            else if (ingrediens7.Equals("Jäst")) { obj_Entity.jast = ingrediensmangd7; obj_Entity.jastmatt = mattenhet7; }
            else if (ingrediens7.Equals("Honung")) { obj_Entity.honung = ingrediensmangd7; obj_Entity.honungmatt = mattenhet7; }
            else if (ingrediens7.Equals("Vatten")) { obj_Entity.vatten = ingrediensmangd7; obj_Entity.vattenmatt = mattenhet7; }
            else if (ingrediens7.Equals("Gräslök")) { obj_Entity.graslok = ingrediensmangd7; obj_Entity.graslokmatt = mattenhet7; }
            else if (ingrediens7.Equals("Persilja")) { obj_Entity.persilja = ingrediensmangd7; obj_Entity.persiljamatt = mattenhet7; }
            else if (ingrediens7.Equals("Krossade tomater")) { obj_Entity.krossadetomater = ingrediensmangd7; obj_Entity.krossadetomatermatt = mattenhet7; }
            else if (ingrediens7.Equals("Olivolja")) { obj_Entity.olivolja = ingrediensmangd7; obj_Entity.olivoljamatt = mattenhet7; }
            else if (ingrediens7.Equals("Valmofrön")) { obj_Entity.valmofron = ingrediensmangd7; obj_Entity.valmofronmatt = mattenhet7; }
            else if (ingrediens7.Equals("Pasta")) { obj_Entity.pasta = ingrediensmangd7; obj_Entity.pastamatt = mattenhet7; }
            else if (ingrediens7.Equals("Ost")) { obj_Entity.ost = ingrediensmangd7; obj_Entity.ostmatt = mattenhet7; }
            else if (ingrediens7.Equals("Ströbröd")) { obj_Entity.strobrod = ingrediensmangd7; obj_Entity.strobrodmatt = mattenhet7; }
            else if (ingrediens7.Equals("Lammkotletter")) { obj_Entity.lammkotletter = ingrediensmangd7; obj_Entity.lammkotlettermatt = mattenhet7; }
            else if (ingrediens7.Equals("Lammfärs")) { obj_Entity.lammfars = ingrediensmangd7; obj_Entity.lammfarsmatt = mattenhet7; }
            else if (ingrediens7.Equals("Rödlök")) { obj_Entity.rodlok = ingrediensmangd7; obj_Entity.rodlokmatt = mattenhet7; }
            else if (ingrediens7.Equals("Gul lök")) { obj_Entity.gullok = ingrediensmangd7; obj_Entity.gullokmatt = mattenhet7; }
            else if (ingrediens7.Equals("Vitlök")) { obj_Entity.vitlok = ingrediensmangd7; obj_Entity.vitlokmatt = mattenhet7; }
            else if (ingrediens7.Equals("Rosmarin")) { obj_Entity.rosmarin = ingrediensmangd7; obj_Entity.rosmarinmatt = mattenhet7; }
            else if (ingrediens7.Equals("Citron")) { obj_Entity.citron = ingrediensmangd7; obj_Entity.citronmatt = mattenhet7; }
            else if (ingrediens7.Equals("Rödvin")) { obj_Entity.rodvin = ingrediensmangd7; obj_Entity.rodvinmatt = mattenhet7; }
            else if (ingrediens7.Equals("Vitt vin")) { obj_Entity.vittvin = ingrediensmangd7; obj_Entity.vittvinmatt = mattenhet7; }
            else if (ingrediens7.Equals("Schalottenlök")) { obj_Entity.schalottenlok = ingrediensmangd7; obj_Entity.schalottenlokmatt = mattenhet7; }
            else if (ingrediens7.Equals("Oxfile")) { obj_Entity.oxfile = ingrediensmangd7; obj_Entity.oxfilematt = mattenhet7; }
            else if (ingrediens7.Equals("Lax")) { obj_Entity.lax = ingrediensmangd7; obj_Entity.laxmatt = mattenhet7; }
            else if (ingrediens7.Equals("Spenat")) { obj_Entity.spenat = ingrediensmangd7; obj_Entity.spenatmatt = mattenhet7; }
            else if (ingrediens7.Equals("Champinjoner")) { obj_Entity.champinjoner = ingrediensmangd7; obj_Entity.champinjonermatt = mattenhet7; }
            else if (ingrediens7.Equals("Kantareller")) { obj_Entity.kantareller = ingrediensmangd7; obj_Entity.kantarellermatt = mattenhet7; }
           
            obj_Entity.antal = int.Parse(antal);
            obj_Entity.Instruktioner = instruktioner;
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
