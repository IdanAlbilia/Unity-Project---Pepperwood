using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToSkinnedMash : MonoBehaviour
{
    [ContextMenu("Convert to skinned mesh")]
    void Convert()
    {
        SkinnedMeshRenderer skinnedMeshRenderer = gameObject.AddComponent<SkinnedMeshRenderer>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        skinnedMeshRenderer.sharedMesh = meshFilter.sharedMesh;
        skinnedMeshRenderer.sharedMaterials = meshRenderer.sharedMaterials;

        DestroyImmediate(meshFilter);
        DestroyImmediate(meshRenderer);
        DestroyImmediate(this);
    }
}