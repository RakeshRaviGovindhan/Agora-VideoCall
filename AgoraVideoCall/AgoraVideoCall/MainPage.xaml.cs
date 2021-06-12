using AgoraVideoCall.Agora.Helpers;
using AgoraVideoCall.Agora.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AgoraVideoCall
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnJoin(object sender, EventArgs e)
        {
            DependencyService.Get<IVideoHandler>().OnJoin();
            Navigation.PushAsync(new VideoChat());
        }
    }
}
