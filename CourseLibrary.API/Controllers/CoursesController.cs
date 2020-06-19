using AutoMapper;
using CourseLibrary.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseLibrary.Application.Commands;
using CourseLibrary.Application.Entities;
using CourseLibrary.Application.Queries;
using CourseLibrary.Application.Queries.Core;
using CourseLibrary.Application.Queries.Courses;

namespace CourseLibrary.API.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly ICourseLibraryQueryService _queryService;
        private readonly IMapper _mapper;
        private readonly QueryBus _queryBus;

        public CoursesController(ICourseLibraryRepository courseLibraryRepository,
            IMapper mapper, ICourseLibraryQueryService queryService, QueryBus queryBus)
        {
            _courseLibraryRepository = courseLibraryRepository ??
                throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _queryService = queryService;
            _queryBus = queryBus;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseView>>> GetCoursesForAuthor(Guid authorId)
        {
            if (!await _queryService.AuthorExists(authorId))
            {
                return NotFound();
            }

            var result = await _queryBus.Execute<GetCoursesQuery, QueryResult<CourseView>>(new GetCoursesQuery(authorId));
            
            return Ok(await result.All);
        }
        
        [HttpGet("paged")]
        public async Task<ActionResult<IEnumerable<CourseView>>> GetCoursesForAuthorPaged(Guid authorId, int pageSize, int page)
        {
            if (!await _queryService.AuthorExists(authorId))
            {
                return NotFound();
            }

            var result = await _queryBus.Execute<GetCoursesQuery, QueryResult<CourseView>>(new GetCoursesQuery(authorId));
            
            return Ok(await result.Paged(pageSize, page));
        }

        [HttpGet("{courseId}", Name = "GetCourseForAuthor")]
        public async Task<ActionResult<CourseView>> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            throw new NotImplementedException();
            
            // if (!await _queryService.AuthorExists(authorId))
            // {
            //     return NotFound();
            // }
            //
            // var courseForAuthorFromRepo = _queryService.GetCourse(new GetCourseQuery {AuthorId = authorId, CourseId = courseId});
            //
            // if (courseForAuthorFromRepo == null)
            // {
            //     return NotFound();
            // }
            //
            // return Ok(courseForAuthorFromRepo);
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourseForAuthor(
            Guid authorId, CourseForCreationDto course)
        {
            if (!await _queryService.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseEntity = _mapper.Map<Course>(course);
            _courseLibraryRepository.AddCourse(authorId, courseEntity);
            _courseLibraryRepository.Save();

            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);
            return CreatedAtRoute("GetCourseForAuthor",
                new { authorId = authorId, courseId = courseToReturn.Id }, 
                courseToReturn);
        }

        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourseForAuthor(Guid authorId, 
            Guid courseId, 
            CourseForUpdateDto course)
        {
            throw new NotImplementedException();

            
            // if (!await _queryService.AuthorExists(authorId))
            // {
            //     return NotFound();
            // }
            //
            // var courseForAuthorFromRepo = await _queryService.GetCourse(new GetCourseQuery {AuthorId = authorId, CourseId = courseId});
            //
            // if (courseForAuthorFromRepo == null)
            // {
            //     var courseToAdd = _mapper.Map<Course>(course);
            //     courseToAdd.Id = courseId;
            //
            //     _courseLibraryRepository.AddCourse(authorId, courseToAdd);
            //
            //     _courseLibraryRepository.Save();
            //
            //     var courseToReturn = _mapper.Map<CourseDto>(courseToAdd);
            //
            //     return CreatedAtRoute("GetCourseForAuthor",
            //         new { authorId, courseId = courseToReturn.Id },
            //         courseToReturn);
            // }
            //
            // // map the entity to a CourseForUpdateDto
            // // apply the updated field values to that dto
            // // map the CourseForUpdateDto back to an entity
            // _mapper.Map(course, courseForAuthorFromRepo);
            //
            // //_courseLibraryRepository.UpdateCourse(courseForAuthorFromRepo);
            //
            // _courseLibraryRepository.Save();
            // return NoContent();
        }

        [HttpPatch("{courseId}")]
        public async Task<ActionResult> PartiallyUpdateCourseForAuthor(Guid authorId, 
            Guid courseId,
            JsonPatchDocument<CourseForUpdateDto> patchDocument)
        {
            throw new NotImplementedException();

            //
            // if (!await _queryService.AuthorExists(authorId))
            // {
            //     return NotFound();
            // }
            //
            // var courseForAuthorFromRepo = await _queryService.GetCourse(new GetCourseQuery {AuthorId = authorId, CourseId = courseId});
            //
            // if (courseForAuthorFromRepo == null)
            // {
            //     var courseDto = new CourseForUpdateDto();
            //     patchDocument.ApplyTo(courseDto, ModelState);
            //
            //     if (!TryValidateModel(courseDto))
            //     {
            //         return ValidationProblem(ModelState);
            //     }
            //
            //     var courseToAdd = _mapper.Map<Course>(courseDto);
            //     courseToAdd.Id = courseId;
            //
            //     _courseLibraryRepository.AddCourse(authorId, courseToAdd);
            //     _courseLibraryRepository.Save();
            //
            //     var courseToReturn = _mapper.Map<CourseDto>(courseToAdd);
            //
            //     return CreatedAtRoute("GetCourseForAuthor",
            //         new { authorId, courseId = courseToReturn.Id }, 
            //         courseToReturn);
            // }
            //
            // var courseToPatch = _mapper.Map<CourseForUpdateDto>(courseForAuthorFromRepo);
            // // add validation
            // patchDocument.ApplyTo(courseToPatch, ModelState);
            //
            // if (!TryValidateModel(courseToPatch))
            // {
            //     return ValidationProblem(ModelState);
            // }
            //
            // _mapper.Map(courseToPatch, courseForAuthorFromRepo);
            //
            // //_courseLibraryRepository.UpdateCourse(courseForAuthorFromRepo);
            //
            // _courseLibraryRepository.Save();
            //
            // return NoContent();
        }

        [HttpDelete("{courseId}")]
        public async Task<ActionResult> DeleteCourseForAuthor(Guid authorId, Guid courseId)
        {
            throw new NotImplementedException();
            //
            // if (!await _queryService.AuthorExists(authorId))
            // {
            //     return NotFound();
            // }
            //
            // var courseForAuthorFromRepo = await _queryService.GetCourse(new GetCourseQuery {AuthorId = authorId, CourseId = courseId});
            //
            // if (courseForAuthorFromRepo == null)
            // {
            //     return NotFound();
            // }
            //
            // //_courseLibraryRepository.DeleteCourse(courseForAuthorFromRepo);
            // _courseLibraryRepository.Save();
            //
            // return NoContent();
        }

        public override ActionResult ValidationProblem(
            [ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices
                .GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}