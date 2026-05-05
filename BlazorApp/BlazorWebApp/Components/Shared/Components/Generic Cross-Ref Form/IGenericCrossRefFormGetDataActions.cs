using System.Threading.Tasks;

namespace BlazorWebApp.Shared.Components.Generic_Cross_Ref_Form
{
    public interface IGenericCrossRefFormGetDataActions<T>
    {
        Task<T> GetData(T t);
    }


}
