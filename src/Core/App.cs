using Godot;

namespace FinalEmblem.Core
{
	public partial class App : Node
	{
		[Export] PackedScene level;
		[Export] PackedScene mainMenu;

		private Node currentScene;


		public override void _Ready()
		{
			LoadMainMenu();
		}

		private void ChangeScene(PackedScene scene)
		{
			currentScene?.QueueFree();
			currentScene = scene.Instantiate();
			AddChild(currentScene);
		}

		public void LoadMainMenu() => ChangeScene(mainMenu);
		public void LoadLevel() => ChangeScene(level);
	}
}

