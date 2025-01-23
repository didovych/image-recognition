namespace NeuralNetwork
{
    public enum LayerType
    {
        Input,
        Middle,
        Output
    }

    internal class Layer
    {
        private double[] _neurons;

        // non-activated values
        private readonly double[] _nonActivatedNeurons;

        private readonly double[] _biases;

        private readonly double[][] _weights;

        // gradients array for the later
        private readonly double[] _grad;

        // Next layer. null for the OutputNeurons layer.
        private readonly Layer? _nextLayer;

        public string Label { get; }

        public int Size { get; }

        public LayerType Type { get; }

        public Layer(int size, LayerType type, Layer? nextLayer = null, string label = "")
        {
            Size = size;
            Type = type;
            Label = label;

            _nextLayer = nextLayer;
            int nextLayerSize = nextLayer == null ? 0 : nextLayer.Size;

            _neurons = new double[size];
            _nonActivatedNeurons = new double[size];
            _biases = new double[nextLayerSize];
            _weights = new double[size][];

            _grad = new double[size];

            SetRandomWeights();
            SetRandomBiases();
        }

        public Layer(LayerDto layerDto, Layer? nextLayer)
        {
            Size = layerDto.Size;
            Type = layerDto.Type;
            Label = layerDto.Label;
            _biases = layerDto.Biases;
            _weights = layerDto.Weights;

            _nextLayer = nextLayer;

            _neurons = new double[Size];
            _nonActivatedNeurons = new double[Size];
            _grad = new double[Size];
        }

        /// <summary>
        /// Set input for the Input layer
        /// </summary>
        public void SetInput(double[] input)
        {
            if (Type == LayerType.Input)
            {
                _neurons = input;
            }
        }

        public void CalculateNextLayer()
        {
            if (_nextLayer == null)
                throw new Exception("There is no next layer");

            for (int i = 0; i < _nextLayer.Size; i++)
            {
                double sum = _biases[i];

                for (int j = 0; j < Size; j++)
                {
                    sum += _neurons[j] * _weights[j][i];
                }

                _nextLayer._nonActivatedNeurons[i] = sum;

                _nextLayer._neurons[i] = Activate(sum);
            }
        }

        public double CalculateTotalLoss()
        {
            double loss = 0.0;

            for (int i = 0; i < Size; i++)
            {
                loss += _grad[i] * _grad[i];
            }

            return loss;
        }

        public void SetGradForOutputLayer(int correctNumber)
        {
            if (Type != LayerType.Output)
            {
                return;
            }

            for (int i = 0; i < _grad.Length; i++)
            {
                _grad[i] = _neurons[i] - (i == correctNumber ? 0.5 : 0);
            }
        }

        public void Backpropagate()
        {
            // start with pre-last layer
            if (_nextLayer == null)
            {
                return;
            }

            int nextLayerSize = _nextLayer.Size;
            var biasGrad = new double[_biases.Length];

            // calculate grad for the layer
            for (int i = 0; i < _grad.Length; i++)
            {
                double sum = 0.0;

                for (int j = 0; j < nextLayerSize; j++)
                {
                    sum += _nextLayer._grad[j]
                        * Deactivate(_nextLayer._nonActivatedNeurons[j])
                        * _weights[i][j];
                }

                _grad[i] = sum;
            }

            // change biases
            for (int i = 0; i < _biases.Length; i++)
            {
                biasGrad[i] = _nextLayer._grad[i] * Deactivate(_nextLayer._nonActivatedNeurons[i]);
                if (Type != LayerType.Input)
                {
                    _biases[i] -= biasGrad[i];
                }
            }

            // change weights
            for (int i = 0; i < _weights.Length; i++)
            {
                for (int j = 0; j < _weights[i].Length; j++)
                {
                    _weights[i][j] -= biasGrad[j] * _neurons[i];
                }
            }
        }

        public NeuralNetworkOutput GetOutput()
        {
            double maxOutput = double.MinValue;
            int resultIndex = -1;

            for (int i = 0; i < _neurons.Length; i++)
            {
                if (_neurons[i] > maxOutput)
                {
                    maxOutput = _neurons[i];
                    resultIndex = i;
                }
            }

            return new NeuralNetworkOutput(resultIndex, maxOutput * 2, _neurons);
        }

        public void PrintMetrics()
        {
            var avg = _neurons.Average();
            var squaredAvg = _neurons.Select(n => n * n).Average();
            Console.WriteLine($"Layer {Label}: Avg = {avg:0.00}, Variance = {squaredAvg - avg} Max = {_neurons.Max():0.00}");
        }

        public void PrintNeurons()
        {
            for (int i = 0; i < _neurons.Length; i++)
            {
                Console.Write($"{i}: {_neurons[i]:0.000} ");
            }
            Console.WriteLine();
        }

        public LayerDto ConvertToDTO()
        {
            return new LayerDto()
            {
                Label = Label,
                Type = Type,
                Size = Size,
                Biases = _biases,
                Weights = _weights,
            };
        }

        private void SetRandomBiases()
        {
            // _biases should be 0 for the Input layer
            if (Type == LayerType.Input)
            {
                return;
            }

            var rand = new Random();
            for (int i = 0; i < _biases.Length; i++)
            {
                _biases[i] = Gaussian(rand.NextDouble());
            }
        }

        private void SetRandomWeights()
        {
            if (_nextLayer == null)
                return;

            var rand = new Random();
            for (int i = 0; i < _weights.Length; i++)
            {
                _weights[i] = new double[_nextLayer.Size];

                for (int j = 0; j < _nextLayer.Size; j++)
                {
                    _weights[i][j] = Gaussian(rand.NextDouble()) / Math.Sqrt(Size);
                }
            }
        }

        private double Activate(double x)
        {
            return Sigmoid(x);
        }

        private double Deactivate(double x)
        {
            return SigmoidPrime(x);
        }

        private double Sigmoid(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        private double SigmoidPrime(double x)
        {
            return Sigmoid(x) * (1 - Sigmoid(x));
        }

        private double Gaussian(double x)
        {
            return Math.Exp(-Math.Pow(x, 2) / 2) / Math.Sqrt(2 * Math.PI);
        }
    }
}
