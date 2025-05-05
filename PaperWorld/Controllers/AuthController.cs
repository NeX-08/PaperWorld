using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<Members> _userManager;
    private readonly SignInManager<Members> _signInManager;

    public AuthController(UserManager<Members> userManager,
                          SignInManager<Members> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [Authorize(Roles = "AdminRole")]
    [HttpGet("users")]
    public IActionResult GetAllUsers()
    {
        var users = _userManager.Users.Select(u => new { u.Id, u.Email });
        return Ok(users);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest model)
    {
        var user = new Members
        {
            UserName = model.Email,
            Email = model.Email,
            Name = model.Name,       
            Address = model.Address
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Member");
            return Ok("User registered successfully.");
        }

        return BadRequest(result.Errors);
    }


    [HttpPost("register-satff")]
    public async Task<IActionResult> RegisterAsSatff([FromBody] RegisterRequest model)
    {
        var user = new Members
        {
            UserName = model.Email,
            Email = model.Email,
            Name = model.Name,       
            Address = model.Address
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Satff");
            return Ok("User registered successfully.");
        }

        return BadRequest(result.Errors);
    }


    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAsAdmin([FromBody] RegisterRequest model)
    {
        var user = new Members
        {
            UserName = model.Email,
            Email = model.Email,
            Name = model.Name,        
            Address = model.Address
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "AdminRole");
            return Ok("User registered successfully.");
        }

        return BadRequest(result.Errors);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return Ok("Logged in successfully.");
        }

        return Unauthorized("Invalid email or password.");
    }


    [HttpPost("login-token")]
    public async Task<IActionResult> LoginForToken([FromBody] LoginRequest model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var roles = await _userManager.GetRolesAsync(user);
            var result = TokenHelper.GenerateToken(user, roles.ToList());

            return Ok(result);
        }

        return Unauthorized("Invalid email or password.");
    }



    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok("Logged out successfully.");
    }
}