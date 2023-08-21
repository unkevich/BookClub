using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BookClubApp.Class
{
    internal class DataBase {
        SqlConnection dbHandle = new SqlConnection(@"Data Source=DESKTOP-1GENUUV; Initial Catalog=BookClubBase; Integrated Security=True");
        // открывает подключение к бд
        public void openConnection() {
            if (dbHandle.State == System.Data.ConnectionState.Closed) { dbHandle.Open(); }
        }
        // закрывает подключение к бд
        public void closeConnection() {
            if (dbHandle.State == System.Data.ConnectionState.Open) { dbHandle.Close(); }
        }
        // вызывает строку подключения
        public SqlConnection getConnection() { return dbHandle; }
    }
}
