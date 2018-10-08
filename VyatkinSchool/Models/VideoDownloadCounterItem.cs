using System;

namespace VyatkinSchool.Models
{
    public sealed class VideoDownloadCounterItem
    {
        public int Id { get; set; }
        public int VideoFileId { get; set; }
        public DateTime LastDownload { get; set; }
        public int DownloadCount { get; set; }
    }
}