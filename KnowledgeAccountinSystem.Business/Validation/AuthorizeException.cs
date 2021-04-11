using System.Net;

namespace KnowledgeAccountinSystem.Business.Validation
{
    public class AuthorizeException : KASException
    {
        public AuthorizeException()
        {
        }

        public AuthorizeException(string message) : base(message)
        {
        }

        public AuthorizeException(string message, HttpStatusCode code) : base(message, code)
        {
        }
    }
}
