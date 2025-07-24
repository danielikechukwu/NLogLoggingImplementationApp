using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NLogLoggingImplementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NLogsController : ControllerBase
    {
        // Dependency injection of ILogger for this controller.
        private readonly ILogger<NLogsController> _logger;

        // Constructor injection for ILogger<T>
        public NLogsController(ILogger<NLogsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("all-logs")]
        public IActionResult LogAllLevels()
        {
            // Log at Trace level (for debugging)
            _logger.LogTrace("LogTrace: Entering the LogAllLevels endpoint with Trace-level logging.");

            // Logging a calculated variable at Trace level.
            int calculation = 5 * 10;
            _logger.LogTrace("LogTrace: Calculation result is {calculation}", calculation);

            // Debug level logs help in debugging the application flow.
            _logger.LogDebug("LogDebug: Initializing debug-level logs for debugging purposes.");

            // Logging a structured object at Debug level.
            var debugInfo = new { Action = "LogAllLevels", Status = "Debugging" };
            _logger.LogDebug("LogDebug: Debug information: {@debugInfo}", debugInfo);

            // Information level logs for successful operations.
            _logger.LogInformation("LogInformation: The LogAllLevels endpoint was reached successfully.");

            // Warning log if a potential issue is detected.
            bool resourceLimitApproaching = true;

            if (resourceLimitApproaching)
            {
                _logger.LogWarning("LogWarning: Resource usage is nearing the limit. Action may be required soon.");
            }

            // Error and Exception logging.
            try
            {
                // Simulate an error scenario
                int x = 0;
                int result = 10 / x; // This will throw an exception
            }
            catch(Exception ex)
            {
                // Error-level logging that captures the exception details.
                _logger.LogError(ex, "LogError: An error occurred while performing a division operation.");
            }

            // Critical-level log for severe failures.
            bool criticalFailure = true;

            if(criticalFailure)
            {
                _logger.LogCritical("LogCritical: A critical system failure has been detected. Immediate attention is required.");
            }

            return Ok("All logging levels demonstrated in this endpoint.");
        }
    }
}
