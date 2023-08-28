using RestueantDB.Models;
using RestueantDB.ModelViews;
using System.Collections.Generic;

namespace RestueantDB.Repostry.IServices
{
    public interface IResturant
    {
         List<ResturantMV> GetAllResturant();
        int AddResturant(ResturantMV resturant );
        public ResturantMV ShowDetails(int id);
    }
}
