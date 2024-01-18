using Godot;

namespace FinalEmblem.Core
{
	[GlobalClass]
	public partial class Weapon : Resource
	{
		[Export] public string Name { get; private set; }
		[Export] public int Damage { get; private set; }
	}
}
