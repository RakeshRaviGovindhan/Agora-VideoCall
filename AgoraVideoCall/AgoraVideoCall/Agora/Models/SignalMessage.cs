using System;
using System.Collections.Generic;
using System.Text;

namespace AgoraVideoCall.Agora.Models
{
    public class SignalMessage
    {
        public uint RtcPeerId { get; set; }

        public string RtmUserName { get; set; }

        public SignalActionTypes Action { get; set; }

        public string Data { get; set; }
    }

    public enum SignalActionTypes
    {
        HandUp = 1,
        HandDown = 2,
        IncomingCall = 3,
        RejectCall = 4
    }
}
