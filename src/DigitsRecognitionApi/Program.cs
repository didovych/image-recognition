using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// load pre-trained model
const string modelPath = "Resources/TrainedModels/model89.json";

builder.Services.AddScoped<IModelLoader, ModelLoader>(serviceProvider => new ModelLoader(modelPath));
builder.Services.AddScoped<IDigitsRecognitionService, DigitsRecognitionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
