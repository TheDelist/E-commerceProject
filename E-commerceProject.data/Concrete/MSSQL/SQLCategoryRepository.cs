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
            // driver, provider
            return new SqlConnection(connectionString);
        }
        public void Create(Category entity)
        {
            using (var connection = getSqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "INSERT INTO Categories (CategoryName, Description, Url) VALUES(@categoryName, @description, @url)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@categoryName", entity.Name);
                    command.Parameters.AddWithValue("@description", entity.Description);
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
                                Description = reader["Description"].ToString(),
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
                    command.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        category = new Category
                        {
                            CategoryId = int.Parse(reader["CategoryID"].ToString()),
                            Name = reader["CategoryName"].ToString(),
                            Description = reader["Description"].ToString(),
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
                    string sql = "UPDATE Categories SET CategoryName = @categoryName, Description = @description, Url = @url WHERE CategryID = @id";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@categoryName", entity.Name);
                    command.Parameters.AddWithValue("@description", entity.Description);
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
    }
}