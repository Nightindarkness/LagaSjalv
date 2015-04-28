using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using Microsoft.WindowsAzure.Storage.Table;
using System.Data.Entity;

namespace MvcWebRole1.Models
{
    public class SaveRecipe : TableEntity
    {
        public SaveRecipe(){}

        public string Receptnamn { get; set; }
        public string Inloggnamn { get; set; }
        
    }
}