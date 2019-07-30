using Domain;

namespace ServiceLayer
{
    public interface IServicePing
    {
        void CreatePing(Ping ping);
        void DeletePing(Ping ping);
        void EditPing(Ping pingModel, int id);
        Ping GetPing(long id);
    }
}