namespace FeatureFlagsDemo.Models
{

    public class FeatureStatusResponse
    {
        public required string Feature { get; set; }
        public bool Enabled { get; set; }
    }

}
