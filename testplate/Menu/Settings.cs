using SillyMenu.Classes;
using UnityEngine;
using static SillyMenu.Menu.Main;

namespace SillyMenu.Menu
{
    internal class Settings
    {
        public static ExtGradient backgroundColor = new ExtGradient { colors = GetSolidGradient(new Color(0.996f, 0.486f, 0.890f)) };
        public static ExtGradient[] buttonColors = new ExtGradient[]
        {
            new ExtGradient{colors = GetSolidGradient(new Color(0.996f, 0.486f, 0.890f)) }, // Disabled1f, 0.408f, 0.882f
            new ExtGradient{colors = GetSolidGradient(Color.black) } // Enabled
        };
        public static Color[] textColors = new Color[]
        {
            Color.white, // Disabled
            Color.white // Enabled
        };
        public static ExtGradient[] secondaryButtonColors = new ExtGradient[]
        {
            new ExtGradient{colors = GetSolidGradient(new Color(1f, 0.408f, 0.882f)) }, // Disabled1f, 0.408f, 0.882f
            new ExtGradient{colors = GetSolidGradient(Color.black) } // Enabled
        };
        public static ExtGradient[] thirdButtonColors = new ExtGradient[]
        {
            new ExtGradient{colors = GetSolidGradient(new Color(1f, 0.408f, 0.882f)) }, // Disabled1f, 0.408f, 0.882f
            new ExtGradient{colors = GetSolidGradient(Color.black) } // Enabled
        };

        public static Font currentFont = Font.CreateDynamicFontFromOSFont("Comic Sans MS", 24);

        public static bool fpsCounter = true;
        public static bool disconnectButton = true;
        public static bool rightHanded = false;
        public static bool femboyImage = false;
        public static bool disableNotifications = false;

        public static KeyCode keyboardButton = KeyCode.Q;

        public static Vector3 menuSize = new Vector3(0.1f, 1f, 1f); // Depth, Width, Height
        public static int buttonsPerPage = 8;
    }
}
