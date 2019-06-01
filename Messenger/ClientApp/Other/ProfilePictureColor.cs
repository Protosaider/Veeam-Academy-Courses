using System;

namespace ClientApp.Other
{
	internal enum EProfilePictureColor
    {
        DarkBlue,
        Green,
        Red,
        Purple,
    }

	internal static class SProfilePictureColorExtension
    {
        private static readonly Int32 s_profilePictureColorCount = Enum.GetNames(typeof(EProfilePictureColor)).Length;
        public static EProfilePictureColor FromLogin(this String login)
        {
            return (EProfilePictureColor)(login.GetHashCode() % s_profilePictureColorCount);
        }

        public static String ToColorCode(this EProfilePictureColor color)
        {
            switch (color)
            {                   
                case EProfilePictureColor.DarkBlue:
                    return "0c6991";

                case EProfilePictureColor.Green:
                    return "00c541";

                case EProfilePictureColor.Red:
                    return "c54141";

                case EProfilePictureColor.Purple:
                    return "c541c5";

                // If none found, return null
                default:
                    return null;
            }
        }
    }
}