namespace NeuralNetwork
{
    public interface INeuralNetworkModel
    {
        public void Forward(double[] input);
        public NeuralNetworkOutput GetOutput();
    }
}
