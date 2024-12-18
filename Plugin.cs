﻿using EFT;
using System;
using BepInEx;
using UnityEngine;
using BepInEx.Configuration;
using DrakiaXYZ.VersionChecker;
using QuickThrowGrenades.Patches;
using Comfort.Common;
using static DrakiaXYZ.VersionChecker.VersionChecker;

namespace QuickThrowGrenades
{
    [BepInPlugin("com.dirtbikercj.QuickThrowGrenades", "Quick Throw Grenades", "1.0.4")]
    internal class Plugin : BaseUnityPlugin
    {
        public const int TarkovVersion = 33420;

        internal static ConfigEntry<bool> Enable;
        internal static ConfigEntry<bool> EnableKeybind;
        internal static ConfigEntry<KeyboardShortcut> Keybind;

        internal static Player MainPlayer = null;

        private void Awake()
        {
            if (!CheckEftVersion(Logger, Info, Config))
            {
                throw new Exception("Invalid EFT Version");
            }

            Enable = Config.Bind(
               "General",
               "Quick Throw Grenades",
               true,
               new ConfigDescription("Enable plugin", null, new ConfigurationManagerAttributes { Order = 3 }));

            EnableKeybind = Config.Bind(
               "General",
               "Enable keybind",
               false,
               new ConfigDescription("Use A Keybind so you can still throw the old way.", null, new ConfigurationManagerAttributes { Order = 2 }));

            Keybind = Config.Bind(
               "General",
               "Quick throw keybind",
               new KeyboardShortcut(KeyCode.G, KeyCode.LeftShift),
               new ConfigDescription("Keybind to quick throw grenades - One of these keybinds must be same as BSG's grenade keybind", null, new ConfigurationManagerAttributes { Order = 1 }));

            new GrenadePatch().Enable();
        }

        private void Update()
        {
            if (Singleton<GameWorld>.Instantiated && MainPlayer == null)
            {
                MainPlayer = Singleton<GameWorld>.Instance.MainPlayer;
            }
        }
    }
}
