using System;
using System.Collections.Generic;
using System.Text;

namespace AgoraVideoCall.Agora
{
    public static class MediaCharacter
    {
        public static string LegalCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#$%&()+,-:;<=.>?@[]^_`{|}~";

        public static string UpdateToLegalMediaString(string value)
        {
            string result = string.Empty;
            foreach (var character in value)
            {
                if (LegalCharacters.IndexOf(character) >= 0)
                {
                    result = $"{result}{character}";
                }
            }
            return result;
        }
    }

}
