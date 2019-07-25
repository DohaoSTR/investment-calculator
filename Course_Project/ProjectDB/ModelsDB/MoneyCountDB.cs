using System.Data.Linq.Mapping;

namespace Course_Project.ModelDB
{
    [Table(Name = "MoneyCount")]
    class MoneyCountDB
    {
        [Column (Name = "Id_Account", IsPrimaryKey = true)]
        public int Id_Account { get; set; }
        [Column (Name = "MoneyCount", CanBeNull = true)]
        public decimal MoneyCount { get; set; }
    }
}
