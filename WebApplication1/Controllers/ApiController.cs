using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using WebApplication1.EFModels;
using WebApplication1.Models.Dtos;

namespace WebApplication1.Controllers
{
    public class ApiController : Controller
    {
        public readonly MyDBContext _dbContext;

        public ApiController(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            System.Threading.Thread.Sleep(5000);
            return Content("<h2>Content, 你好</h2>","text/html",System.Text.Encoding.UTF8);
        }
             
        public  IActionResult Cities()
        {
            var cities = _dbContext.Addresses.Select(x => x.City).Distinct();
            return Json(cities);
        }
        public IActionResult Districts(string city) 
        {
            var districts = _dbContext.Addresses.Where(x => x.City == city).Select(a=>a.SiteId).Distinct();
            return Json(districts);
        }
        public IActionResult Avatar(int id=2)
        {
            Member? member = _dbContext.Members.Find(id);
            if (member != null)
            {
                byte[] img = member.FileData;
                return File(img, "image/jpg");
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Register(UserDto _userDto)
        {
            if (string.IsNullOrEmpty(_userDto.Name))
            {
                _userDto.Name = "Guest";
            }
            return Content($"Hello {_userDto.Name},U r {_userDto.age},Email {_userDto.Email}");
        }
        public IActionResult CheckAccount(string name)
        {
            
            Member? member = _dbContext.Members.Where(x=>x.Name==name).FirstOrDefault();
            if(member != null)
            {
                return Content($"{name}已被註冊"); 
            }
            return Content($"{name} 註冊成功");
        }
        [HttpPost]
        public IActionResult Spots([FromBody]SearchDto _search) 
        {
            var spots = _search.CategoryId == 0 ? _dbContext.SpotImagesSpots : _dbContext.SpotImagesSpots.Where(x => x.CategoryId == _search.CategoryId);

            if(!string.IsNullOrEmpty(_search.Keyword))
            {
               spots = spots.Where(s=>s.SpotTitle.Contains(_search.Keyword) || s.SpotDescription.Contains(_search.Keyword));
            }

            switch (_search.SortBy)
            {
                case "spotTitle":
                    spots = _search.SortType == "asc" ? spots.OrderBy(s => s.SpotTitle) : spots.OrderByDescending(s => s.SpotTitle);
                    break;
                case "CategoryId":
                    spots = _search.SortType == "asc" ? spots.OrderBy(s => s.CategoryId) : spots.OrderByDescending(s => s.CategoryId);
                    break;
                default:
                    spots = _search.SortType == "asc" ? spots.OrderBy(s => s.SpotId) : spots.OrderByDescending(s => s.SpotId);
                    break;
            }

            int totalCount = spots.Count();
            int pageSize = _search.PageSize ?? 9;
            int totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            int page = _search.Page ?? 1;

            spots = spots.Skip((page-1) * pageSize).Take(pageSize);

            SpotsPagingDto spotsPaging = new SpotsPagingDto();
            spotsPaging.TotalPages = totalPages;
            spotsPaging.SpotsReslut = spots.ToList();

            return Json(spotsPaging);
        }
    }
}
