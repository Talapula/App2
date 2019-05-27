using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AppController : Controller
    {
        // GET: App
        public ActionResult Index()
        {
            // Variable to store the list returned by WebApi GetPersonDetails method
            IEnumerable<PersonDetail> list = null;

            using (var client = new HttpClient())
            {
                // Url of Webapi project
                client.BaseAddress = new Uri("http://localhost:60016/api/");
                //HTTP GET
                var responseTask = client.GetAsync("PersonDetails");  // PersonDetails is the WebApi controller name
                // wait for task to complete
                responseTask.Wait();
                // retrieve the result
                var result = responseTask.Result;
                // check the status code for success
                if (result.IsSuccessStatusCode)
                {
                    // read the result
                    var readTask = result.Content.ReadAsAsync<IList<PersonDetail>>();
                    readTask.Wait();
                    // fill the list vairable created above with the returned result
                    list = readTask.Result;
                }
                else //web api sent error response 
                {
                    list = Enumerable.Empty<PersonDetail>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PersonDetail obj)
        {
            using (var client = new HttpClient())
            {
                // Url of Webapi project
                client.BaseAddress = new Uri("http://localhost:60016/api/");
                //HTTP POST
                var postTask = client.PostAsJsonAsync<PersonDetail>("PersonDetails", obj);
                // wait for task to complete
                postTask.Wait();
                // retrieve the result
                var result = postTask.Result;
                // check the status code for success
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            // Add model error
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            // send the view back with model error
            return View(obj);
        }

        public ActionResult Edit(int id)
        {
            // variable to hold the person details retrieved from WebApi
            PersonDetail person = null;

            using (var client = new HttpClient())
            {
                // Url of Webapi project
                client.BaseAddress = new Uri("http://localhost:60016/api/");
                //HTTP GET
                var responseTask = client.GetAsync("PersonDetails/" + id.ToString());
                // wait for task to complete
                responseTask.Wait();
                // retrieve the result
                var result = responseTask.Result;
                // check the status code for success
                if (result.IsSuccessStatusCode)
                {
                    // read the result
                    var readTask = result.Content.ReadAsAsync<PersonDetail>();
                    readTask.Wait();
                    // fill the person vairable created above with the returned result
                    person = readTask.Result;
                }
            }
            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(PersonDetail obj)
        {
            using (var client = new HttpClient())
            {
                // Url of Webapi project
                client.BaseAddress = new Uri("http://localhost:60016/api/");
                //HTTP POST
                var putTask = client.PutAsJsonAsync<PersonDetail>("PersonDetails", obj);
                // wait for task to complete
                putTask.Wait();
                // retrieve the result
                var result = putTask.Result;
                // check the status code for success
                if (result.IsSuccessStatusCode)
                {
                    // Return to Index
                    return RedirectToAction("Index");
                }
            }
            // Add model error
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            // send the view back with model error 
            return View(obj);
        }

        public ActionResult Details(int id)
        {
            // variable to hold the person details retrieved from WebApi
            PersonDetail person = null;

            using (var client = new HttpClient())
            {
                // Url of Webapi project
                client.BaseAddress = new Uri("http://localhost:60016/api/");
                //HTTP GET
                var responseTask = client.GetAsync("PersonDetails?id=" + id.ToString());
                // wait for task to complete
                responseTask.Wait();
                // retrieve the result
                var result = responseTask.Result;
                // check the status code for success
                if (result.IsSuccessStatusCode)
                {
                    // read the result
                    var readTask = result.Content.ReadAsAsync<PersonDetail>();
                    readTask.Wait();
                    // fill the person vairable created above with the returned result
                    person = readTask.Result;
                }
            }
            return View(person);
        }

        public ActionResult Delete(int id)
        {
            // variable to hold the person details retrieved from WebApi
            PersonDetail person = null;

            using (var client = new HttpClient())
            {
                // Url of Webapi project
                client.BaseAddress = new Uri("http://localhost:60016/api/");
                //HTTP Delete
                var responseTask = client.DeleteAsync("PersonDetails/" + id.ToString());
                // wait for task to complete
                responseTask.Wait();
                // retrieve the result
                var deleteTask = responseTask.Result;
                // check the status code for success
                if (deleteTask.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

    }    
}