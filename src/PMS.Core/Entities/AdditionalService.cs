using PMS.Core.Exceptions;

namespace PMS.Core.Entities
{
    public abstract class AdditionalService : Entity
    {
        public string ServiceName { get; protected set; }
        public string ServiceDescription { get; protected set; }



        public AdditionalService(Guid id, string serviceName, string serviceDescription)
        {
            SetId(id);
            SetServiceName(serviceName);
            SetServiceDescription(serviceDescription);
        }



        private void SetId(Guid id)
        {
            if (id == Guid.Empty)
                throw new EmptyIdException("Id in AdditionalServices cannot be Guid.Empty");

            Id = id;
        }

        private void SetServiceName(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                throw new ArgumentNullException(nameof(serviceName), "SerivceName cannot be null or whitespace");

            ServiceName = serviceName;
        }

        private void SetServiceDescription(string serviceDescription)
        {
            if (string.IsNullOrWhiteSpace(serviceDescription))
                throw new ArgumentNullException(nameof(serviceDescription), "SerivceName cannot be null or whitespace");

            ServiceName = serviceDescription;
        }
    }
}