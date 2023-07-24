using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using SharedCoreLibrary.Application.CustomExceptions;

namespace Ewan.HR.API.Common.Validation
{
    public static class HandleModelValidation
    {
        public static IActionResult Handle(ActionContext context)
        {
            foreach (var keyModelStatePair in context.ModelState)
            {
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    if (errors.Count == 1)
                    {
                        var errorMessage = GetErrorMessage(errors[0]);
                        throw new ValidationException(errorMessage);
                    }
                    else
                    {
                        var errorMessages = new string[errors.Count];
                        for (var i = 0; i < errors.Count; i++)
                        {
                            errorMessages[i] = GetErrorMessage(errors[i]);
                            throw new ValidationException(errorMessages[i]);
                        }
                    }
                }
            }

            var result = new BadRequestObjectResult(null);
            result.ContentTypes.Add("application/problem+json");

            return result;
        }

        static string GetErrorMessage(ModelError error)
        {
            return string.IsNullOrEmpty(error.ErrorMessage) ?
                    "The input was not valid." :
                    error.ErrorMessage;
        }
    }
}
