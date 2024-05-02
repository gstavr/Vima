using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vimachem.Models.API;
using Vimachem.Models.Domain;
using Vimachem.Models.Dto.Book;
using Vimachem.Repository.IRepository;

namespace Vimachem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        protected APIResponce _responce;
        private readonly IBookRepository _dbBook;
        private readonly ICategoryRepository _dbCategory;
        private readonly IMapper _mapper;
        public BookController(IBookRepository dbBook, ICategoryRepository dbCategory, IMapper mapper)
        {
            _dbBook = dbBook;
            _dbCategory = dbCategory;
            _mapper = mapper;
            _responce = new();


        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDTO))] 
        public async Task<ActionResult<APIResponce>> GetBooks()
        {

            IEnumerable<Book> list = await _dbBook.GetAllAsync();

            _responce.Result = _mapper.Map<List<BookDTO>>(list);

            return Ok(_responce);
        }

        [HttpGet("{id:int}", Name = "GetBook")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookDTO))] 
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponce>> GetBook(int id)
        {
            if (id.Equals(0))
            {
                _responce.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_responce);
            }
                

            var book = await _dbBook.GetAsync(x => x.Id.Equals(id));

            if (book is null)
            {
                _responce.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_responce);
            }
                

            _responce.Result = _mapper.Map<BookDTO>(book);
            
            return Ok(_responce);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponce>> CreateBook([FromBody] BookCreateDTO createBookCreateDto)
        {
            if (await _dbBook.GetAsync(x => x.Name.ToLower() == createBookCreateDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Book already exists!");
                return BadRequest(ModelState);
            }

            if (await _dbCategory.GetAsync(x => x.Id.Equals(createBookCreateDto.CategoryId)) is null)
            {
                ModelState.AddModelError("CustomError", "Category is invalid!");
                return BadRequest(ModelState);
            }

            var book = _mapper.Map<Book>(createBookCreateDto);

            book.CreatedDate = DateTime.Now;
            await _dbBook.CreateAsync(book);

            _responce.Result = _mapper.Map<BookDTO>(book);
            _responce.StatusCode = HttpStatusCode.Created;

            return CreatedAtRoute("GetBook", new { id = book.Id }, _responce);
        }

        [HttpDelete("{id:int}", Name = "DeleteBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponce>> DeleteBook(int id)
        {
            if (id.Equals(0))
                return BadRequest();

            var book = await _dbBook.GetAsync(c => c.Id == id);

            if (book == null)
                return NotFound();

            await _dbBook.DeleteAsync(book);

            
            _responce.StatusCode = HttpStatusCode.NoContent;

            return Ok(_responce);
        }

        [HttpPut("{id:int}", Name = "UpdateBook")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponce>> UpdateBook(int id, [FromBody] BookUpdateDTO updateBookDto)
        {

            if (id != updateBookDto.Id)
            {
                _responce.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_responce);
            }

            if (await _dbCategory.GetAsync(x => x.Id.Equals(updateBookDto.CategoryId)) is null)
            {
                ModelState.AddModelError("CustomError", "Category is invalid!");
                return BadRequest(ModelState);
            }


            var bookStore = await _dbBook.GetAsync(c => c.Id == id, track:false);
            if (bookStore is null)
            {
                _responce.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_responce);
            }

            Book book = _mapper.Map<Book>(updateBookDto);
            book.UpdatedDate = DateTime.Now;
            book.CreatedDate = bookStore.CreatedDate;

            await _dbBook.UpdateAsync(book);


            _responce.Result = book;
            

            return CreatedAtRoute("GetBook", new { id = book.Id }, _responce);
            
        }

        
    }
}
