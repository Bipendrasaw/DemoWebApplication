using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClassLibrary.Model.Model.RequestModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoyServices.Interface;

namespace DemoWebApplication.Controllers
{
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IPostData postData;
        public IMapper Mapper { get; }

        public HomeController(IPostData postData, IMapper mapper)
        {
            this.postData = postData;
            Mapper = mapper;
        }


        [HttpPost("save")]
		public async Task<IActionResult> PostDataMethod(RequestItem requestItem)
		{
            try
            {
                if (requestItem.Items == null || !requestItem.Items.Any())
                {
                    return BadRequest("Item is null");
                }

                await postData.SaveBulkData(requestItem.Items);
                return Ok("Record inserted successfully!");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error item posting");
            }
		}
	}
}
