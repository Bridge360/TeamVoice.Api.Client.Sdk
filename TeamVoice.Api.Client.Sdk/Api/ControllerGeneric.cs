using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TeamVoice.Api.Attributes;
using TeamVoice.Api.Handlers;
using TeamVoice.Api.Interfaces;
using TeamVoice.Api.Models;

namespace TeamVoice.Api
{
    public class Controller<T> : IController<T> where T : IModel, new()
    {
        private Credentials credentials;

        public Controller(Credentials Credentials)
        {
            if (Credentials == null)
                throw new InvalidOperationException("Credentials cannot be null");
            if (string.IsNullOrWhiteSpace(Credentials.GetAccountKey("TEAMVOICE")))
                throw new InvalidOperationException("Account Key cannot be blank");
            if (string.IsNullOrEmpty(Credentials.GetAppKey("TEAMVOICE")))
                throw new InvalidOperationException("API Key cannot be blank");
            credentials = Credentials;
        }

        #region IController<T>

        public async Task<Results<T>> GetListAsync()
        {
            var result = new Results<T>();
            try
            {
                if (!HasList) throw new InvalidOperationException("Getting " + ModelType.Name + " lists is not supported.");
                System.Net.ServicePointManager.Expect100Continue = false;
                HttpClient client = HttpClientFactory.Create(new HMACAuthenticationHandler(credentials));
                HttpResponseMessage response = await client.GetAsync(ListUrl).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    result.Values = (List<T>)JsonConvert.DeserializeObject<List<T>>(responseString);
                    result.Success = true;
                }
                else
                {
                    string reason = (response.StatusCode.ToString() == response.ReasonPhrase) ? "" : (" " + response.ReasonPhrase);
                    result.Error = string.Format("HTTP error {0}.{1}", response.StatusCode, reason);
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                result.Success = false;
            }
            return result;
        }

        public async Task<Result<T>> GetItemAsync(params object[] Keys)
        {
            var result = new Result<T>();
            try
            {
                if (!HasItem) throw new InvalidOperationException("Getting " + ModelType.Name + " items is not supported.");
                System.Net.ServicePointManager.Expect100Continue = false;
                HttpClient client = HttpClientFactory.Create(new HMACAuthenticationHandler(credentials));
                string endpoint = ItemUrl;
                var keys = Services.GetPrimaryKeys(ModelType.Name);
                for (int i = 0; i < keys.Count; i++)
                    endpoint = endpoint.Replace("{" + keys[i].Name + "}", Keys[i].ToString());
                HttpResponseMessage response = await client.GetAsync(endpoint).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    result.Value = (T)JsonConvert.DeserializeObject<T>(responseString);
                    result.Success = true;
                }
                else
                {
                    string reason = (response.StatusCode.ToString() == response.ReasonPhrase) ? "" : (" " + response.ReasonPhrase);
                    result.Error = string.Format("HTTP error {0}.{1}", response.StatusCode, reason);
                }
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
                result.Success = false;
            }
            return result;
        }

        public Type ModelType { get { return typeof(T); } }
        public string Service { get { return ((IModel)Activator.CreateInstance(typeof(T))).GetService(); } }
        public bool HasItem { get { return ((IModel)Activator.CreateInstance(typeof(T))).HasItem(); } }
        public bool HasList { get { return ((IModel)Activator.CreateInstance(typeof(T))).HasList(); } }
        public string ItemEndPoint { get { return ((IModel)Activator.CreateInstance(typeof(T))).GetItemEndPoint(); } }
        public string ListEndPoint { get { return ((IModel)Activator.CreateInstance(typeof(T))).GetListEndPoint(); } }
        public string ItemUrl { get { return ((IModel)Activator.CreateInstance(typeof(T))).GetItemUrl(); } }
        public string ListUrl { get { return ((IModel)Activator.CreateInstance(typeof(T))).GetListUrl(); } }
        public string MakeUrlExample(string Url, out object[] Arguments) { return ((IModel)Activator.CreateInstance(typeof(T))).MakeUrlExample(Url, out Arguments); }
        public List<ModelField> Fields { get { return ((IModel)Activator.CreateInstance(typeof(T))).GetFields(); } }
        public string ModelDescription { get { return ((IModel)Activator.CreateInstance(typeof(T))).GetModelDescription(); } }

        #endregion

        #region IController

        async Task<Results<IModel>> IController.GetListAsync()
        {
            var res = await GetListAsync();
            var results = new Results<IModel>();
            results.Values = (res.Values as IEnumerable<IModel>).ToList();
            results.Success = res.Success;
            results.Error = res.Error;
            return results;
        }

        async Task<Result<IModel>> IController.GetItemAsync(params object[] Keys)
        {
            var res = await GetItemAsync(Keys);
            var result = new Result<IModel>();
            result.Value = res.Value;
            result.Success = res.Success;
            result.Error = res.Error;
            return result;
        }

        Type IController.ModelType { get { return ModelType; } }
        string IController.Service { get { return Service; } }
        bool IController.HasItem { get { return HasItem; } }
        bool IController.HasList { get { return HasList; } }
        string IController.ItemEndPoint { get { return ItemEndPoint; } }
        string IController.ListEndPoint { get { return ListEndPoint; } }
        string IController.ItemUrl { get { return ItemUrl; } }
        string IController.ListUrl { get { return ListUrl; } }
        string IController.MakeUrlExample(string Url, out object[] Arguments) { return MakeUrlExample(Url, out Arguments); }
        List<ModelField> IController.Fields { get { return Fields; } }
        string IController.ModelDescription { get { return ModelDescription; } }

        #endregion
    }
}
