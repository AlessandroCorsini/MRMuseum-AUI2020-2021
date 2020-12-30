using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockTexure : MonoBehaviour
{
    Material rockMaterial;
    public Material[] materials;
    public static int count = 0;

    void Update()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = materials[count];
    }

    public static void updateCount()
    {
        count += 1;
    }
}
