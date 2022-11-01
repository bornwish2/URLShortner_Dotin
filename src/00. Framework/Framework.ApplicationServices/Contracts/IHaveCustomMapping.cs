using AutoMapper;

namespace Framework.ApplicationServices.Contracts
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile profile);
    }
}
