using AccountingSystem.Domain.Enums;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        // اگر این schema داره برای یک query/route parameter ساخته میشه، Example نذار
        if (context.ParameterInfo != null)
            return;

        if (context.Type == typeof(TransactionType))
        {
            schema.Type = "string";
            schema.Example = new OpenApiString("Payment or Received");
        }
        else if (context.Type == typeof(TransactionStatus))
        {
            schema.Type = "string";
            schema.Example = new OpenApiString("Settled or UnSettled");
        }
    }
}
