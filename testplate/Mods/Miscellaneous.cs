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
    internal class Miscellaneous
    {
        public static void BringGliders()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                foreach (GliderHoldable glider in UnityEngine.GameObject.FindObjectsOfType<GliderHoldable>())
                {
                    glider.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                }
            }
        }
        public static void PinkSnowballs()
        {
            foreach (SnowballThrowable snowball in UnityEngine.GameObject.FindObjectsOfType<SnowballThrowable>())
            {
                snowball.randomizeColor = true;
                GorillaTagger.Instance.offlineVRRig.SetThrowableProjectileColor(false, new Color32(254, 124, 227, 255));
                GorillaTagger.Instance.offlineVRRig.SetThrowableProjectileColor(true, new Color32(254, 124, 227, 255));
            }
        }
        public static bool lastthing = false;
        public static void materializewaterballoon()
        {
            bool thing = ControllerInputPoller.instance.rightGrab;
            if (thing && !lastthing)
            {
                GameObject lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(lhelp, 0.1f);
                lhelp.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                lhelp.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                lhelp.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 204;
                lhelp.GetComponent<Renderer>().enabled = false;
            }
            lastthing = thing;
        }
        public static void materializerockballoon()
        {
            bool thing = ControllerInputPoller.instance.rightGrab;
            if (thing && !lastthing)
            {
                GameObject lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(lhelp, 0.1f);
                lhelp.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                lhelp.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                lhelp.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 231;
                lhelp.GetComponent<Renderer>().enabled = false;
            }
            lastthing = thing;
        }
        public static void materializegishfoodballoon()
        {
            bool thing = ControllerInputPoller.instance.rightGrab;
            if (thing && !lastthing)
            {
                GameObject lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(lhelp, 0.1f);
                lhelp.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                lhelp.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                lhelp.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 252;
                lhelp.GetComponent<Renderer>().enabled = false;
            }
            lastthing = thing;
        }
        public static void materializepresentballoon()
        {
            bool thing = ControllerInputPoller.instance.rightGrab;
            if (thing && !lastthing)
            {
                GameObject lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(lhelp, 0.1f);
                lhelp.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                lhelp.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                lhelp.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 240;
                lhelp.GetComponent<Renderer>().enabled = false;
            }
            lastthing = thing;
        }
        public static void HeadSpinXAxis()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                VRMap head = GorillaTagger.Instance.offlineVRRig.head;
                head.trackingRotationOffset.x = head.trackingRotationOffset.x + 10f;
            }
            else
            {
                VRMap head = GorillaTagger.Instance.offlineVRRig.head;
                head.trackingRotationOffset.x = 0f;
            }
        }
        public static void HeadSpinYAxis()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                VRMap head = GorillaTagger.Instance.offlineVRRig.head;
                head.trackingRotationOffset.y = head.trackingRotationOffset.y + 10f;
            }
            else
            {
                VRMap head = GorillaTagger.Instance.offlineVRRig.head;
                head.trackingRotationOffset.y = 0f;
            }
        }
        public static void HeadSpinZAxis()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                VRMap head = GorillaTagger.Instance.offlineVRRig.head;
                head.trackingRotationOffset.z = head.trackingRotationOffset.z + 10f;
            }
            else
            {
                VRMap head = GorillaTagger.Instance.offlineVRRig.head;
                head.trackingRotationOffset.z = 0f;
            }
        }
    }
}
