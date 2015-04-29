using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using Microsoft.WindowsAzure.Storage.Table;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace MvcWebRole1.Models
{
    public class Recept : TableEntity
    {
      

        public Recept() { }


      
        public string ReceptNamn { get; set; }
        public string Inloggnamn { get; set; }
        public string Instruktioner { get; set; }

        public string mjolk { get; set; }
        public string salt { get; set; }
        public string agg { get; set; }
        public string svartpeppar { get; set; }
        public string curry { get; set; }
        public string smor { get; set; }
        public string vetemjol { get; set; }
        public string rogmjol { get; set; }
        public string vetemjolspecial { get; set; }
        public string brodsirap { get; set; }
        public string vispgradde { get; set; }
        public string choklad { get; set; }
        public string hallon { get; set; }
        public string matyogurt { get; set; }
        public string strosocker { get; set; }
        public string wokgronsaker { get; set; }
        public string ris { get; set; }
        public string potatis { get; set; }
        public string kycklingfile { get; set; }
        public string honsbuljong { get; set; }
        public string matlagningsgradde { get; set; }
        public string cremefraiche { get; set; }
        public string jast { get; set; }
        public string honung { get; set; }
        public string vatten { get; set; }
        public string graslok { get; set; }
        public string persilja { get; set; }
        public string krossadetomater { get; set; }
        public string olivolja { get; set; }
        public string valmofron { get; set; }
        public string pasta { get; set; }
        public string ost { get; set; }
        public string strobrod { get; set; }
        public string lammkotletter { get; set; }
        public string lammfars { get; set; }
        public string rodlok { get; set; }
        public string gullok { get; set; }
        public string vitlok { get; set; }
        public string rosmarin { get; set; }
        public string citron { get; set; }
        public string rodvin { get; set; }
        public string vittvin { get; set; }
        public string schalottenlok { get; set; }
        public string oxfile { get; set; }
        public string lax { get; set; }
        public string spenat { get; set; }
        public string champinjoner { get; set; }
        public string kantareller { get; set; }
        
                

        public string mjolkmatt { get; set; }
        public string saltmatt { get; set; }
        public string aggmatt { get; set; }
        public string svartpepparmatt { get; set; }
        public string currymatt { get; set; }
        public string smormatt { get; set; }
        public string vetemjolmatt { get; set; }
        public string rogmjolmatt { get; set; }
        public string vetemjolspecialmatt { get; set; }
        public string brodsirapmatt { get; set; }
        public string vispgraddematt { get; set; }
        public string chokladmatt { get; set; }
        public string hallonmatt { get; set; }
        public string matyogurtmatt { get; set; }
        public string strosockermatt { get; set; }
        public string wokgronsakermatt { get; set; }
        public string rismatt { get; set; }
        public string potatismatt { get; set; }
        public string kycklingfilematt { get; set; }
        public string honsbuljongmatt { get; set; }
        public string matlagningsgraddematt { get; set; }
        public string cremefraichematt { get; set; }
        public string jastmatt { get; set; }
        public string honungmatt { get; set; }
        public string vattenmatt { get; set; }
        public string graslokmatt { get; set; }
        public string persiljamatt { get; set; }
        public string krossadetomatermatt { get; set; }
        public string olivoljamatt { get; set; }
        public string valmofronmatt { get; set; }
        public string pastamatt { get; set; }
        public string ostmatt { get; set; }
        public string strobrodmatt { get; set; }
        public string lammkotlettermatt { get; set; }
        public string lammfarsmatt { get; set; }
        public string rodlokmatt { get; set; }
        public string gullokmatt { get; set; }
        public string vitlokmatt { get; set; }
        public string rosmarinmatt { get; set; }
        public string citronmatt { get; set; }
        public string rodvinmatt { get; set; }
        public string vittvinmatt { get; set; }
        public string schalottenlokmatt { get; set; }
        public string oxfilematt { get; set; }
        public string laxmatt { get; set; }
        public string spenatmatt { get; set; }
        public string champinjonermatt { get; set; }
        public string kantarellermatt { get; set; }

      
        public int antal { get; set; }
        public string blobnamn { get; set; }
    }
    
}