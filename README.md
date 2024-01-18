# final emblem

## Dependencies

- [Dotnet 7 SDK (for tests)](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)

## Formatting

- `dotnet/format`: Install by running `dotnet tool install -g dotnet-format` in your project folder

## Debugger Setup

### VS 2022

1. Create a new executable debug profile
2. Set the executable path to the Godot editor exe; e.g., `C:/Users/Grahame/Desktop/Godot/Godot4_2.exe`
3. Set the command line arguments to `--path . --verbose`
4. Set the working directory to `.` (current folder)
5. If you want to see better C++ errors from the engine, enable **Native Code Debugging**. Note that it will disable hot reloading to do so.

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
