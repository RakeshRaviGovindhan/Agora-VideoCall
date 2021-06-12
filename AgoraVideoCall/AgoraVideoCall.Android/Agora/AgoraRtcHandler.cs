using AgoraVideoCall.Agora.Helpers;
using AgoraVideoCall.Droid.Agora;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DT.Xamarin.Agora;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;


[assembly: Xamarin.Forms.Dependency(typeof(AgoraRtcHandler))]
namespace AgoraVideoCall.Droid.Agora
{
    public class AgoraRtcHandler : IRtcEngineEventHandler, IVideoHandler
    {
        private RoomActivity _context;

        public AgoraRtcHandler(RoomActivity activity)
        {
            _context = activity;
        }

        public override void OnJoinChannelSuccess(string p0, int p1, int p2)
        {
            _context.OnJoinChannelSuccess(p0, p1, p2);
        }

        public override void OnFirstRemoteVideoDecoded(int p0, int p1, int p2, int p3)
        {
            //Debug.WriteLine($"DidOfflineOfUid {p0}");
            _context.OnFirstRemoteVideoDecoded(p0, p1, p2, p3);
        }

        public override void OnUserJoined(int p0, int p1)
        {
            //Debug.WriteLine($"OnUserJoined {p0}");
        }

        public override void OnUserOffline(int p0, int p1)
        {
            //Debug.WriteLine($"DidOfflineOfUid {p0}");
            _context.OnUserOffline(p0, p1);
        }

        public override void OnUserMuteVideo(int p0, bool p1)
        {
            _context.OnUserMuteVideo(p0, p1);
        }

        public override void OnFirstLocalVideoFrame(int p0, int p1, int p2)
        {
            _context.OnFirstLocalVideoFrame(p0, p1, p2);
        }

        public override void OnTokenPrivilegeWillExpire(string token)
        {
            _context.OnTokenPrivilegeWillExpire(token);
        }

        public void OnJoin()
        {
            _context.OnJoin();
        }
    }
}