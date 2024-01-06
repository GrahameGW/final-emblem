class_name RuntimePortrait 
extends DialogicPortrait


func load_character(new_character: Resource, new_portrait: String) -> void:
	if character is DialogicCharacter:
		character = new_character as DialogicCharacter
	portrait = new_portrait
	apply_character_and_portrait(character, portrait)

func _update_portrait(_c: DialogicCharacter, _p: String) -> void:
	if portrait == '':
		portrait = character.default_portrait
	apply_character_and_portrait(character, portrait)
