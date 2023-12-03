﻿using System;
using System.Collections.Generic;
using Godot;
using FinalEmblem.Core;
using System.Linq;

namespace FinalEmblem.Godot2D
{
    public partial class UnitGroup : Node
    {
        public Faction Faction { get; private set; }
        public List<UnitNode> UnitNodes { get; set; } = new();
        public List<Unit> Units
        {
            get => UnitNodes.Select(u => u.Unit).ToList();
        }

        public UnitGroup(Faction faction)
        {
            Faction = faction;
        }

        public static event Action<UnitGroup> OnUnitActionsCompleted;

        public void StartTurn()
        {

        }
    }
}
