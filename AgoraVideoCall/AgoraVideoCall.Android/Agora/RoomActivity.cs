using AgoraVideoCall.Agora;
using AgoraVideoCall.Agora.Helpers;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using DT.Xamarin.Agora;
using DT.Xamarin.Agora.Video;
using Google.Android.Material.Snackbar;
using System.Linq;
using System.Threading.Tasks;

namespace AgoraVideoCall.Droid.Agora
{
    [Activity(Label = "VideoChat")]
    public class RoomActivity : Activity
    {

        #region JoinActivity Properties

        protected const int REQUEST_ID = 0;
        protected string[] REQUEST_PERMISSIONS = new string[] {
            Manifest.Permission.Camera,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.RecordAudio,
            Manifest.Permission.ModifyAudioSettings,
            Manifest.Permission.Internet,
            Manifest.Permission.AccessNetworkState
        };

        protected RtcEngine AgoraEngine;
        protected AgoraQualityHandler AgoraqualityHandler;
        protected const string QualityFormat = "Current Connection - {0}";
        protected const string VersionFormat = " {0}";
        private View _layout;

        #endregion

        #region RoomActivity Properties

        protected const int MaxLocalVideoDimension = 150;
        //protected RtcEngine AgoraEngine;
        protected AgoraRtcHandler AgoraHandler;
        private bool _isVideoEnabled = true;
        private SurfaceView _localVideoView;
        private uint _localId = 0;
        private uint _remoteId = 0;
        private ProgressBar _progressBar;

        #endregion

        #region RoomActivity Methods
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //SetContentView(Resource.Layout.Join);
            //_layout = FindViewById(Resource.Id.joinLayout);
            CheckPermissions();
            //FindViewById<EditText>(Resource.Id.channelName).Text = AgoraSettings.Current.RoomName;
            //FindViewById<EditText>(Resource.Id.encryptionKey).Text = AgoraSettings.Current.EncryptionPhrase;
            AgoraqualityHandler = new AgoraQualityHandler(this);
            AgoraEngine = RtcEngine.Create(BaseContext, AgoraTestConstants.AgoraAPI, AgoraqualityHandler);
            AgoraEngine.EnableWebSdkInteroperability(true);
            AgoraEngine.EnableLastmileTest();
            //FindViewById<TextView>(Resource.Id.agora_version_text).Text = string.Format(VersionFormat, RtcEngine.SdkVersion);


            //RoomActivity
            //base.OnCreate(savedInstanceState);
            //SetContentView(Resource.Layout.Room);
            //_progressBar = FindViewById<ProgressBar>(Resource.Id.progress_bar);
            InitAgoraEngineAndJoinChannel();
            //FindViewById<TextView>(Resource.Id.room_name).Text = AgoraSettings.Current.RoomName;
        }

        protected async Task<bool> CheckPermissions(bool requestPermissions = true)
        {
            var isGranted = REQUEST_PERMISSIONS.Select(permission => ContextCompat.CheckSelfPermission(this, permission) == (int)Permission.Granted).All(granted => granted);
            if (requestPermissions && !isGranted)
            {
                ActivityCompat.RequestPermissions(this, REQUEST_PERMISSIONS, REQUEST_ID);
            }
            return isGranted;
        }
        public void OnFirstRemoteVideoDecoded(int uid, int width, int height, int elapsed)
        {
            RunOnUiThread(() =>
            {
                SetupRemoteVideo(uid);
            });
        }

        public void OnUserOffline(int uid, int reason)
        {
            RunOnUiThread(() =>
            {
                OnRemoteUserLeft();
            });
        }

        public void OnUserMuteVideo(int uid, bool muted)
        {
            RunOnUiThread(() =>
            {
                OnRemoteUserVideoMuted(uid, muted);
            });
        }

        public void OnJoinChannelSuccess(string channel, int uid, int elapsed)
        {
            RunOnUiThread(() => _progressBar.Visibility = ViewStates.Gone);
            _localId = (uint)uid;
            RefreshDebug();
        }

        public void RefreshDebug()
        {
            RunOnUiThread(() =>
            {
                //FindViewById<TextView>(Resource.Id.debug_data).Text = $"local: {_localId}\nremote: {_remoteId}";
            });
        }

        public void OnFirstLocalVideoFrame(float height, float width, int p2)
        {
            var ratio = height / width;
            var ratioHeight = ratio * MaxLocalVideoDimension;
            var ratioWidth = MaxLocalVideoDimension / ratio;
            var containerHeight = height > width ? MaxLocalVideoDimension : ratioHeight;
            var containerWidth = height > width ? ratioWidth : MaxLocalVideoDimension;
            RunOnUiThread(() =>
            {
                //var videoContainer = FindViewById<RelativeLayout>(Resource.Id.local_video_container);
                //var parameters = videoContainer.LayoutParameters;
                //parameters.Height = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, containerHeight, Resources.DisplayMetrics);
                //parameters.Width = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, containerWidth, Resources.DisplayMetrics);
                //videoContainer.LayoutParameters = parameters;
                //FindViewById(Resource.Id.local_video_container).Visibility = _isVideoEnabled ? ViewStates.Visible : ViewStates.Invisible;
            });
        }

