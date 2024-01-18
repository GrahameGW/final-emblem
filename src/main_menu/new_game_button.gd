extends Button

@export var menu_root: Control
	
func _on_pressed():
	menu_root.get_parent().LoadLevel()
