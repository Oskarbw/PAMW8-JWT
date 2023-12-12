using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Jwt.API.Model;
using Jwt.API.Services;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Jwt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IParcelService _service = new ParcelService();
        public ParcelController(IConfiguration configuartion)
        {
            _configuration = configuartion;
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public ActionResult<List<Parcel>> GetAllParcels()
        {
            var parcels = _service.GetAllParcels();
            if (parcels == null)
            {
                return NotFound();
            }

            return Ok(parcels);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<Parcel> AddParcel([FromBody] ParcelDto parcelDto)
        {
            var parcel = _service.AddParcel(parcelDto);

            if (parcelDto == null)
            {
                return BadRequest();
            }

            return Ok(parcel);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var result = _service.Read(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(404);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _service.Delete(id);
            if (result != null)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
        
}
