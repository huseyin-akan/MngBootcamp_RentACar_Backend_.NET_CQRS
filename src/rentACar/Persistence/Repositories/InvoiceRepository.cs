using Application.Features.Invoices.Dtos;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class InvoiceRepository : EfRepositoryBase<Invoice, BaseDbContext>, IInvoiceRepository
    {
        public InvoiceRepository(BaseDbContext context) : base(context)
        {
            
        }

        public async Task<IPaginate<InvoiceListDto>> GetAllInvoices(
            int index = 0,
            int size = 10,
            CancellationToken cancellationToken = default)
        {
            using (Context)
            {
                var result = from i in Context.Invoices
                             join r in Context.Rentals
                             on i.RentalId equals r.Id
                             join cu in Context.Customers
                             on i.CustomerId equals cu.Id
                             join c in Context.Cars
                             on r.CarId equals c.Id
                             join m in Context.Models
                             on c.ModelId equals m.Id
                             join b in Context.Brands
                             on m.BrandId equals b.Id

                             select new InvoiceListDto
                             {
                                 Id = i.Id,
                                 InvoiceDate = i.InvoiceDate,
                                 InvoiceNo = i.InvoiceNo,
                                 TotalSum = i.TotalSum,
                                 RentedDate = r.RentDate,
                                 ReturnDate = r.ReturnDate,
                                 CustomerMail = cu.Email,
                                 Brand = b.Name,
                                 CarModel = m.Name,
                                 ModelYear = c.ModelYear,
                                 Plate = c.Plate,
                                 RentedKilometer = r.RentedKilometer,
                                 TotalDayCount = (r.ReturnDate.Date - r.RentDate.Date).Days
                             };

                return await result.ToPaginateAsync(index, size, 0, cancellationToken);
            }
        }

        public async Task<IPaginate<InvoiceListDto>> GetAllInvoicesBetweenDates(DateTime startDate, DateTime endDate, int index = 0, int size = 10, CancellationToken cancellationToken = default)
        {
            using (Context)
            {
                var result = from i in Context.Invoices
                             join r in Context.Rentals
                             on i.RentalId equals r.Id
                             join cu in Context.Customers
                             on i.CustomerId equals cu.Id
                             join c in Context.Cars
                             on r.CarId equals c.Id
                             join m in Context.Models
                             on c.ModelId equals m.Id
                             join b in Context.Brands
                             on m.BrandId equals b.Id
                             where startDate <= i.InvoiceDate && endDate >= i.InvoiceDate

                             select new InvoiceListDto
                             {
                                 Id = i.Id,
                                 InvoiceDate = i.InvoiceDate,
                                 InvoiceNo = i.InvoiceNo,
                                 TotalSum = i.TotalSum,
                                 RentedDate = r.RentDate,
                                 ReturnDate = r.ReturnDate,
                                 CustomerMail = cu.Email,
                                 Brand = b.Name,
                                 CarModel = m.Name,
                                 ModelYear = c.ModelYear,
                                 Plate = c.Plate,
                                 RentedKilometer = r.RentedKilometer,
                                 TotalDayCount = (r.ReturnDate.Date - r.RentDate.Date).Days
                             };

                return await result.ToPaginateAsync(index, size, 0, cancellationToken);
            }
        }

        public async Task<IPaginate<InvoiceListDto>> GetAllInvoicesByCustomerId(int customerId, int index = 0, int size = 10, CancellationToken cancellationToken = default)
        {
            using (Context)
            {
                var result = from i in Context.Invoices
                             join r in Context.Rentals
                             on i.RentalId equals r.Id
                             join cu in Context.Customers
                             on i.CustomerId equals cu.Id
                             join c in Context.Cars
                             on r.CarId equals c.Id
                             join m in Context.Models
                             on c.ModelId equals m.Id
                             join b in Context.Brands
                             on m.BrandId equals b.Id
                             where customerId == i.CustomerId

                             select new InvoiceListDto
                             {
                                 Id = i.Id,
                                 InvoiceDate = i.InvoiceDate,
                                 InvoiceNo = i.InvoiceNo,
                                 TotalSum = i.TotalSum,
                                 RentedDate = r.RentDate,
                                 ReturnDate = r.ReturnDate,
                                 CustomerMail = cu.Email,
                                 Brand = b.Name,
                                 CarModel = m.Name,
                                 ModelYear = c.ModelYear,
                                 Plate = c.Plate,
                                 RentedKilometer = r.RentedKilometer,
                                 TotalDayCount = (r.ReturnDate.Date - r.RentDate.Date).Days
                             };

                return await result.ToPaginateAsync(index, size, 0, cancellationToken);
            }
        }
    }
}
