using System;

namespace BikingUltimate.Client.Model
{
    public class Component
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public double Distance { get; set; }
        public double Weight { get; set; }
        public ComponentType ComponentType { get; set; }
        public DateTimeOffset AddedOn { get; set; }
        public DateTimeOffset? RetiredOn { get; set; }

        public override string ToString()
        {
            return
                $@"{nameof(Brand)}: {Brand}, 
{nameof(Model)}: {Model}, 
{nameof(Distance)}: {Distance}, 
{nameof(Weight)}: {Weight}, 
{nameof(ComponentType)}: {ComponentType}, 
{nameof(AddedOn)}: {AddedOn}, 
{nameof(RetiredOn)}: {RetiredOn}";
        }
    }
}