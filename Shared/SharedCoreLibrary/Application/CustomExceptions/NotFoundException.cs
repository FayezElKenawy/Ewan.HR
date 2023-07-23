using SharedCoreLibrary.Application.Abstractions.CustomExceptions;

namespace SharedCoreLibrary.Application.CustomExceptions
{
    public class NotFoundException<TEntity> : NotFoundException
    {
        public NotFoundException()
        {
            message = ResourseFiles.Global.ItemNotFound.ToString();
        }

        public NotFoundException(string message)
        {
            this.message = message;
        }

        public override string Message => message;
    }
}
