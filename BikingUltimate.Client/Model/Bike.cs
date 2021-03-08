namespace BikingUltimate.Client.Model
{
    public class Bike
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public double Distance { get; set; }
        public string Model { get; set; }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Brand)}: {Brand}, {nameof(Distance)}: {Distance}, {nameof(Model)}: {Model}";
        }
    }
}