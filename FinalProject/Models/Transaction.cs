using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string UserEmail { get; set; }

        [NotNull]
        public decimal Amount { get; set; }

        [NotNull]
        public bool IsIncome { get; set; }  //true: gelir, false: gider

        [NotNull]
        public DateTime Date { get; set; }

        public string Description { get; set; }
    }
}
