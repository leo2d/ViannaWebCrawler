using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            Client = new HttpClient();
            FormData = formData;
        }

        public HttpResponseMessage Login()
        {
            foreach (var field in _requestHeader.GetAsStringDictionary())
                Client.DefaultRequestHeaders.Add(field.Key, field.Value);

            HttpContent httpContent = new FormUrlEncodedContent(FormData.GetAsStringDictionary());

            var response = Client.PostAsync("http://aluno.viannajr.edu.br/auth", httpContent);

            while (!response.IsCompleted)
                Thread.Sleep(3000);

            if (!response.Result.IsSuccessStatusCode)
                throw new LoginFailedException("Login was failed!");

            var responseString = response.Result.Content.ReadAsStringAsync().Result;

            return response.Result;
        }
    }
}
