using System.Net;

namespace KnowledgeAccountinSystem.Business.Validation
{
    public class UnuniqueException : KASException
    {
        public UnuniqueException()
        {
        }

        public UnuniqueException(string message) : base(message)
        {
        }

        public UnuniqueException(string message, HttpStatusCode code) : base(message, code)
        {
        }
    }
}
