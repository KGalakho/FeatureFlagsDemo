using Microsoft.AspNetCore.Mvc;
using FeatureFlagsDemo.Services;
using FeatureFlagsDemo.Models;

namespace FeatureFlagsDemo.Controllers
{
    [ApiController]
    [Route("api/features")]
    public class FeaturesController(IFeatureToggleService featureToggleService) : ControllerBase
    {
        [HttpGet("{featureName}")]
        public async Task<IActionResult> IsFeatureEnabled(string featureName)
        {
            var isEnabled = await featureToggleService.IsFeatureEnabledAsync(featureName);
            return Ok(new FeatureStatusResponse 
            { 
                Feature = featureName, 
                Enabled = isEnabled 
            });
        }


        [HttpGet("dashboard")]
        public async Task<ActionResult<FeatureStatusResponse>> IsNewDashboardEnabled()
        {
            var isEnabled = await featureToggleService.IsNewDashboardEnabledAsync();
            return Ok(new FeatureStatusResponse
            {
                Feature = "EnableNewDashboard",
                Enabled = isEnabled
            });
        }

        [HttpGet("search")]
        public async Task<ActionResult<FeatureStatusResponse>> IsAdvancedSearchEnabled()
        {
            var isEnabled = await featureToggleService.IsAdvancedSearchEnabledAsync();
            return Ok(new FeatureStatusResponse
            {
                Feature = "UseAdvancedSearch",
                Enabled = isEnabled
            });
        }
    }
}
