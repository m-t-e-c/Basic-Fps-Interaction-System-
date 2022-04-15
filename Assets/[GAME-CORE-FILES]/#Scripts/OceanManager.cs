using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour
{
    [SerializeField] private float waveHeight = 1f;
    [SerializeField] private float waveFrequency = 0.95f;
    [SerializeField] private float waveSpeed = 1.5f;

    [SerializeField] private Transform ocean;
    public Material oceanMat;
    public Texture2D wavesDisplacement;

    private void Start()
    {
        SetVariables();   
    }

    void SetVariables()
    {
        oceanMat = ocean.GetComponent<Renderer>().sharedMaterial;
        wavesDisplacement = (Texture2D)oceanMat.GetTexture("_FoamTex");
    }

    public float WaterHeightAtPosition(Vector3 pos)
    {
        return ocean.position.y + wavesDisplacement.GetPixelBilinear(pos.x * waveFrequency, pos.z * waveFrequency + Time.time * waveSpeed).g * waveHeight * ocean.localScale.x;
    }

    private void OnValidate()
    {
        if (!oceanMat)
            SetVariables();

        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        oceanMat.SetFloat("_WaveSpeed", waveSpeed);
        oceanMat.SetFloat("_WaveCount", waveFrequency);
        oceanMat.SetFloat("_WaveHeight", waveHeight);
    }
}
