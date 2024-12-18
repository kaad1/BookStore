using BookStore.Data;
using BookStore.Models.Domain;
using BookStore.Models.DTO;
using BookStore.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class CitatsController : ControllerBase
    {
        private readonly ICitatRepository citatRepository;

        public CitatsController( ICitatRepository citatRepository)
        {
            this.citatRepository = citatRepository;
        }

  

        [HttpPost]



       public async Task <IActionResult> CreateAsync(CreateCitatRequestDto request)
       {
            var citat = new Citats
            {
                Citat = request.Citat,
                Author = request.Author,
                Book = request.Book,

            };

           await citatRepository.CreateAsync(citat);

            var response = new CitatsDto
            {
                Id = citat.Id,
                Citat=citat.Citat,
                Author=citat.Author,
                Book=citat.Book,
            };
            return Ok(response);

       }

        [HttpGet]
        public async Task <IActionResult> GetAllCitats()
        {
           var citat= await citatRepository.GetAllAsync();


            var response = new List<CitatPostDto>();
            foreach(var post in citat)
            {
                response.Add(new CitatPostDto
                {
                    Id = post.Id,
                    Author = post.Author,
                    Book = post.Book,
                    Citat=post.Citat,

                });
            }
            return Ok(response);
        }
    }
    



}
