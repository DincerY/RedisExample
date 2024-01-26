using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RedisExample.BasketApi.Core.Application.Repository;
using RedisExample.BasketApi.Core.Domain.Models;

namespace RedisExample.BasketApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<string>> GetById(string id)
    {
        var result = await _userRepository.GetUserAsync(id);
        return Ok(result);
    }

    


    [HttpPost]
    [Route("addproductkey")]
    public async Task<ActionResult<string>> AddProductKeyForUser([FromBody]User user)
    {
        User userResult = await _userRepository.GetUserAsync(user.Id);
        if (userResult == null)
        {
            userResult = new User(user.Id);
        }

        userResult.ProductKeys = user.ProductKeys;
        

        var result = await _userRepository.UpdateUserProductAsync(userResult);
        return Ok(result);
    }
}