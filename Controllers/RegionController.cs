using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.CustomActionFilter;
using NZwalks.API.Data;
using NZwalks.API.Models;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repository.IRepository;
using System.Text.Json;

namespace NZwalks.API.Controllers;

// https::localhost1234/api/region
[Route("api/[controller]")]
[ApiController]
public class RegionController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<RegionController> _logger;

    public RegionController(ApplicationDbContext db, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionController> logger)
    {
        this._db = db;
        this._regionRepository = regionRepository;
        this._mapper = mapper;
        this._logger = logger;
    }

    [HttpGet]
    //[Authorize(Roles = "Reader , Writer")]
    public async Task<IActionResult> GetAll()
    {
        // Get data from the domain model 
        var RegionsDoiman = await _regionRepository.GetAllAsync();
        _logger.LogInformation("Calling the Get All Method");
        // mapping or convert the domain to DTO
        var regionDto = _mapper.Map<List<RegionDto>>(RegionsDoiman);

        _logger.LogInformation($"Finish Calling the Get all method with the the data : {JsonSerializer.Serialize(regionDto)}");
        return Ok(regionDto);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Reader , Writer")]
    public async Task<IActionResult> GetById(Guid id)
    {
        // Get region domian from the database 
        var regionDomian = await _regionRepository.GetByIdAsync(u => u.Id == id);

        if (regionDomian == null)
        {
            return NotFound();
        }

        // Map or convort the Domain model to the DTO 
        var regionDto = _mapper.Map<RegionDto>(regionDomian);

        return Ok(regionDto);
    }

    // post action method to create new region 
    //POST : https://localhost:portnumber/api/regions
    [HttpPost]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
    {

        // map or convert the DTO to the domina model 
        var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);

        await _regionRepository.AddAsync(regionDomainModel);
        await _db.SaveChangesAsync();

        // map from the domain to the dto send it agine to the user 
        var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

        return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

    }

    [HttpPut]
    [Route("{id:Guid}")]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Update([FromRoute] Guid id, UpdateRegionRequestDto updateRegionRequestDto)
    {


        // Get entity using repository
        var RegionDomain = await _regionRepository.GetByIdAsync(u => u.Id == id);

        if (RegionDomain == null)
            return NotFound();

        // map DTO to the domain model
        var regionDomian = _mapper.Map(updateRegionRequestDto, RegionDomain);

        // Call repository to update and save
        var updatedRegion = await _regionRepository.UpdateAsync(regionDomian);

        // map domain model to DTO to resend
        var regionDto = _mapper.Map<RegionDto>(updatedRegion);

        return Ok(regionDto);


    }



    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var RegionDomain = await _regionRepository.GetByIdAsync(u => u.Id == id);
        if (RegionDomain == null)
        {
            return NotFound();
        }
        _regionRepository.RemoveByIdAsync(RegionDomain);
        await _db.SaveChangesAsync();

        var regionDto = _mapper.Map<RegionDto>(RegionDomain);
        return Ok(regionDto);
    }

}