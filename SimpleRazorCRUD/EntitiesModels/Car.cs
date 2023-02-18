namespace SimpleRazorCRUD.EntitiesModels
{
    public class Car : BaseEntity
    {
        public Car()
        {
            Make = "";
            Model = "";
            Color = "";
        }
        
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Doors { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
    }
}
