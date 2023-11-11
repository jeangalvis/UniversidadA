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
public class CursoEscolarController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CursoEscolarController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    //[Authorize(Roles = "Administrator,Employee")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CursoEscolarDto>>> Get1()
    {
        var results = await _unitOfWork.CursoEscolares
                                    .GetAllAsync();
        return _mapper.Map<List<CursoEscolarDto>>(results);
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CursoEscolarDto>> Get2(int id)
    {
        var result = await _unitOfWork.CursoEscolares.GetByIdAsync(id);
        return _mapper.Map<CursoEscolarDto>(result);
    }

    [HttpPost]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CursoEscolar>> Post(CursoEscolarDto resultDto)
    {
        var result = _mapper.Map<CursoEscolar>(resultDto);
        this._unitOfWork.CursoEscolares.Add(result);
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
    public async Task<ActionResult<CursoEscolar>> Put(int id, [FromBody] CursoEscolarDto resultDto)
    {
        var result = _mapper.Map<CursoEscolar>(resultDto);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.CursoEscolares.Update(result);
        await _unitOfWork.SaveAsync();
        return result;
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _unitOfWork.CursoEscolares.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.CursoEscolares.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CursoEscolarDto>>> GetPag([FromQuery] Params resultParams)
    {
        var result = await _unitOfWork.CursoEscolares.GetAllAsync(resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
        var lstResultDto = _mapper.Map<List<CursoEscolarDto>>(result.registros);
        return new Pager<CursoEscolarDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }
}
