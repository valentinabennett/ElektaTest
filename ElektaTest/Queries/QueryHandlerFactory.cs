using System;

namespace ElektaTest.Queries
{
    public class QueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IQueryHandler<TQuery, TResult> Create<TQuery, TResult>(TQuery query)
            where TQuery : IQuery where TResult : class
        {
            var genericType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            return (IQueryHandler<TQuery, TResult>)_serviceProvider.GetService(genericType);
        }
    }
}
