﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AgoraVideoCall.Agora.Helpers
{
    public class BasePropertyChanged : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;
            changed(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
