using Microsoft.FeatureManagement;

namespace FeatureFlagsDemo.Services
{
    public class FeatureToggleService(IFeatureManager featureManager) : IFeatureToggleService
    {
        public Task<bool> IsFeatureEnabledAsync(string featureName)
        {
            return featureManager.IsEnabledAsync(featureName);
        }

        public Task<bool> IsNewDashboardEnabledAsync()
        {
            return featureManager.IsEnabledAsync("EnableNewDashboard");
        }

        public Task<bool> IsAdvancedSearchEnabledAsync()
        {
            return featureManager.IsEnabledAsync("UseAdvancedSearch");
        }

        public Task<bool> IsLegacyEndpointsBlockedAsync()
        {
            return featureManager.IsEnabledAsync("BlockLegacyEndpoints");
        }
    }
}
