namespace TripmateApi.Domain.Entities
{
    public class Step
    {
        public int Id { get; set; }
        public DateTime DepartTime { get; set; }
        public Position PositionDepart { get; set; }
        public Position PositionArrival { get; set; }
        public int? Duration { get; set; }
        public int? Seats { get; set; }
    }
}