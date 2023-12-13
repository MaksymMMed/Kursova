using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SpeechBackend.API.Security;
using SpeechBackend.BLL.DTO.Request;
using SpeechBackend.BLL.DTO.Response;
using SpeechBackend.BLL.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SpeechBackend.API.Controllers
{
    [ApiController]
    [Route("UserController")]
    public class UserController:ControllerBase
    {
        readonly IUserService service;
        readonly IConfiguration configuration;

        public UserController(IUserService service, IConfiguration configuration)
        {
            this.service = service;
            this.configuration = configuration;
        }

        [HttpGet("GetUserByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetUserResponse>> GetUserByEmail(string email, string password)
        {
            try
            {
                var item = await service.GetUserByEmail(email, password);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }



        [HttpPost("SignUp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> SignUp([FromBody] AddUserRequest request)
        {
            try
            {
                await service.AddUser(request);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpPost("SignIn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetUserResponse>> SignIn([FromBody] UserData data)
        {
            if (data.password != null && data.email != null)
            {
                var userData = await service.GetUserByEmail(data.email, data.password);

                var jwt = configuration.GetSection("Jwt").Get<Jwt>();
                if (userData != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt!.Subject!),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id",userData.Id.ToString()),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, userData.Role!),
                        new Claim("Password",userData!.Password!.ToString()),
                        new Claim("Email",userData.Email!.ToString())
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key!));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(jwt.Issuer, jwt.Audience, claims,
                        expires: DateTime.Now.AddMinutes(90), signingCredentials: signIn);
                    userData.Token = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(userData);
                }
                else
                {
                    return BadRequest("Invalid Credentials");
                }
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }


        [HttpPut("UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult> UpdateUser([FromBody] AddUserRequest request)
        {
            try
            {
                await service.UpdateUser(request);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpDelete("DeleteUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await service.DeleteUser(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }
    }
}
