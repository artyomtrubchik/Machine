using Machine.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Machine.Domain.Entities;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Machine.Domain.Concrete
{
    public class CoinSqlRepository : ICoinRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public IEnumerable<Coin> GetCoins()
        {
            List<Coin> users = new List<Coin>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                users = db.Query<Coin>("SELECT * FROM Coins").ToList();
            }
            return users;
        }

        ~CoinSqlRepository()
        {
            
        }
    }
}