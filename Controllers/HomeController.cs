using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using C_Sharp_DojoDachi.Models;
// For Sessions:
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace C_Sharp_DojoDachi.Controllers
{

    public static class SessionExtensions
    {
        // We can call ".SetObjectAsJson" just like our other session set methods, by passing a key and a value
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            // This helper function simply serializes theobject to JSON and stores it as a string in session
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        
        // generic type T is a stand-in indicating that we need to specify the type on retrieval
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            // Upon retrieval the object is deserialized based on the type we specified
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }

    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            // Dachi newDachi = new Dachi();
            if(HttpContext.Session.GetObjectFromJson<Dachi>("DachiNew") == null)
            {
                HttpContext.Session.SetObjectAsJson("DachiNew", new Dachi());
            }    

            ViewBag.DachiNew = HttpContext.Session.GetObjectFromJson<Dachi>("DachiNew");
            if(ViewBag.DachiNew.fullness < 1 || ViewBag.DachiNew.happiness < 1 )
            {
                ViewBag.DachiNew.status = "Dead";
            } 
            if(ViewBag.DachiNew.fullness > 100 && ViewBag.DachiNew.happiness > 100 && ViewBag.DachiNew.energy > 100)
            {
                ViewBag.DachiNew.status = "Champion";
            }
            // return View("Index",newDachi);
            return View("Index");
        }

        [HttpGet]
        [Route("feed")]
        public IActionResult Feed()
        {
            Dachi DachiCurrent = HttpContext.Session.GetObjectFromJson<Dachi>("DachiNew");
            if(DachiCurrent.meals > 0)
            {
                DachiCurrent.feed();
            } 
            if(DachiCurrent.meals == 0)
            {
                DachiCurrent.status = "No Meals! You can not fee your Dachi!";
            }
            HttpContext.Session.SetObjectAsJson("DachiNew", DachiCurrent);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
