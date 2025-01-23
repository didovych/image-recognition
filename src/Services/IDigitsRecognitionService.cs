namespace Services
{
    public interface IDigitsRecognitionService
    {
        int InputSize { get; }
        int RecognizeDigit(IEnumerable<byte> pixels);
    }
}
