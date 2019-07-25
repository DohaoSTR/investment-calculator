using System;
using System.Data.Linq.Mapping;

namespace Course_Project.ModelDB
{
    [Table(Name = "CompletedInvestment")]
    class CompletedDB
    {
        [Column(Name = "Id_Account", IsPrimaryKey = true)]
        public int Id_Account { get; set; }
        [Column(Name = "Id_Operation")]
        public int Id_Operation { get; set; }
        [Column(Name = "NameInvest", CanBeNull = true)]
        public string NameInvest { get; set; }
        [Column(Name = "InitialInvest", CanBeNull = true)]
        public decimal InitialInvest { get; set; }
        [Column(Name = "ProfitMoney", CanBeNull = true)]
        public decimal ProfitMoney { get; set; }
        [Column(Name = "DateOpenInvest", CanBeNull = true)]
        public DateTime DateOpenInvest { get; set; }
        [Column(Name = "DateCloseInvest", CanBeNull = true)]
        public DateTime DateCloseInvest { get; set; }
        [Column(Name = "PercentProfit", CanBeNull = true)]
        public decimal PercentProfit { get; set; }
    }
}
