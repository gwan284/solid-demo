namespace Images.Crawler
{
    public interface IImageRecorder
    {
        void SaveImage(byte[] imageBytes, string name);
        void SetConnectionString(string connectionString);
    }
}