using Contracts.Services;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ModsenTestTask.Controllers
{
  [Route("api/authentication")]
  [ApiController]
  public class AuthenticationController : ControllerBase
  {
    private readonly IServiceManager _service;
    public AuthenticationController(IServiceManager service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDTO userForRegistration)
    {
      var result = await _service.AuthenticationService.RegisterUser(userForRegistration);
      if (!result.Succeeded)
      {
        foreach (var error in result.Errors)
        {
          ModelState.TryAddModelError(error.Code, error.Description);
        }
        return BadRequest(ModelState);
      }
      return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDTO user)
    {
      if (!await _service.AuthenticationService.ValidateUser(user))
        return Unauthorized();
      return Ok(new
      {
        Token = await _service
      .AuthenticationService.CreateToken()
      });
    }

  }
}
