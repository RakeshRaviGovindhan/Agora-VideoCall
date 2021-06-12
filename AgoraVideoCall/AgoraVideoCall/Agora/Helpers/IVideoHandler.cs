using System;
using System.Collections.Generic;
using System.Text;

namespace AgoraVideoCall.Agora.Helpers
{
    public interface IVideoHandler
    {
        void OnJoin();
        void OnJoinChannelSuccess(string p0, int p1, int p2);

        void OnFirstRemoteVideoDecoded(int p0, int p1, int p2, int p3);

        void OnUserJoined(int p0, int p1);

        void OnUserOffline(int p0, int p1);

        void OnUserMuteVideo(int p0, bool p1);

        void OnFirstLocalVideoFrame(int p0, int p1, int p2);

        void OnTokenPrivilegeWillExpire(string token);
    }
}
