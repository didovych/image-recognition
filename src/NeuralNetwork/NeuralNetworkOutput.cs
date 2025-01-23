namespace NeuralNetwork
{
    public class NeuralNetworkOutput
    {
        public int Result { get; }
        public double Confidence { get; }
        public Dictionary<int, double> OutputNeurons { get; }

        public NeuralNetworkOutput(int result, double confidence, double[] outputNeurons)
        {
            Result = result;
            Confidence = confidence;

            OutputNeurons = new Dictionary<int, double>();
            for (int i = 0; i < outputNeurons.Length; i++)
            {
                OutputNeurons.Add(i, Math.Round(outputNeurons[i] * 2, 4));
            }
        }
    }
}
