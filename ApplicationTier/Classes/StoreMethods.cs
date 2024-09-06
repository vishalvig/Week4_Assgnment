using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationTier.Dtos;
using ApplicationTier.Interfaces;
using IndustryConnect_Week_Microservices.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationTier.Classes
{
    public class StoreMethods : IStoreMethods
    {

        private readonly IndustryConnectWeek2Context _context;

        // Constructor with dependency injection
        public StoreMethods(IndustryConnectWeek2Context context)
        {
            _context = context;
        }

        public async Task<StoreDto> AddStore(string name)
        {
            //var context = new IndustryConnectWeek2Context();

            var store = new Store
            {
               Name = name
            };

            _context.Add(store);

            await _context.SaveChangesAsync();

            return Mapper(store);

        }


        public async Task<StoreDto> GetStore(int StoreId)
        {
            //var context = new IndustryConnectWeek2Context();

            var store = await _context.Stores.Include(s => s.Sales)
                .ThenInclude(p => p.Product).FirstOrDefaultAsync(s => s.Id == StoreId);


            return Mapper(store);

        }

        private static StoreDto? Mapper(Store store)
        {
            if (store != null)
            {
                var storeDto = new StoreDto
                {
                    Name = store.Name,
                    Id = store.Id,
                    TotalSale = store.Sales?.Sum(p => p.Product.Price)
                };

                return storeDto;
            }
            else
            {
                return null;
            }
        }
    }
}
