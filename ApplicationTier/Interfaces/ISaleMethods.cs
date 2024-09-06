using ApplicationTier.Dtos;

namespace ApplicationTier.Interfaces
{
    public interface ISaleMethods
    {
        Task<SaleDto> AddSale(int CustomerId, int ProductId,DateTime DateSold, int StoreId);
    }
}
