using ImageReader;
using NeuralNetwork;

namespace Sandbox
{
    internal static class Helpers
    {
        static readonly string trainImages = "../../../Resources/Images/train-images.idx3-ubyte";
        static readonly string trainLabels = "../../../Resources/Images/train-labels.idx1-ubyte";
        static readonly string testImages = "../../../Resources/Images/t10k-images.idx3-ubyte";
        static readonly string testLabels = "../../../Resources/Images/t10k-labels.idx1-ubyte";
        static readonly string bmpsPath = "../../../Resources/Bmps/";

        internal static void TrainModel(NeuralNetworkModel model)
        {
            int trainingSteps = 0;
            double lossSum = 0.0;

            foreach (var image in MnistReader.ReadData(trainLabels, trainImages))
            {
                var input = image.GenerateBlackAndWhiteNetworkInput();
                model.Forward(input);
                model.Backpropagate(image.Label);

                lossSum += model.CalculateLoss();

                if (++trainingSteps % 1000 == 0)
                {
                    Console.WriteLine($"After {trainingSteps} steps AVG loss is {(lossSum / 1000):0.000}");
                    lossSum = 0.0;
                }
            }
        }

        internal static void TestModel(NeuralNetworkModel model, bool saveToBitmap = false)
        {
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

                    if (saveToBitmap)
                    {
                        image.SaveToBitmap(result.Result, bmpsPath);
                    }
                    //model.PrintOutput();

                    //Console.ReadKey();
                }
                else
                {
                    correct++;
                }
            }

            Console.WriteLine($"Success rate: {(double)correct / (correct + wrong)}, total: {correct + wrong}");
        }
    }
}
