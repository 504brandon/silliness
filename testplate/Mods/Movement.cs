using BepInEx;
using ExitGames.Client.Photon;
using GorillaTag.GuidedRefs;
using Photon.Pun;
using Photon.Realtime;
using PlayFab.ClientModels;
using SillyMenu.Classes;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using static SillyMenu.Classes.RigManager;
using static SillyMenu.Menu.Main;
using static SillyMenu.Menu.Settings;
using static UnityEngine.Object;
using static UnityEngine.UI.CanvasScaler;

namespace SillyMenu.Mods
{
    internal class Movement
    {
        public static GameObject LeftPlatform;
        public static GameObject RightPlatform;

        public static void Platforms()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                if (LeftPlatform == null)
                {
                    LeftPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    LeftPlatform.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    LeftPlatform.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                    LeftPlatform.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;

                    ColorChanger colorChanger = LeftPlatform.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = secondaryButtonColors[0];
                    colorChanger.Start();
                }
            } else
            {
                if (LeftPlatform != null)
                {
                    UnityEngine.Object.Destroy(LeftPlatform);
                }
            }
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (RightPlatform == null)
                {
                    RightPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    RightPlatform.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    RightPlatform.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    RightPlatform.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;

                    ColorChanger colorChanger = RightPlatform.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = secondaryButtonColors[0];
                    colorChanger.Start();
                }
            }
            else
            {
                if (RightPlatform != null)
                {
                    UnityEngine.Object.Destroy(RightPlatform);
                }
            }
        }
        public static void Speedboost()
        {
            GorillaLocomotion.Player.Instance.jumpMultiplier = 15;
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 15;
        }
        public static void ZeroGravity()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity -= (Vector3.down * 9.81f) * Time.deltaTime;
            }
        }
        public static void SlipperyHands()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                EverythingSlippery = true;
            } 
            else
            {
                EverythingSlippery = false;
            } 
        }
        public static void GrippyHands()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                EverythingGrippy = true;
            }
            else
            {
                EverythingGrippy = false;
            }

        }
        public static void RigGun()
        {
            RaycastHit PointerPos;
            GameObject Pointer;
            GameObject line = new GameObject("Line");
            LineRenderer PointerLine = line.AddComponent<LineRenderer>();
            Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position, GorillaLocomotion.Player.Instance.rightControllerTransform.forward, out PointerPos);
            if (ControllerInputPoller.instance.rightGrab)
            {
                Pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Pointer.transform.position = PointerPos.point;
                Pointer.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                GameObject.Destroy(Pointer.GetComponent<Collider>());
                GameObject.Destroy(Pointer.GetComponent<Rigidbody>());
                Pointer.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                GameObject.Destroy(Pointer, Time.deltaTime);

                ColorChanger colorChanger = Pointer.AddComponent<ColorChanger>();
                colorChanger.colorInfo = secondaryButtonColors[0];
                colorChanger.Start();

                PointerLine.startWidth = 0.025f; PointerLine.endWidth = 0.025f; PointerLine.positionCount = 2; PointerLine.useWorldSpace = true;
                PointerLine.SetPosition(0, GorillaLocomotion.Player.Instance.rightControllerTransform.position);
                PointerLine.SetPosition(1, Pointer.transform.localPosition);
                PointerLine.material.shader = Shader.Find("GUI/Text Shader");
                ColorChanger pointercolor = PointerLine.AddComponent<ColorChanger>();
                pointercolor.colorInfo = secondaryButtonColors[0];
                pointercolor.Start();
                UnityEngine.Object.Destroy(line, Time.deltaTime);

                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = Pointer.transform.position - new Vector3(-0.1f, -1f, 0f);
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void GrabRig()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                GorillaTagger.Instance.offlineVRRig.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
    }
}
