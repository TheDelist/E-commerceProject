using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.data.Abstract;
using E_commerceProject.entity;

namespace E_commerceProject.data.Concrete.MSSQL
{
    public class SQLProductRepository : IProductRepository
    {
        private SqlConnection getSQLConnections()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=e-commerce;Integrated Security=SSPI;";
            //DRIVER PROVIDER
            return new SqlConnection(connectionString);

        }
        public void Create(Product entity)
        {
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "insert into products (ProductName,CategoryId,Price,ProductImage,ProductDescription,QuantityPerUnit,ProductUrl) values(@name,@categoryId,@price,@Image,@Description,@Quantity,@Url)";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@price", entity.Price);
                    command.Parameters.AddWithValue("@name", entity.Name);
                    command.Parameters.AddWithValue("@categoryId", entity.CategoryId);
                    command.Parameters.AddWithValue("@Image", entity.ImageUrl);
                    command.Parameters.AddWithValue("@Description", entity.Description);
                    command.Parameters.AddWithValue("@Quantity", 1);
                    command.Parameters.AddWithValue("@Url", entity.Url);

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
                    string sql = "DELETE FROM Products WHERE ProductID = @id";
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
                            Url = reader["ProductUrl"]?.ToString(),
                            IsHome = int.Parse(reader["IsHome"]?.ToString()) == 1 ? true : false,
                            Quantity = int.Parse(reader["QuantityPerUnit"].ToString())
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
                            Category = new Category
                            {
                                Name = reader["CategoryName"].ToString(),
                                CategoryId = int.Parse(reader["CategoryId"].ToString()),
                                Url = reader["Url"]?.ToString(),
                            },
                            Price = double.Parse(reader["Price"]?.ToString()),
                            InStock = int.Parse(reader["InStock"]?.ToString()) == 1 ? true : false,
                            ImageUrl = reader["ProductImage"]?.ToString(),
                            Description = reader["ProductDescription"]?.ToString(),
                            Url = reader["ProductUrl"]?.ToString(),
                            IsHome = int.Parse(reader["IsHome"]?.ToString()) == 1 ? true : false,
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
            List<Product> productList = null;
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    //string sql = "SELECT * FROM Categories INNER JOIN products ON products.CategoryID = Categories.CategoryID where Url IN (@name) ORDER BY ProductID OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY;";
                    string sql = "SELECT * FROM Categories, Products WHERE products.CategoryID = Categories.CategoryID AND Categories.Url IN (@name) ORDER BY ProductID OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY;";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@offset", (page - 1) * pageSize);
                    command.Parameters.AddWithValue("@limit", pageSize);

                    SqlDataReader reader = command.ExecuteReader();
                    productList = new List<Product>();
                    while (reader.Read())
                    {
                        productList.Add(new Product
                        {
                            ProductId = int.Parse(reader["ProductID"].ToString()),
                            Name = reader["ProductName"].ToString(),
                            CategoryId = int.Parse(reader["CategoryId"].ToString()),
                            Price = double.Parse(reader["Price"]?.ToString()),
                            InStock = int.Parse(reader["InStock"]?.ToString()) == 1 ? true : false,
                            ImageUrl = reader["ProductImage"]?.ToString(),
                            Description = reader["ProductDescription"]?.ToString(),
                            Url = reader["ProductUrl"]?.ToString(),

                        });
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
            return productList;
        }

        public void Update(Product entity)
        {
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "UPDATE Products SET ProductName=@productName, CategoryID=@CategoryId, Price=@unitPrice, InStock=@InStock, ProductImage=@Image, ProductDescription=@Description, QuantityPerUnit=@Quantity, ProductUrl=@Url, IsHome=@IsHome WHERE ProductID = @id";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@productName", entity.Name);
                    command.Parameters.AddWithValue("@CategoryId", entity.CategoryId);
                    command.Parameters.AddWithValue("@unitPrice", entity.Price);
                    command.Parameters.AddWithValue("@InStock", entity.InStock);
                    command.Parameters.AddWithValue("@Image", entity.ImageUrl);
                    command.Parameters.AddWithValue("@Description", entity.Description);
                    command.Parameters.AddWithValue("@Quantity", entity.Quantity);
                    command.Parameters.AddWithValue("@Url", entity.Url);
                    command.Parameters.AddWithValue("@IsHome", entity.IsHome);
                    command.Parameters.AddWithValue("@id", entity.ProductId);

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

        public List<Product> GetSearchResult(string searchString)
        {
            List<Product> productList = null;
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "select * from products  WHERE (ProductName LIKE '%' +@search +'%') OR (ProductDescription LIKE '%'+ @search+ '%') ORDER BY ProductName";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@search", searchString);

                    SqlDataReader reader = command.ExecuteReader();
                    productList = new List<Product>();
                    while (reader.Read())
                    {
                        productList.Add(new Product
                        {
                            ProductId = int.Parse(reader["ProductID"].ToString()),
                            Name = reader["ProductName"].ToString(),
                            CategoryId = int.Parse(reader["CategoryId"].ToString()),
                            Price = double.Parse(reader["Price"]?.ToString()),
                            InStock = int.Parse(reader["InStock"]?.ToString()) == 1 ? true : false,
                            ImageUrl = reader["ProductImage"]?.ToString(),
                            Description = reader["ProductDescription"]?.ToString(),
                            Url = reader["ProductUrl"]?.ToString(),
                        });
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
            return productList;
        }

        public List<Product> GetHomePageProducts()
        {
            List<Product> productList = null;
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "select * from products where products.IsHome=1";
                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlDataReader reader = command.ExecuteReader();
                    productList = new List<Product>();
                    while (reader.Read())
                    {
                        productList.Add(new Product
                        {
                            ProductId = int.Parse(reader["ProductID"].ToString()),
                            Name = reader["ProductName"].ToString(),
                            CategoryId = int.Parse(reader["CategoryId"].ToString()),
                            Price = double.Parse(reader["Price"]?.ToString()),
                            InStock = int.Parse(reader["InStock"]?.ToString()) == 1 ? true : false,
                            ImageUrl = reader["ProductImage"]?.ToString(),
                            Description = reader["ProductDescription"]?.ToString(),
                            Url = reader["ProductUrl"]?.ToString(),
                            IsHome = int.Parse(reader["IsHome"]?.ToString()) == 1 ? true : false,
                        });
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
            return productList;
        }

        public int GetCountByCategory(string category)
        {
            int count = 0;
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT COUNT(Categories.CategoryID) FROM Categories INNER JOIN products ON products.CategoryID = Categories.CategoryID where Url IN (@name);";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@name", category);

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
                finally
                {
                    connection.Close();
                }
            }
            return count;
        }
        public List<Product> GetAll()
        {
            List<Product> products = null;
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM Products";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    products = new List<Product>();
                    while (reader.Read())
                    {
                        products.Add(
                            new Product
                            {
                                ProductId = int.Parse(reader["ProductID"].ToString()),
                                Name = reader["ProductName"].ToString(),
                                CategoryId = int.Parse(reader["CategoryId"].ToString()),
                                Price = double.Parse(reader["Price"]?.ToString()),
                                InStock = int.Parse(reader["InStock"]?.ToString()) == 1 ? true : false,
                                ImageUrl = reader["ProductImage"].ToString(),
                                Description = reader["ProductDescription"].ToString(),
                                Url = reader["ProductUrl"]?.ToString(),
                                IsHome = int.Parse(reader["IsHome"]?.ToString()) == 1 ? true : false,
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
            return products;
        }
        public List<Product> GetAll(int? page, int? pageSize)
        {
            List<Product> products = null;
            using (var connection = getSQLConnections())
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM Products ORDER BY ProductID OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY";
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@offset", (page - 1) * pageSize);
                    command.Parameters.AddWithValue("@limit", pageSize);

                    SqlDataReader reader = command.ExecuteReader();
                    products = new List<Product>();
                    while (reader.Read())
                    {
                        products.Add(
                            new Product
                            {
                                ProductId = int.Parse(reader["ProductID"].ToString()),
                                Name = reader["ProductName"].ToString(),
                                CategoryId = int.Parse(reader["CategoryId"].ToString()),
                                Price = double.Parse(reader["Price"]?.ToString()),
                                InStock = int.Parse(reader["InStock"]?.ToString()) == 1 ? true : false,
                                ImageUrl = reader["ProductImage"].ToString(),
                                Description = reader["ProductDescription"].ToString(),
                                Url = reader["ProductUrl"]?.ToString(),
                                IsHome = int.Parse(reader["IsHome"]?.ToString()) == 1 ? true : false,
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
            return products;
        }
    }
}