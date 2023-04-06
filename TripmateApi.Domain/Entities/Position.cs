namespace TripmateApi.Domain.Entities
{
    public class Position
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        
        public double Longitude { get; set; }
        
        public double Latitude { get; set; }
        public int Pc { get; set; }
    }
}