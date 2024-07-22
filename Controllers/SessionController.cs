using Microsoft.AspNetCore.Mvc;

public class SessionController : Controller
{
    public IActionResult SessionTimeout()
    {
        return View();
    }
}