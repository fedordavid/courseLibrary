using System.Linq;
using System.Threading.Tasks;
using CourseLibrary.Application.Queries.Core;

namespace CourseLibrary.Application.Queries.Authors
{
    public class GetAuthors : IExecuteQuery<GetAuthorsQuery, AuthorView[]>
    {
        private readonly IAuthorViews _authors;

        public GetAuthors(IAuthorViews authors)
        {
            _authors = authors;
        }

        public Task<AuthorView[]> Execute(GetAuthorsQuery query) => GetAuthorsQuery.Apply(query, _authors.Authors).ToResult();
    }
}