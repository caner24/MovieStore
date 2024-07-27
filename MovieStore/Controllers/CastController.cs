using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Data.Abstract;
using MovieStore.Entity.Dto;
using MovieStore.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MovieStore.ActionFilters;

namespace MovieStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CastController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CastController(IUnitOfWork unitWork, IMapper mapper)
        {
            _unitOfWork = unitWork;
            _mapper = mapper;
        }

        [HttpPost("createCast")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCast([FromBody] CreateCastDto createCastDto)
        {
            var cast = _mapper.Map<BaseUser>(createCastDto);
            var addedDirector = await _unitOfWork.CastDal.AddCast(cast, createCastDto.Password);
            if (!addedDirector.Succeeded)
            {
                foreach (var item in addedDirector.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return Ok(ModelState);
            }
            await _unitOfWork.CastDal.AddAsync(new Cast { BaseUserId = cast.Id });

            return StatusCode(201, cast.Id);
        }


        [HttpGet("getCastByEmail/{email}")]
        public async Task<IActionResult> GetCastyByEmail([FromRoute] string email)
        {
            var cast = await _unitOfWork.CastDal.Get(x =>x.BaseUser.Email==email).FirstOrDefaultAsync();
            if (cast is null)
                return NotFound();

            return Ok(cast);
        }

        [HttpGet("getAllCast")]
        public async Task<IActionResult> GetAllCast()
        {
            var cast = await _unitOfWork.CastDal.GetAll().ToListAsync();
            if (cast is null)
                return NotFound();

            return Ok(cast);
        }

        [HttpDelete("deleteCast/{Id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]

        public async Task<IActionResult> DeleteCast([FromRoute] DeleteCastDto deleteCastDto)
        {
            var cast = await _unitOfWork.CastDal.Get(x => x.BaseUser.Email == deleteCastDto.Id).FirstOrDefaultAsync();
            if (cast is null)
                return NotFound();
            await _unitOfWork.CastDal.DeleteAsync(cast);

            return Ok();
        }

        [HttpPut("addCastMovieByEmail/{email}")]
        public async Task<IActionResult> AddMovieByCastEmail([FromRoute] string email, [FromBody] int[] movieId)
        {
            var cast = await _unitOfWork.CastDal.Get(x => x.BaseUser.Email == email).FirstOrDefaultAsync();
            if (cast is null)
                return NotFound();

            foreach (var item in movieId)
            {
                var movie = await _unitOfWork.MovieDal.Get(x => x.Id == item).FirstOrDefaultAsync();
                if (movie is null)
                    return NotFound();

                cast.Movies.Add(movie);
            }
            await _unitOfWork.CastDal.UpdateAsync(cast);

            return Ok();
        }
    }
}
