using NeuralNetwork;
using Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// load pre-trained model
const string modelPath = "Resources/TrainedModels/model89.json";

var json = File.ReadAllText(modelPath);
var modelDto = JsonSerializer.Deserialize<NeuralNetworkModelDto>(json) ?? throw new Exception("Can not load the model");
var model = new NeuralNetworkModel(modelDto);

builder.Services.AddScoped<INeuralNetworkModel, NeuralNetworkModel>(serviceProvider => model);
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
