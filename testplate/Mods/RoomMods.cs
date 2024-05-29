using BepInEx;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using static SillyMenu.Classes.RigManager;
using static SillyMenu.Menu.Main;
using static SillyMenu.Menu.Settings;
using static UnityEngine.Object;

namespace SillyMenu.Mods
{
    internal class RoomMods
    {
        public static void DisableRoomJoining()
        {
            GameObject joinroomtriggers = GameObject.Find("JoinRoomTriggers_Prefab");
            joinroomtriggers.SetActive(false);
        }
        public static void EnableRoomJoining()
        {
            GameObject joinroomtriggers = GameObject.Find("JoinRoomTriggers_Prefab");
            joinroomtriggers.SetActive(true);
        }
    }
}
