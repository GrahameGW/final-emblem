using Godot;
using System;

namespace FinalEmblem.Core3D
{
    public partial class Grid : Node3D
    {
        private Node3D ground;
        private Vector2I size;

        public void Initialize(Vector2I size)
        {
            this.size = size;
            ground = GetNode<Node3D>("Ground");
            ground.ScaleObjectLocal(new Vector3(size.X, size.Y, 1f));
        }
    }
}

