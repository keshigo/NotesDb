using AutoMapper;
using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Repositories;

public class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<NoteAddDto, Note>()
        .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(_ => false))
        .ForMember(dest => dest.NoteCreationTime, opt => opt.MapFrom(_ => DateTime.Now));
        
        CreateMap<NoteUpdateDto, Note>()
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Note, NoteVM>();
    }
}