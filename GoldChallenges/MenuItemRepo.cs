using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldChallenges
{
    public class MenuItemRepo
    {
        readonly List<MenuItem> _repo = new List<MenuItem>();

        public bool AddMenuItem(MenuItem itemToAdd)
        {
            int addCheck = _repo.Count;
            _repo.Add(itemToAdd);
            bool wasAdded = addCheck < _repo.Count;
            return wasAdded;
        }

        public List<MenuItem> GetMenu()
        {
            return _repo;
        }

        public MenuItem GetMealByNum(int mealNum)
        {
            foreach (MenuItem meal in _repo)
            {
                if (mealNum == meal.MealNumber)
                {
                    return meal;
                }
            }
            return null;
        }

        public bool DeleteMenuItem(MenuItem itemToDelete)
        {
            int startingCount = _repo.Count;
            _repo.Remove(itemToDelete);
            bool wasRemoved = startingCount > _repo.Count;
            return wasRemoved;
        }
    }
}
