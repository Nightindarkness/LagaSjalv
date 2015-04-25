using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using Microsoft.WindowsAzure.Storage.Table;

namespace MvcWebRole1.Models
{
    public class Recept : TableEntity
    {
      

        public Recept() { }

      

        public string ReceptNamn
        {
            get;
            set;
        }

        public string Inloggnamn
        {
            get;
            set;
        }

        public string mjolk
        {
            get;
            set;
        }

        public string salt
        {
            get;
            set;
        }

        public string agg
        {
            get;
            set;
        }

        

        public string mjolkmatt
        {
            get;
            set;
        }

        public string saltmatt
        {
            get;
            set;
        }

        public string aggmatt
        {
            get;
            set;
        }

        public int antal
        {
            get;
            set;
        }

        public string blobnamn
        {
            get;
            set;
        }
    }
}