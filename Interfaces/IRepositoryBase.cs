using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCS.User.Interfaces
{
    interface IRepository<TKey, TModel> where TModel : class
    {
        void Add(TKey key, TModel model);
    }


    interface IUserService : IUserRepository
    {

    }

    public class IUserService
    {

    }

}
