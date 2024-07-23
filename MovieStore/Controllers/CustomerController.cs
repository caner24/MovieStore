using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Data.Abstract;
using MovieStore.Entity.Dto;
using MovieStore.Entity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace MovieStore.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerController(IUnitOfWork unitWork, IMapper mapper)
        {
            _unitOfWork = unitWork;
            _mapper = mapper;
        }

        [HttpPost("createCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto createCustomerDto)
        {
            var customer = _mapper.Map<Customer>(createCustomerDto);
            var addedDirector = await _unitOfWork.CustomerDal.AddCustomer(customer);
            if (!addedDirector.Succeeded)
            {
                foreach (var item in addedDirector.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return Ok(ModelState);
            }
            return StatusCode(201, customer.Id);
        }


        [HttpGet("getCustomerByEmail/{email}")]
        public async Task<IActionResult> GetCustomeryByEmail([FromRoute] string email)
        {
            var cast = await _unitOfWork.CustomerDal.GetCustomerByEmail(email);
            if (cast is null)
                return NotFound();

            return Ok(cast);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginCustomer([FromBody] CreateCustomerDto createCustomerDto)
        {
            var cast = await _unitOfWork.CustomerDal.SignIn(createCustomerDto.Email, createCustomerDto.Password);
            if (!cast.Succeeded)
            {
                if (cast.IsLockedOut)
                    return BadRequest("You've locked out please wait a few minute");
                if (cast.IsNotAllowed)
                    return BadRequest("Password or email you've entered is wrong");
            }

            return Ok(cast);
        }
        [Authorize(Roles = "Customer")]
        [HttpPost("buyMovieWithMovieId")]
        public async Task<IActionResult> BuyMovieWithMovieId([FromBody] int[] movieId, ClaimsPrincipal claims)
        {
            var activeCustomer = claims.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;
            var customer = await _unitOfWork.CustomerDal.GetCustomerByEmail(activeCustomer);

            foreach (var item in movieId)
            {
                var movies = await _unitOfWork.MovieDal.Get(x => x.Id == item).FirstOrDefaultAsync();
                if (movies == null)
                    return BadRequest("Searched movie was not found.");
                customer.Movies.Add(movies);
            }

            return Ok();
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("addFavoriteKind")]
        public async Task<IActionResult> AddFavoriteKind(ClaimsPrincipal claims, int[] kindId)
        {
            var activeCustomer = claims.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;
            var customer = await _unitOfWork.CustomerDal.GetCustomerByEmail(activeCustomer);

            foreach (var item in kindId)
            {
                var kind = await _unitOfWork.KindDal.Get(x => x.Id == item).FirstOrDefaultAsync();
                if (kind == null)
                    return BadRequest("Searched movie was not found.");
                customer.FavoriteKind.Add(kind);
            }

            return Ok();
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("getBoughtMovies")]
        public async Task<IActionResult> GetBoughtMovies(ClaimsPrincipal claims)
        {
            var activeCustomer = claims.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;

            var boughtMovies = await _unitOfWork.Context.Movie.TemporalAll()
                .Where(movie => movie.Customers.Any(c => c.Email == activeCustomer))
                .ToListAsync();

            return Ok(boughtMovies);
        }
    }
}
