namespace Fluffy.Common.Interfaces
{
    using System.Threading.Tasks;
    using Entities;

    public interface IFluffyCommunicationService : ICommunicationService
    {
        Task<NetworkResponse<TestData>> GetDataAsync();
    }
}