using Moq;
using Microsoft.AspNetCore.Mvc;
using FeatureFlagsDemo.Controllers;
using FeatureFlagsDemo.Services;
using FeatureFlagsDemo.Models;

namespace FeatureFlagsDemo.Tests
{
    public class FeaturesControllerTests
    {
        [Fact]
        public async Task IsNewDashboardEnabled_ReturnsExpectedResult()
        {
            var mockService = new Mock<IFeatureToggleService>();
            mockService.Setup(s => s.IsNewDashboardEnabledAsync())
                       .ReturnsAsync(true);

            var controller = new FeaturesController(mockService.Object);

            var actionResult = await controller.IsNewDashboardEnabled();

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var value = Assert.IsType<FeatureStatusResponse>(okResult.Value);
            Assert.Equal("EnableNewDashboard", value.Feature);
            Assert.True(value.Enabled);
        }

        [Fact]
        public async Task IsAdvancedSearchEnabled_ReturnsExpectedResult()
        {
            var mockService = new Mock<IFeatureToggleService>();
            mockService.Setup(s => s.IsAdvancedSearchEnabledAsync())
                       .ReturnsAsync(false);

            var controller = new FeaturesController(mockService.Object);

            var actionResult = await controller.IsAdvancedSearchEnabled();

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var value = Assert.IsType<FeatureStatusResponse>(okResult.Value);
            Assert.Equal("UseAdvancedSearch", value.Feature);
            Assert.False(value.Enabled);
        }

        [Theory]
        [InlineData("EnableNewDashboard", true)]
        [InlineData("UseAdvancedSearch", false)]
        public async Task IsFeatureEnabled_ReturnsExpectedResult(string featureName, bool expectedEnabled)
        {
            var mockService = new Mock<IFeatureToggleService>();
            mockService.Setup(s => s.IsFeatureEnabledAsync(featureName))
                       .ReturnsAsync(expectedEnabled);

            var controller = new FeaturesController(mockService.Object);

            var actionResult = await controller.IsFeatureEnabled(featureName);

            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var value = Assert.IsType<FeatureStatusResponse>(okResult.Value);
            Assert.Equal(featureName, value.Feature);
            Assert.Equal(expectedEnabled, value.Enabled);
        }
    }
}
