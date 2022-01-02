using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using E_commerceProject.business.Abstract;
using E_commerceProject.data.Concrete.MSSQL;
using E_commerceProject.entity;
using E_commerceProject.webui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_commerceProject.webui.Controllers
{
    public class CartController : Controller
    {
        private IProductService _productService;
        private IUserService _userService;
        private IOrderService _orderService;
        private IShipperService _shipperService;
        public CartController(IProductService productService, IUserService userService, IOrderService orderService, IShipperService shipperService)
        {
            _productService = productService;
            _userService = userService;
            _orderService = orderService;
            _shipperService = shipperService;

        }

        public IActionResult CartList()
        {

            if (SQLUserRepository.acc == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var orders = _orderService.GetsById(SQLUserRepository.acc.CustomerID);

            List<Product> products = new List<Product>();
            foreach (var a in orders)
            {
                products.Add(_productService.GetById(a.ProductId));
            }
            var shoppingCart = new ShoppingCartListViewModel()
            {

                Products = products,
                Orders = orders,
                User = SQLUserRepository.acc
            };
            return View(shoppingCart);
        }
        public IActionResult decreaseQuantity(int UserId, int ProductId, int Quantity)
        {
            Quantity--;
            _orderService.SetQuantity(UserId, ProductId, Quantity);
            return RedirectToAction("CartList");
        }
        public IActionResult increaseQuantity(int UserId, int ProductId, int Quantity)
        {
            Quantity++;
            _orderService.SetQuantity(UserId, ProductId, Quantity);
            return RedirectToAction("CartList");
        }
        public IActionResult deleteCartProduct(int UserId, int ProductId)
        {

            _orderService.DeleteOrder(UserId, ProductId);
            return RedirectToAction("CartList");
        }
        public IActionResult Checkout(int TotalSum)
        {
            var Checkout = new CheckoutModelView()
            {

                Shippers = _shipperService.GetAll(),
                User = SQLUserRepository.acc,
                TotalSum = TotalSum,
            };
            return View(Checkout);
        }
        public IActionResult OrderCompleted()
        {
            if (SQLUserRepository.acc == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var orders = _orderService.GetsById(SQLUserRepository.acc.CustomerID);

            List<Product> products = new List<Product>();
            foreach (var a in orders)
            {

                _orderService.CreateOrderHistory(new OrderHistory()
                {
                    Date = DateTime.Now.ToString("yyyy-MM-dd"),
                    ProductId = a.ProductId,
                    UserId = a.UserId,
                    Quantity = a.Quantity

                });
                _orderService.DeleteOrder(a.UserId, a.ProductId);

            }

            return View();
        }
        public IActionResult addToCart(int ProductId, int Quantity, String PreviousPage)

        {

            var order = new Order()
            {
                ProductId = ProductId,
                Quantity = Quantity,
                UserId = SQLUserRepository.acc.CustomerID,
            };

            _orderService.Create(order);
            if (PreviousPage.Equals("ProductsPage"))
            {
                return RedirectToAction("List", "Product");
            }
            else
            {
                return RedirectToAction("Details", "Product", new { @url = PreviousPage });
            }

        }
    }

}