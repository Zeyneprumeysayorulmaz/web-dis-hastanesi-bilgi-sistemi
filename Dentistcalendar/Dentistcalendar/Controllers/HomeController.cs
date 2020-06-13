using Microsoft.AspNetCore.Mvc;

namespace Dentistcalendar.Controllers
{
    public class HomeController : Controller
    {

        // http istegi geldiginde localhost 8080/home/index önce controller da home kelimesini arayacak daha sonra home controller ın index metodunu calıştıracak
        // index ten bir view donecek
        public IActionResult Index()
        {
            return View();
            //redirect kullanımı
            //return RedirectToAction("Deneme");
            //return Redirect("/home/Deneme");
        }


        public IActionResult Privacy()
        {
            return View();
        }
    }
}