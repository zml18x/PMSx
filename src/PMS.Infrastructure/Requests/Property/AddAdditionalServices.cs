using System.ComponentModel.DataAnnotations;

namespace PMS.Infrastructure.Requests.Property
{
    public class AddAdditionalServices
    {
        [Required]
        public string[] ServiceName { get; set; }
        [Required]
        public string[] ServiceDescription { get; set; }



        public AddAdditionalServices(string[] serviceName, string[] serviceDescription)
        {
            ServiceName = serviceName;
            ServiceDescription = serviceDescription;
        }
    }
}
