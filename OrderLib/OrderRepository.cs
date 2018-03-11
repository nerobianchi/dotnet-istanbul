using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OrderLib
{
    public class OrderRepository
    {
        public List<Order> List()
        {
            List<Order> list = new List<Order>();

            SqlConnection connection = new SqlConnection("Server=127.0.0.1;database=testdb; user id=sa;password=Pass123!!");
            using (connection)
            {
                var sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = @"
select
    *
from
    dbo.[Order]
";
                sqlCommand.CommandType = CommandType.Text;

                using (sqlCommand)
                {
                    connection.Open();
                    var sqlDataReader = sqlCommand.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(sqlDataReader);

                    foreach (DataRow row in dataTable.Rows)
                    {
                        list.Add(new Order {Id = (int) row["id"]});
                    }
                }
            }

            return list;
        }
    }
}