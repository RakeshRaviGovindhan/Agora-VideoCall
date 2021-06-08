using System;
using System.Collections.Generic;
using System.Text;

namespace AgoraVideoCall.Agora.ViewModels
{
    public class VideoChatViewModel : BaseViewModel
    {

        #region Properties

        Double footerHeight { get; set; }
        public Double FooterHeight
        {
            get => footerHeight;
            set
            {
                if (value == footerHeight)
                    return;

                footerHeight = value;
                OnPropertyChanged();
            }
        }

        Double containerHeight { get; set; }
        public Double ContainerHeight
        {
            get => containerHeight;
            set
            {
                if (value == containerHeight)
                    return;

                containerHeight = value;
                OnPropertyChanged();
            }
        }

        #endregion

    }
}
