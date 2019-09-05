using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoyalDine.Sql
{
    public class DBConfig
    {
        public string getConnStr()
        {
            //return "Data Source=(local);Initial Catalog=royaldinedb;Integrated Security=SSPI";
            return "workstation id=hifiroyaldinedb.mssql.somee.com;packet size=4096;user id=hifi_SQLLogin_1;pwd=m42nuonznp;data source=hifiroyaldinedb.mssql.somee.com;persist security info=False;initial catalog=hifiroyaldinedb";
        }
    }
}