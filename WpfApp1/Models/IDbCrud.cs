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
        List<EventModel> GetEvents();

        List<SessionModel> GetSessions(int eventId);
        List<EventModel> GetUserSessions(int userId);
        List<CategoryModel> GetCategories();
        List<TypeModel> GetTypes();

        List<EventModel> GetEvents(Date d);

        UserModel SignIn(UserModel user);

        bool SignOn(UserModel user);

        User GetUser(int id);
    }
}
