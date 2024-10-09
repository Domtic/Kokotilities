using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class ScreenShotWithCamera : MonoBehaviour
{
    Camera cam;
    public string pathFolder;
    public Vector2 ImgSize;
    public List<GameObject> sceneObjects;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    [ContextMenu("Screenshot")]
    private void ProcessScreenshots()
    {
        cam = GetComponent<Camera>();
        StartCoroutine(Screenshot());
    }

    private IEnumerator Screenshot()
    {
        for (int i = 0; i < sceneObjects.Count; i++)
        {
            GameObject obj = sceneObjects[i];
            obj.gameObject.SetActive(true);
            yield return null;
            Debug.Log(Application.dataPath);
            TakeShot($"{Application.dataPath}/{pathFolder}/{sceneObjects[i].name}_Icon.png");

            yield return null;
            obj.gameObject.SetActive(false);
        }
    }
    public void TakeShot(string fullPath)
    {
        if (cam == null)
        {
            cam = GetComponent<Camera>();
        }

        RenderTexture rt = new RenderTexture((int)ImgSize.x, (int)ImgSize.y, 24);
        cam.targetTexture = rt;
        Texture2D screenShot = new Texture2D((int)ImgSize.x, (int)ImgSize.y, TextureFormat.RGBA32, false);
        cam.Render();
        RenderTexture.active = rt;

        screenShot.ReadPixels(new Rect(0, 0, ImgSize.x, ImgSize.y), 0, 0);
        cam.targetTexture = null;
        RenderTexture.active = null;

        if (Application.isEditor)
        {
            DestroyImmediate(rt);
        }
        else
        {
            Destroy(rt);
        }

        byte[] bytes = screenShot.EncodeToPNG();
        System.IO.File.WriteAllBytes(fullPath, bytes);
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif

    }

}