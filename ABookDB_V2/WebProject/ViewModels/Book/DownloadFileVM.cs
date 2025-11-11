using Humanizer.Bytes;

namespace WebProject.ViewModels.Book
{
    public class DownloadFileVM
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
}
