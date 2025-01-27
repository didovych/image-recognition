using NeuralNetwork;
using Sandbox;
using System.Text.Json;

const string modelPath = "../../../Resources/TrainedModels/modelBW.json";

var model = new NeuralNetworkModel([784, 16, 16, 10]);

Helpers.TrainModel(model);

//var modelDto = model.ConvertToDto();
//var json = JsonSerializer.Serialize(modelDto);
//File.WriteAllText(modelPath, json);

Helpers.TestModel(model);

//var json = File.ReadAllText(modelPath);
//var modelDto = JsonSerializer.Deserialize<NeuralNetworkModelDto>(json);
//var model = new NeuralNetworkModel(modelDto);

Console.WriteLine("DONE");
Console.ReadKey();