using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI_simple.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                    return Ok($"Role '{roleName}' created successfully!");
                return BadRequest(result.Errors);
            }

            return BadRequest($"Role '{roleName}' already exists.");
        }

     
        [HttpPost("Assign")]
        public async Task<IActionResult> AssignRoleToUser(string username, string roleName)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return NotFound("User not found");

            if (!await _roleManager.RoleExistsAsync(roleName))
                return NotFound("Role not found");

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
                return Ok($"User '{username}' has been assigned to role '{roleName}'");

            return BadRequest(result.Errors);
        }
    }
}
