/*
 * Copyright (c) 2026 AbyssMoth
 * Author: RimuruDev
 * Licensed under the MIT License. See LICENSE in the package root for license information.
 */

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace AbyssMoth
{
    public static class HierarchyPathCopyUtility
    {
        [MenuItem(HierarchyPathCopyConstants.CopyHierarchyMenu)]
        private static void CopySelectedHierarchyPath() =>
            CopySelection(copyLocator: false);

        [MenuItem(HierarchyPathCopyConstants.CopyHierarchyMenu, true)]
        private static bool ValidateCopySelectedHierarchyPath() =>
            HasSelectedTransforms();

        [MenuItem(HierarchyPathCopyConstants.CopyLocatorMenu)]
        private static void CopySelectedObjectLocator() =>
            CopySelection(copyLocator: true);

        [MenuItem(HierarchyPathCopyConstants.CopyLocatorMenu, true)]
        private static bool ValidateCopySelectedObjectLocator() =>
            HasSelectedTransforms();

        private static void CopySelection(bool copyLocator)
        {
            var value = HierarchyPathCopyService.BuildSelectionText(Selection.transforms, copyLocator);
            PublishToClipboard(value, copyLocator);
        }

        private static void PublishToClipboard(string value, bool copyLocator)
        {
            if (string.IsNullOrWhiteSpace(value))
                return;

            EditorGUIUtility.systemCopyBuffer = value;

            var focusedWindow = EditorWindow.focusedWindow;
            if (focusedWindow != null)
            {
                focusedWindow.ShowNotification(new GUIContent(
                    copyLocator
                        ? HierarchyPathCopyConstants.LocatorCopiedNotification
                        : HierarchyPathCopyConstants.HierarchyCopiedNotification));
            }

            Debug.Log(value);
        }

        private static bool HasSelectedTransforms()
        {
            var transforms = Selection.transforms;
            return transforms != null && transforms.Length > 0;
        }
    }
}
#endif
