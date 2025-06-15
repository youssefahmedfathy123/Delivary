namespace Tawsel.SevicesLayer.Product
{
    public interface IProductServices
    {
        Task<string> Add();
        Task<string> Update();
        Task<string> Delete();
        Task<string> GetProduct();

    }
}



