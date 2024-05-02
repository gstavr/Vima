using System.Net;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Vimachem.Models.API;
using Vimachem.Models.Domain;
using Vimachem.Models.Dto.Category;
using Vimachem.Repository.IRepository;

namespace Vimachem.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CategoryController : ControllerBase
    {
        protected APIResponce _responce;
        private readonly ICategoryRepository _dbCategory;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository dbCategory, IMapper mapper)
        {
            _dbCategory = dbCategory;
            _mapper = mapper;
            _responce = new();


        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
        public async Task<ActionResult<APIResponce>> GetCategories()
        {

            IEnumerable<Category> categoryList = await _dbCategory.GetAllAsync();

            _responce.Result = _mapper.Map<List<CategoryDTO>>(categoryList);

            return Ok(_responce);

        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponce>> GetCategory(int id)
        {
            if (id.Equals(0))
            {
                _responce.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_responce);
            }


            var category = await _dbCategory.GetAsync(x => x.Id.Equals(id));

            if (category is null)
            {
                _responce.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_responce);
            }

            _responce.Result = _mapper.Map<CategoryDTO>(category);

            return Ok(_responce);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponce>> CreateCategory([FromBody] CategoryCreateDTO createCategoryCreateDto)
        {
            if (await _dbCategory.GetAsync(x => x.Name.ToLower() == createCategoryCreateDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Category already exists!");
                return BadRequest(ModelState);
            }

            var category = _mapper.Map<Category>(createCategoryCreateDto);

            category.CreatedDate = DateTime.Now;
            category.UpdatedDate = DateTime.Now;
            await _dbCategory.CreateAsync(category);

            _responce.Result = _mapper.Map<CategoryDTO>(category);
            _responce.StatusCode = HttpStatusCode.Created;

            return CreatedAtRoute("GetCategory", new { id = category.Id }, _responce);
        }

        [HttpDelete("{id:int}", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponce>> DeleteCategory(int id)
        {
            if (id.Equals(0))
                return BadRequest();

            var category = await _dbCategory.GetAsync(c => c.Id == id);

            if (category is null)
                return NotFound();

            await _dbCategory.DeleteAsync(category);


            _responce.StatusCode = HttpStatusCode.NoContent;

            return Ok(_responce);
        }

        [HttpPut("{id:int}", Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponce>> UpdateCategory(int id, [FromBody] CategoryUpdateDTO updateCategoryDto)
        {

            if (id != updateCategoryDto.Id)
            {
                _responce.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_responce);
            }

            var getCategory = await _dbCategory.GetAsync(c => c.Id == id, track: false);
            if (getCategory is null)
            {
                _responce.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_responce);
            }

            Category category = _mapper.Map<Category>(updateCategoryDto);
            category.UpdatedDate = DateTime.Now;
            category.CreatedDate = getCategory.CreatedDate;
            await _dbCategory.UpdateAsync(category);

            _responce.Result = category;

            return CreatedAtRoute("GetCategory", new { id = category.Id }, _responce);

        }

        [HttpPatch("{id:int}", Name = "UpdatePartialCategory")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponce>> UpdatePartialCategory(int id, JsonPatchDocument<CategoryUpdateDTO> patchCategoryDto)
        {

            if (id.Equals(0))
            {
                _responce.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_responce);
            }

            var cat = await _dbCategory.GetAsync(c => c.Id == id);

            if (cat is null)
            {
                _responce.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_responce);
            }

            CategoryUpdateDTO categoryDTO = _mapper.Map<CategoryUpdateDTO>(cat);
            cat.UpdatedDate = DateTime.Now;
            patchCategoryDto.ApplyTo(categoryDTO, ModelState);

            Category category = _mapper.Map<Category>(patchCategoryDto);

            await _dbCategory.UpdateAsync(category);

            if (!ModelState.IsValid)
            {
                _responce.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_responce);
            }

            _responce.Result = category;


            return CreatedAtRoute("GetCategory", new { id = category.Id }, _responce);

        }
    }
}
