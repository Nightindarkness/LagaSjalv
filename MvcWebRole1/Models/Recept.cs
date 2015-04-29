using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using Microsoft.WindowsAzure.Storage.Table;
using System.Data.Entity;
using System.Collections;

namespace MvcWebRole1.Models
{
    public class Recept : TableEntity , IEnumerable
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
        
        public IEnumerator GetEnumerator()
        {
            yield return this.ReceptNamn;        
            yield return this.Inloggnamn;
        yield return this.Instruktioner; 

        yield return this.mjolk;
        yield return this.salt ;
            yield return this.agg;
        yield return this.svartpeppar;
        yield return this.curry;
        yield return this.smor;
        yield return this.vetemjol;
         yield return this.rogmjol;
        yield return this.vetemjolspecial;
        yield return this.brodsirap;
         yield return this.vispgradde ;
        yield return this.choklad ;
         yield return this.hallon ;
         yield return this.matyogurt ;
         yield return this.strosocker ;
        yield return this.wokgronsaker;
        yield return this.ris;
        yield return this.potatis;
         yield return this.kycklingfile ;
         yield return this.honsbuljong;
         yield return this.matlagningsgradde;
        yield return this.cremefraiche;
        yield return this.jast ;
        yield return this.honung ;
         yield return this.vatten ;
         yield return this.graslok ;
        yield return this.persilja ;
         yield return this.krossadetomater;
         yield return this.olivolja ;
        yield return this.valmofron ;
        yield return this.pasta ;
        yield return this.ost ;
         yield return this.strobrod ;
         yield return this.lammkotletter ;
         yield return this.lammfars ;
        yield return this.rodlok ;
        yield return this.gullok ;
        yield return this.vitlok ;
        yield return this.rosmarin ;
         yield return this.citron ;
        yield return this.rodvin ;
        yield return this.vittvin ;
        yield return this.schalottenlok ;
        yield return this.oxfile ;
        yield return this.lax ;
       yield return this.spenat ;
         yield return this.champinjoner;
        yield return this.kantareller ;
        
                

        yield return this.mjolkmatt;
         yield return this.saltmatt ;
         yield return this.aggmatt ;
         yield return this.svartpepparmatt ;
         yield return this.currymatt;
         yield return this.smormatt ;
         yield return this.vetemjolmatt ;
        yield return this.rogmjolmatt;
         yield return this.vetemjolspecialmatt ;
        yield return this.brodsirapmatt;
         yield return this.vispgraddematt ;
         yield return this.chokladmatt;
         yield return this.hallonmatt ;
        yield return this.matyogurtmatt;
        yield return this.strosockermatt ;
        yield return this.wokgronsakermatt;
        yield return this.rismatt;
         yield return this.potatismatt ;
        yield return this.kycklingfilematt ;
        yield return this.honsbuljongmatt ;
       yield return this.matlagningsgraddematt ;
        yield return this.cremefraichematt;
        yield return this.jastmatt ;
        yield return this.honungmatt ;
         yield return this.vattenmatt ;
        yield return this.graslokmatt ;
        yield return this.persiljamatt ;
        yield return this.krossadetomatermatt;
       yield return this.olivoljamatt ;
        yield return this.valmofronmatt ;
        yield return this.pastamatt ;
        yield return this.ostmatt ;
        yield return this.strobrodmatt ;
         yield return this.lammkotlettermatt;
       yield return this.lammfarsmatt;
        yield return this.rodlokmatt ;
         yield return this.gullokmatt ;
        yield return this.vitlokmatt ;
         yield return this.rosmarinmatt ;
        yield return this.citronmatt ;
         yield return this.rodvinmatt ;
        yield return this.vittvinmatt;
         yield return this.schalottenlokmatt ;
        yield return this.oxfilematt ;
        yield return this.laxmatt ;
        yield return this.spenatmatt ;
        yield return this.champinjonermatt;
        yield return this.kantarellermatt;


        yield return this.antal;
        yield return this.blobnamn;
        }
    }

    
    
}