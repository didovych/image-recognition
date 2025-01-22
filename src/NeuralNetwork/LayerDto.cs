namespace NeuralNetwork
{
    internal class LayerDto
    {
        public double[] Biases { get; set; }

        public double[][] Weights { get; set; }

        public string Label { get; set; }

        public int Size { get; set; }

        public LayerType Type { get; set; }
    }
}
