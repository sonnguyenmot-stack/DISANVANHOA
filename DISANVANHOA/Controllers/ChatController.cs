
using DISANVANHOA.Areas.Admin.Services;
using DISANVANHOA.Filters;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Controllers
{
    
    public class ChatController : Controller
    {
        // GET: Chat
        private readonly GeminiChatService _gemini = new GeminiChatService();
        [RequireLoginPopup]
        public ActionResult Index() => View();

        [HttpPost]
        public async Task<JsonResult> Ask(string question, string chatHistory, HttpPostedFileBase file)
        {
            string fileBase64 = null;
            string mimeType = null;
            string extractedDocText = null;

            if (file != null && file.ContentLength > 0)// nếu file khác null và file >0
            {
                mimeType = file.ContentType;// thực hiện câu lệnh này

                // Nếu file là DOCX → đọc text
                if (mimeType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
                {
                    using (var ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        try
                        {
                            extractedDocText = ReadDocxText(ms);
                        }
                        catch (Exception ex)
                        {
                            extractedDocText = $"Không thể đọc file DOCX: {ex.Message}";
                        }
                    }
                }
                else
                {
                    using (var ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        fileBase64 = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }

            // Gộp lịch sử chat + file + câu hỏi hiện tại
            string fullQuestion = "";
            if (!string.IsNullOrEmpty(chatHistory))
                fullQuestion += chatHistory + "\n\n";
            if (!string.IsNullOrEmpty(extractedDocText))
                fullQuestion += "--- Nội dung từ file ---\n" + extractedDocText + "\n\n";
            fullQuestion += question;

            string answer = await _gemini.AskGeminiAsync(fullQuestion, fileBase64, mimeType);

            return Json(new { answer }, JsonRequestBehavior.AllowGet);
        }

        private string ReadDocxText(Stream stream)
        {
            StringBuilder sb = new StringBuilder();
            using (WordprocessingDocument doc = WordprocessingDocument.Open(stream, false))
            {
                Body body = doc.MainDocumentPart.Document.Body;
                foreach (var para in body.Elements<Paragraph>())
                {
                    sb.AppendLine(para.InnerText);
                }
            }
            return sb.ToString();
        }
    }
}