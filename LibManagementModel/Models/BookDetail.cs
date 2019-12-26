using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibManagementModel
{
    public class BookDetail
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BookDetailId { get; set; }

        public string BookName { get; set; }

        public string AuthorName { get; set; }
        public string BookCategory { get; set; }

        public string Edition { get; set; }

        public int Price { get; set; }
        
        public DateTime CreatedDate { get; set; }

    }
}
