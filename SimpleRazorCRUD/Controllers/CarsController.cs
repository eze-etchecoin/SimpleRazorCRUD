using Microsoft.AspNetCore.Mvc;
using SimpleRazorCRUD.DataRepositories.Interfaces;
using SimpleRazorCRUD.EntitiesModels;
using SimpleRazorCRUD.Models;

namespace SimpleRazorCRUD.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarsController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        
        public ActionResult Index()
        {
            var cars = _carRepository.GetAll();
            
            return View("List", cars);
        }
        public ActionResult Details(int id)
        {
            var car = _carRepository.GetOne(id);

            if(car == null)
                return NotFound();

            var model = MapCarToCarModel(car);

            return View(model);
        }

        public ActionResult Add()
        {
            return View(new CarModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var car = _carRepository.GetOne(id);

            if (car == null)
                return NotFound();

            var model = MapCarToCarModel(car);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var car = _carRepository.GetOne(id);

            if (car == null)
                return NotFound();

            var model = MapCarToCarModel(car);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private static CarModel MapCarToCarModel(Car car) => new()
        {
            Id = car.Id,
            Color = car.Color,
            Doors = car.Doors,
            Make = car.Make,
            Model = car.Model,
            Price = car.Price,
            Year = car.Year
        };
    }
}
