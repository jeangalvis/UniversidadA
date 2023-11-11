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
public class PersonaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PersonaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    //[Authorize(Roles = "Administrator,Employee")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> Get1()
    {
        var results = await _unitOfWork.Personas
                                    .GetAllAsync();
        return _mapper.Map<List<PersonaDto>>(results);
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PersonaDto>> Get2(int id)
    {
        var result = await _unitOfWork.Personas.GetByIdAsync(id);
        return _mapper.Map<PersonaDto>(result);
    }

    [HttpPost]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Persona>> Post(PersonaDto resultDto)
    {
        var result = _mapper.Map<Persona>(resultDto);
        this._unitOfWork.Personas.Add(result);
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
    public async Task<ActionResult<Persona>> Put(int id, [FromBody] PersonaDto resultDto)
    {
        var result = _mapper.Map<Persona>(resultDto);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Personas.Update(result);
        await _unitOfWork.SaveAsync();
        return result;
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Personas.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Personas.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet]
    [MapToApiVersion("1.1")]
    //[Authorize(Roles = "Administrator,Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PersonaDto>>> GetPag([FromQuery] Params resultParams)
    {
        var result = await _unitOfWork.Personas.GetAllAsync(resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
        var lstResultDto = _mapper.Map<List<PersonaDto>>(result.registros);
        return new Pager<PersonaDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }

    [HttpGet("GetAlumnosxNombre")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<AlumnosxNombreDto>>> Get3()
    {
        var results = await _unitOfWork.Personas
                                    .GetAlumnosxNombre();
        return _mapper.Map<List<AlumnosxNombreDto>>(results);
    }

    [HttpGet("GetAlumnosSinTelefono")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<AlumnosxNombreDto>>> Get4()
    {
        var results = await _unitOfWork.Personas
                                    .GetAlumnosSinTelefono();
        return _mapper.Map<List<AlumnosxNombreDto>>(results);
    }

    [HttpGet("GetAlumnosNacieron1999")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> Get5()
    {
        var results = await _unitOfWork.Personas
                                    .GetAlumnosNacieron1999();
        return _mapper.Map<List<PersonaDto>>(results);
    }

    [HttpGet("GetProfesoresSinTelefono")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> Get6()
    {
        var results = await _unitOfWork.Personas
                                    .GetProfesoresSinTelefono();
        return _mapper.Map<List<PersonaDto>>(results);
    }
    [HttpGet("GetAlumnasEnSistemas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> Get7()
    {
        var results = await _unitOfWork.Personas
                                    .GetAlumnasEnSistemas();
        return _mapper.Map<List<PersonaDto>>(results);
    }
    [HttpGet("GetProfesoresConDepartamento")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> Get8()
    {
        var results = await _unitOfWork.Personas
                                    .GetProfesoresConDepartamento();
        return _mapper.Map<List<PersonaDto>>(results);
    }
}
