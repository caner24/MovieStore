using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStore.Data.Abstract;
using MovieStore.Entity;
using MovieStore.Entity.Dto;

namespace MovieStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KindController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public KindController(IUnitOfWork unitWork, IMapper mapper)
        {
            _unitOfWork = unitWork;
            _mapper = mapper;
        }

        [HttpGet("getAllKind")]
        public async Task<IActionResult> GetAllKind(CreateKindDto createKindDto)
        {
            var kinds = await _unitOfWork.KindDal.GetAll().ToListAsync();

            return Ok(kinds);
        }
        [HttpPost("addKind")]
        public async Task<IActionResult> AddKind(CreateKindDto createKindDto)
        {
            var mapped = _mapper.Map<Kind>(createKindDto);
            var addedKind = await _unitOfWork.KindDal.AddAsync(mapped);
            await _unitOfWork.Context.SaveChangesAsync();

            return Ok(addedKind);
        }
        [HttpPost("deletKind/{id}")]
        public async Task<IActionResult> DeletKind(int id)
        {
            var kind = await _unitOfWork.KindDal.Get(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            if (kind == null)
                return NotFound();
            await _unitOfWork.KindDal.DeleteAsync(kind);
            await _unitOfWork.Context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("updateKind")]
        public async Task<IActionResult> UpdateKind(UpdateKindDto updateKindDto)
        {
            var kind = await _unitOfWork.KindDal.Get(x => x.Id == updateKindDto.Id).FirstOrDefaultAsync();
            if (kind == null)
                return NotFound();

            var mapped = _mapper.Map(updateKindDto, kind);

            await _unitOfWork.KindDal.UpdateAsync(mapped);
            await _unitOfWork.Context.SaveChangesAsync();

            return Ok();
        }
    }
}
