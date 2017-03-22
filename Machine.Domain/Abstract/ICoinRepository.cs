using Machine.Domain.Entities;
using System.Collections.Generic;


namespace Machine.Domain.Abstract
{
    public interface ICoinRepository
    {
        IEnumerable<Coin> GetCoins();
    }
}
