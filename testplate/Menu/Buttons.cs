using SillyMenu.Classes;
using SillyMenu.Mods;
using static SillyMenu.Menu.Settings;

namespace SillyMenu.Menu
{
    internal class Buttons
    {
        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            new ButtonInfo[] { // Main Mods
                new ButtonInfo { buttonText = "settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "settitings"},
                new ButtonInfo { buttonText = "movement mods", method =() => Global.MovementMods(), isTogglable = false},
                new ButtonInfo { buttonText = "miscellaneous mods", method =() => Global.MiscellaneousMods(), isTogglable = false},
                new ButtonInfo { buttonText = "room mods", method =() => Global.RoomMods(), isTogglable = false},
            },

            new ButtonInfo[] { // Settings
                new ButtonInfo { buttonText = "return to main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "main page"},
                new ButtonInfo { buttonText = "menu", method =() => SettingsMods.MenuSettings(), isTogglable = false, toolTip = "stupid fucking menu settings"},
            },

            new ButtonInfo[] { // Menu Settings
                new ButtonInfo { buttonText = "return to settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "settitingssa"},
                new ButtonInfo { buttonText = "change theme [pink]", method =() => Global.incrementing(), isTogglable = false, toolTip = "Theme."},
                new ButtonInfo { buttonText = "right hand", enableMethod =() => SettingsMods.RightHand(), disableMethod =() => SettingsMods.LeftHand(), toolTip = "who uses this?"},
                new ButtonInfo { buttonText = "toggle images", enableMethod =() => SettingsMods.EnableFemboyImage(), disableMethod =() => SettingsMods.DisableFemboyImage(),  toolTip = "toggles the femboy images"},
                new ButtonInfo { buttonText = "notifications", enableMethod =() => SettingsMods.EnableNotifications(), disableMethod =() => SettingsMods.DisableNotifications(), enabled = !disableNotifications, toolTip = "toggle notifs"},
                new ButtonInfo { buttonText = "FPS counter", enableMethod =() => SettingsMods.EnableFPSCounter(), disableMethod =() => SettingsMods.DisableFPSCounter(), enabled = fpsCounter, toolTip = "begone fps"},
                new ButtonInfo { buttonText = "disconnect button", enableMethod =() => SettingsMods.EnableDisconnectButton(), disableMethod =() => SettingsMods.DisableDisconnectButton(), enabled = disconnectButton, toolTip = "destroy"},
            },

            new ButtonInfo[] { // Movement Settings
                new ButtonInfo { buttonText = "Return to Settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "Returns to the main settings page for the menu."},
            },

            new ButtonInfo[] { // Projectile Settings
                new ButtonInfo { buttonText = "Return to Settings", method =() => SettingsMods.MenuSettings(), isTogglable = false, toolTip = "Opens the settings for the menu."},
            },

            new ButtonInfo[] { // Movement Mods
                new ButtonInfo { buttonText = "return to main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "main page"},
                new ButtonInfo { buttonText = "platforms", method =() => Movement.Platforms(),  toolTip = "stupid platforms"},
                new ButtonInfo { buttonText = "speedboost", method =() => Movement.Speedboost(), toolTip = "speedboost i think, i dont know i never tested it"},
                new ButtonInfo { buttonText = "slippery hands <color=white>[</color><color=green>R</color><color=white>]</color>", method =() => Movement.SlipperyHands(), toolTip = "dude i cant play like this"},
                new ButtonInfo { buttonText = "grippy hands <color=white>[</color><color=green>R</color><color=white>]</color>", method =() => Movement.GrippyHands(), toolTip = "you wanna see how good i am at ice?"},
                new ButtonInfo { buttonText = "zero gravity <color=white>[</color><color=green>R</color><color=white>]</color>", method =() => Movement.ZeroGravity(), toolTip = "WHAT THE FUCK IM FLOATING"},
                new ButtonInfo { buttonText = "rig gun <color=white>[</color><color=green>R</color><color=white>]</color>", method =() => Movement.RigGun(), toolTip = "dude im insane at the game"},
            },

            new ButtonInfo[] { // Miscellaneous Mods
                new ButtonInfo { buttonText = "return to main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "main page"},
                new ButtonInfo { buttonText = "bring gliders", method =() => Miscellaneous.BringGliders(),  toolTip = "free glider real"},
                new ButtonInfo { buttonText = "pink snowballs", method =() => Miscellaneous.PinkSnowballs(), toolTip = "HOLY SHIT PINK SNOWBALLS"},
                new ButtonInfo { buttonText = "materialize water balloon", method =() => Miscellaneous.materializewaterballoon(), toolTip = "begone"},
                new ButtonInfo { buttonText = "materialize rock balloon", method =() => Miscellaneous.materializerockballoon(), toolTip = "IVE HAD IT *tosses a rock at you*"},
                new ButtonInfo { buttonText = "materialize present balloon", method =() => Miscellaneous.materializepresentballoon(), toolTip = "GET IN THE CHRISTMAS SPIRIT DAMNIT"},
                new ButtonInfo { buttonText = "materialize SHIT", method =() => Miscellaneous.materializegishfoodballoon(), toolTip = "true monkey buisness"},
            },

            new ButtonInfo[] { // Room Mods
                new ButtonInfo { buttonText = "return to main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "main page"},
                new ButtonInfo { buttonText = "disable join room triggers", enableMethod =() => RoomMods.EnableRoomJoining(), disableMethod =() => RoomMods.DisableRoomJoining(), toolTip = "no more screaming little kids"},
            },

            new ButtonInfo[] { // stupid disconnect
                new ButtonInfo { buttonText = "Disconnect", method =() => SettingsMods.Disconnect(), isTogglable = false},
            }
        };
    }
}
