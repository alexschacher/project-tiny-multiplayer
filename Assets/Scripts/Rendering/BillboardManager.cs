using UnityEngine;

public class BillboardManager : MonoBehaviour
{
    private MeshRenderer rend;
    private MaterialPropertyBlock properties;
    private bool flipX;
    private bool flipY;
    private Vector2 frameLocation;

    public void SetFlipY(bool input)
    {
        flipY = input;
        UpdateMaterialProperties();
    }

    public void SetFlipX(bool input)
    {
        flipX = input;
        UpdateMaterialProperties();
    }

    public void SetFrame(int x, int y)
    {
        frameLocation = new Vector2(x, y);
        UpdateMaterialProperties();
    }

    public void SetMaterial(Material mat)
    {
        rend.sharedMaterial = mat;
    }

    public void SetVisiblity(bool visibility)
    {
        rend.enabled = visibility;
    }

    private void Awake()
    {
        properties = new MaterialPropertyBlock();
        rend = GetComponent<MeshRenderer>();
    }

    private void UpdateMaterialProperties()
    {
        rend.GetPropertyBlock(properties);
            properties.SetFloat("_FlipX", flipX ? 1f : 0f);
            properties.SetFloat("_FlipY", flipY ? 1f : 0f);
            properties.SetVector("_FrameLocation", frameLocation);
        rend.SetPropertyBlock(properties);
    }
}