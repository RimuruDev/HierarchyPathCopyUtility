/*
 * Copyright (c) 2026 AbyssMoth
 * Author: RimuruDev
 * Licensed under the MIT License. See LICENSE in the package root for license information.
 */

#if UNITY_EDITOR
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AbyssMoth
{
    internal static class HierarchyPathCopyService
    {
        public static string BuildSelectionText(IReadOnlyList<Transform> transforms, bool copyLocator)
        {
            if (transforms == null || transforms.Count == 0)
                return string.Empty;

            var builder = new StringBuilder(capacity: 256);
            for (var i = 0; i < transforms.Count; i++)
            {
                var transform = transforms[i];
                if (transform == null)
                    continue;

                var line = copyLocator
                    ? BuildObjectLocator(transform)
                    : BuildHierarchyPath(transform);

                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (builder.Length > 0)
                    builder.AppendLine();

                builder.Append(line);
            }

            return builder.ToString();
        }

        public static string BuildObjectLocator(Transform transform)
        {
            if (transform == null)
                return string.Empty;

            var hierarchyPath = BuildHierarchyPath(transform);
            if (string.IsNullOrWhiteSpace(hierarchyPath))
                return string.Empty;

            if (TryGetCurrentPrefabStage(transform, out var prefabStagePath))
                return string.Concat(prefabStagePath, HierarchyPathCopyConstants.LocatorSeparator, hierarchyPath);

            var scene = transform.gameObject.scene;
            var sceneLabel = scene.IsValid()
                ? string.Concat(HierarchyPathCopyConstants.ScenePrefix, scene.name)
                : HierarchyPathCopyConstants.UnknownSceneLabel;

            var prefabAssetPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(transform.gameObject);
            if (!string.IsNullOrWhiteSpace(prefabAssetPath))
            {
                return string.Concat(
                    sceneLabel,
                    HierarchyPathCopyConstants.LocatorSeparator,
                    hierarchyPath,
                    HierarchyPathCopyConstants.LocatorSeparator,
                    HierarchyPathCopyConstants.PrefabPrefix,
                    prefabAssetPath);
            }

            return string.Concat(sceneLabel, HierarchyPathCopyConstants.LocatorSeparator, hierarchyPath);
        }

        public static string BuildHierarchyPath(Transform transform)
        {
            if (transform == null)
                return string.Empty;

            var root = ResolveRoot(transform);
            var names = new List<string>(capacity: 16);
            var current = transform;

            while (current != null)
            {
                names.Add(current.name);
                if (current == root)
                    break;

                current = current.parent;
            }

            names.Reverse();
            return string.Join("/", names);
        }

        private static Transform ResolveRoot(Transform transform)
        {
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null &&
                prefabStage.prefabContentsRoot != null &&
                transform.IsChildOf(prefabStage.prefabContentsRoot.transform))
            {
                return prefabStage.prefabContentsRoot.transform;
            }

            var current = transform;
            while (current.parent != null)
                current = current.parent;

            return current;
        }

        private static bool TryGetCurrentPrefabStage(Transform transform, out string prefabAssetPath)
        {
            prefabAssetPath = string.Empty;

            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage == null ||
                prefabStage.prefabContentsRoot == null ||
                !transform.IsChildOf(prefabStage.prefabContentsRoot.transform) ||
                string.IsNullOrWhiteSpace(prefabStage.assetPath))
            {
                return false;
            }

            prefabAssetPath = prefabStage.assetPath;
            return true;
        }
    }
}
#endif
