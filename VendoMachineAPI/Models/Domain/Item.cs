namespace VendoMachineAPI.Models.Domain
{
    public class Item
    {

        public int itemId { get; set; }
        public string itemFood { get; set; } = "";
        public int itemStock { get; set; }
        public double itemPrice { get; set; }
    }
}
