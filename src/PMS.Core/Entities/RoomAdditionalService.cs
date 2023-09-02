using PMS.Core.Exceptions;

namespace PMS.Core.Entities
{
    public class RoomAdditionalService : AdditionalService
    {
        public Guid RoomId { get; protected set; }



        public RoomAdditionalService(Guid id, string serviceName, string serviceDescription, Guid roomId) : base(id,serviceName,serviceDescription)
        {
            SetRoomId(roomId);
        }



        private void SetRoomId(Guid roomId)
        {
            if (roomId == Guid.Empty)
                throw new EmptyIdException("RoomId cannot be Guid.Empty");

            RoomId = roomId;
        }
    }
}