namespace AcunmedyaSunum.Models
{
    public class CreateOrderModel
    {
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public decimal InvoiceAmount { get; set; }
    }
}
