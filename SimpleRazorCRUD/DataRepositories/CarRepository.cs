using SimpleRazorCRUD.DataRepositories.Interfaces;
using SimpleRazorCRUD.EntitiesModels;

namespace SimpleRazorCRUD.DataRepositories
{
    public class CarRepository : ICarRepository
    {
        private List<Car> cars = new()
        {
            new Car { Id = 1, Make = "Audi", Model = "R8", Year = 2018, Doors = 2, Color = "Red", Price = 79995 },
            new Car { Id = 2, Make = "Tesla", Model = "3", Year = 2018, Doors = 4, Color = "Black", Price = 54995 },
            new Car { Id = 3, Make = "Porsche", Model = "911", Year = 2020, Doors = 2, Color = "White", Price = 155000 },
            new Car { Id = 4, Make = "Mercedes-Benz", Model = "GLE 63S", Year = 2021, Doors = 5, Color = "Blue", Price = 83995 },
            new Car { Id = 5, Make = "BMW", Model = "X6 M", Year = 2020, Doors = 5, Color = "Silver", Price = 62995 }
        };

        public IEnumerable<Car> GetAll()
        {
            return cars;
        }

        public Car? GetOne(int id)
        {
            return cars.FirstOrDefault(x => x.Id == id);
        }

        public void Delete(Car entity)
        {
            cars = cars
                .Where(x => x.Id != entity.Id)
                .ToList();
        }

        public void Insert(Car entity)
        {
            entity.Id = GetNextId();
            
            cars.Add(entity);
        }

        public void Update(Car entity)
        {
            var existing = cars.FirstOrDefault(x => x.Id == entity.Id);

            if (existing == null)
                return;

            existing.Make = entity.Make;
            existing.Model = entity.Model;
            existing.Year = entity.Year;
            existing.Doors = entity.Doors;
            existing.Color = entity.Color;
            existing.Price = entity.Price;
        }

        private int GetNextId()
        {
            return cars
                .OrderBy(x => x.Id)
                .LastOrDefault()?
                .Id + 1 ?? 1;
        }
    }
}
