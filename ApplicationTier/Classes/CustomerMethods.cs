using IndustryConnect_Week_Microservices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationTier.Dtos;
using Microsoft.EntityFrameworkCore;
using ApplicationTier.Interfaces;

namespace ApplicationTier.Classes
{
    public class CustomerMethods : ICustomerMethods
    {
        private readonly IndustryConnectWeek2Context _context;

        // Constructor with dependency injection
        public CustomerMethods(IndustryConnectWeek2Context context)
        {
            _context = context;
        }

        public async Task<CustomerDto> AddCustomer(string firstName, string lastName, DateTime? dateOfBirth)
        {
            //var context = new IndustryConnectWeek2Context();

            var customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth
            };

            _context.Add(customer);

            await _context.SaveChangesAsync();
        
            return Mapper(customer);
       
        }

        public async Task<CustomerDto> GetCustomer(int CustomerId)
        {
            //var context = new IndustryConnectWeek2Context();

            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Id == CustomerId);

            return Mapper(customer);
        }

        public async Task<CustomerDto> DeleteCustomer(int CustomerId)
        {
                            // Retrieve the customer entity from the database by ID
                var customer = await _context.Customers.FindAsync(CustomerId);

                // Check if the customer exists
                if (customer == null)
                {
                    // If the customer is not found, you could return null or throw an exception
                    return null;
                }

                // Prepare a CustomerDto to return customer details after deletion
                var customerDto = new CustomerDto
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    DateOfBirth = customer.DateOfBirth
                };

                // Remove the customer entity from the database
                _context.Customers.Remove(customer);

                // Save changes to the database to persist the deletion
                await _context.SaveChangesAsync();

                // Return the CustomerDto with details of the deleted customer
                return customerDto;
            
        }

        private static CustomerDto Mapper(Customer? customer)
        {
            if (customer != null)
            {
                var customerDto = new CustomerDto
                {
                    FirstName = customer?.FirstName,
                    LastName = customer?.LastName,
                    DateOfBirth = customer?.DateOfBirth,
                    Id = customer.Id
                };

                return customerDto;
            }
            else
            {
                return null;
            }
           
        }

    }
}
