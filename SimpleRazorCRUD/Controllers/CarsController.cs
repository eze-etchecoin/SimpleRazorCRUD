using Microsoft.AspNetCore.Mvc;
using SimpleRazorCRUD.DataRepositories.Interfaces;
using SimpleRazorCRUD.EntitiesModels;
using SimpleRazorCRUD.Models.Cars;

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
        public ActionResult Add(CarModel carModel)
        {
            var car = MapCarModelToCar(carModel);
            _carRepository.Insert(car);

            return RedirectToAction(nameof(Index));
        }

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
            ViewData["OkButtonText"] = "Save";

            return View("Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarModel carModel)
        {
            var car = MapCarModelToCar(carModel);
            _carRepository.Update(car);

            return RedirectToAction(nameof(Index));
        }

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
            ViewData["OkButtonText"] = "OK";

            return View("Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(CarModel carModel)
        {
            var car = MapCarModelToCar(carModel);
            _carRepository.Delete(car);

            return RedirectToAction(nameof(Index));
        }


        public ActionResult GuessPrice(int id)
        {
            var car = _carRepository.GetOne(id);

            if (car == null)
                return NotFound();

            var model = MapCarToCarModel(car);

            ViewData["Title"] = $"Guess price of car - {model.Make} {model.Model}";

            return View(model);
        }

        [HttpPost]
        public PriceGuessResponse PriceGuess([FromBody] PriceGuessRequest request)
        {
            var response = new PriceGuessResponse();

            var car = _carRepository.GetOne(request.CarId);
            if (car == null)
                return response;

            var guessValue = int.TryParse(request.Value, out var value)
                ? value
                : 0;

            var difference = Math.Abs(car.Price - guessValue);

            response.Result = difference <= 5000;

            return response;
        }

        // This methods were created to map data between Models from views and Entities.
        // A mapping library like Automapper could be use instead.

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

        private static Car MapCarModelToCar(CarModel carModel) => new()
        {
            Id = carModel.Id,
            Color = carModel.Color,
            Doors = carModel.Doors,
            Make = carModel.Make,
            Model = carModel.Model,
            Price = carModel.Price,
            Year = carModel.Year
        };
    }
}
