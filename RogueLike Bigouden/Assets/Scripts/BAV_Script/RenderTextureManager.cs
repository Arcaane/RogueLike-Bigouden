using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureManager : MonoBehaviour {

    [SerializeField] private Material setToMaterial;

    private void Awake() {
        var tex = new RenderTexture(Screen.width, Screen.height, 8);
        Debug.Assert(tex.Create(), "Failed to create camera blending render texture");

        Camera cam = GetComponent<Camera>();
        cam.targetTexture = tex;
        setToMaterial.SetTexture("_MainTex", tex);
    }
}