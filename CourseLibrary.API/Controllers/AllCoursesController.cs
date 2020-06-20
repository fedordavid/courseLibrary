using System.Threading.Tasks;
using CourseLibrary.Application.Queries.Core;
using CourseLibrary.Application.Queries.Courses;
using Microsoft.AspNetCore.Mvc;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class AllCoursesController : CqrsController
    {
        public AllCoursesController(QueryBus queryBus) : base(queryBus) { }

        [HttpGet]
        public Task<ActionResult<PagedResult<CourseView>>> GetAllPaged(int pageSize, int page)
        {
            return ExecutePagedQueryWithInfo(new GetAllCoursesQuery(), pageSize, page);
        }
    }
}