using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AgoraVideoCall.Agora
{
    public class AgoraTokenResponse
    {
        [JsonProperty("key")]
        public string Key { get; set; }
    }

    public class AgoraTokenService
    {
        public static async Task<string> GetRtcToken(string channelName)
        {
            if (!string.IsNullOrEmpty(AgoraTestConstants.RtcToken))
                return AgoraTestConstants.RtcToken;

            var request = WebRequest.Create($"{AgoraTestConstants.TokenServerBaseUrl}/rtcToken?channelName={channelName}");
            return await GetStringResponse(request);
        }

        public static async Task<string> GetRtmToken(string userName)
        {
            if (!string.IsNullOrEmpty(AgoraTestConstants.RtmToken))
                return AgoraTestConstants.RtmToken;

            var request = WebRequest.Create($"{AgoraTestConstants.TokenServerBaseUrl}/rtmToken?account={userName}");
            return await GetStringResponse(request);
        }

        private static async Task<string> GetStringResponse(WebRequest request)
        {
            try
            {
                var response = await request.GetResponseAsync();
                var result = string.Empty;
                using (var dataStream = response.GetResponseStream())
                {
                    var reader = new StreamReader(dataStream);
                    result = await reader.ReadToEndAsync();
                }
                var tokenResponse = JsonConvert.DeserializeObject<AgoraTokenResponse>(result);
                return tokenResponse.Key;
            }
            catch (Exception e)
            {
                //ignore
            }
            return string.Empty;
        }
    }
}
