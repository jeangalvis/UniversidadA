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
public class DepartamentoController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DepartamentoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    //[Authorize(Roles = "Administrator,Employee")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DepartamentoDto>>> Get1()
    {
        var results = await _unitOfWork.Departamentos
                                    .GetAllAsync();
        return _mapper.Map<List<DepartamentoDto>>(results);
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DepartamentoDto>> Get2(int id)
    {
        var result = await _unitOfWork.Departamentos.GetByIdAsync(id);
        return _mapper.Map<DepartamentoDto>(result);
    }

    [HttpPost]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Departamento>> Post(DepartamentoDto resultDto)
    {
        var result = _mapper.Map<Departamento>(resultDto);
        this._unitOfWork.Departamentos.Add(result);
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
    public async Task<ActionResult<Departamento>> Put(int id, [FromBody] DepartamentoDto resultDto)
    {
        var result = _mapper.Map<Departamento>(resultDto);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Departamentos.Update(result);
        await _unitOfWork.SaveAsync();
        return result;
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Departamentos.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Departamentos.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<DepartamentoDto>>> GetPag([FromQuery] Params resultParams)
    {
        var result = await _unitOfWork.Departamentos.GetAllAsync(resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
        var lstResultDto = _mapper.Map<List<DepartamentoDto>>(result.registros);
        return new Pager<DepartamentoDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }

    [HttpGet("GetDepartamentoProfesoresInformatica")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DepartamentoDto>>> Get3()
    {
        var results = await _unitOfWork.Departamentos
                                    .GetDepartamentoProfesoresInformatica();
        return _mapper.Map<List<DepartamentoDto>>(results);
    }
}
