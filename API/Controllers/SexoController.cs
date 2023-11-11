using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class SexoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SexoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    //[Authorize(Roles = "Administrator,Employee")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SexoDto>>> Get1()
    {
        var results = await _unitOfWork.Sexos
                                    .GetAllAsync();
        return _mapper.Map<List<SexoDto>>(results);
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SexoDto>> Get2(int id)
    {
        var result = await _unitOfWork.Sexos.GetByIdAsync(id);
        return _mapper.Map<SexoDto>(result);
    }

    [HttpPost]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Sexo>> Post(SexoDto resultDto)
    {
        var result = _mapper.Map<Sexo>(resultDto);
        this._unitOfWork.Sexos.Add(result);
        await _unitOfWork.SaveAsync();
        if (result == null)
        {
            return BadRequest();
        }
        resultDto.Id = result.Id;
        return CreatedAtAction(nameof(Post), new { id = resultDto.Id }, resultDto);
    }

    [HttpPut("{id}")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Sexo>> Put(int id, [FromBody] SexoDto resultDto)
    {
        var result = _mapper.Map<Sexo>(resultDto);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Sexos.Update(result);
        await _unitOfWork.SaveAsync();
        return result;
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Sexos.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Sexos.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<SexoDto>>> GetPag([FromQuery] Params resultParams)
    {
        var result = await _unitOfWork.Sexos.GetAllAsync(resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
        var lstResultDto = _mapper.Map<List<SexoDto>>(result.registros);
        return new Pager<SexoDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }
}
