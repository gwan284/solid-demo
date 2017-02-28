using System.Drawing;
using System.IO;

namespace Images.Crawler
{
    public class DatabaseFileManager : IImageRecorder
    {
        string connectionString = string.Empty;

        public DatabaseFileManager()
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            this.SetConnectionString(cs);
        }

        public DatabaseFileManager(string connectionstring)
        {
            this.SetConnectionString(connectionstring);
        }

        public void SetConnectionString(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void SaveImage(byte[] imageBytes, string name)
        {
            System.Drawing.Bitmap bmp;
            using (var ms = new MemoryStream(imageData))
            {
                bmp = new Bitmap(ms);
                bmp.Save(name);
            }
        }
    }
}
