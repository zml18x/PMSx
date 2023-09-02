using PMS.Core.Exceptions;

namespace PMS.Core.Entities
{
    public class PropertyAdditionalService : AdditionalService
    {
        public Guid PropertyId { get; protected set; }



        public PropertyAdditionalService(Guid id, string serviceName, string serviceDescription, Guid propertyId) : base(id,serviceName, serviceDescription)
        {
            SetPropertyId(propertyId);
        }



        private void SetPropertyId(Guid propertyId)
        {
            if (propertyId == Guid.Empty)
                throw new EmptyIdException("PropertyId cannot be Guid.Empty");

            PropertyId = propertyId;
        }
    }
}