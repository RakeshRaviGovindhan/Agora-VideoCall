using System;
using System.Collections.Generic;
using System.Text;

namespace AgoraVideoCall.Agora
{
    public static class AgoraTestConstants
    {
        /// <summary>
        /// App ID from https://dashboard.agora.io/
        /// </summary>
		public static string AgoraAPI { get; }

        public static string TokenServerBaseUrl { get; }

        /// <summary>
        /// Temp token generated in https://dashboard.agora.io/ or Token from your API
        /// </summary>
        public static string RtcToken { get; }

        public static string RtmToken { get; }

        public const string ShareString = "Hey check out Xamarin Agora sample app at: https://github.com/DreamTeamMobile";
    }

}
