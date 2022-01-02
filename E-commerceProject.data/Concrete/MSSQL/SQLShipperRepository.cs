using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.data.Abstract;
using E_commerceProject.entity;


namespace E_commerceProject.data.Concrete.MSSQL
{
    public class SQLShipperRepository : IShipperRepository

    {
        private SqlConnection getSQLConnections()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=e-commerce;Integrated Security=SSPI;";
            //DRIVER PROVIDER
            return new SqlConnection(connectionString);

        }


        public List<Shipper> GetAll()
        {
            List<Shipper> Shippers = null;
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM Shippers";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    Shippers = new List<Shipper>();
                    while (reader.Read())
                    {
                        Shippers.Add(
                            new Shipper
                            {
                                ShipperID = int.Parse(reader["ShipperID"].ToString()),
                                CompanyName = reader["CompanyName"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Price = int.Parse(reader["Price"].ToString()),

                            }
                        );
                    }
                    reader.Close();
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return Shippers;
        }



    }
}