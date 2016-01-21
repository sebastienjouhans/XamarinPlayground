namespace Fluffy.Services.Communication
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Common;
    using Common.Entities;
    using Common.Interfaces;
    public class FluffyCommunicationService : CommunicationService, IFluffyCommunicationService
    {
        public FluffyCommunicationService(IActivityReportingService activityReportingService, IJsonSerializer jsonSerializer):base (activityReportingService, jsonSerializer)
        {
            
        }

        public async Task<NetworkResponse<TestData>> GetDataAsync()
        {
            var requestUri =
                new Uri(@"http://www.mocky.io/v2/55e6c13113cc83ec0dda068a");

            var request = this.GetPriorityMomentsRequest(HttpMethod.Post, requestUri);

            var result =
                await
                this.GetNetworkResponseAsync<TestData>(requestUri, request, this.jsonSerializer, true)
                    .ConfigureAwait(false);

            return result;
        }

        private HttpRequestMessage GetPriorityMomentsRequest(HttpMethod method, Uri requestUri)
        {
            var request = new HttpRequestMessage(method, requestUri);
            //request.Headers.Add(DeviceIdHeader, this.deviceInformationService.DeviceUniqueId);
            //request.Headers.Add(AppVersionHeader, this.deviceInformationService.CodeBaseVersion);
            //request.Headers.Add(AppNameHeader, AppNameValue);
            //request.Headers.Add(UserAgentHeader, this.userAgent);
            return request;
        }
    }
}