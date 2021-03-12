using System;

namespace BikingUltimate.Server.Features.Activities
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}