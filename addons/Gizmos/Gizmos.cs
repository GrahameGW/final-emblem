#if TOOLS
using Godot;

namespace TiercelFoundry.GDGizmos
{
    [Tool]
    public partial class Gizmos : EditorPlugin
    {
        public override void _EnterTree()
        {
            // Initialization of the plugin goes here.
        }

        public override void _ExitTree()
        {
            // Clean-up of the plugin goes here.
        }
    }

    [Tool]
    public partial class GizmoSphere : MeshInstance3D
    {
        public static void DrawSphere(Vector3 position, float radius, Color color, Node parent)
        {
            var sphere = new SphereMesh();
            sphere.Radius = radius < 0f ? 0f : radius;

            var material = new StandardMaterial3D();
            material.AlbedoColor = color;

            var instance = new MeshInstance3D();
            instance.MaterialOverride = material;
            instance.Position = position;
            instance.Mesh = sphere;

            parent.AddChild(instance);
        }

        public override void _EnterTree()
        {
            Mesh = new SphereMesh();
        }
    }
}
#endif
