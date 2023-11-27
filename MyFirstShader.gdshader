shader_type canvas_item;

uniform vec4 color : source_color = vec4(1.0);

void fragment() {
	vec2 uv = UV;
	COLOR = texture(TEXTURE, uv) * color;
}