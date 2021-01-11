using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject
{
    public float planetRadius = 1;
    public NoiseLayer[] noiseLayers;

    public void InitShapeSettings(int NoiseLayerCount, float PlanetRadius)
    {
        this.noiseLayers = new NoiseLayer[NoiseLayerCount];
        this.planetRadius = PlanetRadius;
        SetupNoiseLayers();
    }

    public void SetupNoiseLayers()
    {
        for (int i = 0; i < noiseLayers.Length; i++)
        {
            noiseLayers[i] = new NoiseLayer();
            noiseLayers[i].noiseSettings = new NoiseSettings();
            noiseLayers[i].noiseSettings.simpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();
            noiseLayers[i].noiseSettings.ridgidNoiseSettings = new NoiseSettings.RidgidNoiseSettings();
        }
    }

    [System.Serializable]
    public class NoiseLayer
    {
        public bool enabled = true;
        public bool useFirstLayerAsMask = true;
        public NoiseSettings noiseSettings;
    }

}
