namespace Rental.Core.Models
{
    public class RentalItem: BaseModel
    {
        public int EquipmentId { get; set; }       
        public Equipment Equipment { get; set; }
        public int DaysOfRental { get; set; }
        public int OrderId { get; set; }
    }
}
