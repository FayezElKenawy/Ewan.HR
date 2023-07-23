namespace SharedCoreLibrary.Application.Abstractions.CustomExceptions
{
    public abstract class NotFoundException : ApplicationException
    {
        protected string message = string.Empty;
    }
}
