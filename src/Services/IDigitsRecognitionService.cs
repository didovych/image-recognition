using NeuralNetwork;

namespace Services
{
    public interface IDigitsRecognitionService
    {
        int InputSize { get; }
        NeuralNetworkOutput RecognizeDigit(IEnumerable<byte> pixels);
    }
}
