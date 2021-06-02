namespace ProductSales.Application.Services
{
    public interface IBusService
    {
        void PublishToMessageQueue(string exchange,string integrationEvent, string eventData);
    }
}