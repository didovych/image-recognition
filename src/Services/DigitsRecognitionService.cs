using NeuralNetwork;

namespace Services
{
    public class DigitsRecognitionService : IDigitsRecognitionService
    {
        private readonly INeuralNetworkModel _model;

        public DigitsRecognitionService(INeuralNetworkModel model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public int InputSize => _model.InputSize;

        public int RecognizeDigit(IEnumerable<byte> pixels)
        {
            var input = ConvertPixelsToModelInput(pixels);

            _model.Forward(input);

            var output = _model.GetOutput();

            return output.Result;
        }

        private double[] ConvertPixelsToModelInput(IEnumerable<byte> pixels)
        {
            var input = pixels.Select(p => (double)p / byte.MaxValue).ToArray();

            return input;
        }
    }
}
