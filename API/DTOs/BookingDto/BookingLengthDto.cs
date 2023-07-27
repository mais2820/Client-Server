namespace API.DTOs.BookingDto
{
    public class BookingLengthDto
    {
        public Guid RoomGuid { get; set; }
        public string RoomName { get; set; }
        public double BookingLength { get; set; }
    }
}
