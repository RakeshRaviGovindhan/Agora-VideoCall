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
		public static string AgoraAPI { get { return "ca68b658d96d46d29d9f41cc34b0170b"; } }

        public static string TokenServerBaseUrl { get; }

        /// <summary>
        /// Temp token generated in https://dashboard.agora.io/ or Token from your API
        /// </summary>
        public static string RtcToken { get { return "006ca68b658d96d46d29d9f41cc34b0170bIAAs68CMwAsQG6dhm73noXbxlpSudEUDEvnSQoMrqLJwNomfRIsAAAAAEACDSUn7J4/EYAEAAQAmj8Rg"; } }

        public static string RtmToken { get { return "006ca68b658d96d46d29d9f41cc34b0170bIAAs68CMwAsQG6dhm73noXbxlpSudEUDEvnSQoMrqLJwNomfRIsAAAAAEACDSUn7J4/EYAEAAQAmj8Rg"; } }

        public const string ShareString = "Hey check out Xamarin Agora sample app at: https://github.com/DreamTeamMobile";
    }

}
