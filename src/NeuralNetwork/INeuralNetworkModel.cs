namespace NeuralNetwork
{
    public interface INeuralNetworkModel
    {
        int InputSize { get; }
        void Forward(double[] input);
        NeuralNetworkOutput GetOutput();
    }
}
