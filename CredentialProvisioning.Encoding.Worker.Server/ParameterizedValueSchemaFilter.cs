using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using Leosac.CredentialProvisioning.Encoding;

namespace Leosac.CredentialProvisioning.Encoding.Worker.Server
{
    public sealed class ParameterizedValueSchemaFilter : ISchemaFilter
    {
        public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
        {
            if (!context.Type.IsGenericType || context.Type.GetGenericTypeDefinition() != typeof(ParameterizedValue<>))
                return;

            var valueType = context.Type.GetGenericArguments()[0];
            schema.OneOf.Add(new OpenApiSchema
            {
                Type = valueType == typeof(byte) || valueType == typeof(uint) ? JsonSchemaType.Integer : JsonSchemaType.Number,
                Format = valueType == typeof(byte) ? "int32" : "int64"
            });
            schema.OneOf.Add(new OpenApiSchema
            {
                Type = JsonSchemaType.String,
                Pattern = "^\\$\\{[^}]+\\}$"
            });
        }
    }
}
