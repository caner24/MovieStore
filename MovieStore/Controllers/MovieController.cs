using Microsoft.AspNetCore.Mvc;
using MovieStore.Entity.Dto;
using MovieStore.Entity;
using AutoMapper;
using MovieStore.Data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace MovieStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public MovieController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("createMovie")]
        public async Task<IActionResult> CreateMovie([FromBody] CreateMovieDto createMovieDto)
        {
            var movie = _mapper.Map<Movie>(createMovieDto);
            var addedMovie = await _unitOfWork.MovieDal.AddAsync(movie);

            return StatusCode(201, addedMovie.Id);
        }

        [HttpDelete("deleteMovie/{id}")]
        public async Task<IActionResult> DeleteMovie([FromRoute] int id)
        {
            var movie = await _unitOfWork.MovieDal.Get(x => x.Id == id).FirstOrDefaultAsync();
            if (movie != null)
                return NotFound();
            var movieCustomers = _unitOfWork.Context.MovieCustomer.Where(mc => mc.MovieId == id);
            _unitOfWork.Context.MovieCustomer.RemoveRange(movieCustomers);
            _unitOfWork.Context.Movie.Remove(movie);
            await _unitOfWork.Context.SaveChangesAsync();
            return Ok();
        }

    }
}
