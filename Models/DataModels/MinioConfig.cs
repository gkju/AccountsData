using Amazon;

namespace AccountsData.Models.DataModels
{
    public class MinioConfig
    {
        public string BucketName { get; set; }
        public string VideoBucketName { get; set; }
        public static readonly string AuthenticationRegion = RegionEndpoint.EUCentral1.SystemName;
    }
}