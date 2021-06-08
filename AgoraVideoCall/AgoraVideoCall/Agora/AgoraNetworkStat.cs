using System;
using System.Collections.Generic;
using System.Text;

namespace AgoraVideoCall.Agora
{
    public class AgoraNetworkStat
    {
        public AgoraQuality Rx;
        public AgoraQuality Tx;

        public AgoraNetworkStat() { }

        public AgoraNetworkStat(int rx, int tx)
        {
            Rx = (AgoraQuality)rx;
            Tx = (AgoraQuality)tx;
        }
    }

}
