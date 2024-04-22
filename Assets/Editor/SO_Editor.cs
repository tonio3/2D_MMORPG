using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
 

public static class SOEditor
{
    public static void reName(string assetPath,string name)
    {
        if (AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.ScriptableObject)) != null)
        {
            name += "_1";
        }

        AssetDatabase.RenameAsset(assetPath, name);
        AssetDatabase.Refresh();
        Debug.Log("AssetRenamed: " + name);
    }
}

[CustomEditor(typeof(ResourceSO)), CanEditMultipleObjects]
public class ResourceSOEditor : Editor
{
    bool renameOnValidate = true;
    ResourceSO example;

    private void renameMe()
    {
        if (!renameOnValidate || example.Spr == null || example.name == example.Spr.name) return;
        string assetPath = AssetDatabase.GetAssetPath(example);
        SOEditor.reName(assetPath, example.Spr.name);
        renameOnValidate = false;
    }

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        example = (ResourceSO)target;

        if (example == null || example.Spr == null)
            return null;

        // example.PreviewIcon must be a supported format: ARGB32, RGBA32, RGB24,
        // Alpha8 or one of float formats
        Texture2D tex = new Texture2D(width, height);
        EditorUtility.CopySerialized(example.Spr.texture, tex);

        renameMe();

        return tex;
    }
}


[CustomEditor(typeof(EnemySO)), CanEditMultipleObjects]
public class EnemySOEditor : Editor
{
    bool renameOnValidate = true;
    EnemySO example;

    private void renameMe()
    {
        if (!renameOnValidate || example.Spr == null || example.name == example.Spr.name) return;
        string assetPath = AssetDatabase.GetAssetPath(example);
        SOEditor.reName(assetPath, example.name);
        renameOnValidate = false;
    }

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        example = (EnemySO)target;

        if (example == null || example.Spr == null)
            return null;

        // example.PreviewIcon must be a supported format: ARGB32, RGBA32, RGB24,
        // Alpha8 or one of float formats
        Texture2D tex = new Texture2D(width, height);
        EditorUtility.CopySerialized(example.Spr.texture, tex);

        renameMe();

        return tex;
    }
}


[CustomEditor(typeof(ItemSO)), CanEditMultipleObjects]
public class ScrDatabaseEditor : Editor
{
    bool renameOnValidate = true;
    ItemSO example;

    private void renameMe()
    {
        if (!renameOnValidate || example.Spr == null || example.name == example.Spr.name) return;
        string assetPath = AssetDatabase.GetAssetPath(example);
        SOEditor.reName(assetPath, example.name);
        renameOnValidate = false;
    }

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        example = (ItemSO)target;

        if (example == null || example.Spr == null)
            return null;

        // example.PreviewIcon must be a supported format: ARGB32, RGBA32, RGB24,
        // Alpha8 or one of float formats
        Texture2D tex = new Texture2D(width, height);
        EditorUtility.CopySerialized(example.Spr.texture, tex);

        renameMe();

        return tex;
    }
}

