using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace TeamVoice.Api.Handlers
{
    public class HMACAuthenticationHandler : DelegatingHandler
    {
        private Credentials credentials;
        
        public HMACAuthenticationHandler(Credentials Credentials)
        {
            credentials = Credentials;
        }
        
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            string requestContentBase64String = string.Empty;
            string requestUri = WebUtility.UrlEncode(request.RequestUri.AbsoluteUri.ToLower()).ToLower();
            string requestHttpMethod = request.Method.Method;

            // Calculate UNIX time
            DateTime epochStart = new DateTime(1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan timeSpan = DateTime.UtcNow - epochStart;
            string requestTimeStamp = Convert.ToUInt64(timeSpan.TotalSeconds).ToString();

            // Create random nonce for each request
            string nonce = Guid.NewGuid().ToString("N");

            // Hash the request body
            if (request.Content != null)
            {
                byte[] content = await request.Content.ReadAsByteArrayAsync();
                MD5 md5 = MD5.Create();
                // Any change in request body will result in different hash, ensuring message integrity
                byte[] requestContentHash = md5.ComputeHash(content);
                requestContentBase64String = Convert.ToBase64String(requestContentHash);
            }

            // Create the raw signature string
            string signatureRawData = String.Format("{0}|{1}|{2}|{3}|{4}|{5}", credentials.GetAccountKey("TEAMVOICE"), 
                requestHttpMethod, requestUri, requestTimeStamp, nonce, requestContentBase64String);
            string requestSignatureBase64String = GetHMACSignature(signatureRawData, credentials.GetAppKey("TEAMVOICE"));

            //Set the values in the Authorization header using custom scheme (tvs)
            request.Headers.Authorization = new AuthenticationHeaderValue("tvs", string.Format("{0}:{1}:{2}:{3}",
                credentials.GetAccountKey("TEAMVOICE"), requestSignatureBase64String, nonce, requestTimeStamp));

            // Send request
            response = await base.SendAsync(request, cancellationToken);
            return response;
        }

        private string GetHMACSignature(string message, string secret)
        {
            var encoding = new System.Text.ASCIIEncoding();
            using (var hmac = new HMACSHA256(encoding.GetBytes(secret ?? "")))
                return Convert.ToBase64String(hmac.ComputeHash(encoding.GetBytes(message ?? "")));
        }
    }
}
