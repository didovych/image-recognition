namespace NeuralNetwork
{
    public class NeuralNetworkOutput
    {
        public int Result { get; }
        public double Confidence { get; }
        public double[] OutputNeurons { get; }

        public NeuralNetworkOutput(int result, double confidence, double[] outputNeurons)
        {
            Result = result;
            Confidence = confidence;
            OutputNeurons = outputNeurons;
        }
    }
}
