using System.Text.Json;

namespace NeuralNetwork
{
    public class NeuralNetworkModel : INeuralNetworkModel
    {
        private readonly Layer[] _layers;

        public int InputSize => _layers[0].Size;

        public NeuralNetworkModel(int[] sizes)
        {
            _layers = new Layer[sizes.Length];

            for (int i = _layers.Length - 1; i >= 0; i--)
            {
                if (i == _layers.Length - 1)
                {
                    _layers[i] = new Layer(sizes[i], LayerType.Output, null, "Output");
                }
                else if (i == 0)
                {
                    _layers[i] = new Layer(sizes[i], LayerType.Input, _layers[i + 1], "Input");
                }
                else
                {
                    _layers[i] = new Layer(sizes[i], LayerType.Middle, _layers[i + 1], $"Middle{i}");
                }
            }
        }

        public NeuralNetworkModel(NeuralNetworkModelDto modelDto)
        {
            _layers = new Layer[modelDto.Layers.Length];

            for (int i = modelDto.Layers.Length - 1; i >= 0; i--)
            {
                var nextLayer = i == modelDto.Layers.Length - 1
                ? null
                : _layers[i + 1];

                _layers[i] = new Layer(modelDto.Layers[i], nextLayer);
            }
        }

        public void Forward(double[] input)
        {
            // set input
            _layers[0].SetInput(input);

            for (int i = 0; i < _layers.Length - 1; i++)
            {
                _layers[i].CalculateNextLayer();
            }
        }

        public void Backpropagate(int number)
        {
            // set _grad for the output layer
            _layers[_layers.Length - 1].SetGradForOutputLayer(number);

            for (int i = _layers.Length - 2; i >= 0; i--)
            {
                _layers[i].Backpropagate();
            }
        }

        public NeuralNetworkOutput GetOutput()
        {
            return _layers.Last().GetOutput();
        }

        public NeuralNetworkModelDto ConvertToDto()
        {
            return new NeuralNetworkModelDto()
            {
                Layers = _layers.Select(l => l.ConvertToDTO()).ToArray(),
            };
        }

        public double CalculateLoss()
        {
            return _layers.Last().CalculateTotalLoss();
        }
    }

}
