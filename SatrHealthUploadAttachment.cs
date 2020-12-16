using DownloadAttachmentConsole.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace DownloadAttachmentConsole
{
    public class SatrHealthUploadAttachment
    {
        public static void UploadAttachments(string DocPath, string FileName)
        {
            FileUploadResponse fileUploadResponse=null;
            if (!string.IsNullOrEmpty(DocPath))
            {
                var downFileResponse = DownloadFileAttachment(DocPath, FileName);
                if (downFileResponse != null && downFileResponse.FileBytes.IsValid())
                {
                    if (downFileResponse.FileBytes.Length > 0)
                    {
                        using (var stream = new MemoryStream(downFileResponse.FileBytes))
                        {
                            fileUploadResponse = UploadToFileSystem(FileName, stream);
                        }
                    }
                    if (fileUploadResponse.IsValid() && fileUploadResponse.IsSuccess) 
                    {
                        var Attchment= MapDownloadAttachment(fileUploadResponse);
                        DBUtil.Instance.UpdateAttachmentData(Attchment);
                    }
                }
                else
                {
                    throw new System.Exception("File Upload : " + FileName + " has no data");
                }
            }
            else
                throw new System.Exception("LetterStream is missing");


        }
        private static DownloadFileResponse DownloadFileAttachment(string docPath, string fileName)
        {

            string url = ConfigurationManager.AppSettings.Get("DownloadFileUrl");
            var headers = new Dictionary<string, string>()
                                    {
                                            {"Authorization", "Basic "+ ConfigurationManager.AppSettings.Get("DownloadFileHeader") },
                                            { "Content-Type", "application/json"}
                                    };
            ;
            SHFileResponse resp = null;
            try
            {
                WebRequest request = WebRequest.Create(url + docPath);

                if (headers != null)
                {
                    foreach (var v in headers)
                    {
                        request.Headers.Add(v.Key, v.Value);
                    }
                }
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string fileData = reader.ReadToEnd();
                reader.Close();
                response.Close();

                if (string.IsNullOrEmpty(fileData))
                    return new DownloadFileResponse() { IsSuccess = false, ErrorCode = "No File Found", FileName = docPath };
                else
                {
                    resp = JsonConvert.DeserializeObject<SHFileResponse>(fileData);

                    if (resp.Content.Length <= 0)
                        new DownloadFileResponse() { IsSuccess = false, ErrorCode = "File has no data", FileName = resp.Name };

                    return new DownloadFileResponse() { IsSuccess = true, FileBytes = resp.Content, FileName = resp.Name };

                }

            }
            catch (Exception ex)
            {
                return new DownloadFileResponse() { IsSuccess = false, ErrorCode = "Upload File Error : " + ex.Message };
            }
        }
        private static FileUploadResponse UploadToFileSystem(string FileName, Stream stream)
        {
            try
            {
                string folderName = "IHXPro-" + DateTime.Now.ToString("yyyy_MM_dd");
                string folderPath = ConfigurationManager.AppSettings.Get("UploadFileSytemPath") + "\\" + folderName;

                FileInfo fileInfo = new FileInfo(FileName);
                string filename = Guid.NewGuid().ToString() + fileInfo.Extension; //add extension

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                else
                {
                    if (File.Exists(folderPath + "\\" + filename))
                        filename += "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
                }

                UploadedFileDetail uploadedFileDetail = new UploadedFileDetail();
                using (FileStream fs = File.Create(folderPath + "\\" + filename))
                {
                    uploadedFileDetail.FileGuid = folderName + "\\" + filename;
                    uploadedFileDetail.FileName = FileName;
                    uploadedFileDetail.IsSuccess = true;

                    Byte[] body = ((MemoryStream)stream).ToArray();
                    fs.Write(body, 0, body.Length);
                    fs.Close();
                }

                return new FileUploadResponse() { IsSuccess = true, UploadedDetails = new List<UploadedFileDetail>() { { uploadedFileDetail } } };
            }
            catch (Exception ex)
            {
                return new FileUploadResponse() { IsSuccess = false, ErrorMsg = "Upload File Error : " + ex.Message };
            }

        }

        private static PayerHospActionAttachment MapDownloadAttachment(FileUploadResponse fileUploadResponse)
        {
            return new PayerHospActionAttachment() { AttachName = fileUploadResponse.UploadedDetails[0].FileName, AttachPath = fileUploadResponse.UploadedDetails[0].FileGuid };


        }



    }
}
