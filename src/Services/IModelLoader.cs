using NeuralNetwork;

namespace Services
{
    public interface IModelLoader
    {
        INeuralNetworkModel LoadModel();
    }
}
