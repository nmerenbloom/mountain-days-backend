using DotNetPracticeApp.Data;
using DotNetPracticeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPracticeApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MountainDaysController : ControllerBase
    {
        private readonly ApiContext _context;

        public MountainDaysController(ApiContext context)
        {
            _context = context; 
        }
        
        [HttpPost]
        public JsonResult CreateEdit(MountainDay mountainDay)
        {
            if (mountainDay.Id == 0)
            {

                _context.MountainDays.Add(mountainDay);
            } else
            {
                var mountainDayInDb = _context.MountainDays.Find(mountainDay.Id);
                if (mountainDayInDb == null)
                {
                    return new JsonResult(NotFound());
                }

                //System.Diagnostics.Debug.WriteLine("Hit");
                //mountainDayInDb = mountainDay;
                _context.Entry(mountainDayInDb).CurrentValues.SetValues(mountainDay);

            }
            _context.SaveChanges();

            return new JsonResult(Ok(mountainDay));
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.MountainDays.Find(id);
            if (result == null)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.MountainDays.Find(id);
            if (result == null)
                return new JsonResult(NotFound("day not found"));

            _context.MountainDays.Remove(result);
            _context.SaveChanges();

            return new JsonResult(Ok());
        }

        [HttpGet()]
        public JsonResult GetAll()
        {
            var result = _context.MountainDays.ToList();
            if (result.Count > 0)
            {
                result.Sort((x, y) => DateTime.Compare(y.Date, x.Date));
            }
            
            return new JsonResult(Ok(result));
        }
    }
}
