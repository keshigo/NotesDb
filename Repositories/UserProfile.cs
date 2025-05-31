using AutoMapper;
using ConsoleProject.NET.Contract;
using ConsoleProject.NET.Models;

namespace ConsoleProject.NET.Repositories;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserAddDto, User>();
        CreateMap<User, UserVm>();
    }
}