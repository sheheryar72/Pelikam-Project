using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pelikan_Project.DataManager;
using Pelikan_Project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Pelikan_Project.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddInspection([FromBody] Inspection inspection)
        {
            InspectionManager im = new InspectionManager();
            string is_inserted = "";
            foreach (var operation in inspection.Operation_Obj)
            {
                inspection.Operation = operation.Key;
                inspection.DEF = operation.Value;
                is_inserted = im.AddInspection(inspection);
            }

            if (is_inserted == null)
            {
                return Ok(new { status = 400, data = (object)null });
            }

            return Ok(new { status = 200, data = is_inserted });
        }
        [HttpGet]
        public IActionResult GetAllInspectionByDate([FromQuery] DateTime inspection_Date)
        {
            var current_date = DateTime.Now;
            InspectionManager im = new InspectionManager();
            var data = im.GetAllInspectionByDate(current_date);
            return Ok(data);
        }
    }
}
