using KattHem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace KattHem.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            ViewData["About"] = "This website was made by Åsa Lodesjö as a part of a course called ASP.NET med C#";
            ViewBag.String = "It's built with the purpose of lerning ASP.NET using MVC";
            return View();
        }
        public IActionResult Cats()
        {
            //Read JSON file

            var JsonStr = System.IO.File.ReadAllText("CatModel.json");
            var JsonObj = JsonConvert.DeserializeObject<IEnumerable<CatModel>>(JsonStr);

            return View(JsonObj);
        }

        public IActionResult AddNew()
        {
            //Read session variable
            string NewCat = HttpContext.Session.GetString("CatName");
            ViewBag.Name = NewCat;
            string NewMessage = HttpContext.Session.GetString("AddedMessage");
            ViewBag.Message = NewMessage;

            return View();
        }

        [HttpPost]
        public IActionResult AddNew(CatModel model)
        {
            if (ModelState.IsValid)
            {
                // Read existing file
                var JsonStr = System.IO.File.ReadAllText("CatModel.json");
                var JsonObj = JsonConvert.DeserializeObject<List<CatModel>>(JsonStr);
                JsonObj.Add(model);

                // Store name input in session variable
                ViewBag.name = model.Name;
                string CatName = ViewBag.name;
                HttpContext.Session.SetString("CatName", CatName);
                ViewBag.message = " has been added";
                string AddedMessage = ViewBag.message;
                HttpContext.Session.SetString("AddedMessage", AddedMessage);

                // Convert and store input into Json file
                System.IO.File.WriteAllText("CatModel.json", JsonConvert.SerializeObject(JsonObj, Formatting.Indented));

                // Clear form
                ModelState.Clear();

                return View();
            }

            else

            { 
                return View();
            }
        }
        public List<CatModel> GetData()
        {
            // Open the JSON-file and read all of its contents
            string json = System.IO.File.ReadAllText("CatModel.json");

            // Deserialize the JSON into a list
            var data = JsonConvert.DeserializeObject<List<CatModel>>(json);

            return data;
        }

        public void SaveData(List<CatModel> cats)
        {
            // Serialize the updated data
            var UpdateCat = JsonConvert.SerializeObject(cats);

            // Write the updated data to file
            System.IO.File.WriteAllText("CatModel.json", UpdateCat);
        }


        [Route("Home/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            // Fetch data from JSON file
            List<CatModel> cats = GetData();

            // Find the cat with a given ID
            CatModel m = cats.Find(x => x.Id == id);

            // Remove the matching ID
            cats.Remove(m);

            SaveData(cats);

            // Return view
            return View();
        }

    }
}
