using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DISANVANHOA.Areas.Admin.Services
{
    public class GeminiChatService
    {
        private readonly string apiKey;
        private readonly HttpClient client;

        public GeminiChatService()
        {
            apiKey = System.Configuration.ConfigurationManager.AppSettings["GeminiApiKey"];
            client = new HttpClient();
        }

        public async Task<string> AskGeminiAsync(string question, string fileBase64 = null, string mimeType = null)
        {
            var parts = new List<object>();

            if (!string.IsNullOrWhiteSpace(question))
                parts.Add(new { text = question });

            if (!string.IsNullOrEmpty(fileBase64))
            {
                parts.Add(new
                {
                    inline_data = new
                    {
                        mime_type = mimeType,
                        data = fileBase64
                    }
                });
            }

            var body = new
            {
                contents = new[]
                {
                    new { parts = parts.ToArray() }
                },
                tools = new object[]
                {
                    new { google_search = new { } }
                }
            };

            var request = new HttpRequestMessage(HttpMethod.Post,
                "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent");
            request.Headers.Add("x-goog-api-key", apiKey);
            request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            var respText = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return $"Lỗi API ({(int)response.StatusCode}): {respText}";

            try
            {
                var json = JObject.Parse(respText);
                var candidate = json["candidates"]?[0]?["content"]?["parts"];
                if (candidate != null)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var part in candidate)
                    {
                        var text = part["text"]?.ToString();
                        if (!string.IsNullOrEmpty(text))
                        {
                            sb.AppendLine(text.Trim());
                        }
                    }
                    return sb.ToString();
                }
                return "Phản hồi rỗng: " + respText;
            }
            catch
            {
                return "Lỗi parse JSON: " + respText;
            }
        }
    }
}
