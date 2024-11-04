using System.Data;
using TestTSOUA.Models;
using TestTSOUA.DB;
using Dapper;
namespace TestTSOUA.Repository
{
    public class MainDataRepository : IMainDataRepository
    {

        private readonly IDbConnection _connection;
        public MainDataRepository()
        {
            _connection = Database.Instance.Connection;
        }

        public IEnumerable<HoursModel> GetDataHours(string date)
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            string sql = $"SELECT id_obj, date_start FROM hours where date_start like '{date}%'";
            return _connection.Query<HoursModel>(sql);
        }    
    }
}
