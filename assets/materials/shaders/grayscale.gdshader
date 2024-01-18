shader_type canvas_item;
uniform float saturation : hint_range(0.0, 1.0) = 0.5f;

void fragment() {
	vec3 grayscale = vec3(saturation);
	vec4 tex = texture(TEXTURE, UV);
	COLOR = vec4(vec3(dot(tex.rgb, grayscale)), tex.a);
}
