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
public class TipoAsignaturaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TipoAsignaturaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    //[Authorize(Roles = "Administrator,Employee")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoAsignaturaDto>>> Get1()
    {
        var results = await _unitOfWork.TipoAsignaturas
                                    .GetAllAsync();
        return _mapper.Map<List<TipoAsignaturaDto>>(results);
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoAsignaturaDto>> Get2(int id)
    {
        var result = await _unitOfWork.TipoAsignaturas.GetByIdAsync(id);
        return _mapper.Map<TipoAsignaturaDto>(result);
    }

    [HttpPost]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoAsignatura>> Post(TipoAsignaturaDto resultDto)
    {
        var result = _mapper.Map<TipoAsignatura>(resultDto);
        this._unitOfWork.TipoAsignaturas.Add(result);
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
    public async Task<ActionResult<TipoAsignatura>> Put(int id, [FromBody] TipoAsignaturaDto resultDto)
    {
        var result = _mapper.Map<TipoAsignatura>(resultDto);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoAsignaturas.Update(result);
        await _unitOfWork.SaveAsync();
        return result;
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _unitOfWork.TipoAsignaturas.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoAsignaturas.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<TipoAsignaturaDto>>> GetPag([FromQuery] Params resultParams)
    {
        var result = await _unitOfWork.TipoAsignaturas.GetAllAsync(resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
        var lstResultDto = _mapper.Map<List<TipoAsignaturaDto>>(result.registros);
        return new Pager<TipoAsignaturaDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }
}
