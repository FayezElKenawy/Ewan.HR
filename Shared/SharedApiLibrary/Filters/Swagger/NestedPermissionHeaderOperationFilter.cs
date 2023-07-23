using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SharedApiLibrary.Filters.Swagger
{
    /// <summary>
    /// Operation filter to add the requirement of the custom header
    /// </summary>
    public class NestedPermissionHeaderOperationFilter : IOperationFilter
    {

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Nested-Permission",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "String" },
                Required = false // set to true if this is required
            });
        }
    }
}