using Jwt.API.Model;

namespace Jwt.API.Services
{
    public interface IParcelService
    {
        public List<Parcel> GetAllParcels();
        public Parcel AddParcel(ParcelDto parcel);
        public Parcel Delete(int id);
        public Parcel Read(int id);
    }
}
