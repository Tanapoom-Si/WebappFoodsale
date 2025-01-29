using System.ComponentModel.DataAnnotations;

namespace WebappFoodsale.Models
{
    public class Food
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }

        public string Region { get; set; }
        public string City { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
    }
}
