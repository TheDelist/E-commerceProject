using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using System.Threading.Tasks;
using E_commerceProject.data.Abstract;
using E_commerceProject.entity;

namespace E_commerceProject.data.Concrete.MSSQL
{
    public class SQLProductRepository:IProductRepository
    {
         private SqlConnection getSQLConnections()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=e-commerce;Integrated Security=SSPI;";
            //DRİVER PROVİDER
            return new SqlConnection(connectionString);

        }
        public int Create(Product entity)
        {
            int result = 0;
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "insert into products (ProductName,CategoryId,Price,InStock,ProductImage,ProductDescription,QuantityPerUnit,ProductUrl) values(@name,@categoryId,@price,@inStock,@Image,@Description,@Quantity,@Url)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@price", entity.Price);
                    command.Parameters.AddWithValue("@name", entity.Name);
                    command.Parameters.AddWithValue("@categoryId", entity.CategoryId);
                    command.Parameters.AddWithValue("@inStock", entity.InStock);
                    command.Parameters.AddWithValue("@Image", entity.ImageUrl);
                    command.Parameters.AddWithValue("@Description", entity.Description);
                    command.Parameters.AddWithValue("@Quantity", 1);
                    command.Parameters.AddWithValue("@Url", entity.Url);

                    result = command.ExecuteNonQuery();
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            return result;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll(int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
             Product product = null;
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "select * from products where ProductID=@productId";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@productId", id);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        product = new Product()
                        {
                            ProductId = int.Parse(reader["ProductID"].ToString()),
                            Name = reader["ProductName"].ToString(),
                            CategoryId = int.Parse(reader["CategoryId"].ToString()),
                            Price = double.Parse(reader["Price"]?.ToString()),
                            InStock = int.Parse(reader["InStock"]?.ToString()) == 1 ? true : false,
                            ImageUrl = reader["ProductImage"]?.ToString(),
                            Description = reader["ProductDescription"]?.ToString(),
                            Url=reader["ProductUrl"]?.ToString(),
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
            return product;
        }

        public List<Product> GetPopularProducts()
        {
            throw new NotImplementedException();
        }

        public Product GetProductDetails(string productname)
        {
           Product product = null;
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM Categories LEFT JOIN products ON products.CategoryID = Categories.CategoryID where products.ProductUrl=@Url;";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@Url", productname);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        product = new Product()
                        {
                            ProductId = int.Parse(reader["ProductID"].ToString()),
                            Name = reader["ProductName"].ToString(),
                            CategoryId = int.Parse(reader["CategoryId"].ToString()),
                            Category=new Category{
                                Name = reader["CategoryName"].ToString(),
                                CategoryId = int.Parse(reader["CategoryId"].ToString()),
                                Description = reader["Description"]?.ToString(),
                                Url = reader["Url"]?.ToString(),
                            
                            },
                            Price = double.Parse(reader["Price"]?.ToString()),
                            InStock = int.Parse(reader["InStock"]?.ToString()) == 1 ? true : false,
                            ImageUrl = reader["ProductImage"]?.ToString(),
                            Description = reader["ProductDescription"]?.ToString(),
                            Url=reader["ProductUrl"]?.ToString()
                        };

                    }
                    reader.Close();
                }
                catch (System.Exception)
                {

                    throw;
                }
            }
            return product;
        }

        public List<Product> GetProductsByCategory(string name, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
        public int Count()
        {
            int count = 0;
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "select count(*) from products";
                    SqlCommand command = new SqlCommand(sql, connection);
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
       
        
    }
}