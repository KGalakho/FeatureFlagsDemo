using Microsoft.FeatureManagement;

namespace FeatureFlagsDemo.Middleware
{
    public class FeatureToggleMiddleware(RequestDelegate next, string featureName)
    {
        public async Task InvokeAsync(HttpContext context, IFeatureManager featureManager)
        {
            if (!await featureManager.IsEnabledAsync(featureName))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync($"Feature '{featureName}' is disabled.");
                return;
            }

            await next(context);
        }
    }
}
