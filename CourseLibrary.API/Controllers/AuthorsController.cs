using AutoMapper;
using CourseLibrary.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CourseLibrary.Application.Commands;
using CourseLibrary.Application.Entities;
using CourseLibrary.Application.Queries;
using CourseLibrary.Application.Queries.Authors;
using CourseLibrary.Application.Queries.Core;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : CqrsController
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;
        
        private readonly ICourseLibraryQueryService _queryService;

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository,
            IMapper mapper, ICourseLibraryQueryService queryService, QueryBus queryBus)
        : base(queryBus)
        {
            _courseLibraryRepository = courseLibraryRepository;
            _mapper = mapper;
            _queryService = queryService;
        }

        [HttpGet]
        [HttpHead]
        public Task<ActionResult<AuthorView[]>> GetAuthors(string searchQuery, string mainCategory)
        {
            return ExecuteQuery(new GetAuthorsQuery(searchQuery, mainCategory));
        }

        [HttpGet("{authorId}", Name ="GetAuthor")]
        public async Task<IActionResult> GetAuthor(Guid authorId)
        {
            var authorFromRepo = await _queryService.GetAuthor(authorId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }
             
            return Ok(authorFromRepo);
        }

        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(AuthorForCreationDto author)
        {
            var authorEntity = _mapper.Map<Author>(author);
            _courseLibraryRepository.AddAuthor(authorEntity);
            _courseLibraryRepository.Save();

            var authorToReturn = _mapper.Map<AuthorDto>(authorEntity);
            return CreatedAtRoute("GetAuthor",
                new { authorId = authorToReturn.Id },
                authorToReturn);
        }

        [HttpOptions]
        public IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }

        [HttpDelete("{authorId}")]
        public async Task<ActionResult> DeleteAuthor(Guid authorId)
        {
            var authorFromRepo = await _queryService.GetAuthor(authorId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }

            //_courseLibraryRepository.DeleteAuthor(authorFromRepo);

            _courseLibraryRepository.Save();

            return NoContent();
        }
    }
}
