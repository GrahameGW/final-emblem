extends Button

@export var level: PackedScene
var parent: Node

func _enter_tree():
	parent = get_parent()
	
func _on_pressed():
	if parent != null:
		get_tree().change_scene_to_packed(level)
