namespace TripmateApi.Domain.Entities
{
    public class Step
    {
        public int Id { get; set; }
        public DateTime DepartTime { get; set; }
        public Position PostitionDepart { get; set; }
        public Position PostitionArrival { get; set; }
        public int? Duration { get; set; }
        public int Seats { get; set; }
    }
}