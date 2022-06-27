using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSpawner : MonoBehaviour
{
    [Header("Spawn properties")]
    [SerializeField]
    private int amount;
    [SerializeField]
    private int horizontalSpace = 5;
    [SerializeField]
    private int verticalSpace = 5;

    [Header("Configurations to test")]
    [SerializeField]
    private bool instantiateWithPropertyBlock;

    [SerializeField]
    private GameObject prefabPropertyBlock;
    [SerializeField]
    private GameObject simplePrefab;

    void Start()
    {
        SpawnModels();
    }

    private void SpawnModels()
    {
        Vector3 position = Vector3.zero;

        for(int i = 0; i< amount; i++)
        {
            GameObject prefab = instantiateWithPropertyBlock ? prefabPropertyBlock : simplePrefab;

            GameObject prefabInstance = Instantiate(prefab, position, Quaternion.identity);
            
            if(i % 5 == 0)
            {
                position.x = 0;
                position.z += verticalSpace;
            }
            else
            {
                position.x += horizontalSpace;
            }

            if(instantiateWithPropertyBlock)
            {
                prefabInstance.GetComponent<ColorMaskMaterialPropertyBlock>().SetColors(GetRandomColor(), GetRandomColor(), GetRandomColor());

                continue;
            }

            Material mat = prefabInstance.GetComponentInChildren<Renderer>().material;

            mat.SetColor("_RChannel", GetRandomColor());
            mat.SetColor("_GChannel", GetRandomColor());
            mat.SetColor("_BChannel", GetRandomColor());
        }
    }

    private Color GetRandomColor()
    {
        float rChannel = Random.Range(0f, 1f);
        float gChannel = Random.Range(0f, 1f);
        float bChannel = Random.Range(0f, 1f);

        return new Color(rChannel, gChannel, bChannel);
    }

    private void SpawnPrefabCoroutine()
    {

    }
}