        private void InitAgoraEngineAndJoinChannel()
        {
            InitializeAgoraEngine();
            SetupVideoProfile();
            SetupLocalVideo();
            JoinChannel();
        }

        //protected override void OnDestroy()
        //{
        //    base.OnDestroy();
        //}

        [Java.Interop.Export("OnLocalVideoMuteClicked")]
        public void OnLocalVideoMuteClicked(bool bSelected)   //(View view)
        {
            //ImageView iv = (ImageView)view;
            //if (iv.Selected)
            //{
            //    iv.Selected = false;
            //    iv.SetImageResource(Resource.Drawable.ic_cam_active_call);
            //}
            //else
            //{
            //    iv.Selected = true;
            //    iv.SetImageResource(Resource.Drawable.ic_cam_disabled_call);
            //}
            AgoraEngine.MuteLocalVideoStream(bSelected);    // (iv.Selected);
            //_isVideoEnabled = !iv.Selected;
            //FindViewById(Resource.Id.local_video_container).Visibility = _isVideoEnabled ? ViewStates.Visible : ViewStates.Gone;
            //_localVideoView.Visibility = _isVideoEnabled ? ViewStates.Visible : ViewStates.Gone;
        }

        [Java.Interop.Export("OnLocalAudioMuteClicked")]
        public void OnLocalAudioMuteClicked(bool bSelected)   //(View view)
        {
            //ImageView iv = (ImageView)view;
            //if (iv.Selected)
            //{
            //    iv.Selected = false;
            //    iv.SetImageResource(Resource.Drawable.ic_mic_active_call);
            //}
            //else
            //{
            //    iv.Selected = true;
            //    iv.SetImageResource(Resource.Drawable.ic_mic_inactive_call);
            //}
            AgoraEngine.MuteLocalAudioStream(bSelected);
            //var visibleMutedLayers = iv.Selected ? ViewStates.Visible : ViewStates.Invisible;
            //FindViewById(Resource.Id.local_video_overlay).Visibility = visibleMutedLayers;
            //FindViewById(Resource.Id.local_video_muted).Visibility = visibleMutedLayers;
        }

        [Java.Interop.Export("OnSwitchCameraClicked")]
        public void OnSwitchCameraClicked(View view)
        {
            AgoraEngine.SwitchCamera();
        }

        [Java.Interop.Export("OnEncCallClicked")]
        public void OnEncCallClicked(View view)
        {
            AgoraEngine.StopPreview();
            AgoraEngine.SetupLocalVideo(null);
            AgoraEngine.LeaveChannel();
            AgoraEngine.Dispose();
            AgoraEngine = null;
            Finish();
        }

        public async Task OnTokenPrivilegeWillExpire(string token)
        {
            var newToken = await AgoraTokenService.GetRtcToken(AgoraSettings.Current.RoomName);
            if (!string.IsNullOrEmpty(token))
            {
                AgoraEngine.RenewToken(newToken);
            }
        }

        private void InitializeAgoraEngine()
        {
            AgoraHandler = new AgoraRtcHandler(this);
            AgoraEngine = RtcEngine.Create(BaseContext, AgoraTestConstants.AgoraAPI, AgoraHandler);
        }

        private void SetupVideoProfile()
        {
            AgoraEngine.EnableVideo();
            AgoraEngine.SetVideoProfile(AgoraSettings.Current.UseMySettings ? AgoraSettings.Current.Profile : Constants.VideoProfile360p, false);
        }

        private void SetupLocalVideo()
        {
            //FrameLayout container = (FrameLayout)FindViewById(Resource.Id.local_video_view_container);
            //_localVideoView = RtcEngine.CreateRendererView(BaseContext);
            //_localVideoView.SetZOrderMediaOverlay(true);
            //container.AddView(_localVideoView);
            AgoraEngine.SetupLocalVideo(new VideoCanvas(_localVideoView, VideoCanvas.RenderModeAdaptive, 0));
            if (!string.IsNullOrEmpty(AgoraSettings.Current.EncryptionPhrase))
            {
                AgoraEngine.SetEncryptionMode(AgoraSettings.Current.EncryptionType.GetModeString());
                AgoraEngine.SetEncryptionSecret(AgoraSettings.Current.EncryptionPhrase);
            }
            AgoraEngine.StartPreview();
        }

        private async Task JoinChannel()
        {
            //_progressBar.Visibility = ViewStates.Visible;
            var token = await AgoraTokenService.GetRtcToken(AgoraSettings.Current.RoomName);
            //if (string.IsNullOrEmpty(token))
            //{
            //    _progressBar.Visibility = ViewStates.Gone;
            //}
            //else
            //{
                AgoraEngine.JoinChannel(token, AgoraSettings.Current.RoomName, string.Empty, 0); // if you do not specify the uid, we will generate the uid for you
            //}
        }

