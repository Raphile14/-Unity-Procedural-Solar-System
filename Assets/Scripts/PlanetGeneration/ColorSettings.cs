using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColorSettings : ScriptableObject
{
    public Material planetMaterial;
    public BiomeColorSettings biomeColorSettings;
    public Gradient oceanColor;

    public void InitColorSettings(Material planetMaterial, int BiomeCount)
    {
        this.planetMaterial = planetMaterial;
        biomeColorSettings = new BiomeColorSettings();
        biomeColorSettings.noise = new NoiseSettings();
        biomeColorSettings.noise.simpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();
        biomeColorSettings.noise.ridgidNoiseSettings = new NoiseSettings.RidgidNoiseSettings();
        oceanColor = new Gradient();
        GenerateBiomes(BiomeCount);
    }    

    void GenerateBiomes(int BiomeCount)
    {
        biomeColorSettings.biomes = new BiomeColorSettings.Biome[BiomeCount];        
        for (int i = 0; i < biomeColorSettings.biomes.Length; i++)
        {
            biomeColorSettings.biomes[i] = new BiomeColorSettings.Biome();
            biomeColorSettings.biomes[i].gradient = new Gradient();
        }
    }

    [System.Serializable]
    public class BiomeColorSettings
    {
        public Biome[] biomes;
        public NoiseSettings noise;
        public float noiseOffset;
        public float noiseStrength;
        [Range(0,1)]
        public float blendAmount;

        [System.Serializable]
        public class Biome
        {
            public Gradient gradient;
            public Color tint;
            [Range(0,1)]
            public float startHeight;
            [Range(0, 1)]
            public float tintPercent;
        }
    }
}
