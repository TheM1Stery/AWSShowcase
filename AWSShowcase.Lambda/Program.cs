using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints(options =>
{
    options.SourceGeneratorDiscoveredTypes = DiscoveredTypes.All;
});

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.DocumentName = "Initial Release";
        s.Title = "Web API";
        s.Version = "v0.0";
        s.SchemaType = NJsonSchema.SchemaType.OpenApi3;
    };
    o.SerializerSettings = x => x.PropertyNamingPolicy = null;
    o.TagCase = TagCase.TitleCase;
    o.RemoveEmptyRequestSchema = false;
});


var app = builder.Build();
app.UseDefaultExceptionHandler();
app.UseAuthorization();
app.UseFastEndpoints();

if (!app.Environment.IsProduction())
{
    app.Map("/", () => Results.Redirect("/swagger"));
    app.UseSwaggerGen(uiConfig: settings => settings.ConfigureDefaults());
}


app.Run();
