using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Server_PHP_For_Business.Data;
using Server_PHP_For_Business.Dtos;
using Server_PHP_For_Business.Models;
using Server_PHP_For_Business.OptimisationMethods;

namespace Server_PHP_For_Business.Controllers
{
  // api/commands
  [Route("api/[action]")]
  [ApiController]
  public class CommandsController : ControllerBase
  {
    private readonly ICommanderRepo _repository;
    private readonly IMapper _mapper;

    public CommandsController(ICommanderRepo repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    [HttpGet]
    [ActionName("users")]
    public ActionResult<IEnumerable<CommandReadDto>> GetAllUsers()
    {
      var users = _repository.GetAllUsers();
      return Ok(users);
    }

    [HttpGet("{id}", Name = "GetUserById")]
    [ActionName("users")]
    public ActionResult<CommandReadDto> GetUserById(int id)
    {
      var user = _repository.GetUserById(id);
      if (user == null)
        return NotFound();

      return Ok(user);
    }

    [HttpPost]
    [ActionName("users")]
    public ActionResult CreateUser(User userCreateDto)
    {
      _repository.CreateUser(userCreateDto);
      _repository.SaveChanges();

      return CreatedAtRoute(nameof(GetUserById), new {Id = userCreateDto.Id}, userCreateDto);
    }

    [HttpPost("{id}")]
    [ActionName("users")]
    public ActionResult UpdateUser(int id, User userUpdateDto)
    {
      var userModel = _repository.GetUserById(id);
      if (userModel == null)
        return NotFound();

      Models.User.CopyValues(userUpdateDto, userModel);

      _repository.UpdateUser(userModel);
      _repository.SaveChanges();

      return NoContent();
    }

    [HttpDelete("{id}")]
    [ActionName("users")]
    public ActionResult DeleteUser(int id)
    {
      var userRepo = _repository.GetUserById(id);
      if (userRepo == null)
        return NotFound();

      _repository.DeleteUser(userRepo);
      _repository.SaveChanges();
      return Ok();
    }

    [HttpGet]
    [ActionName("bracelets")]
    public ActionResult<IList<CommandReadDto>> GetAllBracelets()
    {
      var bracelets = _repository.GetAllBracelets();
      return Ok(bracelets);
    }

    [HttpGet("{id}", Name = "GetBraceletById")]
    [ActionName("bracelets")]
    public ActionResult<CommandReadDto> GetBraceletById(int id)
    {
      var user = _repository.GetBraceletById(id);
      if (user == null)
        return NotFound();

      return Ok(user);
    }

    [HttpPost]
    [ActionName("bracelets")]
    public ActionResult<CommandReadDto> CreateBracelets(Bracelet bracelets)
    {
      _repository.CreateBracelet(bracelets);
      _repository.SaveChanges();

      return CreatedAtRoute(nameof(GetBraceletById), new {Id = bracelets.Id}, bracelets);
    }

    [HttpPost("{id}")]
    [ActionName("bracelets")]
    public ActionResult UpdateBracelet(int id, Bracelet braceletUpdateDto)
    {
      var braceletModel = _repository.GetBraceletById(id);
      if (braceletModel == null)
        return NotFound();

      Bracelet.CopyValues(braceletUpdateDto, braceletModel);

      _repository.UpdateBracelet(braceletModel);
      _repository.SaveChanges();

      return NoContent();
    }

    [HttpDelete("{id}")]
    [ActionName("bracelets")]
    public ActionResult DeleteBracelet(int id)
    {
      var braceletRepo = _repository.GetBraceletById(id);
      if (braceletRepo == null)
        return NotFound();

      _repository.DeleteBracelet(braceletRepo);
      _repository.SaveChanges();
      return NoContent();
    }

    [HttpGet]
    [ActionName("halls")]
    public ActionResult<IList<CommandReadDto>> GetAllHalls()
    {
      var halls = _repository.GetAllHalls();
      return Ok(halls);
    }

    [HttpGet("{id}", Name = "GetHallById")]
    [ActionName("halls")]
    public ActionResult<CommandReadDto> GetHallById(int id)
    {
      var hall = _repository.GetHallById(id);
      if (hall == null)
        return NotFound();

      return Ok(hall);
    }

    [HttpPost]
    [ActionName("halls")]
    public ActionResult<CommandReadDto> CreateHall(Hall hall)
    {
      _repository.CreateHall(hall);
      _repository.SaveChanges();

      return CreatedAtRoute(nameof(GetHallById), new {Id = hall.Id}, hall);
    }

    [HttpPost("{id}")]
    [ActionName("halls")]
    public ActionResult UpdateHall(int id, Hall hallUpdateDto)
    {
      var hallModel = _repository.GetHallById(id);

      if (hallModel == null)
        return NotFound();

      Hall.CopyValues(hallUpdateDto, hallModel);
      hallModel.Seats.CheckDangerAndPerformActionsIfNeeded();

      _repository.UpdateHall(hallModel);
      _repository.SaveChanges();

      return NoContent();
    }

    [HttpDelete("{id}")]
    [ActionName("halls")]
    public ActionResult DeleteHall(int id)
    {
      var hallRepo = _repository.GetHallById(id);
      if (hallRepo == null)
        return NotFound();

      _repository.DeleteHall(hallRepo);
      _repository.SaveChanges();
      return NoContent();
    }

    [HttpGet]
    [ActionName("businesses")]
    public ActionResult<IList<CommandReadDto>> GetAllBusinesses()
    {
      var businesses = _repository.GetAllBusinesses();
      return Ok(businesses);
    }

    [HttpGet("{id}", Name = "GetBusinessById")]
    [ActionName("businesses")]
    public ActionResult<CommandReadDto> GetBusinessById(int id)
    {
      var hall = _repository.GetBusinessById(id);
      if (hall == null)
        return NotFound();

      return Ok(hall);
    }

    [HttpPost]
    [ActionName("businesses")]
    public ActionResult<CommandReadDto> CreateBusiness(Business business)
    {
      _repository.CreateBusiness(business);
      _repository.SaveChanges();

      return CreatedAtRoute(nameof(GetBusinessById), new {Id = business.Id}, business);
    }

    [HttpPost("{id}")]
    [ActionName("businesses")]
    public ActionResult UpdateBusiness(int id, Business businessUpdateDto)
    {
      var businessModel = _repository.GetBusinessById(id);
      if (businessModel == null)
        return NotFound();

      Business.CopyValues(businessUpdateDto, businessModel);

      _repository.UpdateBusiness(businessModel);
      _repository.SaveChanges();

      return NoContent();
    }

    [HttpDelete("{id}")]
    [ActionName("businesses")]
    public ActionResult DeleteBusiness(int id)
    {
      var businessRepo = _repository.GetBusinessById(id);
      if (businessRepo == null)
        return NotFound();

      _repository.DeleteBusiness(businessRepo);
      _repository.SaveChanges();
      return NoContent();
    }

    // GET api/commands
    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
    {
      var commandItens = _repository.GetAllCommands();
      return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItens));
    }

