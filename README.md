# final emblem

## Dependencies

- [Dotnet 7 SDK (for tests)](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)

## Formatting

- `dotnet/format`: Install by running `dotnet tool install -g dotnet-format` in your project folder

## Troubleshooting

### Attack animation

Godot seems to arbitrarily reset my positioning on this, so here is how it should be laid out:

- Root (Control): Anchors Preset Top Left
- VboxContainer: Layout Mode Anchors
  - Preset: Custom
  - Offsets: 0, -12, 0, 64
  - Grow Direction: H: Right, V: Both
- Each Panel Container: Sizing Fill (Both)
- HBox Container: Sizing Fill (Both)
- Name Text: H Fill Expand, V Fill
- Progress Bar: H Shrink End Expand, V Fill
