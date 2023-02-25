using _1_API.ViewModel.NhanVien;
using Data.ModelsClass;

namespace ProjectViews.IServices
{
    public interface IAllServices
    {
        Task<T> GetById<T>(string url,Guid? id);
        Task<T> Add<T>(string url,T model);
        Task<int> Remove<T>(string urlGetById, string urlRemove, Guid id);
        Task<T> Update<T>(string url, T model, Guid id);
        Task<IEnumerable<T>> GetAll<T>(string url);
    }
}
