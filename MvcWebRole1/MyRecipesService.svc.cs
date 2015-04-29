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
using Newtonsoft.Json.Linq;
using System.Collections;


namespace MvcWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyRecipesService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MyRecipesService.svc or MyRecipesService.svc.cs at the Solution Explorer and start debugging.
    
    public class MyRecipesService : IMyRecipesService
    {

        public String Login(string username, string password)
        {
            string values = "";
            if(WebSecurity.Login(username,password)){
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
            
            JObject json = new JObject();
            
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
                                string valueItem = "";
                                int i = 0;
                                if(recItem.ReceptNamn != null && recItem.Inloggnamn !=null){
        valueItem = recItem.ReceptNamn +" för " +recItem.antal +" personer.";
    
             }
                                  if(recItem.mjolk != null){
                                      valueItem = valueItem + "!!Mjölk: " + recItem.mjolk + " " + recItem.mjolkmatt;
             }
             if(recItem.salt != null){
                 valueItem = valueItem + "!!Salt: " + recItem.salt + " " + recItem.saltmatt;
             }
             if(recItem.agg != null){
                 valueItem = valueItem + "!!Ägg: " + recItem.agg + " " + recItem.aggmatt;
             }
             if(recItem.svartpeppar != null){
                 valueItem = valueItem + "!!Svartpeppar:  " + recItem.svartpeppar + " " + recItem.svartpepparmatt;
             }
             if(recItem.curry != null){
                 valueItem = valueItem + "!!Curry: " + recItem.curry + " " + recItem.currymatt;
             }
             if(recItem.smor != null){
                 valueItem = valueItem + "!!Smör:  " + recItem.smor + " " + recItem.smormatt;
             }
             if(recItem.vetemjol != null){
                 valueItem = valueItem + "!!Vetemjöl: " + recItem.vetemjol + " " + recItem.vetemjolmatt;
             }
             if(recItem.rogmjol != null){
                 valueItem = valueItem + "!!Rågmjöl:  " + recItem.rogmjol + " " + recItem.rogmjolmatt;
             }
        if(recItem.vetemjolspecial != null){
            valueItem = valueItem + "!!Vetemjöl Special: " + recItem.vetemjolspecial + " " + recItem.vetemjolspecialmatt;
             }
             if(recItem.brodsirap != null){
                 valueItem = valueItem + "!!Brödsirap: " + recItem.brodsirap + " " + recItem.brodsirapmatt;
             }
             if(recItem.vispgradde != null){
                 valueItem = valueItem + "!!Vispgrädde: " + recItem.vispgradde + " " + recItem.vispgraddematt;
             }
             if(recItem.choklad != null){
                 valueItem = valueItem + "!!Chocklad: " + recItem.choklad + " " + recItem.chokladmatt;
             }
             if(recItem.hallon != null){
                 valueItem = valueItem + "!!Hallon: " + recItem.hallon + " " + recItem.hallonmatt;
             }
             if(recItem.matyogurt != null){
                 valueItem = valueItem + "!!Matyoghurt " + recItem.matyogurt + " " + recItem.matyogurtmatt;
             }
             if(recItem.strosocker != null){
                 valueItem = valueItem + "!!Strösocker: " + recItem.strosocker + " " + recItem.strosockermatt;
             }
             if(recItem.wokgronsaker != null){
                 valueItem = valueItem + "!!Wokgrönsaker: " + recItem.wokgronsaker + " " + recItem.wokgronsakermatt;
             }
             if(recItem.ris != null){
                 valueItem = valueItem + "!!Ris: " + recItem.ris + " " + recItem.rismatt;
             }
             if(recItem.potatis != null){
                 valueItem = valueItem + "!!Potatis: " + recItem.potatis + " " + recItem.potatismatt;
             }
             if(recItem.kycklingfile != null){
                 valueItem = valueItem + "!!Kycklingfile: " + recItem.kycklingfile + " " + recItem.kycklingfilematt;
             }
             if(recItem.honsbuljong != null){
                 valueItem = valueItem + "!!Hönsbuljong: " + recItem.honsbuljong + " " + recItem.honsbuljongmatt;
             }
             if(recItem.matlagningsgradde != null){
                 valueItem = valueItem + "!!Matlagningsgradde: " + recItem.matlagningsgradde + " " + recItem.matlagningsgraddematt;
             }
             if(recItem.cremefraiche != null){
                 valueItem = valueItem + "!!Creme Fraiche: " + recItem.cremefraiche + " " + recItem.cremefraichematt;
             }
             if(recItem.jast != null){
                 valueItem = valueItem + "!!Jäst: " + recItem.jast + " " + recItem.jastmatt;
             }
             if(recItem.honung != null){
                 valueItem = valueItem + "!!Honung: " + recItem.honung + " " + recItem.honungmatt;
             }
             if(recItem.vatten != null){
                 valueItem = valueItem + "!!Vatten: " + recItem.vatten + " " + recItem.vattenmatt;
             }
             if(recItem.graslok != null){
                 valueItem = valueItem + "!!Gräslök: " + recItem.graslok + " " + recItem.graslokmatt;
             }
             if(recItem.persilja != null){
                 valueItem = valueItem + "!!Persilja " + recItem.persilja + " " + recItem.persiljamatt;
             }
             if(recItem.krossadetomater != null){
                 valueItem = valueItem + "!!Krossade tomater: " + recItem.krossadetomater + " " + recItem.krossadetomatermatt;
             }
             if(recItem.olivolja != null){
                 valueItem = valueItem + "!!Olivolja: " + recItem.olivolja + " " + recItem.olivoljamatt;
             }
             if(recItem.valmofron != null){
                 valueItem = valueItem + "!!Vallmofrön: " + recItem.valmofron + " " + recItem.valmofronmatt;
             }
             if(recItem.pasta != null){
                 valueItem = valueItem + "!!Pasta: " + recItem.pasta + " " + recItem.pastamatt;
             }
             if(recItem.ost != null){
                 valueItem = valueItem + "!!Ost: " + recItem.ost + " " + recItem.ostmatt;
             }
             if(recItem.strobrod != null){
                 valueItem = valueItem + "!!Ströbröd " + recItem.strobrod + " " + recItem.strobrodmatt;
             }
             if(recItem.lammkotletter != null){
                 valueItem = valueItem + "!!Lammkotletter: " + recItem.lammkotletter + " " + recItem.lammkotlettermatt;
             }
             if(recItem.lammfars != null){
                 valueItem = valueItem + "!!Lammfärs: " + recItem.lammfars + " " + recItem.lammfars;
             }
             if(recItem.rodlok != null){
                 valueItem = valueItem + "!!Rödlök: " + recItem.rodlok + " " + recItem.rodlok;
             }
             if(recItem.gullok != null){
                 valueItem = valueItem + "!!Gullök: " + recItem.gullok + " " + recItem.gullokmatt;
             }
             if(recItem.vitlok != null){
                 valueItem = valueItem + "!!Vitlök: " + recItem.vitlok + " " + recItem.vitlokmatt;
             }
             if(recItem.rosmarin != null){
                 valueItem = valueItem + "!!Rosmarin: " + recItem.rosmarin + " " + recItem.rosmarinmatt;
             }
             if(recItem.citron != null){
                 valueItem = valueItem + "!!Citron: " + recItem.citron + " " + recItem.citronmatt;
             }
             if(recItem.rodvin != null){
                 valueItem = valueItem + "!!Rödvin: " + recItem.rodvin + " " + recItem.rodvinmatt;
             }
             if(recItem.vittvin != null){
                 valueItem = valueItem + "!!Vitt vin: " + recItem.vittvin + " " + recItem.vittvinmatt;
             }
             if(recItem.schalottenlok != null){
                 valueItem = valueItem + "!!Schalottenlok: " + recItem.schalottenlok + " " + recItem.schalottenlokmatt;   
             }
             if(recItem.oxfile != null){
                 valueItem = valueItem + "!!Oxfile: " + recItem.oxfile + " " + recItem.oxfilematt;
             }
             if(recItem.lax != null){
                 valueItem = valueItem + "!!Lax: " + recItem.lax + " " + recItem.laxmatt;
             }
             if(recItem.spenat != null){
                 valueItem = valueItem + "!!Spenat: " + recItem.spenat + " " + recItem.spenatmatt;
             }
             
             if(recItem.champinjoner != null){
                 valueItem = valueItem + "!!Champinjoner: " + recItem.champinjoner + " " + recItem.champinjonermatt;
             }
             if(recItem.kantareller != null){
                 valueItem = valueItem + "!!Kantareller: " + recItem.kantareller + " " + recItem.kantarellermatt;
             }
             if(recItem.Instruktioner != null){
                 valueItem = valueItem + "!!Instruktioner: " + recItem.Instruktioner;
             }

             values = valueItem+ "|" +valueItem+"|";
             //json[username+i]=valueItem;
             i++;
                              //  IEnumerable<Recept> ing = (IEnumerable<Recept>)recItem;
                                //List<Recept> ing = ()recItem;
                              //  foreach (var ingredients in  recItem)
                               // {

                              //  if(ingredients != null){
                                //    System.Diagnostics.Debug.WriteLine(ingredients);
                             //   }
                              //  }
                                //Tab.Where(TableQuery.GenerateFilterCondition("blobnamn", QueryComparisons.Equals,blobItem.Uri.ToString())));
                                // values.Add(recItem.blobnamn); //0
                                //values.Add(rec.PartitionKey);//1
                                //values.Add(recItem.Instruktioner);//2
                                blobs.Add(blobItem.Uri.ToString());
                                //reci.Add(values);
                            }
                        }
                    }
                }
            }
            }else{

                values = "Du har angivit fel användarnamn eller lösenord.";
            }
            //var json = JsonConvert.SerializeObject(values);
            // reci.ElementAt(0).ElementAt(0);
            
            

            return values;
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
