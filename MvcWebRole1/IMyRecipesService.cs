using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MvcWebRole1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMyRecipesService" in both code and config file together.
    [ServiceContract]
    public interface IMyRecipesService
    {
        [OperationContract]
        String Login(string username, string password);
    }

}
