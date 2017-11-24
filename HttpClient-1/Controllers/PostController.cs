using HttpClient_1.HttpClientHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using HttpClient_1.Models;
using System.Threading.Tasks;
using static HttpClient_1.AuthorisationTypes;

namespace HttpClient_1.Controllers
{
    public class PostController : Controller
    {
        private IAuthenticationService _authService;

        public PostController(IAuthenticationService httpClientService)
        {
            _authService = httpClientService;
        }
        // GET: Post
        public async Task<ActionResult> Index()
        {
            var _clientType = HttpClientTypes.Unauthorized.GetDescription();
            Uri baseAddress = new Uri("https://jsonplaceholder.typicode.com/posts");
            List<Post> items = await _authService.GetList<Post>(_clientType, baseAddress.ToString());
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