    // GET api/commands/{id}
    [HttpGet("{id}", Name = "GetCommandById")]
    public ActionResult<CommandReadDto> GetCommandById(int id)
    {
      var commandItem = _repository.GetCommandById(id);
      if (commandItem == null)
        return NotFound();

      return Ok(_mapper.Map<CommandReadDto>(commandItem));
    }

    //POST api/commands
    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
    {
      var commandModel = _mapper.Map<Command>(commandCreateDto);
      _repository.CreateCommand(commandModel);
      _repository.SaveChanges();

      var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

      return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto);
    }

    //PUT api/commands/{id}
    [HttpPut("{id}")]
    public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
    {
      var commandModelFromRepo = _repository.GetCommandById(id);
      if (commandModelFromRepo == null)
        return NotFound();

      _mapper.Map(commandUpdateDto, commandModelFromRepo);
      _repository.UpdateCommand(commandModelFromRepo);
      _repository.SaveChanges();

      return NoContent();
    }

    //PATCH api/commands/{id}
    [HttpPatch("{id}")]
    public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
    {
      var commandModelFromRepo = _repository.GetCommandById(id);
      if (commandModelFromRepo == null)
        return NotFound();

      var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
      patchDoc.ApplyTo(commandToPatch, ModelState);
      if (!TryValidateModel(commandToPatch))
        return ValidationProblem(ModelState);

      _mapper.Map(commandToPatch, commandModelFromRepo);
      _repository.UpdateCommand(commandModelFromRepo);
      _repository.SaveChanges();

      return NoContent();
    }

    //DELETE api/commands/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteCommand(int id)
    {
      var commandModelFromRepo = _repository.GetCommandById(id);
      if (commandModelFromRepo == null)
        return NotFound();

      _repository.DeleteCommand(commandModelFromRepo);
      _repository.SaveChanges();
      return NoContent();
    }
  }
}
