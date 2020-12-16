using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadAttachmentConsole.Model
{
    public class DownloadFileResponse
    {
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }
        public bool IsSuccess { get; set; }

        public bool IsStatus { get; set; } // only for star
        public string ErrorCode { get; set; }
    }
}
