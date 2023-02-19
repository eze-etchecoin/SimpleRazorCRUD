namespace SimpleRazorCRUD.Models.Cars
{
    public class PriceGuessRequest
    {
        public PriceGuessRequest()
        {
            Value = "0";
        }

        public int CarId { get; set; }
        public string Value { get; set; }
    }
}
