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
        public const string CopyHierarchyMenu = "AbyssMoth/Copy Selection/Hierarchy Path";
        public const string CopyLocatorMenu = "AbyssMoth/Copy Selection/Object Locator";
        public const string CopyHierarchyGameObjectMenu = "GameObject/AbyssMoth/Copy Hierarchy Path";
        public const string CopyLocatorGameObjectMenu = "GameObject/AbyssMoth/Copy Object Locator";
        public const string CopyHierarchyContextMenu = "CONTEXT/Transform/Copy Hierarchy Path";
        public const string CopyLocatorContextMenu = "CONTEXT/Transform/Copy Object Locator";
        public const string HierarchyCopiedNotification = "Hierarchy path copied";
        public const string LocatorCopiedNotification = "Object locator copied";
        public const string UnknownSceneLabel = "Scene:Unknown";
        public const string ScenePrefix = "Scene:";
        public const string PrefabPrefix = "Prefab:";
        public const string LocatorSeparator = " :: ";
    }
}
#endif
