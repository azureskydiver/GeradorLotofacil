using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle
{
 public class Conexao
    {
        public static string cadeia = ConfigurationManager.ConnectionStrings["cadeia"].ToString();
    }
}