        private void SetupRemoteVideo(int uid)
        {
            //_remoteId = (uint)uid;
            //FrameLayout container = (FrameLayout)FindViewById(Resource.Id.remote_video_view_container);
            //if (container.ChildCount >= 1)
            //{
            //    return;
            //}
            //SurfaceView surfaceView = RtcEngine.CreateRendererView(BaseContext);
            //container.AddView(surfaceView);
            //AgoraEngine.SetupRemoteVideo(new VideoCanvas(surfaceView, VideoCanvas.RenderModeAdaptive, uid));
            //surfaceView.Tag = uid; // for mark purpose
            //RefreshDebug();
        }

        private void LeaveChannel()
        {
            AgoraEngine.LeaveChannel();
        }

        private void OnRemoteUserLeft()
        {
            //FrameLayout container = (FrameLayout)FindViewById(Resource.Id.clremote_video_view_container);
            //container.RemoveAllViews();
        }

        private void OnRemoteUserVideoMuted(int uid, bool muted)
        {
            //FrameLayout container = (FrameLayout)FindViewById(Resource.Id.remote_video_view_container);
            //SurfaceView surfaceView = (SurfaceView)container.GetChildAt(0);
            //if (surfaceView == null)
            //    return;
            //var tag = surfaceView.Tag;
            //if (tag != null && (int)tag == uid)
            //{
            //    surfaceView.Visibility = muted ? ViewStates.Gone : ViewStates.Visible;
            //}
        }

        #endregion

        #region JoinRoomActivity Methods

        //[Java.Interop.Export("OnJoin")]
        public void OnJoin()//(View v)
        {
            AgoraSettings.Current.RoomName = "drmtm.us"; // FindViewById<EditText>(Resource.Id.channelName).Text;
            AgoraSettings.Current.EncryptionPhrase = "";    // FindViewById<EditText>(Resource.Id.encryptionKey).Text;
            CheckPermissionsAndStartCall();
        }

        private async Task CheckPermissionsAndStartCall()
        {
            if (await CheckPermissions(false))
            {
                StartActivity(typeof(RoomActivity));
            }
            else
            {
               // Snackbar.Make(_layout, Resource.String.permissions_not_granted, Snackbar.LengthShort).Show();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //MenuInflater.Inflate(Resource.Menu.top_menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            //switch (item.ItemId)
            //{
            //    case Resource.Id.menu_settings:
            //        StartActivity(typeof(SettingsActivity));
            //        break;
            //    case Resource.Id.menu_share:
            //        ShareActivity();
            //        break;
            //}
            return true;
        }

        private void ShareActivity()
        {
            Intent sendIntent = new Intent();
            sendIntent.SetAction(Intent.ActionSend);
            sendIntent.PutExtra(Intent.ExtraText, AgoraTestConstants.ShareString);
            sendIntent.SetType("text/plain");
            StartActivity(sendIntent);
        }

        internal void OnLastmileQuality(int p0)
        {
            RunOnUiThread(() =>
            {
                //var textQuality = FindViewById<TextView>(Resource.Id.quality_text);
                //var imageQuality = FindViewById<ImageView>(Resource.Id.quality_image);
                //string quality = string.Empty;
                //switch (p0)
                //{
                //    case Constants.QualityExcellent:
                //        quality = "Excellent";
                //        imageQuality.SetImageResource(Resource.Drawable.ic_connection_5);
                //        break;
                //    case Constants.QualityGood:
                //        quality = "Good";
                //        imageQuality.SetImageResource(Resource.Drawable.ic_connection_4);
                //        break;
                //    case Constants.QualityPoor:
                //        quality = "Poor";
                //        imageQuality.SetImageResource(Resource.Drawable.ic_connection_3);
                //        break;
                //    case Constants.QualityBad:
                //        quality = "Bad";
                //        imageQuality.SetImageResource(Resource.Drawable.ic_connection_2);
                //        break;
                //    case Constants.QualityVbad:
                //        quality = "Very Bad";
                //        imageQuality.SetImageResource(Resource.Drawable.ic_connection_1);
                //        break;
                //    case Constants.QualityDown:
                //        quality = "Down";
                //        imageQuality.SetImageResource(Resource.Drawable.ic_connection_0);
                //        break;
                //    default:
                //        quality = "Unknown";
                //        imageQuality.SetImageResource(Resource.Drawable.ic_connection_0);
                //        break;
                //}
                //textQuality.Text = string.Format(QualityFormat, quality);
            });
        }

        protected override void OnDestroy()
        {
            if (AgoraHandler != null)
            {
                AgoraHandler.Dispose();
                AgoraHandler = null;
            }
            if (AgoraEngine != null)
            {
                AgoraEngine.Dispose();
                AgoraEngine = null;
            }
            base.OnDestroy();
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (CurrentFocus != null)
            {
                InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);
            }
            return base.OnTouchEvent(e);
        }

        #endregion

    }
}