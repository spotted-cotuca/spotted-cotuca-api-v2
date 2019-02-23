using System;

namespace SpottedCotuca.API.Exceptions
{
    public class InvalidStatusException : Exception
    {
        public InvalidStatusException() { }

        public InvalidStatusException(string message) : base(message) { }

        public InvalidStatusException(string message, Exception inner) : base(message, inner) { }
    }
}
