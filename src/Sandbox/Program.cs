using NeuralNetwork;
using Sandbox;
using System.Text.Json;

const string modelPath = "../../../Resources/TrainedModels/modelBW88.json";

//var model = new NeuralNetworkModel([784, 16, 16, 10]);

//Helpers.TrainModel(model);

//var modelDto = model.ConvertToDto();
//var json = JsonSerializer.Serialize(modelDto);
//File.WriteAllText(modelPath, json);

var json = File.ReadAllText(modelPath);
var modelDto = JsonSerializer.Deserialize<NeuralNetworkModelDto>(json);
var model = new NeuralNetworkModel(modelDto);

Helpers.TestModel(model);

Console.WriteLine("DONE");
Console.ReadKey();