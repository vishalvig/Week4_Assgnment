using ApplicationTier.Dtos;
using ApplicationTier.Interfaces;
using IndustryConnect_Week_Microservices.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApplicationTier.Classes
{
    public class SaleMethods : ISaleMethods
    {
        private readonly IndustryConnectWeek2Context _context;

        public SaleMethods(IndustryConnectWeek2Context context)
        {
            _context = context;
        }
        public async Task<SaleDto> AddSale(int customerid, int productid, DateTime datesold, int storeid)
        {
            

            var sale = new Sale
            {
                CustomerId = customerid,
                ProductId = productid,
                DateSold = datesold,
                StoreId = storeid
            };
            _context.Sales.Add(sale);

            await _context.SaveChangesAsync();

            return Mapper(sale);
        }

        private static SaleDto Mapper(Sale? sale)
        {
            if (sale != null)
            {
                var saleDto = new SaleDto
                {
                    Id = sale.Id,
                    CustomerId = sale?.CustomerId,
                    ProductId = sale?.ProductId,
                    DateSold = sale?.DateSold,
                    StoreId = sale?.StoreId
                    //  Id = sale.Id
                };

                return saleDto;
            }
            else
            {
                return null;
            }
        }
    }
}
