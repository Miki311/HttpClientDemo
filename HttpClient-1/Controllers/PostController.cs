using HttpClient_1.HttpClientHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using HttpClient_1.Models;
using System.Threading.Tasks;

namespace HttpClient_1.Controllers
{
    public class PostController : Controller
    {
        private IHttpClientService _httpClientService;

        public PostController(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }
        // GET: Post
        public async Task<ActionResult> Index()
        {
            List<Post> items = await _httpClientService.GetListItems<Post>("posts");
            return View(items);
        }

        // GET: Post/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Post/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Post/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
