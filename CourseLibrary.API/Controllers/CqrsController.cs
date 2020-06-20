using System.Threading.Tasks;
using CourseLibrary.Application.Queries.Core;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    public class CqrsController : ControllerBase
    {
        private readonly QueryBus _queryBus;

        public CqrsController(QueryBus queryBus)
        {
            _queryBus = queryBus;
        }

        protected async Task<ActionResult<TResult>> ExecuteQuery<TResult>(Query<TResult> query)
        {
            var result = await _queryBus.Execute(query);
            
            return Ok(result);
        }
        
        protected async Task<ActionResult<TResult[]>> ExecutePagedQuery<TResult>(Query<QueryResult<TResult>> query)
        {
            var result = await _queryBus.Execute(query);
            
            return Ok(await result.All);
        }
        
        protected async Task<ActionResult<TResult[]>> ExecutePagedQuery<TResult>(Query<QueryResult<TResult>> query, int pageSize, int page)
        {
            var result = await _queryBus.Execute(query);
            
            return Ok(await result.Paged(pageSize, page));
        }
        
        protected async Task<ActionResult<PagedResult<TResult>>> ExecutePagedQueryWithInfo<TResult>(Query<QueryResult<TResult>> query, int pageSize, int page)
        {
            var result = await _queryBus.Execute(query);
            
            return Ok(await result.PagedWithInfo(pageSize, page));
        }
    }
}