using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    public class Card
    {
        public int ID { get; set; }
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public string pic { get; set; }
    }

    public class CardDBContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
    }
}
