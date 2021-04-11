using System.Net;

namespace KnowledgeAccountinSystem.Business.Validation
{
    public class ModelException : KASException
    {
        public ModelException()
        {
        }

        public ModelException(string message) : base(message)
        {
        }

        public ModelException(string message, HttpStatusCode code) : base(message, code)
        {
        }
    }
}
