using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
namespace MyEShop.Controllers
{
    public class ShopCartController : Controller
    {
        private UnitOfWork _db = new UnitOfWork();
        // GET: ShopCart

        public ActionResult ShowCart()
        {
            List<DataLayer.ShopCartItemViewModel> list = new List<DataLayer.ShopCartItemViewModel>();
            if (Session["ShopCart"] != null)
            {
                List<DataLayer.ShopCartItem> listShop = (List<DataLayer.ShopCartItem>)Session["ShopCart"];

                foreach (var item in listShop)
                {
                    var product = _db.ProductsRepository.GetAll().Where(p => p.ProductID == item.ProductID).Select(p => new
                    {
                        p.Title,
                        p.ImageName
                    }).Single();

                    list.Add(new DataLayer.ShopCartItemViewModel()
                    {
                        Count = item.Count,
                        ProductID = item.ProductID,
                        Title = product.Title,
                        ImageName = product.ImageName
                    });
                }
            }
            return PartialView(list);
        }
        public ActionResult Index()
        {
            return View();
        }

        List<DataLayer.ShowOrderViewModel> GetOrderList()
        {
            List<DataLayer.ShowOrderViewModel> list = new List<ShowOrderViewModel>();

            if (Session["ShopCart"] != null)
            {
                List<DataLayer.ShopCartItem> cartItem = Session["ShopCart"] as List<DataLayer.ShopCartItem>;
                foreach (var item in cartItem)
                {
                    var product =_db.ProductsRepository.GetAll().Where(p => p.ProductID == item.ProductID).Select(p => new
                    {
                        p.Title,
                        p.ImageName,
                        p.Price
                    }).Single();
                    list.Add(new DataLayer.ShowOrderViewModel()
                    {
                        Count = item.Count,
                        ProductID = item.ProductID,
                        Price = product.Price,
                        ImageName = product.ImageName,
                        Title = product.Title,
                        // Sum of order => جمع فاکتور
                        sum = item.Count * product.Price
                    });
                }
            }
            return list;
        }
        public ActionResult ListOrder()
        {
            return PartialView(GetOrderList());
        }

        public ActionResult OrderCommands(int id, int count)
        {
            List<DataLayer.ShopCartItem> cartItem = Session["ShopCart"] as List<DataLayer.ShopCartItem>;
            int index = cartItem.FindIndex(p => p.ProductID == id);
            if (count == 0)
            {
                cartItem.RemoveAt(index);
            }
            else
            {
                cartItem[index].Count = count;
            }
            return PartialView("ListOrder", GetOrderList());
        }

        [Authorize]
        public ActionResult Payment()
        {
            int UserId = _db.UsersRepository.GetAll().Single(u => u.UserName == User.Identity.Name).UserID;
            Orders order = new Orders()
            {
                UserID = UserId,
                Date = DateTime.Now,
                IsFinally = false,
            };
            _db.Orders_Repository.Insert(order);

            var ListDetail = GetOrderList();
            foreach (var item in ListDetail)
            {
                _db.OrderDetails_Repository.Insert(new OrderDetails()
                {
                    Count = item.Count,
                    OrderID = order.OrderID,
                    Price = item.Price,
                    ProductID = item.ProductID
                });
            }
            _db.Save();

            // TODO : Online Payment

            return null;
        }
    }
}