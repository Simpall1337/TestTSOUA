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
                var objectsList = _mainDataRepository.GetObjectsHours(date).ToList();
                var hoursAnalyzer = _mainDataRepository.GetHoursAnalyzerHours(date).ToList();

                DateTimeSeparator.SplitDateTime(hoursList);

                var obj = new
                {
                    hoursList,
                    objectsList,
                    hoursAnalyzer
                };
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
           
        }

        public class DateTimeSeparator
        {
            public static void SplitDateTime(List<HoursModel> events)
            {
                foreach (var eventItem in events)
                {
                    if (DateTime.TryParseExact(eventItem.date_start, "d-M-yyyy H:mm:ss",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None, out DateTime parsedDateTime))
                    {
                        
                        eventItem.date_start = parsedDateTime.ToString("d-M-yyyy");
                        eventItem.time_start = parsedDateTime.ToString("H:mm:ss");
                    }
                    else
                    {
                       throw new FormatException("Не вірний формат дати");
                    }
                }
            }
        }
    }
}
