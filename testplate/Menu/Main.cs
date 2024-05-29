using BepInEx;
using HarmonyLib;
using SillyMenu.Notifications;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static SillyMenu.Menu.Buttons;
using static SillyMenu.Menu.Settings;
using SillyMenu.Classes;
using SillyMenu.Mods;
using SillyMenu;
using OVR.OpenVR;
using System.IO;
using System.Net;
using PlayFab.ClientModels;
using UnityEngine.UIElements;

namespace SillyMenu.Menu
{
    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("LateUpdate", MethodType.Normal)]
    public class Main : MonoBehaviour
    {
        // Constant
        public static int themeType = 1;
        public static void Prefix()
        {
            // Initialize Menu
            try
            {
                bool toOpen = !rightHanded && ControllerInputPoller.instance.leftControllerSecondaryButton || rightHanded && ControllerInputPoller.instance.rightControllerSecondaryButton;
                bool keyboardOpen = UnityInput.Current.GetKey(keyboardButton);

                if (menu == null)
                {
                    if (toOpen || keyboardOpen)
                    {
                        CreateMenu();
                        RecenterMenu(rightHanded, keyboardOpen);
                        if (reference == null)
                        {
                            CreateReference(rightHanded);
                        }
                    }
                }
                else
                {
                    if (toOpen || keyboardOpen)
                    {
                        RecenterMenu(rightHanded, keyboardOpen);
                    }
                    else
                    {
                        Rigidbody comp = menu.AddComponent(typeof(Rigidbody)) as Rigidbody;
                        if (rightHanded)
                        {
                            comp.velocity = GorillaLocomotion.Player.Instance.rightHandCenterVelocityTracker.GetAverageVelocity(true, 0);
                        }
                        else
                        {
                            comp.velocity = GorillaLocomotion.Player.Instance.leftHandCenterVelocityTracker.GetAverageVelocity(true, 0);
                        }

                        Destroy(menu, 2);
                        menu = null;

                        Destroy(reference);
                        reference = null;
                    }
                }
            }
            catch (Exception exc)
            {
                Debug.LogError(string.Format("{0} // Error initializing at {1}: {2}", PluginInfo.Name, exc.StackTrace, exc.Message));
            }

            // Constant
            try
            {
                // Pre-Execution
                if (fpsObject != null)
                {
                    fpsObject.text = Mathf.Ceil(1f / Time.unscaledDeltaTime).ToString();
                }

                // Execute Enabled mods
                foreach (ButtonInfo[] buttonlist in buttons)
                {
                    foreach (ButtonInfo v in buttonlist)
                    {
                        if (v.enabled)
                        {
                            if (v.method != null)
                            {
                                try
                                {
                                    v.method.Invoke();
                                }
                                catch (Exception exc)
                                {
                                    Debug.LogError(string.Format("{0} // Error with mod {1} at {2}: {3}", PluginInfo.Name, v.buttonText, exc.StackTrace, exc.Message));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Debug.LogError(string.Format("{0} // Error with executing mods at {1}: {2}", PluginInfo.Name, exc.StackTrace, exc.Message));
            }
        }

        // Functions
        public static Texture2D LoadTextureFromURL(string resourcePath, string fileName)
        {
            Texture2D texture = new Texture2D(2, 2);

            if (!Directory.Exists("adlibsreal"))
            {
                Directory.CreateDirectory("adlibsreal");
            }
            if (!File.Exists("adlibsreal/" + fileName))
            {
                UnityEngine.Debug.Log("Downloading " + fileName);
                WebClient stream = new WebClient();
                stream.DownloadFile(resourcePath, "adlibsreal/" + fileName);
            }

            byte[] bytes = File.ReadAllBytes("adlibsreal/" + fileName);
            texture.LoadImage(bytes);

            return texture;
        }
        public static int themeNumber = 1;
        public static int imageNumber = 1;
        public static void ChangeTheme()
        {
            if (themeNumber == 1)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [pink]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(new Color(0.996f, 0.486f, 0.890f)) };
                buttonColors = new ExtGradient[]
                {

                new ExtGradient{colors = GetSolidGradient(new Color(0.996f, 0.486f, 0.890f)) }, // Disabled
                new ExtGradient{colors = GetSolidGradient(Color.black) } // Enabled

                };
                secondaryButtonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(1f, 0.408f, 0.882f)) }, // Disabled1f, 0.408f, 0.882f
                    new ExtGradient{colors = GetSolidGradient(Color.black) } // Enabled
                };
                thirdButtonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(1f, 0.408f, 0.882f)) }, // Disabled1f, 0.408f, 0.882f
                    new ExtGradient{colors = GetSolidGradient(Color.black) } // Enabled
                };

                textColors = new Color[]
                {
                    Color.white, // Disabled
                    Color.white // Enabled
                };
            }
            if (themeNumber == 2)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [dark]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(Color.black) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(0.15f, 0.15f, 0.15f)) }, // Disabled
                    new ExtGradient{colors = GetSolidGradient(Color.black) }, // Enabled
                };
                secondaryButtonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(0.2f, 0.2f, 0.2f)) }, // Disabled1f, 0.408f, 0.882f
                    new ExtGradient{colors = GetSolidGradient(Color.black) } // Enabled
                };
                thirdButtonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(0.2f, 0.2f, 0.2f)) }, // Disabled1f, 0.408f, 0.882f
                    new ExtGradient{colors = GetSolidGradient(Color.black) } // Enabled
                };
                textColors = new Color[]
                {
                    Color.white, // Disabled
                    Color.white // Enabled
                };
            }
            if (themeNumber == 3)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [blue]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(new Color(0.357f, 0.349f, 1f)) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(0.357f, 0.349f, 1f)) }, // Disabled0.278f, 0.271f, 1f
                    new ExtGradient{colors = GetSolidGradient(Color.black) }, // Enabled
                };
                secondaryButtonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(0.278f, 0.271f, 1f)) }, // Disabled1f, 0.408f, 0.882f
                    new ExtGradient{colors = GetSolidGradient(Color.black) } // Enabled
                };
                thirdButtonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(0.278f, 0.271f, 1f)) }, // Disabled1f, 0.408f, 0.882f
                    new ExtGradient{colors = GetSolidGradient(Color.black) } // Enabled
                };
                textColors = new Color[]
                {
                    Color.white, // Disabled
                    Color.white // Enabled
                };
            }
            if (themeNumber == 4)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [orange]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(new Color(1f, 0.58f, 0.271f)) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(1f, 0.58f, 0.271f)) }, // Disabled0.278f, 0.271f, 1f
                    new ExtGradient{colors = GetSolidGradient(Color.black) }, // Enabled
                };
                secondaryButtonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(1f, 0.533f, 0.184f)) }, // Disabled1f, 0.408f, 0.882f
                    new ExtGradient{colors = GetSolidGradient(Color.black) } // Enabled
                };
                thirdButtonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(1f, 0.533f, 0.184f)) }, // Disabled1f, 0.408f, 0.882f
                    new ExtGradient{colors = GetSolidGradient(Color.black) } // Enabled
                };
                textColors = new Color[]
                {
                    Color.white, // Disabled
                    Color.white // Enabled
                };
            }
            if (themeNumber == 5)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [purple]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(new Color(0.702f, 0.357f, 1f)) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(0.702f, 0.357f, 1f)) }, // Disabled0.278f, 0.271f, 1f
                    new ExtGradient{colors = GetSolidGradient(Color.black) }, // Enabled
                };
                secondaryButtonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(0.741f, 0.443f, 1f)) }, // Disabled1f, 0.408f, 0.882f
                    new ExtGradient{colors = GetSolidGradient(Color.black) } // Enabled
                };
                thirdButtonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(0.741f, 0.443f, 1f)) }, // Disabled1f, 0.408f, 0.882f
                    new ExtGradient{colors = GetSolidGradient(Color.black) } // Enabled
                };
                textColors = new Color[]
                {
                    Color.white, // Disabled
                    Color.white // Enabled
                };
            }
            if (themeNumber == 6)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [red]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(new Color(1f, 0.271f, 0.271f)) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(1f, 0.271f, 0.271f)) }, // Disabled0.278f, 0.271f, 1f
                    new ExtGradient{colors = GetSolidGradient(Color.black) }, // Enabled
                };
                secondaryButtonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(1f, 0.2f, 0.2f)) }, // Disabled1f, 0.408f, 0.882f
                    new ExtGradient{colors = GetSolidGradient(Color.black) } // Enabled
                };
                thirdButtonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(1f, 0.2f, 0.2f)) }, // Disabled1f, 0.408f, 0.882f
                    new ExtGradient{colors = GetSolidGradient(Color.black) } // Enabled
                };
                textColors = new Color[]
                {
                    Color.white, // Disabled
                    Color.white // Enabled
                };
            }
            if (themeNumber == 7)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [gay]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(Color.white) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(Color.white) }, // Disabled0.278f, 0.271f, 1f
                    new ExtGradient{colors = GetSolidGradient(Color.black) }, // Enabled
                };
                secondaryButtonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(Color.clear) }, // Disabled1f, 0.408f, 0.882f
                    new ExtGradient{colors = GetSolidGradient(Color.clear) } // Enabled
                };
                thirdButtonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(Color.white) }, // Disabled1f, 0.408f, 0.882f
                    new ExtGradient{colors = GetSolidGradient(Color.black) } // Enabled
                };
                textColors = new Color[]
                {
                    Color.black, // Disabled
                    Color.gray // Enabled
                };
            }
        }
        public static void CreateMenu()
        {
            // Menu Holder
            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(menu.GetComponent<Rigidbody>());
            Destroy(menu.GetComponent<BoxCollider>());
            Destroy(menu.GetComponent<Renderer>());
            menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.3825f);

            // Menu Background
            menuBackground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(menuBackground.GetComponent<Rigidbody>());
            Destroy(menuBackground.GetComponent<BoxCollider>());
            menuBackground.transform.parent = menu.transform;
            menuBackground.transform.rotation = Quaternion.identity;
            menuBackground.transform.localScale = menuSize;
            menuBackground.transform.position = new Vector3(0.05f, 0f, 0f);
            menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
            menuBackground.GetComponent<Renderer>().material.color = new Color32(241, 161, 160, 255);

            ColorChanger colorChanger = menuBackground.AddComponent<ColorChanger>();
            colorChanger.colorInfo = secondaryButtonColors[0];
            colorChanger.Start();

            menuTrueBackground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(menuTrueBackground.GetComponent<Rigidbody>());
            Destroy(menuTrueBackground.GetComponent<BoxCollider>());
            menuTrueBackground.transform.parent = menu.transform;
            menuTrueBackground.transform.rotation = Quaternion.identity;
            menuTrueBackground.transform.localScale = new Vector3(0.1f, 0.95f, 0.95f);
            menuTrueBackground.transform.position = new Vector3(0.052f, 0f, 0f);
            menuTrueBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
            menuTrueBackground.GetComponent<Renderer>().material.color = new Color(0.996f, 0.486f, 0.890f);
            if (themeNumber == 7)
            {
                menuTrueBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/gayflag.png", "gayflag.png");
            }

            colorChanger = menuTrueBackground.AddComponent<ColorChanger>();
            colorChanger.colorInfo = backgroundColor;
            colorChanger.Start();

            menuPage = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(menuPage.GetComponent<Rigidbody>());
            Destroy(menuPage.GetComponent<BoxCollider>());
            menuPage.transform.parent = menu.transform;
            menuPage.transform.rotation = Quaternion.identity;
            menuPage.transform.localScale = new Vector3(0.1f, 0.9f, 0.12f);
            menuPage.transform.localPosition = new Vector3(0.55f, 0f, 0.395f);
            menuPage.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
            menuPage.GetComponent<Renderer>().material.color = new Color(1f, 0.408f, 0.882f);
            if (themeNumber == 7)
            {
                menuPage.GetComponent<Renderer>().enabled = false;
            };

            colorChanger = menuPage.AddComponent<ColorChanger>();
            colorChanger.colorInfo = thirdButtonColors[0];
            colorChanger.Start();

            if (femboyImage)
            {
                menuImage = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(menuImage.GetComponent<Rigidbody>());
                Destroy(menuImage.GetComponent<BoxCollider>());
                menuImage.transform.parent = menu.transform;
                menuImage.transform.rotation = Quaternion.identity;
                menuImage.transform.localScale = new Vector3(0.01f, 0.5f, 0.42f);
                menuImage.transform.position = new Vector3(0.05f, 0.32f, 0.096f);
                menuImage.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuImage.GetComponent<Renderer>().material.color = Color.white;
                menuImage.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/FfYMUDwXkAAOj2j.jpg", "FfYMUDwXkAAOj2j.jpg");

                menuImage = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(menuImage.GetComponent<Rigidbody>());
                Destroy(menuImage.GetComponent<BoxCollider>());
                menuImage.transform.parent = menu.transform;
                menuImage.transform.rotation = new Quaternion(90f, 0f, 0f, 0f);
                menuImage.transform.localScale = new Vector3(0.01f, 0.35f, 0.4f);
                menuImage.transform.position = new Vector3(0.05f, -0.3f, 0.1f);
                menuImage.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuImage.GetComponent<Renderer>().material.color = Color.white;
                menuImage.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/8g8ZjzUgLEc.jpg", "8g8ZjzUgLEc.jpg");
            }


            // Canvas
            canvasObject = new GameObject();
            canvasObject.transform.parent = menu.transform;
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = 1000f;

            menuPage = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(menuPage.GetComponent<Rigidbody>());
            Destroy(menuPage.GetComponent<BoxCollider>());
            menuPage.transform.parent = menu.transform;
            menuPage.transform.rotation = Quaternion.identity;
            menuPage.transform.localScale = new Vector3(0.1f, 0.2f, 0.15f);
            menuPage.transform.position = new Vector3(0.055f, -0.195f, 0.235f); // 0.5f, 0f, 0.62f
            menuPage.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
            menuPage.GetComponent<Renderer>().material.color = new Color(0.996f, 0.486f, 0.890f);

            colorChanger = menuPage.AddComponent<ColorChanger>();
            colorChanger.colorInfo = buttonColors[0];
            colorChanger.Start();

            menuPage = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(menuPage.GetComponent<Rigidbody>());
            Destroy(menuPage.GetComponent<BoxCollider>());
            menuPage.transform.parent = menu.transform;
            menuPage.transform.rotation = Quaternion.identity;
            menuPage.transform.localScale = new Vector3(0.1f, 0.2f, 0.15f);
            menuPage.transform.position = new Vector3(0.055f, 0.195f, 0.235f);
            menuPage.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
            menuPage.GetComponent<Renderer>().material.color = new Color(0.996f, 0.486f, 0.890f);

            colorChanger = menuPage.AddComponent<ColorChanger>();
            colorChanger.colorInfo = buttonColors[0];
            colorChanger.Start();

            // Title and FPS
            Text text = new GameObject
            {
                transform =
                    {
                        parent = canvasObject.transform
                    }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = " <color=grey>[</color><color=white>" + (pageNumber + 1).ToString() + "</color><color=grey>]</color>";
            text.fontSize = 1;
            text.color = textColors[0];
            text.supportRichText = true;
            text.fontStyle = FontStyle.Italic;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.28f, 0.05f);
            component.position = new Vector3(0.065f, -0.185f, 0.245f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            if (themeNumber == 7)
            {
                text.text = " <color=grey>[</color><color=black>" + (pageNumber + 1).ToString() + "</color><color=grey>]</color>";
            };

            Text discontext2 = new GameObject
            {
                transform =
                            {
                                parent = canvasObject.transform
                            }
            }.AddComponent<Text>();
            discontext2.text = PluginInfo.Name;
            discontext2.font = currentFont;
            discontext2.fontSize = 1;
            discontext2.color = textColors[0];
            discontext2.alignment = TextAnchor.MiddleCenter;
            discontext2.resizeTextForBestFit = true;
            discontext2.resizeTextMinSize = 0;

            RectTransform rectt = discontext2.GetComponent<RectTransform>();
            rectt.localPosition = Vector3.zero;
            rectt.sizeDelta = new Vector2(0.28f, 0.05f);
            rectt.localPosition = new Vector3(0.065f, 0f, 0.16f);
            rectt.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            if (fpsCounter)
            {
                fpsObject = new GameObject
                {
                    transform =
                    {
                        parent = canvasObject.transform
                    }
                }.AddComponent<Text>();
                fpsObject.font = currentFont;
                fpsObject.text = Mathf.Ceil(1f / Time.unscaledDeltaTime).ToString();
                fpsObject.color = textColors[0];
                fpsObject.fontSize = 5;
                fpsObject.supportRichText = true;
                fpsObject.fontStyle = FontStyle.Italic;
                fpsObject.alignment = TextAnchor.MiddleCenter;
                fpsObject.horizontalOverflow = HorizontalWrapMode.Overflow;
                fpsObject.resizeTextForBestFit = true;
                fpsObject.resizeTextMinSize = 0;
                RectTransform component2 = fpsObject.GetComponent<RectTransform>();
                component2.localPosition = Vector3.zero;
                component2.sizeDelta = new Vector2(0.15f, 0.05f);
                component2.position = new Vector3(0.065f, 0.2f, 0.242f);
                component2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }

            // Buttons
            // Disconnect
            if (disconnectButton)
            {
                GameObject disconnectbutton = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    disconnectbutton.layer = 2;
                }
                Destroy(disconnectbutton.GetComponent<Rigidbody>());
                disconnectbutton.GetComponent<BoxCollider>().isTrigger = true;
                disconnectbutton.transform.parent = menu.transform;
                disconnectbutton.transform.rotation = Quaternion.identity;
                disconnectbutton.transform.localScale = new Vector3(0.14f, 0.9f, 0.15f);
                disconnectbutton.transform.localPosition = new Vector3(0.5f, 0f, 0.62f);
                disconnectbutton.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
                disconnectbutton.AddComponent<Classes.Button>().relatedText = "Disconnect";

                colorChanger = disconnectbutton.AddComponent<ColorChanger>();
                colorChanger.colorInfo = buttonColors[0];
                colorChanger.Start();

                menuPage = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(menuPage.GetComponent<Rigidbody>());
                Destroy(menuPage.GetComponent<BoxCollider>());
                menuPage.transform.parent = menu.transform;
                menuPage.transform.rotation = Quaternion.identity;
                menuPage.transform.localScale = new Vector3(0.135f, 0.95f, 0.2f);
                menuPage.transform.localPosition = new Vector3(0.5f, 0f, 0.62f);
                menuPage.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuPage.GetComponent<Renderer>().material.color = new Color(1f, 0.408f, 0.882f);

                colorChanger = menuPage.AddComponent<ColorChanger>();
                colorChanger.colorInfo = secondaryButtonColors[0];
                colorChanger.Start();

                Text discontext = new GameObject
                {
                    transform =
                            {
                                parent = canvasObject.transform
                            }
                }.AddComponent<Text>();
                discontext.text = "Disconnect";
                discontext.font = currentFont;
                discontext.fontSize = 1;
                discontext.color = textColors[0];
                discontext.alignment = TextAnchor.MiddleCenter;
                discontext.resizeTextForBestFit = true;
                discontext.resizeTextMinSize = 0;

                RectTransform recttt = discontext.GetComponent<RectTransform>();
                recttt.localPosition = Vector3.zero;
                recttt.sizeDelta = new Vector2(0.2f, 0.07f);
                recttt.localPosition = new Vector3(0.064f, 0f, 0.24f);
                recttt.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }

            // Page Buttons
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject.layer = 2;
            }
            Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.089f, 0.19f, 0.95f);
            gameObject.transform.localPosition = new Vector3(0.56f, 0.65f, 0);
            gameObject.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
            gameObject.AddComponent<Classes.Button>().relatedText = "PreviousPage";

            colorChanger = gameObject.AddComponent<ColorChanger>();
            colorChanger.colorInfo = buttonColors[0];
            colorChanger.Start();

            menuPage = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(menuPage.GetComponent<Rigidbody>());
            Destroy(menuPage.GetComponent<BoxCollider>());
            menuPage.transform.parent = menu.transform;
            menuPage.transform.rotation = Quaternion.identity;
            menuPage.transform.localScale = new Vector3(0.08f, 0.25f, 1f);
            menuPage.transform.localPosition = new Vector3(0.56f, 0.65f, 0);
            menuPage.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
            menuPage.GetComponent<Renderer>().material.color = new Color(1f, 0.408f, 0.882f);

            colorChanger = menuPage.AddComponent<ColorChanger>();
            colorChanger.colorInfo = secondaryButtonColors[0];
            colorChanger.Start();

            text = new GameObject
            {
                transform =
                        {
                            parent = canvasObject.transform
                        }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = "<";
            text.fontSize = 1;
            text.color = textColors[0];
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.2f, 0.03f);
            component.localPosition = new Vector3(0.064f, 0.195f, 0f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject.layer = 2;
            }
            Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.089f, 0.19f, 0.95f);
            gameObject.transform.localPosition = new Vector3(0.56f, -0.65f, 0);
            gameObject.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
            gameObject.AddComponent<Classes.Button>().relatedText = "NextPage";

            colorChanger = gameObject.AddComponent<ColorChanger>();
            colorChanger.colorInfo = buttonColors[0];
            colorChanger.Start();

            menuPage = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(menuPage.GetComponent<Rigidbody>());
            Destroy(menuPage.GetComponent<BoxCollider>());
            menuPage.transform.parent = menu.transform;
            menuPage.transform.rotation = Quaternion.identity;
            menuPage.transform.localScale = new Vector3(0.08f, 0.25f, 1f);
            menuPage.transform.localPosition = new Vector3(0.56f, -0.65f, 0);
            menuPage.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
            menuPage.GetComponent<Renderer>().material.color = new Color(1f, 0.408f, 0.882f);

            colorChanger = menuPage.AddComponent<ColorChanger>();
            colorChanger.colorInfo = secondaryButtonColors[0];
            colorChanger.Start();

            text = new GameObject
            {
                transform =
                        {
                            parent = canvasObject.transform
                        }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = ">";
            text.fontSize = 1;
            text.color = textColors[0];
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.2f, 0.03f);
            component.localPosition = new Vector3(0.064f, -0.195f, 0f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            // Mod Buttons
            ButtonInfo[] activeButtons = buttons[buttonsType].Skip(pageNumber * buttonsPerPage).Take(buttonsPerPage).ToArray();
            for (int i = 0; i < activeButtons.Length; i++)
            {
                CreateButton(i * 0.1f, activeButtons[i]);
            }
        }

        public static void CreateButton(float offset, ButtonInfo method)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject.layer = 2;
            }
            Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.09f, 0.9f, 0.08f);
            gameObject.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - offset);
            gameObject.AddComponent<Classes.Button>().relatedText = method.buttonText;
            if (themeNumber == 7)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            };

            ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
            if (method.enabled)
            {
                colorChanger.colorInfo = thirdButtonColors[1];
            }
            else
            {
                colorChanger.colorInfo = thirdButtonColors[0];
            }
            colorChanger.Start();

            Text text = new GameObject
            {
                transform =
                {
                    parent = canvasObject.transform
                }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = method.buttonText;
            if (method.overlapText != null)
            {
                text.text = method.overlapText;
            }
            text.supportRichText = true;
            text.fontSize = 1;
            if (method.enabled)
            {
                text.color = textColors[1];
            }
            else
            {
                text.color = textColors[0];
            }
            text.alignment = TextAnchor.MiddleCenter;
            text.fontStyle = FontStyle.Italic;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(.2f, .03f);
            component.localPosition = new Vector3(.064f, 0, .111f - offset / 2.6f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static void RecreateMenu()
        {
            if (menu != null)
            {
                Destroy(menu);
                menu = null;

                CreateMenu();
                RecenterMenu(rightHanded, UnityInput.Current.GetKey(keyboardButton));
            }
        }

        public static void RecenterMenu(bool isRightHanded, bool isKeyboardCondition)
        {
            if (!isKeyboardCondition)
            {
                if (!isRightHanded)
                {
                    menu.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                    menu.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                }
                else
                {
                    menu.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    Vector3 rotation = GorillaTagger.Instance.rightHandTransform.rotation.eulerAngles;
                    rotation += new Vector3(0f, 0f, 180f);
                    menu.transform.rotation = Quaternion.Euler(rotation);
                }
            }
            else
            {
                try
                {
                    TPC = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera").GetComponent<Camera>();
                }
                catch { }
                if (TPC != null)
                {
                    TPC.transform.position = new Vector3(-999f, -999f, -999f);
                    TPC.transform.rotation = Quaternion.identity;
                    GameObject bg = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    bg.transform.localScale = new Vector3(10f, 10f, 0.01f);
                    bg.transform.transform.position = TPC.transform.position + TPC.transform.forward;
                    bg.GetComponent<Renderer>().material.color = new Color32((byte)(backgroundColor.colors[0].color.r * 50), (byte)(backgroundColor.colors[0].color.g * 50), (byte)(backgroundColor.colors[0].color.b * 50), 255);
                    Destroy(bg, Time.deltaTime);
                    menu.transform.parent = TPC.transform;
                    menu.transform.position = TPC.transform.position + Vector3.Scale(TPC.transform.forward, new Vector3(0.5f, 0.5f, 0.5f)) + Vector3.Scale(TPC.transform.up, new Vector3(-0.02f, -0.02f, -0.02f));
                    Vector3 rot = TPC.transform.rotation.eulerAngles;
                    rot = new Vector3(rot.x - 90, rot.y + 90, rot.z);
                    menu.transform.rotation = Quaternion.Euler(rot);

                    if (reference != null)
                    {
                        if (Mouse.current.leftButton.isPressed)
                        {
                            Ray ray = TPC.ScreenPointToRay(Mouse.current.position.ReadValue());
                            RaycastHit hit;
                            bool worked = Physics.Raycast(ray, out hit, 100);
                            if (worked)
                            {
                                Classes.Button collide = hit.transform.gameObject.GetComponent<Classes.Button>();
                                if (collide != null)
                                {
                                    collide.OnTriggerEnter(buttonCollider);
                                }
                            }
                        }
                        else
                        {
                            reference.transform.position = new Vector3(999f, -999f, -999f);
                        }
                    }
                }
            }
        }

        public static void CreateReference(bool isRightHanded)
        {
            reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            if (isRightHanded)
            {
                reference.transform.parent = GorillaTagger.Instance.leftHandTransform;
            }
            else
            {
                reference.transform.parent = GorillaTagger.Instance.rightHandTransform;
            }
            reference.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
            reference.transform.localPosition = new Vector3(0f, -0.1f, 0f);
            reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            buttonCollider = reference.GetComponent<SphereCollider>();

            ColorChanger colorChanger = reference.AddComponent<ColorChanger>();
            colorChanger.colorInfo = backgroundColor;
            colorChanger.Start();
        }

        public static void Toggle(string buttonText)
        {
            int lastPage = (buttons[buttonsType].Length + buttonsPerPage - 1) / buttonsPerPage - 1;
            if (buttonText == "PreviousPage")
            {
                pageNumber--;
                if (pageNumber < 0)
                {
                    pageNumber = lastPage;
                }
            }
            else
            {
                if (buttonText == "NextPage")
                {
                    pageNumber++;
                    if (pageNumber > lastPage)
                    {
                        pageNumber = 0;
                    }
                }
                else
                {
                    ButtonInfo target = GetIndex(buttonText);
                    if (target != null)
                    {
                        if (target.isTogglable)
                        {
                            target.enabled = !target.enabled;
                            if (target.enabled)
                            {
                                NotifiLib.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);
                                if (target.enableMethod != null)
                                {
                                    try { target.enableMethod.Invoke(); } catch { }
                                }
                            }
                            else
                            {
                                NotifiLib.SendNotification("<color=grey>[</color><color=red>DISABLE</color><color=grey>]</color> " + target.toolTip);
                                if (target.disableMethod != null)
                                {
                                    try { target.disableMethod.Invoke(); } catch { }
                                }
                            }
                        }
                        else
                        {
                            NotifiLib.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);
                            if (target.method != null)
                            {
                                try { target.method.Invoke(); } catch { }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError(buttonText + " does not exist");
                    }
                }
            }
            RecreateMenu();
        }

        public static GradientColorKey[] GetSolidGradient(Color color)
        {
            return new GradientColorKey[] { new GradientColorKey(color, 0f), new GradientColorKey(color, 1f) };
        }

        public static ButtonInfo GetIndex(string buttonText)
        {
            foreach (ButtonInfo[] buttons in buttons)
            {
                foreach (ButtonInfo button in buttons)
                {
                    if (button.buttonText == buttonText)
                    {
                        return button;
                    }
                }
            }

            return null;
        }

        // Variables
        // Important
        // Objects
        public static GameObject menu;
        public static GameObject menuBackground;
        public static GameObject menuImage;
        public static GameObject reference;
        public static GameObject canvasObject;
        public static GameObject menuPage;
        public static GameObject menuTrueBackground;

        public static SphereCollider buttonCollider;
        public static Camera TPC;
        public static Text fpsObject;
        public static Text menuName;
        public static bool EverythingSlippery = false;
        public static bool EverythingGrippy = false;


        // Data
        public static int pageNumber = 0;
        public static int buttonsType = 0;
    }
}
