using AutoMapper;
using Server_PHP_For_Business.Dtos;
using Server_PHP_For_Business.Models;

namespace Server_PHP_For_Business.Profiles
{
  public class CommandsProfile : Profile
  {
    public CommandsProfile()
    {
      //Source -> Target
      CreateMap<Command, CommandReadDto>();

      CreateMap<CommandCreateDto, Command>();
      CreateMap<CommandUpdateDto, Command>();
      CreateMap<Command, CommandUpdateDto>();
    }
  }
}
