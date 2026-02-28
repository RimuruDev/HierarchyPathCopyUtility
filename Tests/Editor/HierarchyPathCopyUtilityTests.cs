/*
 * Copyright (c) 2026 AbyssMoth
 * Author: RimuruDev
 * Licensed under the MIT License. See LICENSE in the package root for license information.
 */

using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace AbyssMoth.Tests
{
    public sealed class HierarchyPathCopyUtilityTests
    {
        private const string TestRootFolder = "Assets/HierarchyPathCopyUtilityTests.Generated";

        [SetUp]
        public void SetUp()
        {
            CleanupGeneratedAssets();
            EnsureFolder(TestRootFolder);
        }

        [TearDown]
        public void TearDown() =>
            CleanupGeneratedAssets();

        [Test]
        public void BuildHierarchyPath_ReturnsFullPath()
        {
            var root = new GameObject("Root");
            try
            {
                var child = new GameObject("Child");
                child.transform.SetParent(root.transform);

                var target = new GameObject("Target");
                target.transform.SetParent(child.transform);

                var path = HierarchyPathCopyService.BuildHierarchyPath(target.transform);

                Assert.That(path, Is.EqualTo("Root/Child/Target"));
            }
            finally
            {
                Object.DestroyImmediate(root);
            }
        }

        [Test]
        public void BuildObjectLocator_ForSceneObject_ContainsSceneAndHierarchyPath()
        {
            var root = new GameObject("LocatorRoot");
            try
            {
                var child = new GameObject("LocatorChild");
                child.transform.SetParent(root.transform);

                var locator = HierarchyPathCopyService.BuildObjectLocator(child.transform);

                Assert.That(locator, Does.Contain($"Scene:{root.scene.name}"));
                Assert.That(locator, Does.Contain("LocatorRoot/LocatorChild"));
            }
            finally
            {
                Object.DestroyImmediate(root);
            }
        }

        [Test]
        public void BuildObjectLocator_ForPrefabInstance_ContainsPrefabAssetPath()
        {
            var prefabPath = $"{TestRootFolder}/HierarchyLocator.prefab";
            var prefabRoot = new GameObject("PrefabRoot");

            try
            {
                var child = new GameObject("PrefabChild");
                child.transform.SetParent(prefabRoot.transform);
                PrefabUtility.SaveAsPrefabAsset(prefabRoot, prefabPath);
                AssetDatabase.SaveAssets();
            }
            finally
            {
                Object.DestroyImmediate(prefabRoot);
            }

            var prefabAsset = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            var instance = PrefabUtility.InstantiatePrefab(prefabAsset) as GameObject;

            Assert.That(instance, Is.Not.Null);

            try
            {
                var locator = HierarchyPathCopyService.BuildObjectLocator(instance.transform.Find("PrefabChild"));

                Assert.That(locator, Does.Contain("PrefabRoot/PrefabChild"));
                Assert.That(locator, Does.Contain($"Prefab:{prefabPath}"));
            }
            finally
            {
                Object.DestroyImmediate(instance);
            }
        }

        private static void EnsureFolder(string folderPath)
        {
            if (AssetDatabase.IsValidFolder(folderPath))
                return;

            var segments = folderPath.Split('/');
            var currentPath = segments[0];
            for (var i = 1; i < segments.Length; i++)
            {
                var nextPath = $"{currentPath}/{segments[i]}";
                if (!AssetDatabase.IsValidFolder(nextPath))
                    AssetDatabase.CreateFolder(currentPath, segments[i]);

                currentPath = nextPath;
            }
        }

        private static void CleanupGeneratedAssets()
        {
            if (AssetDatabase.IsValidFolder(TestRootFolder))
                AssetDatabase.DeleteAsset(TestRootFolder);

            AssetDatabase.Refresh();
        }
    }
}
