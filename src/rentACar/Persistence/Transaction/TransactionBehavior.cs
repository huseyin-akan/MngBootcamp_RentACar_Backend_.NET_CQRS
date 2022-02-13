using Core.Application.Pipelines.Transaction;
using MediatR;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Transaction
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ITransactionalRequest
    {
        private readonly BaseDbContext _context;

        public TransactionBehavior(BaseDbContext context)
        {
            _context = context;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            var response = await next();
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return response;
        }
    }
}
