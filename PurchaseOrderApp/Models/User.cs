namespace PurchaseOrderApp.Models
{
    public class User
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public DateTime RegistrationDate { get; set; }

        public virtual ICollection<PurchaseOrder> PurchaseOrders
        {
            get; set;
        }
    }
}
