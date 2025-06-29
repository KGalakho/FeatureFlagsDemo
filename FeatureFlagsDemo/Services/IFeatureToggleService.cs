namespace FeatureFlagsDemo.Services
{
    public interface IFeatureToggleService
    {
        Task<bool> IsFeatureEnabledAsync(string featureName);
        Task<bool> IsNewDashboardEnabledAsync();
        Task<bool> IsAdvancedSearchEnabledAsync();
        Task<bool> IsLegacyEndpointsBlockedAsync();
    }
}
