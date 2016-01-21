namespace Fluffy.Services.Communication
{
    using Common.Interfaces;
    public class FluffyCommunicationService : CommunicationService, IFluffyCommunicationService
    {
        public FluffyCommunicationService(IActivityReportingService activityReportingService, IJsonSerializer jsonSerializer):base (activityReportingService, jsonSerializer)
        {
            
        }
    }
}