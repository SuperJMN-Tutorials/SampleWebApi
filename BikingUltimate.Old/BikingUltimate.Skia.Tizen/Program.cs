using Tizen.Applications;
using Uno.UI.Runtime.Skia;

namespace BikingUltimate.Skia.Tizen
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new TizenHost(() => new BikingUltimate.App(), args);
            host.Run();
        }
    }
}
