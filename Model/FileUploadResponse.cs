using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadAttachmentConsole.Model
{
    public class FileUploadResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMsg { get; set; }

        public List<UploadedFileDetail> UploadedDetails { get; set; }
    }

    public class FileUploadRequest
    {
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }

        public string DocPath { get; set; }
    }

    public class UploadedFileDetail
    {
        public bool IsSuccess { get; set; }
        public string ErrorMsg { get; set; }
        public string FileGuid { get; set; }
        public string FileName { get; set; }
    }

}
