﻿using Godot;

namespace FinalEmblem.Core
{
    public partial class Environment : Node2D
    {
        public Grid Grid { get; private set; }

        private GameMap gameMap;

        public override void _Ready()
        {
            gameMap = GetNode<GameMap>("GameMap");
            Grid = gameMap.GenerateGridFromMap();
        }
    }
}

