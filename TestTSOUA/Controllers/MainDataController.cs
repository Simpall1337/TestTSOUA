using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using TestTSOUA.Models;

namespace TestTSOUA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainDataController : ControllerBase
    {
        [HttpGet]
        public IActionResult getMainData(string date)
        {
            var hoursList = new List<HoursModel>();

            string connectionString = "Data Source=tasks_db.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {

                connection.Open();

                // Выполнение SELECT запроса
                string selectSql = $"SELECT id_obj, date_start FROM hours where date_start like '{date}%'";
                using (var command = new SQLiteCommand(selectSql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var hours = new HoursModel
                            {
                                id_obj = reader.GetString(1), // Первый столбец (id)
                                data_start = reader.GetString(1) // Второй столбец (name)
                            };
                            hoursList.Add(hours);
                        }
                    }
                }
                return Ok(hoursList);
            }
                
        }
    }
}
