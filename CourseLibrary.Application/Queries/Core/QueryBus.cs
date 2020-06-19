using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CourseLibrary.Application.Queries.Core
{
    public class QueryBus
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResult> Execute<TQuery, TResult>(TQuery query) where TQuery : Query<TResult>
        {
            var executor = _serviceProvider.GetService<IExecuteQuery<TQuery, TResult>>();
            
            return executor.Execute(query);
        }
        
        //Execute<GetCoursesQuery, CourseViews[]>(new GetCoursesQuery())
    }
}