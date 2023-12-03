using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreCommon.Book
{
    public class OrderSummary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int summaryId { get; set; }
      
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public OrderPlaced OrderPlaced { get; set; }
        
    }
}
