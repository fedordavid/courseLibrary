using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CourseLibrary.Application.Queries.Core
{
    public class QueryResult<TView>
    {
        private readonly IQueryable<TView> _views;

        internal QueryResult(IQueryable<TView> views)
        {
            _views = views;
        }

        public Task<TView[]> All => _views.ToArrayAsync();

        public Task<PagedResultWithInfo<TView>> PagedWithInfo(int pageSize, int page)
        {
            return PagedResultWithInfo<TView>.From(pageSize, page, _views);
        }
        
        public Task<PagedResult<TView>> Paged(int pageSize, int page)
        {
            return PagedResult<TView>.From(pageSize, page, _views);
        }
    }
    
    public class PagedResultWithInfo<TView>
    {
        public int Count { get; }
        public TView[] Data { get; }
        public int PageSize { get; }
        public int Page { get; }
        
        private PagedResultWithInfo(int pageSize, int page, TView[] data, int count)
        {
            Count = count;
            Data = data;
            PageSize = pageSize;
            Page = page;
        }

        internal static async Task<PagedResultWithInfo<TView>> From(int pageSize, int page, IQueryable<TView> views)
        {
            var count = await views.CountAsync();
            
            var data = await views
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToArrayAsync();
            
            return new PagedResultWithInfo<TView>(pageSize, page, data, count);
        }
        
    }
    
    public class PagedResult<TView> : Collection<TView>
    {
        private PagedResult(IList<TView> list) : base(list)
        {
        }

        internal static async Task<PagedResult<TView>> From(int pageSize, int page, IQueryable<TView> views)
        {
            var data = await views
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToArrayAsync();
            
            return new PagedResult<TView>(data);
        }
    }
}