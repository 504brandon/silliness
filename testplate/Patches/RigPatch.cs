﻿using HarmonyLib;
using UnityEngine;

namespace SillyMenu.Patches
{
    [HarmonyPatch(typeof(VRRig), "OnDisable")]
    internal class GhostPatch : MonoBehaviour
    {
        public static bool Prefix(VRRig __instance)
        {
            return !(__instance == GorillaTagger.Instance.offlineVRRig);
        }
    }
}
