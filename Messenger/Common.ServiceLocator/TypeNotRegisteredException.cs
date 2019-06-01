using System;
using System.Runtime.Serialization;

namespace Common.ServiceLocator
{
    [Serializable]
    internal class TypeNotRegisteredException : Exception
    {
        public TypeNotRegisteredException()
        {
        }

        public TypeNotRegisteredException(String message) : base(message)
        {
        }

        public TypeNotRegisteredException(String message, Exception innerException) : base(message, innerException)
        {
        }

        protected TypeNotRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}