using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStore.ActionFilters;
using MovieStore.Data.Abstract;
using MovieStore.Entity;
using MovieStore.Entity.Dto;

namespace MovieStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DirectorController(IUnitOfWork unitWork, IMapper mapper)
        {
            _unitOfWork = unitWork;
            _mapper = mapper;
        }

        [HttpGet("getAllDirector")]
        public async Task<IActionResult> GetAllDirector()
        {
            var director = await _unitOfWork.DirectorDal.GetAll().ToListAsync();
            if (director is null)
                return NotFound();

            return Ok(director);
        }

        [HttpDelete("deleteDirector/{Id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]

        public async Task<IActionResult> DeleteCast([FromRoute] DeleteDirectorDto deleteDirectorDto)
        {
            var director = await _unitOfWork.DirectorDal.Get(x => x.BaseUser.Email == deleteDirectorDto.Id).FirstOrDefaultAsync();
            if (director is null)
                return NotFound();
            await _unitOfWork.DirectorDal.DeleteAsync(director);

            return Ok();
        }

        [HttpPut("updateDirector/{Email}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]

        public async Task<IActionResult> UpdateDirector([FromRoute] UpdateDirectorDto updateDirectorDto)
        {
            var director = await _unitOfWork.DirectorDal.Get(x => x.BaseUser.Email == updateDirectorDto.Email).FirstOrDefaultAsync();
            if (director is null)
                return NotFound();
            _mapper.Map<Director>(updateDirectorDto);
            await _unitOfWork.DirectorDal.UpdateAsync(director);

            return Ok();
        }

        [HttpPost("createDirector")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]

        public async Task<IActionResult> CreateDirector([FromBody] CreateDirectorDto createDirectorDto)
        {
            var director = _mapper.Map<BaseUser>(createDirectorDto);
            var addedDirector = await _unitOfWork.DirectorDal.AddDirector(director,createDirectorDto.Password);
            if (!addedDirector.Succeeded)
            {
                foreach (var item in addedDirector.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return Ok(ModelState);
            }
            await _unitOfWork.DirectorDal.AddAsync(new Director { BaseUserId = director.Id });

            return StatusCode(201, director.Id);
        }


        [HttpGet("getDirectorByEmail/{email}")]
        public async Task<IActionResult> GetDirectoryByEmail([FromRoute] string email)
        {
            var director = await _unitOfWork.CastDal.Get(x => x.BaseUser.Email == email).FirstOrDefaultAsync();
            if (director is null)
                return NotFound();

            return Ok(director);
        }

        [HttpPut("addDirectorMovieByEmail/{email}")]
        public async Task<IActionResult> AddMovieByDirectorEmail([FromRoute] string email, [FromBody] int[] movieId)
        {
            var director = await _unitOfWork.DirectorDal.Get(x => x.BaseUser.Email == email).FirstOrDefaultAsync();
            if (director is null)
                return NotFound();

            foreach (var item in movieId)
            {
                var movie = await _unitOfWork.MovieDal.Get(x => x.Id == item).FirstOrDefaultAsync();
                if (movie is null)
                    return NotFound();

                director.Movies.Add(movie);
            }
            await _unitOfWork.DirectorDal.UpdateAsync(director);

            return Ok();
        }


    }
}
