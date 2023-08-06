using System.ComponentModel.DataAnnotations;

namespace PMS.Infrastructure.Requests.Property
{
    public class AddRooms
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public string[] Number { get; set; }
        [Required]
        public string[] Name { get; set;}
        [Required]
        public string[] Description { get; set;}
        [Required]
        public string[] Type { get; set; }
        [Required]
        public int[] SingleBedCount { get; set; }
        [Required]
        public int[] DoubleBedCount { get; set; }



        public AddRooms(int amount, string[] number, string[] name, string[] description, string[] type, int[] singleBedCount, int[] doubleBedCount)
        {
            Amount = amount;
            Number = number;
            Name = name; 
            Description = description; 
            Type = type; 
            SingleBedCount = singleBedCount;
            DoubleBedCount = doubleBedCount;

            RequestValidation();
        }


        private void RequestValidation()
        {
            if (Number.Length < Amount || Name.Length < Amount || Description.Length < Amount || Type.Length < Amount
                || SingleBedCount.Length < Amount || DoubleBedCount.Length < Amount)
                throw new ArgumentException("Complete the data for the given number of rooms");  
        }
    }
}