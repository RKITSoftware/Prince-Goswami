using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

/// <summary>
/// Operation filter to add an Authorization header parameter to Swagger documentation for endpoints requiring authorization.
/// </summary>
public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
{
    /// <summary>
    /// Applies the operation filter to modify the Swagger operation based on authorization requirements.
    /// </summary>
    /// <param name="operation">The Swagger operation being modified.</param>
    /// <param name="schemaRegistry">The schema registry.</param>
    /// <param name="apiDescription">The API description.</param>
    public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
    {
        var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline();
        var isAuthorized = filterPipeline
            .Select(filterInfo => filterInfo.Instance)
            .Any(filter => filter is AuthorizeAttribute);

        var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();

        if (isAuthorized && !allowAnonymous)
        {
            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }

            operation.parameters.Add(new Parameter
            {
                name = "Authorization",
                @in = "header",
                type = "string",
                required = true,
                description = "Bearer token"
            });
        }
    }
}
