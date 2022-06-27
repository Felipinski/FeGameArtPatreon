using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMaskMaterialPropertyBlock : MonoBehaviour
{
    private Renderer prefabRenderer;
    private MaterialPropertyBlock propertyBlock;
        
    void Awake()
    {
        prefabRenderer = GetComponentInChildren<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
    }

    public void SetColors(Color rChannel, Color gChannel, Color bChannel)
    {
        prefabRenderer.GetPropertyBlock(propertyBlock);

        propertyBlock.SetColor("_RChannel", rChannel);
        propertyBlock.SetColor("_GChannel", gChannel);
        propertyBlock.SetColor("_BChannel", bChannel);

        prefabRenderer.SetPropertyBlock(propertyBlock); 
    }
}
