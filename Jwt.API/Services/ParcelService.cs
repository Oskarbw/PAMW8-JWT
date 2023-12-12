
using Jwt.API.Model;

namespace Jwt.API.Services
{
    public class ParcelService : IParcelService
    {

        private static List<Parcel> _parcels = new List<Parcel>
        {
            new Parcel
            {
                Id = 1,
                Sender = "John",
                Receiver = "Frank",
                Weight = 12
            },
            new Parcel
            {
                Id = 2,
                Sender = "Janice",
                Receiver = "Hilary",
                Weight = 17
            }
        };

        public List<Parcel> GetAllParcels()
        {
            return _parcels;
        }

        public Parcel AddParcel(ParcelDto parcelDto)
        {
            var parcel = new Parcel
            {
                Id = 3,
                Sender = parcelDto.Sender,
                Receiver = parcelDto.Receiver,
                Weight = parcelDto.Weight,
            };
            if (parcel == null)
            {
                return null;
            }

            _parcels.Add(parcel);
            return parcel;
        }

        public Parcel Delete(int id)
        {
            return null;
        }

        public Parcel Read(int id)
        {
            return _parcels[1];
        }
    }
}
