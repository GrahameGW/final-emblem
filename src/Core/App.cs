﻿using Godot;

namespace FinalEmblem.Core
{
    public partial class App : Node
    {
        [Export] PackedScene level;
        [Export] PackedScene mainMenu;

        public const string CHANGE_SCENE_TO_PACKED = "change_scene_to_packed";

        public void LoadMainMenu()
        {
            CallDeferred(CHANGE_SCENE_TO_PACKED, mainMenu);
        }
    }
}
