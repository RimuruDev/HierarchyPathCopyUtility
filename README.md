# Hierarchy Path Copy Utility

Hierarchy Path Copy Utility is an editor package that copies hierarchy paths and object locator strings for selected objects directly from Unity menus.

## Installation

### Unity Package Manager

1. Open `Window > Package Manager`.
2. Click the `+` button.
3. Select `Add package from git URL...`.
4. Paste the Git URL of this package repository and confirm.

Example Git URL after publishing the repository:

`https://github.com/RimuruDev/HierarchyPathCopyUtility.git`

### Release Download

1. Open the Releases page of this package repository.
2. Download the published `.unitypackage` file if one is attached to the release.
3. Import it through `Assets > Import Package > Custom Package...`.

If the release contains the package source instead of a `.unitypackage`, copy the `HierarchyPathCopyUtility` folder into:

`Assets/Plugins/AbyssMoth/`

## Usage

1. Select one or more objects in the Hierarchy.
2. Use `AbyssMoth/Copy Selection/Hierarchy Path` to copy a plain hierarchy path.
3. Use `AbyssMoth/Copy Selection/Object Locator` to copy a path with scene or prefab context.
4. The same commands are also available in `GameObject/AbyssMoth/...` and the `Transform` context menu.

Hierarchy Path copies values like `Root/Child/Target`.

Object Locator copies values like `Scene:SampleScene :: Root/Child/Target` or `Scene:SampleScene :: Root/Child/Target :: Prefab:Assets/Example.prefab`.

The package currently targets Unity `6000.3.6f1`.

License: MIT
