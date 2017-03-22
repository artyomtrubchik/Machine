using Machine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine.Domain.Abstract
{
    public interface IDrinkRepository
    {
        IEnumerable<Drink> GetDrinks();
        void Update(Cart cart);
    }
}
