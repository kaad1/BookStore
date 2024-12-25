using BookStore.Models.Domain;
using BookStore.Models.DTO;
using BookStore.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
    
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
       
            this.categoryRepository = categoryRepository;
        }
        [HttpPost]

        public async Task<IActionResult> CreateCategoty([FromBody]CreateCategoryRequestDto requestDto)
        {
            //Map Dto to Domain Model

            var category = new Category
            {
                Name = requestDto.Name,
                UrlHandle = requestDto.UrlHandle,
            };
        
            await categoryRepository.CreateAsync(category);

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = requestDto.Name,
                UrlHandle = requestDto.UrlHandle
            };
            return Ok(response);
        }


        //Get: http://localhost:5210/api/Categories
        [HttpGet]
       
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();

            //Map domain model to Dto
            var response = new List<CategoryDto>();
            foreach (var category in categories)
            {
                response.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle,
                });
            }
            return Ok(response);
        }
       
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task <IActionResult> GetCategoryId([FromRoute]Guid id)
        {
          var existingCategory=  await categoryRepository.GetById(id);
            if(existingCategory is null)
            {
                return NotFound();
            }
            var response = new CategoryDto
            {
                Id = existingCategory.Id,
                Name = existingCategory.Name,
                UrlHandle = existingCategory.UrlHandle,
            };
            return Ok(response);
        }

        [HttpPut]
        [Route("{id:Guid}")]       
        public async Task <IActionResult> EditCategory([FromRoute] Guid id, UpdateCategoryRequestDto request )
        {
            //Convert DTO to Domain model

            var category = new Category
            {
                Id = id,
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };
           category= await categoryRepository.UpdateAsnync(category);
            if(category== null)
            {
                return NotFound();
            }
            //Convert Domain model to DTO

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await categoryRepository.DeleteAsync(id);
            if (category is null)
            {
                return NotFound();
            }

            var response = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                UrlHandle = category.UrlHandle,
            };
            return Ok();
        }
    }
}
