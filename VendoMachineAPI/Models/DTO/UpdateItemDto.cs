namespace VendoMachineAPI.Models.DTO
{
    public class UpdateItemDto
    {
        public string itemFood { get; set; } = "";
        public int itemStock { get; set; }
        public double itemPrice { get; set; }
    }
}
