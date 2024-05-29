using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

// Summary: This class implements the IOperationFilter interface from Swashbuckle Swagger
// to automatically add an "Authorization" header parameter to Swagger documentation
// for API endpoints that require authorization.
public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
{
    // Summary: Implementation of the Apply method from IOperationFilter interface.
    // This method is called to apply the filter logic.
    public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
    {
        // Summary: Checking if the current API endpoint requires authorization by
        // inspecting the action's filter pipeline.
        var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline();
        var isAuthorized = filterPipeline
            .Select(filterInfo => filterInfo.Instance)
            .Any(filter => filter is AuthorizeAttribute);

        // Summary: Checking if the AllowAnonymousAttribute is applied to the action,
        // meaning the endpoint allows anonymous access.
        var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();

        // Summary: Adding an "Authorization" header parameter to Swagger operation
        // if the endpoint requires authorization and doesn't allow anonymous access.
        if (isAuthorized && !allowAnonymous)
        {
            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }

            operation.parameters.Add(new Parameter
            {
                name = "Authorization", // Name of the parameter
                @in = "header", // Location of the parameter (in header)
                type = "string", // Data type of the parameter
                required = true, // Whether the parameter is required
                description = "Bearer token", // Description of the parameter
                @default = "Bearer " // Default value of the parameter
            });
        }
    }
}
