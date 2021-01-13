using System.Threading.Tasks;

namespace Poke.App.LazyLoading
{
    public interface ILazyLoadAsync
    {
        Task InitializeAsync(int pageSize);
        Task LoadNextAsync(int pageSize);
    }
}
