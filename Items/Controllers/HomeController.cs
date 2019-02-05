using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Items.Models;
using Microsoft.EntityFrameworkCore;

namespace Items.Controllers
{

   
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ItemdataContext context = new ItemdataContext();
            return View(context.Item);
        }
        public IActionResult Lis()
        {
            ViewData["Message"] = "Lisäyssivu.";

            return View();
        }

        public async Task<IActionResult> Hae(string searchString)  // LISÄÄ ELSE, jos stringiä ei löydy
        {
            ItemdataContext context = new ItemdataContext();
            var tavara = from t in context.Item
                         select t;

            if (!String.IsNullOrEmpty(searchString))
            {
                tavara = tavara.Where(s => s.ItemName.Contains(searchString));
            }

            return View(await tavara.ToListAsync());
        }

        
        public async Task<IActionResult> Lisaa(string itemName, string itemLocation, string itemClass)
        {

            ItemdataContext context = new ItemdataContext();

            Item tavara = new Item() { ItemName = itemName, ItemLocation = itemLocation, ItemClass = itemClass };

            if (tavara.ItemName != null && tavara.ItemLocation != null && tavara.ItemClass != null)
            {
                context.Item.Add(tavara);
                ViewData["ItemName"] = itemName;
                await context.SaveChangesAsync();
                // return Ok(tavara);
                //return View(await context.SaveChangesAsync());
                return View();
            }
            else
            {
                return RedirectToAction("Lis");
            }
        }

        //public IActionResult Hae(string itemname)
        //{
        //    ViewData["Message"] = "Hakulomake";
        //    // UUSI
        //    ItemdataContext context = new ItemdataContext();
        //    if (itemname != null)
        //    {
        //        Item tavara = context.Item.Find(itemname);
        //        return View(tavara);
        //    }
        //    return null;
        //    //return View();
        //}


        public IActionResult Poista(int? itemId)  // hakasuluisssa tieto, mistä parametrit tulevat
        {
            ItemdataContext context = new ItemdataContext();

            bool OnkoOlemassa = context.Item.Any(u => u.ItemId == itemId);
            switch (OnkoOlemassa)
            {
                case false:
                    break;
                default:
                    Item tavara = context.Item.Find(itemId);
                    context.Remove(tavara);
                    context.SaveChanges();
                    ViewData["ItemId"] = itemId;
                    ViewData["Tavara"] = tavara.ItemName;
                    return View();
            }

            ViewData["ItemId"] = itemId;
            return View("JoPoistettu");

        }



        //public IActionResult Poista(int? itemId)  // hakasuluisssa tieto, mistä parametrit tulevat
        //{
        //    ItemdataContext context = new ItemdataContext();

        //    bool OnkoOlemassa = context.Item.Any(u => u.ItemId == itemId);  
        //    switch (OnkoOlemassa)
        //    {
        //        case false:
        //            break;
        //        default:
        //            Item tavara = context.Item.Find(itemId);
        //            context.Remove(tavara); 
        //            context.SaveChanges();
        //            ViewData["ItemId"] = itemId;
        //            ViewData["Tavara"] = tavara.ItemName;
        //            return View();         
        //    }

        //    ViewData["ItemId"] = itemId;
        //    return View("JoPoistettu");

        //}


        //public IActionResult Lisaa()  // parametrina oletettu tuleva muoto
        //{
        //    ItemdataContext context = new ItemdataContext();

        //    // Create a new Order object.
        //    Item tavara = new Item
        //    {
        //        ItemId = 1,
        //        ItemName = "laukku",
        //        ItemLocation = "komero",
        //        ItemBox = "-",
        //        ItemDescription = "-",
        //        ItemOwner = "-",
        //        ItemClass = "-"
        //    };

        //    // Add the new object to the Orders collection.
        //    context.Item.InsertOnSubmit(tavara);

        //    // Submit the change to the database.
        //    try
        //    {
        //        context.SubmitChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        // Make some adjustments.
        //        // ...
        //        // Try again.
        //        context.SubmitChanges();
        //    }

        //    //context.Item.Add(tavara);
        //    //context.SaveChanges();
        //    //ViewData["Message"] = "Lisää tietokantaan.";
        //    //return View();

        //}



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
      

    }
}
