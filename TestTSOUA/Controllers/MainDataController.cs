using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SQLite;
using TestTSOUA.Models;
using TestTSOUA.Repository;

namespace TestTSOUA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainDataController : ControllerBase
    {
        private readonly IMainDataRepository _mainDataRepository;
        private readonly IDbConnection _connection;
        public MainDataController(IMainDataRepository mainDataRepository)
        {
            _mainDataRepository = mainDataRepository;
        }

        [HttpGet]
        public IActionResult GetMainData(string date)
        {
            try
            {
                var hoursList = _mainDataRepository.GetDataHours(date).ToList();
                return Ok(hoursList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
           
            
                
        }
    }
}
