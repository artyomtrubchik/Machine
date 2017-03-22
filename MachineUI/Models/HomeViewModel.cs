using Machine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MachineUI.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Coin> Coins { get; set; }
        public IEnumerable<Drink> Drinks { get; set; }
       
        [Range(0, int.MaxValue, ErrorMessage = "Число должно быть целым и не меньше нуля")]       
        public int Balance { get; set; }

    }
}