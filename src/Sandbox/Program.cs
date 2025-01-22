using ImageReader;
using NeuralNetwork;
using System.IO;
using System.Text.Json;

const string trainImages = "../../../Resources/Images/train-images.idx3-ubyte";
const string trainLabels = "../../../Resources/Images/train-labels.idx1-ubyte";
const string testImages = "../../../Resources/Images/t10k-images.idx3-ubyte";
const string testLabels = "../../../Resources/Images/t10k-labels.idx1-ubyte";

const string modelPath = "../../../Resources/TrainedModels/model89.json";

const string bmpsPath = "../../../Resources/Bmps/";

//var model = new NeuralNetworkModel([784, 16, 16, 10]);

//int trainingSteps = 0;
//double lossSum = 0.0;

//foreach (var image in MnistReader.ReadData(trainLabels, trainImages))
//{
//    var input = image.GenerateNetworkInput();
//    model.Forward(input);
//    model.Backpropagate(image.Label);

//    lossSum += model.CalculateLoss();

//    if (++trainingSteps % 1000 == 0)
//    {
//        Console.WriteLine($"After {trainingSteps} steps AVG loss is {(lossSum / 1000):0.000}");
//        lossSum = 0.0;
//    }
//}

//var modelDto = model.ConvertToDto();
//var json = JsonSerializer.Serialize(modelDto);
//File.WriteAllText(modelPath, json);

var json = File.ReadAllText(modelPath);
var modelDto = JsonSerializer.Deserialize<NeuralNetworkModelDto>(json);
var model = new NeuralNetworkModel(modelDto);

int correct = 0;
int wrong = 0;

foreach (var image in MnistReader.ReadData(testLabels, testImages))
{
    var input = image.GenerateNetworkInput();
    model.Forward(input);
    var result = model.GetOutput();

    if (result.Result != image.Label)
    {
        wrong++;

        image.SaveToBitmap(result.Result, bmpsPath);
        //model.PrintOutput();

        //Console.ReadKey();
    }
    else
    {
        correct++;
    }
}

Console.WriteLine($"Success rate: {(double)correct / (correct + wrong)}, total: {correct + wrong}");

Console.WriteLine("DONE");
Console.ReadKey();