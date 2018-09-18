using FermeAvicole.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FermeAvicole.Controllers
{
    public class OrderController : baseCustomerController
    {
        FermeDBEntities db = new FermeDBEntities();
        // GET: Order
        public ActionResult ViewOrderList()
        {
            List<OrderVM> ViewOrderList = new List<OrderVM>();
            ViewOrderList = (from o in db.Orders
                         select new OrderVM()
                         {
                             Order_Id=o.Order_Id,
                             User_Id=o.User_Id,
                             Date=o.Date,
                             Total_Amount=o.Total_Amount,
                             OrderStatus_Id=o.OrderStatus_Id,
                             Order_Status=o.OrderStatu.Order_Status,
                             Customer_Name=o.Registration.First_Name
                         }).ToList();
            return View(ViewOrderList);
        }
        public ActionResult ViewOrder(int id)
        {
            OrderVM order = new OrderVM();


            order.OrderItems = (from ot in db.OrderTransactions.Where(m => m.Order_Id == id)
                                select new OrderTransactionVM()
                         {
                             OrderTransaction_Id = ot.Order_Id,
                             Order_Id = ot.Order_Id,
                             Item_Id = ot.Item_Id,
                             Item_Name=ot.Item.Item_Name,
                             Quantity = ot.Quantity,
                             Amount = ot.Amount,
                             Total = ot.Total,
                            
                         }).ToList();

            order.PaymentTransaction = (from pt in db.PaymentTransactions.Where(m => m.Order_Id == id)
                                        select new PaymentTransactionVM()
                                {
                                    PaymentTransaction_Id = pt.PaymentTransaction_Id,
                                    Date= pt.Date,
                                    Order_Id = pt.Order_Id,
                                    User_Id = pt.User_Id,
                                    Amount = pt.Amount,
                                    Payment_Mode = pt.Payment_Mode,

                                }).FirstOrDefault();



            return View(order);
        }
      
    }
}