using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.data.Abstract;
using E_commerceProject.entity;

namespace E_commerceProject.data.Concrete.MSSQL
{
    public class SQLUserRepository : IUserRepository

    {
        public static Account acc = null;


        public Account Login2(String Username, String Password)
        {
            acc = Login(Username, Password);
            return acc;
        }

        private static SqlConnection getSQLConnections()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=e-commerce;Integrated Security=SSPI;";
            //DRIVER PROVIDER
            return new SqlConnection(connectionString);

        }
        public void Register(Account Account)
        {
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "INSERT INTO Customers (Name,Surname,Email,Sex,Type,BirthDate,Address,Phone,Password,Username) VALUES(@Name,@Surname,@Email,@Sex,@Type,@BirthDate,@Address,@Phone,@Password,@Username)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Name", Account.Name);
                    command.Parameters.AddWithValue("@Surname", Account.Surname);
                    command.Parameters.AddWithValue("@Email", Account.Email);
                    command.Parameters.AddWithValue("@Sex", Account.Sex);
                    command.Parameters.AddWithValue("@Type", Account.Type);
                    command.Parameters.AddWithValue("@BirthDate", Account.BirthDate);
                    command.Parameters.AddWithValue("@Address", Account.Address);
                    command.Parameters.AddWithValue("@Phone", Account.Phone);
                    command.Parameters.AddWithValue("@Password", Account.Password);
                    command.Parameters.AddWithValue("@Username", Account.Username);

                    command.ExecuteNonQuery();


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
        }
        public void Delete(int id)
        {
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "DELETE FROM Customers WHERE UserId = @id";
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
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
        }




        public int Count(int UserId)
        {
            int count = 0;
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "select count(*) from Customers where UserId=@UserId";

                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@UserId", UserId);
                    object obj = command.ExecuteScalar();
                    if (obj != null)
                    {
                        count = Convert.ToInt32(obj);
                    }
                }
                catch (System.Exception)
                {
                    throw;
                }
            }
            return count;
        }
        public Account GetById(int id)
        {
            Account account = null;
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "select * from Customers where UserId=@UserId";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@UserId", id);

                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        DateTime birthDate = new DateTime(2008, 5, 1, 8, 30, 52);
                        if (reader["BirthDate"] is DateTime)
                        {
                            birthDate = (DateTime)reader["BirthDate"];

                        }

                        account = new Account()

                        {
                            Name = reader["Name"].ToString(),
                            Surname = reader["Surname"].ToString(),
                            Email = reader["Email"].ToString(),
                            Sex = int.Parse(reader["Sex"].ToString()),
                            Type = reader["Type"]?.ToString(),
                            BirthDate = birthDate,
                            Address = reader["Address"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Password = reader["Password"].ToString(),
                            Username = reader["Username"].ToString(),
                            CustomerID = int.Parse(reader["CustomerId"]
                            .ToString()),
                        };
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
            return account;
        }
        public static Account Login(String Username, String Password)
        {

            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "select * from Customers where Username=@UserName and Password=@Password";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@UserName", Username);
                    command.Parameters.AddWithValue("@Password", Password);

                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        DateTime birthDate = new DateTime(2008, 5, 1, 8, 30, 52);
                        if (reader["BirthDate"] is DateTime)
                        {
                            birthDate = (DateTime)reader["BirthDate"];

                        }


                        acc = new Account()

                        {
                            Name = reader["Name"].ToString(),
                            Surname = reader["Surname"].ToString(),
                            Email = reader["Email"].ToString(),
                            Sex = int.Parse(reader["Sex"].ToString()),
                            Type = reader["Type"]?.ToString(),
                            BirthDate = DateTime.ParseExact("2009-05-08 14:40:52,531", "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture),
                            Address = reader["Address"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Password = reader["Password"].ToString(),
                            Username = reader["Username"].ToString(),
                            CustomerID = int.Parse(reader["CustomerId"]
                            .ToString()),
                        };
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
            return acc;
        }

    }
}