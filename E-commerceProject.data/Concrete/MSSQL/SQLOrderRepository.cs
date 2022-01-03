using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.data.Abstract;
using E_commerceProject.entity;

namespace E_commerceProject.data.Concrete.MSSQL
{
    public class SQLOrderRepository : IOrderRepository

    {
        private SqlConnection getSQLConnections()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=e-commerce;Integrated Security=SSPI;";
            //DRIVER PROVIDER
            return new SqlConnection(connectionString);

        }
        public void Create(Order order)
        {
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();


                    string sql = "IF EXISTS(select * from CartItems where ProductId=@ProductId and UserId=@UserId) UPDATE CartItems set Quantity=Quantity+1 where ProductId=@ProductId and UserId=@UserId else insert into CartItems (ProductId,Quantity,UserId) values(@ProductId,@Quantity,@UserId)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@ProductId", order.ProductId);
                    command.Parameters.AddWithValue("@Quantity", order.Quantity);
                    command.Parameters.AddWithValue("@UserId", order.UserId);

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
        public void SetQuantity(int UserId, int ProductId, int Quantity)
        {
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();

                    string sql = "UPDATE CartItems SET Quantity=@Quantity WHERE UserId = @id and ProductId=@ProductId ";
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@id", UserId);
                    command.Parameters.AddWithValue("@ProductId", ProductId);
                    command.Parameters.AddWithValue("@Quantity", Quantity);
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
        public void DeleteOrder(int id, int ProductId)
        {
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "DELETE FROM CartItems WHERE UserId = @id and ProductId=@ProductId";
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@ProductId", ProductId);
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
        public List<OrderHistory> getOrderHistoryList(int userId)
        {
            List<OrderHistory> OrderHistories = null;
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM OrderHistory where UserId=@userId";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = command.ExecuteReader();
                    OrderHistories = new List<OrderHistory>();
                    while (reader.Read())
                    {
                        OrderHistories.Add(
                            new OrderHistory
                            {
                                HistoryId = int.Parse(reader["HistoryId"].ToString()),
                                Date = reader["Date"].ToString(),
                                ProductId = int.Parse(reader["ProductId"].ToString()),
                                UserId = int.Parse(reader["UserId"].ToString()),
                                Quantity = int.Parse(reader["Quantity"].ToString()),

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
            return OrderHistories;

        }
        public void CreateOrderHistory(OrderHistory orderHistory)
        {
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();


                    string sql = "insert into OrderHistory (Date,ProductId,UserId,Quantity) values(@Date,@ProductId,@UserId,@Quantity)";
                    SqlCommand command = new SqlCommand(sql, connection);

                    command.Parameters.AddWithValue("@Date", orderHistory.Date);
                    command.Parameters.AddWithValue("@ProductId", orderHistory.ProductId);
                    command.Parameters.AddWithValue("@UserId", orderHistory.UserId);
                    command.Parameters.AddWithValue("@Quantity", orderHistory.Quantity);

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
                    string sql = "select count(*) from CartItems where UserId=@UserId";

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


        public List<Order> GetAll()
        {
            List<Order> Orders = null;
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM CartItems";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    Orders = new List<Order>();
                    while (reader.Read())
                    {
                        Orders.Add(
                            new Order
                            {
                                OrderId = int.Parse(reader["OrderId"].ToString()),
                                ProductId = int.Parse(reader["ProductId"].ToString()),
                                Quantity = int.Parse(reader["Quantity"].ToString()),
                                UserId = int.Parse(reader["UserId"].ToString()),

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
            return Orders;
        }

        public List<Order> GetsById(int id)
        {

            List<Order> Orders = null;
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();

                    string sql = "SELECT * FROM CartItems where UserId=@id";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    Orders = new List<Order>();

                    while (reader.Read())
                    {
                        Orders.Add(
                            new Order
                            {
                                OrderId = int.Parse(reader["OrderId"].ToString()),
                                ProductId = int.Parse(reader["ProductId"].ToString()),
                                Quantity = int.Parse(reader["Quantity"].ToString()),
                                UserId = int.Parse(reader["UserId"].ToString()),

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
            return Orders;
        }

        public void Update(Order entity)
        {
            throw new NotImplementedException();
        }

        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}