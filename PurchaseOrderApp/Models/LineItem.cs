using System.Web;
using System.Web.Mvc;

namespace PurchaseOrderApp.Models
{
    public class LineItem
    {
        public int LineItemID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int PurchaseOrderID { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }


        //public List<SelectListItem> LineItemList { get; set; }
    }
}
