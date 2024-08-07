using Google.Apis.Auth.OAuth2;

namespace FCMService
{
    public class TokenService
    {
        public async Task<string> GetAccessTokenAsync()
        {
            string[] scopes = { "https://www.googleapis.com/auth/firebase.messaging" };
            string credentialPath = "your_json_file_name"; // Kimlik bilgileri dosyanızın yolu

            GoogleCredential credential;
            using (var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(scopes);
            }

            var token = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
            return token;
        }
    }

}
