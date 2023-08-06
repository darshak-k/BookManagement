using BookWeb.Data;
using BookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _context.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Display Order cannot exactly match the same");
            }

            if(ModelState.IsValid)
            {
                _context.Categories.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

		public IActionResult Edit(int? id)
		{
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var CategoryFromDb = _context.Categories.Find(id);
			//var CategoryFromDbFromFirst = _context.Categories.FirstOrDefault(u => u.Id == id);
			//var CategoryFromDbFromSingle = _context.Categories.SingleOrDefault(u => u.Id == id);

            if(CategoryFromDb == null)
            {
				return NotFound();
			}
			return View(CategoryFromDb);
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("CustomError", "Display Order cannot exactly match the same");
			}

			if (ModelState.IsValid)
			{
				_context.Categories.Update(obj);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(obj);
		}


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var CategoryFromDb = _context.Categories.Find(id);
            //var CategoryFromDbFromFirst = _context.Categories.FirstOrDefault(u => u.Id == id);
            //var CategoryFromDbFromSingle = _context.Categories.SingleOrDefault(u => u.Id == id);

            if (CategoryFromDb == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(CategoryFromDb);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
