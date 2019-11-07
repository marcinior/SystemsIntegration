using StorageHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace ComputersWebService
{
    /// <summary>
    /// Summary description for ComputerWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ComputerWebService : System.Web.Services.WebService
    {
        private readonly string connectionString;
        private DbHelper dbHelper;

        public ComputerWebService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            dbHelper = new DbHelper(connectionString);
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(Description = "Returns number of computers in database by producer")]
        [SoapDocumentMethod(ResponseElementName = "GetCountOfComputersByProducer")] 
        public int ComputersCount(string producer)
        {
            int count = 0;

            try
            {
                 count = dbHelper.GetCountOfComputersByProducer(producer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return count;
        }

        [WebMethod]
        public List<Computer> GetComputersByDisplayType(string displayType)
        {
            List<Computer> computers = null;

            try
            {
                computers = dbHelper.GetComputersByDisplayType(displayType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return computers;
        }
    }
}
