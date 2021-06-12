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
using System.Text;

namespace AgoraVideoCall.Droid.Agora
{
    public class AgoraQualityHandler : IRtcEngineEventHandler
    {
        private RoomActivity _context;

        public AgoraQualityHandler(RoomActivity activity)
        {
            _context = activity;
        }

        public override void OnLastmileQuality(int p0)
        {
            _context.OnLastmileQuality(p0);
        }
    }
}