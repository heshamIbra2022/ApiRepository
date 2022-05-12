using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController (UserManager<ApplicationUser> userManager , IConfiguration Configuration)
        {
            this.userManager = userManager;
            configuration = Configuration;
        }


        [HttpGet("getAdmins")]
        public async Task<IActionResult> GetAll()
        {
            var users = await userManager.GetUsersInRoleAsync("admin");
           List< RegisterDTO> registerDTO = new List<RegisterDTO>();
           
           for (int i=0;i<users.Count;i++)
            {
                RegisterDTO registerDTOobject = new RegisterDTO();
                registerDTOobject.Name = users[i].UserName;
                registerDTOobject.password = users[i].PasswordHash;
                registerDTOobject.email = users[i].Email;
                registerDTOobject.MobileNo = users[i].PhoneNumber;

                registerDTO.Add(registerDTOobject);
            }

          
            return Ok(registerDTO);
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllgetusera()
        {
            List<ApplicationUser> users = await  userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("Register")]
        public async Task< IActionResult> Register(RegisterDTO registerDTO)
        {
        
            if (ModelState.IsValid== false)
            {
                return BadRequest(ModelState);
            }
            ApplicationUser userModel = new ApplicationUser();
            userModel.UserName = registerDTO.Name;
            userModel.Email = registerDTO.email;
            userModel.PhoneNumber = registerDTO.MobileNo;
         IdentityResult result= await userManager.CreateAsync(userModel,registerDTO.password  );
            IdentityResult roleResult = await userManager.AddToRoleAsync(userModel, "user");

            if (result.Succeeded)
            {
                return Ok("added success");
            }
            else
            {
foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
                return BadRequest(ModelState);
            }
        }



        [HttpPost("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            ApplicationUser userModel = new ApplicationUser();
            userModel.UserName = registerDTO.Name;
            userModel.Email = registerDTO.email;
          

             IdentityResult result = await userManager.CreateAsync(userModel, registerDTO.password);
            IdentityResult roleResult = await userManager.AddToRoleAsync(userModel, "admin");
            if (result.Succeeded)
            {
                
                return Ok("added success");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return BadRequest(ModelState);
            }
        }























        [HttpPost("login")]
public async Task<IActionResult> login (LoginDTO loginDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
           
          ApplicationUser userModel=  await userManager.FindByNameAsync(loginDTO.UserName);
            if(userModel !=null)
            {
                if(await userManager.CheckPasswordAsync (userModel,loginDTO.password)== true)
                {
                    
                       var mytoken = await GenerateToke(userModel);
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                        expiration = mytoken.ValidTo
                    });
                }
                else
                {
                    return Unauthorized();
                }

            }
            return Unauthorized();
        }


        [NonAction]
        public async Task<JwtSecurityToken> GenerateToke(ApplicationUser userModel)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("intakeNo", "42"));//Custom
            claims.Add(new Claim(ClaimTypes.Name, userModel.UserName));
            claims.Add(new Claim("name", userModel.UserName));
            claims.Add(new Claim("Id", userModel.Id));
           
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userModel.Id));
            var roles = await userManager.GetRolesAsync(userModel);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            //Jti "Identifier Token
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            //---------------------------------(: Token :)--------------------------------------
            var key =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecrtKey"]));
            var mytoken = new JwtSecurityToken(
                audience: configuration["JWT:ValidAudience"],
                issuer: configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials:
                       new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            return mytoken;
        }


    }
}
