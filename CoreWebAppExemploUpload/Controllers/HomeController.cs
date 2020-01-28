using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebAppExemploUpload.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreWebAppExemploUpload.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(new Cliente { ClienteId = 1, Nome = "Diogo Rodrigo" });
        }

        [HttpPost]
        public IActionResult Index(Cliente cliente)
        { 
            ViewBag.Imagem = ConverterToBase64(cliente.Foto);

            return View("ExibeDados", cliente);
        }


        private string ConverterToBase64(Microsoft.AspNetCore.Http.IFormFile file)
        {
            using (var ms = new System.IO.MemoryStream())
            {
                file.CopyTo(ms);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}
