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
public class GradoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GradoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    //[Authorize(Roles = "Administrator,Employee")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GradoDto>>> Get1()
    {
        var results = await _unitOfWork.Grados
                                    .GetAllAsync();
        return _mapper.Map<List<GradoDto>>(results);
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GradoDto>> Get2(int id)
    {
        var result = await _unitOfWork.Grados.GetByIdAsync(id);
        return _mapper.Map<GradoDto>(result);
    }

    [HttpPost]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Grado>> Post(GradoDto resultDto)
    {
        var result = _mapper.Map<Grado>(resultDto);
        this._unitOfWork.Grados.Add(result);
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
    public async Task<ActionResult<Grado>> Put(int id, [FromBody] GradoDto resultDto)
    {
        var result = _mapper.Map<Grado>(resultDto);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Grados.Update(result);
        await _unitOfWork.SaveAsync();
        return result;
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Grados.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Grados.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<GradoDto>>> GetPag([FromQuery] Params resultParams)
    {
        var result = await _unitOfWork.Grados.GetAllAsync(resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
        var lstResultDto = _mapper.Map<List<GradoDto>>(result.registros);
        return new Pager<GradoDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }

    [HttpGet("GetGradosConAsignaturas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GradosConAsignaturasDto>>> Get3()
    {
        var results = await _unitOfWork.Grados
                                    .GetGradosConAsignaturas();
        return _mapper.Map<List<GradosConAsignaturasDto>>(results);
    }
    [HttpGet("GetGradosConAsignaturasMas40")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GradosConAsignaturasDto>>> Get4()
    {
        var results = await _unitOfWork.Grados
                                    .GetGradosConAsignaturasMas40();
        return _mapper.Map<List<GradosConAsignaturasDto>>(results);
    }
    [HttpGet("GetGradoSumaCreditos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GradosConAsignaturasDto>>> Get5()
    {
        var results = await _unitOfWork.Grados
                                    .GetGradoSumaCreditos();
        return _mapper.Map<List<GradosConAsignaturasDto>>(results);
    }
}
