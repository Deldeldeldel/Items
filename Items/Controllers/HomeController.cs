﻿using System;
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

        public async Task<IActionResult> Hae(string searchString)
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

        public IActionResult Lisaa()
        {
            ViewData["Message"] = "Lisää tietokantaan.";

            return View();
        }

        public IActionResult Poista()
        {
            ViewData["Message"] = "Poista tietokannasta.";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
      

    }
}
