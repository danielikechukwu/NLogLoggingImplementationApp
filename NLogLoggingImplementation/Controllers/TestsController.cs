using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NLogLoggingImplementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly ILogger<TestsController> _logger;

        public TestsController(ILogger<TestsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("test-logging")]
        public IActionResult TestLogging()
        {
            // Log at Error level
            var UniqueId = Guid.NewGuid();

            try
            {
                _logger.LogTrace("{UniqueId} This is a Trace log, the most detailed information.", UniqueId);

                _logger.LogDebug("{UniqueId} This is a Debug log, useful for debugging.", UniqueId);

                _logger.LogInformation("{UniqueId} This is an Information log, general info about app flow.", UniqueId);

                _logger.LogWarning("This is a Warning log, indicating a potential issue.{UniqueId}", UniqueId);

                _logger.LogCritical("This is a Critical log, indicating a serious failure in the application.");

                // Simulate an error scenario
                int x = 0;
                int result = 10 / x; // This will throw an exception
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UniqueId} This is an Error log, indicating a failure in the current operation.", UniqueId);
            }

            return Ok("Check your logs to see the different logging levels in action!");
        }

    }
}
