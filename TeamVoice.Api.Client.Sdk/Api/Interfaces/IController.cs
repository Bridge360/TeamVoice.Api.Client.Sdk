using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamVoice.Api.Interfaces
{
    public interface IController
    {
        Task<Results<IModel>> GetListAsync();
        Task<Result<IModel>> GetItemAsync(params object[] Keys);
        Type ModelType { get; }
        string Service { get; }
        bool HasItem { get; }
        bool HasList { get; }
        string ItemEndPoint { get; }
        string ListEndPoint { get; }
        string ItemUrl { get; }
        string ListUrl { get; }
        string MakeUrlExample(string Url, out object[] Arguments);
        List<ModelField> Fields { get; }
        string ModelDescription { get; }
    }

    public interface IController<T> : IController where T: IModel, new()
    {
    }
}
