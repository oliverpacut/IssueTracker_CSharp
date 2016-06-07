using AutoMapper;
using System.Web.Mvc;

namespace WebApp
{
    public class BaseController : Controller
    {
        public virtual void RegisterMapping(IConfiguration mapper) { }
    }
}