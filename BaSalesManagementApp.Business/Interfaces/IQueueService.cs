using BaSalesManagementApp.Entites.DbSets;

namespace BaSalesManagementApp.Business.Interfaces
{
    public interface IQueueService
    {
        // Kuyruğa öğe ekleme 
        Task EnqueueAsync(Queue item);

        // Kuyruktan öğe alma
        Task<Queue> DequeueAsync();
    }
}
