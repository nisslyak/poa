using System.ComponentModel.DataAnnotations;

namespace PurchaseOrderApp.Models
{
    public class PurchaseOrder
    {
        public int PurchaseOrderID { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime CreationDate { get; set; }
        public String Name { get; set; }
        public int TotalAmount { get; set; }
        public int UserID { get; set; }
        public Status Status { get; set; }

        public virtual User User { get; set; }

        [MinLength(1, ErrorMessage = "Please provide at least 1 line item"), MaxLength(10, ErrorMessage = "Please provide no more than 10 line items")]
        public virtual ICollection<LineItem> LineItems { get; set; }

    }
    public enum Status
    {
        SUBMITTED, DRAFT
    }
}
