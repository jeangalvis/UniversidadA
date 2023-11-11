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
public class TipoPersonaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TipoPersonaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    //[Authorize(Roles = "Administrator,Employee")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoPersonaDto>>> Get1()
    {
        var results = await _unitOfWork.TipoPersonas
                                    .GetAllAsync();
        return _mapper.Map<List<TipoPersonaDto>>(results);
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoPersonaDto>> Get2(int id)
    {
        var result = await _unitOfWork.TipoPersonas.GetByIdAsync(id);
        return _mapper.Map<TipoPersonaDto>(result);
    }

    [HttpPost]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoPersona>> Post(TipoPersonaDto resultDto)
    {
        var result = _mapper.Map<TipoPersona>(resultDto);
        this._unitOfWork.TipoPersonas.Add(result);
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
    public async Task<ActionResult<TipoPersona>> Put(int id, [FromBody] TipoPersonaDto resultDto)
    {
        var result = _mapper.Map<TipoPersona>(resultDto);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoPersonas.Update(result);
        await _unitOfWork.SaveAsync();
        return result;
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _unitOfWork.TipoPersonas.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoPersonas.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TipoPersonaDto>>> GetPag([FromQuery] Params resultParams)
    {
        var result = await _unitOfWork.TipoPersonas.GetAllAsync(resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
        var lstResultDto = _mapper.Map<List<TipoPersonaDto>>(result.registros);
        return new Pager<TipoPersonaDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }
}
