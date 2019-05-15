using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using FluentValidation;
using Library.Author.Service;
using Library.Author.Service.Requests;
using Library.Author.Service.Responses;
using Library.Author.Service.ServiceModels;
using Library.Common.Data;
using Microsoft.AspNetCore.Mvc;

namespace Library.Author.Api.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetAuthorsHttpResponse), (int) HttpStatusCode.OK)]
        public IActionResult Get([FromQuery] GetAuthorHttpRequest request)
        {
            var serviceResponse = _authorService.GetAuthors(request.Offset, request.Limit);

            var httpResponse = new GetAuthorsHttpResponse
                               {
                                   Total = serviceResponse.Total, Authors = serviceResponse.Authors
                               };

            return StatusCode((int) HttpStatusCode.OK, httpResponse);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetAuthorsHttpResponse), (int) HttpStatusCode.OK)]
        public IActionResult Get([FromRoute] string id)
        {
            var serviceResponse = _authorService.GetAuthor(id);

            var httpResponse = new GetAuthorsHttpResponse
                               {
                                   Authors = new List<AuthorModel> {serviceResponse.Author}
                               };

            return StatusCode((int) HttpStatusCode.OK, httpResponse);
        }


        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationException), (int) HttpStatusCode.BadRequest)]
        public IActionResult Post([FromBody] PostAuthorHttpRequest request)
        {
            _authorService.InsertAuthor(new InsertAuthorServiceRequest
                                        {
                                            Author = new AuthorModel
                                                     {
                                                         Name = request.Name, Data = new AuthorModelMetaData
                                                                                     {
                                                                                         Bio = request.Bio
                                                                                         , Dead = request.Dead
                                                                                         , Books = request.Books
                                                                                         , Birthday = request.Birthday
                                                                                         , Location = request.Location
                                                                                         , ImageUrl = request.ImageUrl
                                                                                     }
                                                     }
                                        });

            return StatusCode((int) HttpStatusCode.Created);
        }


        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationException), (int) HttpStatusCode.BadRequest)]
        public IActionResult Put([FromRoute] string id, [FromBody] PutAuthorHttpRequest request)
        {
            _authorService.UpdateAuthor(new AuthorServiceRequest
                                        {
                                            Id = id, Author = new AuthorModel
                                                              {
                                                                  Name = request.Name, 
                                                                  Data = new AuthorModelMetaData
                                                                                              {
                                                                                                  Bio = request.Bio
                                                                                                  , Dead = request.Dead
                                                                                                  , Books = request
                                                                                                      .Books
                                                                                                  , Birthday =
                                                                                                      request.Birthday
                                                                                                  , Location =
                                                                                                      request.Location
                                                                                                  , ImageUrl =
                                                                                                      request.ImageUrl
                                                                                              }
                                                              }
                                        });

            return StatusCode((int) HttpStatusCode.Accepted);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] string id)
        {
            _authorService.DeleteAuthor(id);

            return StatusCode((int) HttpStatusCode.Accepted);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(GetAuthorsHttpResponse),(int)HttpStatusCode.OK)]
        public IActionResult Get([FromQuery] GetAuthorSearchHttpRequest request)
        {
            var serviceResponse = _authorService.GetAuthor(request.SearchText, SearchType.TEXT);

            
            var httpResponse = new GetAuthorsHttpResponse
                               {
                                   Authors = new List<AuthorModel> {serviceResponse.Author}
                               };
            
            return StatusCode((int) HttpStatusCode.OK, httpResponse);
        }
    }
}