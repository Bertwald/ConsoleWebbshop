using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebbshopCodeFirst.Interfaces;

namespace TestWebbshopCodeFirst.Models
{
    public class Product : IPrintable
    {
        private static Random rand = new();
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            Categories = new HashSet<Category>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? SupplierId { get; set; }
        [StringLength(255)]
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public double Vat { get; set; }
        public int UnitsInStock { get; set; }
        [StringLength(255)]
        public string? Description { get; set; }
        [StringLength(4000)]
        public string? LongDescription { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

        public void Print()
        {
            string[] exclamations = new string[] { "Wow!!!!!", "Super!!!", "Megasale!!", "Chosen!", "Unbelievable!", "Impressive", "Ultralow", "#!@F&>&" };
            ConsoleColor[] colors = new ConsoleColor[] { ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Blue };
            Console.ForegroundColor = colors[rand.Next(0, colors.Length)];
            (int left, int top) = Console.GetCursorPosition();
            Console.Write(exclamations[rand.Next(0, exclamations.Length)]);
            Console.SetCursorPosition(left, top+1);
            Console.Write(Name + " " + Math.Round(Price * (1d + Vat / 100), 2));
            Console.ResetColor();
        }

        public override string ToString()
        {
            return Name + "  " + Description + "  " + Math.Round(Price * (1d + Vat / 100), 2);
        }
    }
}
