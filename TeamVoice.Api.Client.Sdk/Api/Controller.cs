using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TeamVoice.Api.Attributes;
using TeamVoice.Api.Interfaces;

namespace TeamVoice.Api
{
    public class Controller : IController
    {
        public const string ApiPath = "api/v1/";
        private Credentials credentials;
        private string modelName;
        private Type modelType;
        private Type controllerType;
        private Type resultType;
        private Type resultsType;
        private object controller;

        public Controller(Credentials Credentials, string ModelName)
        {
            if (Credentials == null)
                throw new InvalidOperationException("Credentials cannot be null");
            if (string.IsNullOrWhiteSpace(Credentials.GetAccountKey("TEAMVOICE")))
                throw new InvalidOperationException("Account Key cannot be blank");
            if (string.IsNullOrEmpty(Credentials.GetAppKey("TEAMVOICE")))
                throw new InvalidOperationException("API Key cannot be blank");
            if (string.IsNullOrEmpty(ModelName))
                throw new InvalidOperationException("ModelName cannot be blank");
            credentials = Credentials;
            modelName = ModelName;
            modelType = Services.GetModelType(modelName);
            controllerType = typeof(Controller<>).MakeGenericType(new Type[] { modelType });
            resultType = typeof(Result<>).MakeGenericType(new Type[] { modelType });
            resultsType = typeof(Results<>).MakeGenericType(new Type[] { modelType });
            controller = Activator.CreateInstance(controllerType, new object[] { credentials });
        }

        public static string BaseAddress
        {
            get
            {
                if (Services.Environment == Environments.Production) return "https://api.teamvoice.co.uk/";
                else if (Services.Environment == Environments.Development) return "https://localhost:44307/";
                else throw new InvalidOperationException("Unknown environment " + Services.Environment.ToString());
            }
        }

        #region IController

        public async Task<Results<IModel>> GetListAsync()
        {
            try
            {
                var task = ((Task)controllerType.GetMethod("GetListAsync").Invoke(controller, null));
                await task;
                var res = typeof(Task<>).MakeGenericType(resultsType).GetProperty("Result").GetValue(task);
                var results = new Results<IModel>();
                results.Values = (res.GetType().GetProperty("Values").GetValue(res) as IEnumerable<IModel>).ToList();
                results.Success = (bool)res.GetType().GetProperty("Success").GetValue(res);
                results.Error = (string)res.GetType().GetProperty("Error").GetValue(res);
                return results;
            }
            catch (Exception ex)
            {
                return new Results<IModel> { Error = ex.Message, Success = false };
            }
        }

        public async Task<Result<IModel>> GetItemAsync(params object[] Keys)
        {
            try
            {
                var task = (Task)controllerType.GetMethod("GetItemAsync").Invoke(controller, new object[] { Keys });
                await task;
                var res = typeof(Task<>).MakeGenericType(resultType).GetProperty("Result").GetValue(task);
                var result = new Result<IModel>();
                result.Value = res.GetType().GetProperty("Value").GetValue(res) as IModel;
                result.Success = (bool)res.GetType().GetProperty("Success").GetValue(res);
                result.Error = (string)res.GetType().GetProperty("Error").GetValue(res);
                return result;
            }
            catch (Exception ex)
            {
                return new Result<IModel> { Error = ex.Message, Success = false };
            }
        }

        public Type ModelType { get { return modelType; } }
        public string Service { get { return controller.GetType().GetProperty("Service").GetValue(controller) as string; } }
        public bool HasItem { get { return (bool)controller.GetType().GetProperty("HasItem").GetValue(controller); } }
        public bool HasList { get { return (bool)controller.GetType().GetProperty("HasList").GetValue(controller); } }
        public string ItemEndPoint { get { return controller.GetType().GetProperty("ItemEndPoint").GetValue(controller) as string; } }
        public string ListEndPoint { get { return controller.GetType().GetProperty("ListEndPoint").GetValue(controller) as string; } }
        public string ItemUrl { get { return controller.GetType().GetProperty("ItemUrl").GetValue(controller) as string; } }
        public string ListUrl { get { return controller.GetType().GetProperty("ListUrl").GetValue(controller) as string; } }

        public string MakeUrlExample(string Url, out object[] Arguments)
        {
            object[] parms = new object[] { Url, null };
            string result = controller.GetType().GetMethod("MakeUrlExample").Invoke(controller, parms) as string;
            Arguments = (object[])parms[1];
            return result;
        }

        public List<ModelField> Fields { get { return controller.GetType().GetProperty("Fields").GetValue(controller) as List<ModelField>; } }
        public string ModelDescription { get { return controller.GetType().GetProperty("ModelDescription").GetValue(controller) as string; } }

        #endregion
    }
}