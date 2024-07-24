using Microsoft.AspNetCore.Mvc;
using MovieStore.Entity.Dto;
using MovieStore.Entity;
using AutoMapper;
using MovieStore.Data.Abstract;

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

    }
}
