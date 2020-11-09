using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialInstance : MonoBehaviour
{
    private MeshRenderer rend;
    private MaterialPropertyBlock properties;
    public bool flipX;
    public bool flipY;
    private float flipTime;
    private float flipTimer;
    public Vector2 frameLocation;

    private void Awake()
    {
        flipTime = Random.Range(1f, 1.5f);
        frameLocation.x = Random.Range(0, 2);
        frameLocation.y = Random.Range(0, 2);

        properties = new MaterialPropertyBlock();
        rend = GetComponent<MeshRenderer>();
        rend.GetPropertyBlock(properties);
    }

    private void Update()
    {
        flipTimer += Time.deltaTime;
        if (flipTimer > flipTime)
        {
            flipX = !flipX;
            flipTimer -= flipTime;
        }

        properties.SetFloat("_FlipX", flipX ? 1f : 0f);
        properties.SetFloat("_FlipY", flipY ? 1f : 0f);
        properties.SetVector("_FrameLocation", frameLocation);
        rend.SetPropertyBlock(properties);
    }
}
