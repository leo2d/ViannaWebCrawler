using System.Net;
using System.Net.Http;
using ViannaWebCrawler.Controls.LoginControl;

namespace ViannaWebCrawler
{
    public class LoginRequest
    {
        LoginHeader _requestHeader = LoginHeader.GetInstance;

        public LoginData FormData { get; set; }
        public LoginHeader RequestHeader { get => _requestHeader; }
        public HttpClient Client { get; }

        public LoginRequest(LoginData formData)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => true;

            Client = new HttpClient();
            FormData = formData;
        }

        public HttpResponseMessage Login()
        {
            foreach (var field in _requestHeader.GetAsStringDictionary())
                Client.DefaultRequestHeaders.Add(field.Key, field.Value);

            HttpContent httpContent = new FormUrlEncodedContent(FormData.GetAsStringDictionary());

            var response = Client.PostAsync("https://aluno.vianna.edu.br/auth", httpContent);
            response.Wait();

            if (!response.Result.IsSuccessStatusCode)
                throw new LoginFailedException("Login request was failed!");

            var responseString = response.Result.Content.ReadAsStringAsync().Result;

            if (responseString.Contains("Login"))
                throw new LoginFailedException("Login was failed!");

            return response.Result;
        }
    }
}
