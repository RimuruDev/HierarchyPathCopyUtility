/*
 * Copyright (c) 2026 AbyssMoth
 * Author: RimuruDev
 * Licensed under the MIT License. See LICENSE in the package root for license information.
 */

#if UNITY_EDITOR
namespace AbyssMoth
{
    internal static class HierarchyPathCopyConstants
    {
        public const string CopyHierarchyMenu = "AbyssMoth/Copy Selection/Hierarchy Path %&c";
        public const string CopyLocatorMenu = "AbyssMoth/Copy Selection/Object Locator %#&c";
        public const string HierarchyCopiedNotification = "Hierarchy path copied";
        public const string LocatorCopiedNotification = "Object locator copied";
        public const string HierarchyHotkeyDescription = "Cmd+Option+C on macOS, Ctrl+Alt+C on Windows";
        public const string LocatorHotkeyDescription = "Cmd+Shift+Option+C on macOS, Ctrl+Shift+Alt+C on Windows";
        public const string UnknownSceneLabel = "Scene:Unknown";
        public const string ScenePrefix = "Scene:";
        public const string PrefabPrefix = "Prefab:";
        public const string LocatorSeparator = " :: ";
    }
}
#endif
