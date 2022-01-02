using E_commerceProject.business.Abstract;
using E_commerceProject.data.Concrete.MSSQL;
using E_commerceProject.entity;
using E_commerceProject.webui.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;



namespace E_commerceProject.webui.Controllers
{
    public class AccountController : Controller
    {

        public static Account Account;
        private IUserService _userservice;

        private IOrderService _orderservice;
        private IProductService _productservice;

        public AccountController(IUserService userservice, IOrderService orderservice, IProductService productservice)
        {
            _userservice = userservice;
            _orderservice = orderservice;
            _productservice = productservice;
        }
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
        public ActionResult Profile()
        {
            if (SQLUserRepository.acc == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var list = _orderservice.getOrderHistoryList(SQLUserRepository.acc.CustomerID);
            foreach (var item in list)
            {
                item.Product = _productservice.GetById(item.ProductId);
            }
            var user = new UserViewModel()
            {
                OrderHistories = list,
                User = SQLUserRepository.acc,

            };
            return View(user);
        }

        SqlConnection connectionString()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=e-commerce;Integrated Security=SSPI;";
            //DRIVER PROVIDER
            return new SqlConnection(connectionString);
        }
        [HttpPost]
        public ActionResult Verify(String Username, String Password)
        {

            Account = _userservice.Login(Username, Password);
            if (Account == null)
            {

                return View("Login");
            }
            else
            {
                ViewData["isAdmin"] = Account.Type.Equals("customer") ? 0 : 1;
                return RedirectToAction("Index", "Home");
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

            Account = _userservice.Login(acc.Username, acc.Password);
            con.Close();
            return RedirectToAction("Index", "Home");


        }

    }
}