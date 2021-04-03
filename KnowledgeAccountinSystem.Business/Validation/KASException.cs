using System;
using System.Net;

namespace KnowledgeAccountinSystem.Business.Validation
{
    public class KASException : Exception
    {
        private readonly HttpStatusCode statusCode;
        public KASException() : base()
        {

        }

        public KASException(string message) : base(message)
        {

        }

        public KASException(string message, HttpStatusCode code) : base(message)
        {
            statusCode = code;
        }
    }
}
