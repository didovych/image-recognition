using NeuralNetwork;
using System.Text.Json;

namespace Services
{
    public class ModelLoader : IModelLoader
    {
        private readonly string _modelPath;

        public ModelLoader(string modelPath)
        {
            _modelPath = modelPath ?? throw new ArgumentNullException(nameof(modelPath));
        }

        public INeuralNetworkModel LoadModel()
        {
            var json = File.ReadAllText(_modelPath);
            var modelDto = JsonSerializer.Deserialize<NeuralNetworkModelDto>(json) ?? throw new Exception("Can not load the model");
            return new NeuralNetworkModel(modelDto);
        }
    }
}
