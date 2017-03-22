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
    public class DrinkSqlRepository : IDrinkRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public IEnumerable<Drink> GetDrinks()
        {
            List<Drink> drinks = new List<Drink>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                drinks = db.Query<Drink>("SELECT * FROM Drinks").ToList();
            }
            return drinks;
        }

        public void Update(Cart cart)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery =
                "UPDATE Drinks " +
                "SET Quantity = Quantity - @Quantity " +
                "WHERE Id = @Id";
                db.Execute(sqlQuery, cart);
            }
            
        }

        ~DrinkSqlRepository()
        {
           
        }
    }
}