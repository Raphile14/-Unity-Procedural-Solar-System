using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject
{
    [Header("Planet Details")]
    public float planetRadius = 1;
    public NoiseLayer[] noiseLayers;
    public float RidgidNoiseLayerChance = 50f;    

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

            // Generate Type of Noise
            if (RidgidNoiseLayerChance < Random.Range(0, 100))
            {
                noiseLayers[i].noiseSettings.filterType = NoiseSettings.FilterType.Ridgid;
            }        
            else
            {
                noiseLayers[i].noiseSettings.filterType = NoiseSettings.FilterType.Simple;
            }

            // Generate needed Noise Settings            
            noiseLayers[i].noiseSettings.simpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();
            noiseLayers[i].noiseSettings.ridgidNoiseSettings = new NoiseSettings.RidgidNoiseSettings();
            
            // Generate Values for Simple Noise
            noiseLayers[i].noiseSettings.simpleNoiseSettings.strength = Random.Range(Control.MinStrength, Control.MaxStrength);
            noiseLayers[i].noiseSettings.simpleNoiseSettings.numLayers = Random.Range(Control.MinNumLayers, Control.MaxNumLayers);
            noiseLayers[i].noiseSettings.simpleNoiseSettings.baseRoughness = Random.Range(Control.MinBaseRoughness, Control.MaxBaseRoughness);
            noiseLayers[i].noiseSettings.simpleNoiseSettings.roughness = Random.Range(Control.MinRoughness, Control.MaxRoughness);
            noiseLayers[i].noiseSettings.simpleNoiseSettings.persistence = Random.Range(Control.MinPersistence, Control.MaxPersistence);

            // Generate Values for Ridgid Noise
            noiseLayers[i].noiseSettings.ridgidNoiseSettings.strength = Random.Range(Control.MinStrength, Control.MaxStrength);
            noiseLayers[i].noiseSettings.ridgidNoiseSettings.numLayers = Random.Range(Control.MinNumLayers, Control.MaxNumLayers);
            noiseLayers[i].noiseSettings.ridgidNoiseSettings.baseRoughness = Random.Range(Control.MinBaseRoughness, Control.MaxBaseRoughness);
            noiseLayers[i].noiseSettings.ridgidNoiseSettings.roughness = Random.Range(Control.MinRoughness, Control.MaxRoughness);
            noiseLayers[i].noiseSettings.ridgidNoiseSettings.persistence = Random.Range(Control.MinPersistence, Control.MaxPersistence);
            noiseLayers[i].noiseSettings.ridgidNoiseSettings.weightMultiplier = Random.Range(Control.MinWeightMultiplier, Control.MaxWeightMultiplier);
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
