namespace Jwt.API.Model
{
    public class Parcel
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public int Weight { get; set; }
    }
}
