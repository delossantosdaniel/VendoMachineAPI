namespace VendoMachineAPI.Models.DTO
{
    public class AddItemDto
    {
        public string itemFood { get; set; } = "";
        public int itemStock { get; set; }
        public double itemPrice { get; set; }
    }
}
