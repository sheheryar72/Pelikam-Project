using Microsoft.AspNetCore.Mvc;
using Pelikan_Project.DataManager;
using Pelikan_Project.Models;

namespace Pelikan_Project.Controllers
{
    public class OperatorController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AuthenticateOperator([FromBody] Operator oper)
        {
            OperatorManager om = new OperatorManager();
            var op = om.AuthenticateOperator(oper.OperatorID, oper.Password);
            if (op.OperatorID == 0)
            {
                return Ok(new { status = 400, data = (object)null });
            }
            return Ok(new { status = 200, data = op });
        }

        [HttpPost]
        public IActionResult InserOperator([FromBody] Operator oper)
        {
            OperatorManager om = new OperatorManager();
            var op = om.AddOperator(oper);
            if(op ==  null)
            {
                return Ok(new { status = 400, data = (object)null });

            }
            return Ok(new { status = 200, data = op });
        }
        [HttpGet]
        public IActionResult GetOperatorByID([FromQuery] int OperatorID)
        {
            OperatorManager om = new OperatorManager();
            var op = om.GetOperatorByID(OperatorID);
            if (op.OperatorID == null)
            {
                return Ok(new { status = 400, data = (object)null });
            }
            return Ok(new { status = 200, data = op });
        }
        [HttpPost]
        public IActionResult AddInspection2(Inspection inspection)
        {
            return Ok();
        }
    }
}
