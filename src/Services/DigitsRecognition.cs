using NeuralNetwork;

namespace Services
{
    public class DigitsRecognition : IDigitsRecognition
    {
        private readonly INeuralNetworkModel _model;

        public DigitsRecognition(INeuralNetworkModel model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public int RecognizeDigit(byte[] pixels)
        {
            var input = ConvertPixelsToModelInput(pixels);

            _model.Forward(input);

            var output = _model.GetOutput();

            return output.Result;
        }

        private double[] ConvertPixelsToModelInput(byte[] pixels)
        {
            var input = pixels.Select(p => (double)p / byte.MaxValue).ToArray();

            return input;
        }
    }
}
