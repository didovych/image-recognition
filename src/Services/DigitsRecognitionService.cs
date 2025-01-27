using NeuralNetwork;

namespace Services
{
    public class DigitsRecognitionService : IDigitsRecognitionService
    {
        private readonly IModelLoader _modelLoader;
        private readonly INeuralNetworkModel _model;

        public DigitsRecognitionService(IModelLoader modelLoader)
        {
            _modelLoader = modelLoader ?? throw new ArgumentNullException(nameof(modelLoader));
            _model = _modelLoader.LoadModel();
        }

        public int InputSize => _model.InputSize;

        public NeuralNetworkOutput RecognizeDigit(IEnumerable<byte> pixels, bool convertToBlackAndWhite)
        {
            var input = convertToBlackAndWhite
                ? ConvertPixelsToBlackAndWhiteModelInput(pixels)
                : ConvertPixelsToModelInput(pixels);

            _model.Forward(input);

            return _model.GetOutput();
        }

        private double[] ConvertPixelsToModelInput(IEnumerable<byte> pixels)
        {
            var input = pixels.Select(p => (double)p / byte.MaxValue).ToArray();

            return input;
        }

        private double[] ConvertPixelsToBlackAndWhiteModelInput(IEnumerable<byte> pixels)
        {
            var input = pixels.Select(p => p > byte.MaxValue / 2 ? 1.0 : 0.0).ToArray();

            return input;
        }
    }
}
