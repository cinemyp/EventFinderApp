using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public interface IDbCrud
    {
        List<EventModel> GetEvents(int cityId);
        List<SessionModel> GetSessions(int eventId);
        List<EventModel> GetUserSessions(int userId);
        List<EventModel> GetTodaysEvent(int userId);
        List<CategoryModel> GetCategories();
        List<TypeModel> GetTypes();
        List<CityModel> GetCities();
        List<EventModel> GetEvents(int cityId, Date d);
        UserModel SignIn(UserModel user);
        bool SignOn(UserModel user);
        UserModel GetUser(int id);
        bool MakeFavourite(int userId, int sessionId);
    }
}
