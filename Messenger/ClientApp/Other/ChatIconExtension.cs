using System;

namespace ClientApp.Other
{
	internal static class SChatIconExtension
    {
        public static String ToFontAwesome(this EChatType type)
        {
            // Return a FontAwesome string based on the icon type
            switch (type)
            {
                case EChatType.Common:
                    return "\uf007";

                case EChatType.Group:
                    return "\uf0c0";

                case EChatType.Protected:
                    return "\uf023";

                // If none found, return null
                default:
                    return null;
            }
        }
    }
}
