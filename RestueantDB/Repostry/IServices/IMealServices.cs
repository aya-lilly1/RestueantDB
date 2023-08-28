using RestueantDB.ModelViews;
using System.Collections.Generic;

namespace RestueantDB.Repostry.IServices
{
    public interface IMealServices
    {
        int AddMeal(MealMV meal,int resId);
        List<MealMV> GetMeal(int id);
        MealMV ShowMealDetails(int id);
    }
}