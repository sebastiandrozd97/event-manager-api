using Microsoft.AspNetCore.Mvc;

namespace EventmanagerApi.Controllers
{
	public class Test : ControllerBase
	{
		[HttpGet("api/user")]
		public IActionResult Get()
		{
			return Ok(new {Name = "Seba"});
		}
	}
}