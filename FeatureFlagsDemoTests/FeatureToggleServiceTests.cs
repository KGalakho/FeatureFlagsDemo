using Moq;
using Microsoft.FeatureManagement;
using FeatureFlagsDemo.Services;

namespace FeatureFlagsDemo.Tests
{
    public class FeatureToggleServiceTests
    {
        [Fact]
        public async Task IsFeatureEnabledAsync_ReturnsTrue_WhenFeatureIsEnabled()
        {
            // Arrange
            var mockFeatureManager = new Mock<IFeatureManager>();
            mockFeatureManager
                .Setup(fm => fm.IsEnabledAsync("EnableNewDashboard"))
                .ReturnsAsync(true);

            var service = new FeatureToggleService(mockFeatureManager.Object);

            // Act
            var result = await service.IsFeatureEnabledAsync("EnableNewDashboard");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsFeatureEnabledAsync_ReturnsFalse_WhenFeatureIsDisabled()
        {
            // Arrange
            var mockFeatureManager = new Mock<IFeatureManager>();
            mockFeatureManager
                .Setup(fm => fm.IsEnabledAsync("UseAdvancedSearch"))
                .ReturnsAsync(false);

            var service = new FeatureToggleService(mockFeatureManager.Object);

            // Act
            var result = await service.IsFeatureEnabledAsync("UseAdvancedSearch");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task IsFeatureEnabledAsync_ReturnsFalse_WhenFeatureIsUnknown()
        {
            // Arrange
            var mockFeatureManager = new Mock<IFeatureManager>();
            mockFeatureManager
                .Setup(fm => fm.IsEnabledAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            var service = new FeatureToggleService(mockFeatureManager.Object);

            // Act
            var result = await service.IsFeatureEnabledAsync("UnknownFeature");

            // Assert
            Assert.False(result);
        }
    }
}