using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace SqlLite
{
    class Program
    {

      

        static void Main(string[] args)
        {
            
           
            SQLiteConnection conn = new SQLiteConnection("data source=Robert");
            conn.Open();
            
          
            using (SQLiteCommand cmd = conn.CreateCommand())
            {
                
                for (int i = 0; i < 100; i++)
                {

                    cmd.CommandText = string.Format("insert into Person values('{0}')","Robert"+i.ToString());
                    cmd.ExecuteNonQuery();

                }
            }
            Console.Read();
        }
    }
}
