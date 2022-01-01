using E_commerceProject.webui.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;



namespace E_commerceProject.webui.Controllers
{
    public class AccountController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        //GET : Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        SqlConnection connectionString()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=e-commerce;Integrated Security=SSPI;";
            //DRIVER PROVIDER
            return new SqlConnection(connectionString);
        }
        [HttpPost]
        public ActionResult Verify(Account acc)
        {
            con = connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from Customers where Username='" + acc.Username + "' and Password= '" + acc.Password + "'";
            dr = com.ExecuteReader();


            if (dr.Read())
            {
                ViewBag.message = "success";

                acc.Name = dr["Name"].ToString();
                acc.Surname = dr["Surname"].ToString();
                acc.Email = dr["Email"].ToString();
                if (dr["Sex"] is bool)
                {
                    acc.Sex = (bool)dr["Sex"];

                }
                acc.Type = dr["Type"].ToString();

                if (dr["BirthDate"] is DateTime)
                {
                    acc.BirthDate = (DateTime)dr["BirthDate"];

                }

                acc.Address = dr["Address"].ToString();
                acc.Phone = dr["Phone"].ToString();


                /*  CurrentAccount(acc); */
                /*  kimin giriş yaptıgını ekle */
                var profile = new CurrentAccount(acc);
                Console.WriteLine("welcome" + profile.Profile.Name);
                con.Close();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                con.Close();
                return View("Login");
            }
        }

        [HttpPost]
        public ActionResult VerifyReg(Account acc)
        {
            con = connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "INSERT INTO Customers (Name,Surname,Email,Sex,Type,BirthDate,Address,Phone,Password,Username) VALUES ( '" + acc.Name + "',  '" + acc.Surname + "', '" + acc.Email + "', '" + acc.Sex + "', '" + "customer" + "',  '" + acc.BirthDate.Year + "-" + acc.BirthDate.Month + "-" + acc.BirthDate.Day + "','" + acc.Address + "', '" + acc.Phone + "','" + acc.Password + "','" + acc.Username + "')";
            dr = com.ExecuteReader();
            var profile = new CurrentAccount(acc);

            Console.WriteLine("welcome" + profile.Profile.Name);
            con.Close();
            return RedirectToAction("Index", "Home");


        }

    }
}