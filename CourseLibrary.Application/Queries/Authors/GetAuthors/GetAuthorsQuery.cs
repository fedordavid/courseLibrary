using System.Linq;
using CourseLibrary.Application.Queries.Core;

namespace CourseLibrary.Application.Queries.Authors
{
    public class GetAuthorsQuery : Query<AuthorView[]>
    {
        private readonly string _mainCategory;
        private readonly string _searchQuery;

        public GetAuthorsQuery(string searchQuery, string mainCategory)
        {
            _searchQuery = searchQuery?.Trim();
            _mainCategory = mainCategory?.Trim();
        }

        internal IQueryable<AuthorView> ApplyTo(IQueryable<AuthorView> authors)
        {
            if (!string.IsNullOrWhiteSpace(_mainCategory))
            {
                authors = authors.Where(a => a.MainCategory == _mainCategory);
            }

            if (!string.IsNullOrWhiteSpace(_searchQuery))
            {
                authors = authors.Where(a => a.MainCategory.Contains(_searchQuery)
                                             || a.Name.Contains(_searchQuery));
            }

            return authors;
        }
    }
}