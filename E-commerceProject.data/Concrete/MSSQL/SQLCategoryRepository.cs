using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.data.Abstract;
using E_commerceProject.entity;

namespace E_commerceProject.data.Concrete.MSSQL
{
    public class SQLCategoryRepository : ICategoryRepository
    {
        private SqlConnection getSqlConnection()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=e-commerce;Integrated Security=SSPI";
            //driver, provider
            return new SqlConnection(connectionString);
        }
        public void Create(Category entity)
        {
             using (var connection = getSqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "INSERT INTO Categories (CategoryName, Url) VALUES(@categoryName, @url)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@categoryName", entity.Name);
                    // command.Parameters.AddWithValue("@description", entity.Description);
                    command.Parameters.AddWithValue("@url", entity.Url);

                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = getSqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "DELETE FROM Categories WHERE CategoryID = @id";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Category> GetAll()
        {
            List<Category> categories = null;
            using (var connection = getSqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM Categories";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    categories = new List<Category>();

                    while (reader.Read())
                    {
                        categories.Add(
                            new Category
                            {
                                CategoryId = int.Parse(reader["CategoryID"].ToString()),
                                Name = reader["CategoryName"].ToString(),
                                // Description = reader["Description"].ToString(),
                                Url = reader["Url"].ToString()
                            }
                        );
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return categories;
        }

        public Category GetById(int id)
        {
            Category category = null;
            using (var connection = getSqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM Categories WHERE CategoryId = @id";
                    SqlCommand command = new SqlCommand(sql, connection);
                    //command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
                    command.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        category = new Category
                        {
                            CategoryId = int.Parse(reader["CategoryID"].ToString()),
                            Name = reader["CategoryName"].ToString(),
                            // Description = reader["Description"].ToString(),
                            Url = reader["Url"].ToString()
                        };
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return category;
        }

        public void Update(Category entity)
        {
            using (var connection = getSqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "UPDATE Categories SET CategoryName = @categoryName, Url = @url WHERE CategoryID = @id";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@categoryName", entity.Name);
                    // command.Parameters.AddWithValue("@description", entity.Description);
                    command.Parameters.AddWithValue("@url", entity.Url);
                    command.Parameters.AddWithValue("@id", entity.CategoryId);

                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Category GetByIdWithProducts(int categoryId)
        {
            Category category = null;
            using (var connection = getSqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM Categories WHERE CategoryID = @id";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@id", categoryId);

                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        category = new Category
                        {
                            CategoryId = int.Parse(reader["CategoryID"].ToString()),
                            Name = reader["CategoryName"].ToString(),
                            Url = reader["Url"].ToString()
                        };
                    }
                    reader.Close();
                    
                    // Second query for taking products with category
                    string sql2 = "SELECT * FROM Products WHERE CategoryID = @id";
                    SqlCommand command2 = new SqlCommand(sql2, connection);
                    command2.Parameters.AddWithValue("@id", categoryId);

                    SqlDataReader reader2 = command2.ExecuteReader();
                    var productList = new List<Product>();
                    while (reader2.Read())
                    {
                        productList.Add(new Product
                        {
                            ProductId = int.Parse(reader2["ProductID"].ToString()),
                            Name = reader2["ProductName"].ToString(),
                            CategoryId = int.Parse(reader2["CategoryId"].ToString()),
                            Price = double.Parse(reader2["Price"]?.ToString()),
                            InStock = int.Parse(reader2["InStock"]?.ToString()) == 1 ? true : false,
                            ImageUrl = reader2["ProductImage"]?.ToString(),
                            Description = reader2["ProductDescription"]?.ToString(),
                            Url = reader2["ProductUrl"]?.ToString()
                        });
                    }
                    reader2.Close();
                    category.Products = productList;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }    
            }  
            return category;
        }
    }
}