extends MeshInstance3D

# Called when the node enters the scene tree for the first time.
func _ready():
	var surface_array = []
	surface_array.resize(Mesh.ARRAY_MAX)
	var verts = PackedVector3Array()
	var uvs = PackedVector2Array()
	var normals = PackedVector3Array()
	var indices = PackedInt32Array()
	
	for i in range(0, 1):
		verts.append(Vector3(i, 0, i))
		verts.append(Vector3(i + 1, 0, 0))
		verts.append(Vector3(i + 1, 0, i + 1))
		verts.append(Vector3(i, 0, i))
		verts.append(Vector3(i + 1, 0, i + 1))
		verts.append(Vector3(i, 0, i + 1))
		
	for vert in verts:
		normals.append(vert.normalized())
		
	
	surface_array[Mesh.ARRAY_VERTEX] = verts
	# surface_array[Mesh.ARRAY_TEX_UV] = uvs
	surface_array[Mesh.ARRAY_NORMAL] = normals
	# surface_array[Mesh.ARRAY_INDEX] = indices

	mesh.add_surface_from_arrays(
		Mesh.PRIMITIVE_TRIANGLES,
		surface_array
	)

