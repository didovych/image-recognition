using Microsoft.AspNetCore.Mvc;
using Services;

namespace DigitsRecognitionApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DigitsRecognitionController : ControllerBase
    {
        private readonly IDigitsRecognitionService _digitsRecognitionService;

        public DigitsRecognitionController(IDigitsRecognitionService digitsRecognitionService)
        {
            _digitsRecognitionService = digitsRecognitionService ?? throw new ArgumentNullException(nameof(digitsRecognitionService));
        }

        [HttpGet(Name = "RecognizeDigit")]
        public ActionResult<int> RecognizeDigit([FromBody] List<byte> pixels)
        {
            if (pixels.Count != _digitsRecognitionService.InputSize)
            {
                return BadRequest("Invalid input size.");
            }

            var digit = _digitsRecognitionService.RecognizeDigit(pixels);

            return Ok(digit);
        }
    }
}
