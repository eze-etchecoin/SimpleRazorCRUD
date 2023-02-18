namespace SimpleRazorCRUD.Models
{
    // I consider a good practice separating the data classes from the view model classes
    // in order to give the view more adaptability when extra data is needed, besides the data class fields.
    public class CarModel
    {
        public CarModel()
        {
            Make = "";
            Model = "";
            Color = "";
        }
        
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Doors { get; set; }
        public string Color { get; set; }
        public int Price { get; set; }
    }
}
