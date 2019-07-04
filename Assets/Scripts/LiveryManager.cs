using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveryManager : MonoBehaviour
{
    public Texture2D[] textures;

    public MeshRenderer[] meshRenderers;
    public MeshRenderer[] nacelleRenderer;

    public Material originalMaterial;
    public Material originalNacelleMaterial;

    void Start()
    {
        // Copy and assign materials
        Material material = new Material(originalMaterial);

        for (int i = 0; i < meshRenderers.Length; i++)
            meshRenderers[i].sharedMaterial = material;

        Material nacelleMaterial = new Material(originalNacelleMaterial);


        for (int i = 0; i < nacelleRenderer.Length; i++)
        {
            Material[] allNacelleMaterials = nacelleRenderer[i].sharedMaterials;
            allNacelleMaterials[1] = nacelleMaterial;
            nacelleRenderer[i].sharedMaterials = allNacelleMaterials;
        }

        Texture2D livery = textures[Random.Range(0, textures.Length)];

        // Pick colors

        Color colorPrimary;

        if (Random.value < 0.5f)
            colorPrimary = Color.white;
        else colorPrimary = Random.ColorHSV();

        Color colorSecondary = Random.ColorHSV();
        Color colorTertiary = Random.ColorHSV();

        Color nacelleColor = Random.value < 0.5f ? colorPrimary : colorSecondary;

        // Set colors to material

        material.SetTexture("_MainTex", livery);
        material.SetColor("_Color", colorPrimary);
        material.SetColor("_ColorSecondary", colorSecondary);
        material.SetColor("_ColorTertiary", colorTertiary);

        nacelleMaterial.SetColor("_Color", nacelleColor);
    }
}
