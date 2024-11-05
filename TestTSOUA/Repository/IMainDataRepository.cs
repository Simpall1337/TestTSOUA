using Microsoft.AspNetCore.Http.Metadata;
using TestTSOUA.Models;

namespace TestTSOUA.Repository
{
    public interface IMainDataRepository
    {
        public IEnumerable<HoursModel> GetDataHours(string date);
        public IEnumerable<ObjectsModel> GetObjectsHours(string date);
        public IEnumerable<HoursAnalyzerModel> GetHoursAnalyzerHours(string date);
        
    }
}
