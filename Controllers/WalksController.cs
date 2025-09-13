using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.CustomActionFilter;
using NZwalks.API.Data;
using NZwalks.API.Models;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repository.IRepository;

namespace NZwalks.API.Controllers
{
    // /api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalksRepository _walksRepository;
        private readonly ApplicationDbContext _db;

        public WalksController(IMapper mapper, IWalksRepository walksRepository, ApplicationDbContext db)
        {
            this._mapper = mapper;
            this._walksRepository = walksRepository;
            this._db = db;
        }

        // CREATE 
        // post: /api/walks
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {

            // map the addWalkRequestDto to 
            var domainWalk = _mapper.Map<Walk>(addWalkRequestDto);

            // crate walk
            await _walksRepository.AddAsync(domainWalk);
            await _db.SaveChangesAsync();

            // map the domian model to DTO 
            var walkDto = _mapper.Map<WalkDto>(domainWalk);

            return Ok(walkDto);

        }

        // GET BY ID
        // GET : /api/walks/GetById/{id}?
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer , Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomain = await _walksRepository.GetByIdAsync(u => u.Id == id);

            if (walkDomain == null) { return NotFound(); }

            // map the walkDomain  the walkDto
            var DtoWalk = _mapper.Map<WalkDto>(walkDomain);


            return Ok(DtoWalk);
        }

        // GET : /api/walks?Filteron=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSiz=10
        [HttpGet]
        [Authorize(Roles = "Writer , Reader")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            var walkDomian = await _walksRepository.GetAllByFilterAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);

            // map the domian to the dto the send to the clinte 
            var walks = _mapper.Map<List<WalkDto>>(walkDomian);
            return Ok(walks);

        }

        // DELETE 
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkWantToDelete = await _walksRepository.GetByIdAsync(u => u.Id == id);

            if (walkWantToDelete == null) { NotFound(); }

            await _walksRepository.RemoveByIdAsync(walkWantToDelete);
            _db.SaveChanges();
            return Ok();
        }

        // PUT: api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {


            // Get the Walk from the database
            var walkFromDb = await _walksRepository.GetByIdAsync(u => u.Id == id);
            if (walkFromDb == null)
                return NotFound();

            // Map DTO to the existing domain model
            _mapper.Map(updateWalkRequestDto, walkFromDb);

            // Update in DB
            await _walksRepository.UpdateAsync(walkFromDb);

            // Return updated Walk as DTO
            var walkDto = _mapper.Map<WalkDto>(walkFromDb);
            return Ok(walkDto);

        }





    }
}
