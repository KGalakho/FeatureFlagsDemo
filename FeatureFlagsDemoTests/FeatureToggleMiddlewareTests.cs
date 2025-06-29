using Moq;
using Microsoft.AspNetCore.Http;
using Microsoft.FeatureManagement;
using FeatureFlagsDemo.Middleware;

namespace FeatureFlagsDemo.Tests
{
    public class FeatureToggleMiddlewareTests
    {
        [Fact]
        public async Task Middleware_BlocksRequest_WhenFeatureIsDisabled()
        {
            // Arrange
            var mockFeatureManager = new Mock<IFeatureManager>();
            mockFeatureManager
                .Setup(fm => fm.IsEnabledAsync("BlockLegacyEndpoints"))
                .ReturnsAsync(false);

            var context = new DefaultHttpContext();
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            RequestDelegate next = (HttpContext ctx) => Task.CompletedTask;

            var middleware = new FeatureToggleMiddleware(next, "BlockLegacyEndpoints");

            // Act
            await middleware.InvokeAsync(context, mockFeatureManager.Object);

            // Assert
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(context.Response.Body);
            var responseText = await reader.ReadToEndAsync();

            Assert.Equal(StatusCodes.Status403Forbidden, context.Response.StatusCode);
            Assert.Contains("disabled", responseText);
        }

        [Fact]
        public async Task Middleware_AllowsRequest_WhenFeatureIsEnabled()
        {
            // Arrange
            var mockFeatureManager = new Mock<IFeatureManager>();
            mockFeatureManager
                .Setup(fm => fm.IsEnabledAsync("BlockLegacyEndpoints"))
                .ReturnsAsync(true);

            var context = new DefaultHttpContext();
            var wasCalled = false;

            RequestDelegate next = (HttpContext ctx) =>
            {
                wasCalled = true;
                return Task.CompletedTask;
            };

            var middleware = new FeatureToggleMiddleware(next, "BlockLegacyEndpoints");

            // Act
            await middleware.InvokeAsync(context, mockFeatureManager.Object);

            // Assert
            Assert.True(wasCalled);
        }
    }
}
