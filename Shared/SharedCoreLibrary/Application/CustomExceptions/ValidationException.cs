﻿
namespace SharedCoreLibrary.Application.CustomExceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}
