using AutoMapper;
using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Profiles;

public class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<NoteAddDto, Note>()
            .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.NoteCreationTime, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<NoteUpdateDto, Note>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Note, NoteVM>();
    }
}