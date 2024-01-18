extends Node
@export var mouse_pix_label := Label
@export var tile_coords_label := Label
@export var tile_pos_label := Label

var level
var mouse_pos: Vector2

func _enter_tree():
	level = get_parent()
	
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	var pos = get_viewport().get_mouse_position()
	if (mouse_pos != pos):
		mouse_pos = pos
		mouse_pix_label.text = str(pos)
		tile_coords_label.text = str(level.Map.DebugUnderPointerCoords)
		tile_pos_label.text = str(level.Map.DebugUnderPointerWorldPos)
