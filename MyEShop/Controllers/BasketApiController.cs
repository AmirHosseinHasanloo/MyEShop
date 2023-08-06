using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MyEShop.Controllers
{
    public class BasketApiController : ApiController
    {
        // GET: api/BasketApi
        public int Get()
        {
            List<DataLayer.ShopCartItem> list = new List<DataLayer.ShopCartItem>();
            var Sessions = HttpContext.Current.Session;
            if (Sessions["ShopCart"] != null)
            {
                list = Sessions["ShopCart"] as List<DataLayer.ShopCartItem>;
            }
            return list.Sum(l => l.Count);
        }

        // GET: api/BasketApi/5
        public int Get(int id)
        {
            List<DataLayer.ShopCartItem> list = new List<DataLayer.ShopCartItem>();
            var Sessions = HttpContext.Current.Session;
            if (Sessions["ShopCart"] != null)
            {
                list = Sessions["ShopCart"] as List<DataLayer.ShopCartItem>;
            }
            if (list.Any(P => P.ProductID == id))
            {
                int index = list.FindIndex(p => p.ProductID == id);
                list[index].Count += 1;
            }
            else
            {
                list.Add(new DataLayer.ShopCartItem()
                {
                    ProductID = id,
                    Count = 1
                });
            }
            Sessions["ShopCart"] = list;
            return Get();
        }
    }
}
