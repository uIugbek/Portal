using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Portal.Apis.Core.Auth;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Core.Helpers;
using Portal.Apis.Models;

namespace Portal.Apis.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly FacebookAuthSettings _fbAuthSettings;

        public AuthController(
            IMapper mapper,
            IJwtFactory jwtFactory,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IOptions<JwtIssuerOptions> jwtOptions,
            IOptions<FacebookAuthSettings> fbAuthSettingsAccessor
        )
        {
            _mapper = mapper;
            _jwtFactory = jwtFactory;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtOptions = jwtOptions.Value;
            _fbAuthSettings = fbAuthSettingsAccessor.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userIdentity = _mapper.Map<User>(model);
                var result = await _userManager.CreateAsync(userIdentity, model.Password);

                if (result.Succeeded)
                    return Ok("Account created");
                else
                    Errors.AddErrorsToModelState(result, ModelState);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]CredentialsViewModel credentials)
        {
            if (ModelState.IsValid)
            {
                var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);

                if (identity != null)
                {
                    var jwt = await Tokens.GenerateJwt(
                        identity,
                        _jwtFactory,
                        credentials.UserName,
                        _jwtOptions,
                        new JsonSerializerSettings { Formatting = Formatting.Indented }
                    );

                    return Ok(jwt);
                }
                else
                    ModelState.AddModelError("login_failure", "Invalid username or password.");
            }

            return BadRequest(ModelState);
        }

        [HttpPost("facebook")]
        public async Task<IActionResult> Facebook([FromBody]FacebookAuthViewModel model)
        {
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(
               await GetHttpResponse(_fbAuthSettings.GetAppAccessUrl())
            );
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(
                await GetHttpResponse(_fbAuthSettings.GetUserAccessUrl(model.AccessToken, appAccessToken.AccessToken))
            );
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(
                await GetHttpResponse(_fbAuthSettings.GetUserInfoUrl(model.AccessToken))
            );

            if (!userAccessTokenValidation.Data.IsValid)
                return BadRequest(
                    Errors.AddErrorToModelState("login_failure", "Invalid facebook token.", ModelState)
                );

            var user = await _userManager.FindByEmailAsync(userInfo.Email);
            if (user == null)
            {
                var roleId = _roleManager.FindByNameAsync("User").Id;
                var newUser = new User
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    FacebookId = userInfo.Id,
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    Photo = userInfo.Picture.Data.Url,
                };

                var result = await _userManager.CreateAsync(newUser, "123qweR!");

                if (!result.Succeeded)
                    return BadRequest(
                        Errors.AddErrorsToModelState(result, ModelState)
                    );
            }

            var localUser = await _userManager.FindByNameAsync(userInfo.Email);

            if (localUser == null)
                return BadRequest(
                    Errors.AddErrorToModelState("login_failure", "Failed to create local user account.", ModelState)
                );

            var jwt = await Tokens.GenerateJwt(
                _jwtFactory.GenerateClaimsIdentity(localUser.UserName, localUser.Id.ToString()),
                _jwtFactory,
                localUser.UserName,
                _jwtOptions,
                new JsonSerializerSettings { Formatting = Formatting.Indented }
            );

            return Ok(jwt);
        }

        private Task<string> GetHttpResponse(string url)
        {
            HttpClient httpClient = new HttpClient();

            return httpClient.GetStringAsync(
                _fbAuthSettings.GetAppAccessUrl()
            );
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                var userToVerify = await _userManager.FindByNameAsync(userName);

                if (userToVerify != null)
                {
                    if (await _userManager.CheckPasswordAsync(userToVerify, password))
                        return await Task.FromResult(
                            _jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id.ToString())
                        );
                }
            }

            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
