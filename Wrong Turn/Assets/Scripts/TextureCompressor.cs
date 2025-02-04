using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCompressor : MonoBehaviour
{
    public TextureFormat compressionFormat = TextureFormat.DXT1;

    void Start()
    {
        CompressAllTextures();
    }

    void CompressAllTextures()
    {
        Renderer[] renderers = FindObjectsOfType<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            foreach (Material mat in renderer.sharedMaterials)
            {
                if (mat != null && mat.mainTexture is Texture2D)
                {
                    Texture2D texture = (Texture2D)mat.mainTexture;
                    CompressTexture(texture);
                }
            }
        }
    }

    void CompressTexture(Texture2D texture)
    {
        if (!texture.isReadable)
        {
            Debug.LogWarning($"Texture {texture.name} is not readable. Skipping compression.");
            return;
        }
        texture.Compress(true);
        texture.Apply();
    }
}
