using System;

namespace Shuttle.Core.Container
{
    public class TypeResolutionException : Exception
    {
        public TypeResolutionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public TypeResolutionException(string message) : base(message)
        {
        }
    }
}