using System.Threading.Tasks;

namespace CourseLibrary.Application.Queries.Core
{
    // public interface IExecuteQuery
    // {
    //     Task<TResult> Execute<TQuery, TResult>(TQuery query) where TQuery : Query<TResult>;
    // }
    
    public interface IExecuteQuery<in TQuery, TResult> 
        where TQuery : Query<TResult>
    {
        Task<TResult> Execute(TQuery query);
    }
}