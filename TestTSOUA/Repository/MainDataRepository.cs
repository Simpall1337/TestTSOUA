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
            string sql = $"SELECT id_obj, date_start,value_one, value_two FROM hours where date_start like '{date}%'";
            return _connection.Query<HoursModel>(sql);
        }
        public IEnumerable<ObjectsModel> GetObjectsHours(string date)
        {
            if (_connection.State == ConnectionState.Closed)
            {
               _connection.Open();
            }
            string sql = $"select DISTINCT id_obj as id from hours where date_start like '{date}%'";
            return _connection.Query<ObjectsModel>(sql);
        }
        public IEnumerable<HoursAnalyzerModel> GetHoursAnalyzerHours(string date)
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            string sql = $"SELECT id_obj, SUM(value_one) as value_one,ROUND(AVG(value_two),2) as value_two FROM hours where date_start like '{date}%' group by id_obj ";
            return _connection.Query<HoursAnalyzerModel>(sql);
        }
    }
}
