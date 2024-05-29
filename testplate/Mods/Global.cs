using SillyMenu.Menu;
using System.IO;
using System.Net;
using UnityEngine;
using static SillyMenu.Menu.Main;

namespace SillyMenu.Mods
{
    internal class Global
    {
        public static void ReturnHome()
        {
            buttonsType = 0;
        }

        public static void MovementMods()
        {
            buttonsType = 5;
        }
        public static void MiscellaneousMods()
        {
            buttonsType = 6;
        }
        public static void RoomMods()
        {
            buttonsType = 7;
        }
        public static Texture2D LoadTextureFromURL(string resourcePath, string fileName)
        {
            Texture2D texture = new Texture2D(2, 2);

            if (!Directory.Exists("SillyMenu"))
            {
                Directory.CreateDirectory("SillyMenu");
            }
            if (!File.Exists("SillyMenu/" + fileName))
            {
                UnityEngine.Debug.Log("Downloading " + fileName);
                WebClient stream = new WebClient();
                stream.DownloadFile(resourcePath, "SillyMenu/" + fileName);
            }

            byte[] bytes = File.ReadAllBytes("SillyMenu/" + fileName);
            texture.LoadImage(bytes);

            return texture;
        }
        public static void incrementing()
        {
            themeNumber++;
            if (themeNumber > 7)
            {
                themeNumber = 1;
            }
            Main.ChangeTheme();
        }
    }
}