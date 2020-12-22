using IntroduccionMvcNinja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

/* action metodos del controlador que devuelve contenido al usuario final*/
namespace IntroduccionMvcNinja.Controllers
{ 
    
    public class HomeController : Controller
    {
        ObjectCache cache = MemoryCache.Default;
        List<Customer> customers;
        
        public HomeController()
        {
            customers = cache["customers"] as List<Customer>;
            if(customers == null)
            {
                customers = new List<Customer>();
            }  
        }
        public void savecache()
        {
            cache["customers"] = customers;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.MySuperProperty = "Hola esta es una prueba";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ViewCustomer(string id) 
        {
            Customer customer = customers.FirstOrDefault(c=> c.Id == id)
            if (customer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customer);
            }
            
             
        }

        public ActionResult addCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addcustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid().ToString();
            customers.Add(customer);
            savecache();
            return RedirectToAction("CustomerList");
        }


     public ActionResult CustomerList()
        {
          
            return View(customers);
        }
    }
}