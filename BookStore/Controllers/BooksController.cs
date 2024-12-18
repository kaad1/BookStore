using Azure.Core;
using BookStore.Models.Domain;
using BookStore.Models.DTO;
using BookStore.Repositories.Implementation;
using BookStore.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookPostRepository bookPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BooksController(IBookPostRepository bookPostRepository, ICategoryRepository categoryRepository  )
        {
            this.bookPostRepository = bookPostRepository;
            this.categoryRepository = categoryRepository;
        }

        public CategoryRepository CategoryRepository { get; }
        public ICategoryRepository CategoryRepository1 { get; }

        //Post:{apibaseurl}/api/book
        [HttpPost]

        public async Task <IActionResult> CreateBook([FromBody]CreateBookPostRequestDto request)
        {
            //Convert from dto to domain

            var bookpost = new Books
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Author = request.Author,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                PublishedDate = request.PublishedDate,
                Categories=new List<Category>()


            };

            foreach(var categoryGuid in request.Categories)
            {
                var existingCategory= await categoryRepository.GetById(categoryGuid);
                if(existingCategory is not null)
                {
                    bookpost.Categories.Add(existingCategory);
                }
            }
           var bookPost= await bookPostRepository.CreateAsync(bookpost);

            var response = new BookPostDto
            {
                Id = bookpost.Id,
                Title = bookpost.Title,
                ShortDescription = bookpost.ShortDescription,
                Author = bookpost.Author,
                FeaturedImageUrl = bookpost.FeaturedImageUrl,
                UrlHandle = bookpost.UrlHandle,
                PublishedDate = bookpost.PublishedDate,
                Categories = bookpost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()

            };
            return Ok(response);
        }

        //Get :/api/bookposts

        [HttpGet]
        public async Task <IActionResult> GetAllBookPosts()
        {
          var bookPosts=  await bookPostRepository.GetAllAsync();

            //Convert domain model on Dto
            var response= new List<BookPostDto>();

            foreach(var bookpost in bookPosts)
            {
                response.Add(new BookPostDto
                {
                    Id=bookpost.Id,
                    Title = bookpost.Title,
                    ShortDescription = bookpost.ShortDescription,
                    Author = bookpost.Author,
                    FeaturedImageUrl = bookpost.FeaturedImageUrl,
                    UrlHandle = bookpost.UrlHandle,
                    PublishedDate = bookpost.PublishedDate,
                    Categories = bookpost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()


                });
            }

                return Ok(response);
        }

        // GET: {apiBaseUrl}/api/blogposts/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            // Get the BlogPost from Repo
            var bookPost = await bookPostRepository.GetByIdAsync(id);

            if (bookPost is null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var response = new BookPostDto
            {
                Id = bookPost.Id,
                Title = bookPost.Title,
                ShortDescription = bookPost.ShortDescription,
                Author = bookPost.Author,
                FeaturedImageUrl = bookPost.FeaturedImageUrl,
                UrlHandle = bookPost.UrlHandle,
                PublishedDate = bookPost.PublishedDate,
                Categories = bookPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{urlHandle}")]
      
        
        
        public async Task <IActionResult> GetBookByUrlHandle([FromRoute] string urlHandle)
        {
            //Get bookpost details from repository

           var bookpost= await bookPostRepository.GetByUrlHandleAsync(urlHandle);
             if(bookpost is null)
            {
                return NotFound();
            }
            //Convert to Dto

            var response = new BookPostDto
            {

                Id = bookpost.Id,
                Title = bookpost.Title,
                ShortDescription = bookpost.ShortDescription,
                Author = bookpost.Author,
                FeaturedImageUrl = bookpost.FeaturedImageUrl,
                UrlHandle = bookpost.UrlHandle,
                PublishedDate = bookpost.PublishedDate,
                Categories = bookpost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()

            };
            return Ok(response);
        }

        // Httep Put :/api/bookposts/{id}

        [HttpPut]
        [Route("{id:Guid}")]
   
        public async Task <IActionResult> UpdateBookPostById([FromRoute] Guid id, UpdateBookPostRequestDto request)
        {
            //Convert from Dto To Domain model

            var bookpost = new Books
            {
                Id = id,
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Author = request.Author,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                PublishedDate = request.PublishedDate,
                Categories = new List<Category>()

            };

            foreach (var categoryGuid in request.Categories) 
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);

                if (existingCategory != null)
                {
                    bookpost.Categories.Add(existingCategory);
                }
            }

           var updatedBookPost= await bookPostRepository.UpdateAsync(bookpost);

            if(updatedBookPost == null)
            {
                return NotFound();
            }
            var response = new BookPostDto
            {
                Id = bookpost.Id,
                Title = bookpost.Title,
                ShortDescription = bookpost.ShortDescription,
                Author = bookpost.Author,
                FeaturedImageUrl = bookpost.FeaturedImageUrl,
                UrlHandle = bookpost.UrlHandle,
                PublishedDate = bookpost.PublishedDate,
                Categories = bookpost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()


            };
            return Ok(response);
         
        }

        [HttpDelete]

        [Route("{id:Guid}")]
     
        public async Task<IActionResult> DeleteBookPost([FromRoute] Guid id)
        {
           var deletedBookPost= await bookPostRepository.DeleteAsync(id);
            if(deletedBookPost == null)
            {
                return NotFound();  
            }

            var response = new BookPostDto
            {
                Id = deletedBookPost.Id,
                Title = deletedBookPost.Title,
                ShortDescription = deletedBookPost.ShortDescription,
                Author = deletedBookPost.Author,
                FeaturedImageUrl = deletedBookPost.FeaturedImageUrl,
                UrlHandle = deletedBookPost.UrlHandle,
                PublishedDate = deletedBookPost.PublishedDate,
            };
            return Ok(response);
        }
    }

}
