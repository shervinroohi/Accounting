using AccountingSystem.Application.DTOs.Transaction;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class CreateTransactionExampleFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.MethodInfo.Name != "CreateTransaction")
            return;

        if (operation.RequestBody == null)
            return;

        if (!operation.RequestBody.Content.TryGetValue("application/json", out var mediaType))
            return;

        if (mediaType.Example != null)
            return;

        mediaType.Example = new OpenApiObject
        {
            ["amount"] = new OpenApiInteger(0),
            ["type"] = new OpenApiString("Payment or Received"),
            ["status"] = new OpenApiString("Settled or UnSettled"),
            ["transactionDate"] = new OpenApiString("2026-08-09T07:23:03"),
            ["description"] = new OpenApiString("string"),
            ["partyId"] = new OpenApiInteger(0)
        };
    }
}