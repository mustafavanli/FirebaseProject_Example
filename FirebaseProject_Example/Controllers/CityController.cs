using Firebase.Database;
using Firebase.Database.Query;
using FirebaseProject_Example.GenericService;
using FirebaseProject_Example.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirebaseProject_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        IFirebaseHelper<City> Helper;
        public CityController(IFirebaseHelper<City> _helper)
        {
            Helper = _helper;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add(City city)
        {
            city.Id = Guid.NewGuid().ToString("N"); // GENERATE ID !
            await Helper.Add(city.Id.ToString(),city);
            return Ok();
        }
        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            var data = await Helper.GetAll();
            return Ok(data);
        }
        [HttpGet("GetBy")]
        public async Task<IActionResult> GetBy(string Id)
        {
            return Ok(await Helper.Get(Id));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(string Id)
        {
            return Ok(await Helper.Delete(Id));
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(City city)
        {
            return Ok(await Helper.Update(city));
        }
    }
}
