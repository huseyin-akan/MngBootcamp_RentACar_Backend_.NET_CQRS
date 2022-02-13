using Core.Application.Pipelines.Transaction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Core.Application.Transaction
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ITransactionalRequest
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            using (TransactionScope transactionScope = new TransactionScope(
                TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var response = await next();
                    transactionScope.Complete();
                    return response;
                }
                catch (Exception)
                {
                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}
