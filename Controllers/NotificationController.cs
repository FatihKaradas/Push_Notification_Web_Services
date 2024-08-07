using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;

namespace FCMService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public NotificationController()
        {
            // FirebaseApp'ı sadece bir kez başlatıyoruz
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("path/to/serviceAccountKey.json"),
                });
            }
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                return BadRequest("Token is required");
            }

            var message = new Message()
            {
                Notification = new Notification()
                {
                    Title = "Your Notification Title",
                    Body = "Your Notification Body",
                },
                Data = new Dictionary<string, string>()
                {
                    { "clickableText1", "Red" },
                    { "clickableText1Color", "#FF0000" }, // Kırmızı
                    { "clickableText2", "Onay" },
                    { "clickableText2Color", "#0000FF" }, // Mavi
                },
                Token = request.Token,
                Android = new AndroidConfig()
                {
                    Notification = new AndroidNotification()
                    {
                        ClickAction = "FLUTTER_NOTIFICATION_CLICK",
                        Color = "#ff0000", // Bildirim rengi
                    },
                }
            };

            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            return Ok(new { MessageId = response });
        }
    }

    public class NotificationRequest
    {
        public string Token { get; set; }
    }
}
