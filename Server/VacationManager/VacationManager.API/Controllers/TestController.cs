using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VacationManager.API.Controllers
{
    [ApiController]
    [Route("Test")]
    public class TestController : Controller
    {
        [HttpPost]
        [Route("Test")]
        public string Test(TestRequest req)
        {
            return req.Test;
        }
    }
}
