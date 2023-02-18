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
            var cars = _carRepository
                .GetAll()
                .Select(MapCarToCarModel);
            
            return View("List", cars);
        }

        public ActionResult Details(int id)
        {
            var car = _carRepository.GetOne(id);

            if(car == null)
                return NotFound();

            var model = MapCarToCarFormModel(car);

            ViewData["Title"] = $"Car details - {model.Make} {model.Model}";
            ViewData["PageTitle"] = "Car details";
            ViewData["ShowId"] = true;
            ViewData["FormAction"] = "";
            ViewData["DisabledFieldset"] = true;
            ViewData["OkButtonText"] = "";

            return View("Form", model);
        }

        public ActionResult Add()
        {
            ViewData["Title"] = "Add new car";
            ViewData["PageTitle"] = "Add new car";
            ViewData["ShowId"] = false;
            ViewData["FormAction"] = "Add";
            ViewData["DisabledFieldset"] = false;
            ViewData["OkButtonText"] = "Save";

            return View("Form", new CarFormModel());
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

        [Route("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var car = _carRepository.GetOne(id);

            if (car == null)
                return NotFound();

            var model = MapCarToCarFormModel(car);

            ViewData["Title"] = $"Edit car - {model.Make} {model.Model}";
            ViewData["PageTitle"] = "Editing car";
            ViewData["ShowId"] = true;
            ViewData["FormAction"] = "Edit";
            ViewData["DisabledFieldset"] = false;

            return View("Form", model);
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

        [Route("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            var car = _carRepository.GetOne(id);

            if (car == null)
                return NotFound();

            var model = MapCarToCarFormModel(car);

            ViewData["Title"] = $"Delete car - {model.Make} {model.Model}";
            ViewData["PageTitle"] = "Confirm car deletion";
            ViewData["ShowId"] = true;
            ViewData["FormAction"] = "Delete";
            ViewData["DisabledFieldset"] = true;

            return View("Form", model);
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

        private static CarFormModel MapCarToCarFormModel(Car car) => new()
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
