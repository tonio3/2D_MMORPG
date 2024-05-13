using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScriptableObjectWithIcon), true), CanEditMultipleObjects]
public class SOEditor : Editor
{
    private ScriptableObjectWithIcon example;

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        example = (ScriptableObjectWithIcon)target;

        if (example == null || example.Spr == null)
            return null;

        Texture2D tex = new Texture2D(width, height);
        EditorUtility.CopySerialized(example.Spr.texture, tex);

        return tex;
    }
}
