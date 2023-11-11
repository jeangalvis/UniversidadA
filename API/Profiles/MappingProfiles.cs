using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AlumnoMatriculaAsignatura, AlumnoMatriculaAsignaturaDto>().ReverseMap();
            CreateMap<Asignatura, AsignaturaDto>().ReverseMap();
            CreateMap<CursoEscolar, CursoEscolarDto>().ReverseMap();
            CreateMap<Departamento, DepartamentoDto>().ReverseMap();
            CreateMap<Grado, GradoDto>().ReverseMap();
            CreateMap<Persona, PersonaDto>().ReverseMap();
            CreateMap<Profesor, ProfesorDto>().ReverseMap();
            CreateMap<Sexo, SexoDto>().ReverseMap();
            CreateMap<TipoAsignatura, TipoAsignaturaDto>().ReverseMap();
            CreateMap<TipoPersona, TipoPersonaDto>().ReverseMap();
        }
    }
}