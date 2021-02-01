using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    // Public Values
    [Header("Solar System Settings")]    
    public string Seed;        
    public int MinNumberOfPlanets = 1;
    public int MaxNumberOfPlanets = 5;
    public int MinNumberOfSuns = 1;
    public int MaxNumberOfSuns = 1;
    public float MaxRotationSpeed = 10f;
    public float MaxOrbitSpeed = 100f;
    public float MaxTiltAngle = 50f;
    [ReadOnly]
    public int NumberOfPlanets;
    [ReadOnly]
    public int NumberOfSuns;
    public float MaxPlanetDistance = 500f;
    public float MinPlanetDistance = 400f;

    [Header("Solar System Details")]
    [ReadOnly]
    public Planet[] Planets;    

    [Header("Planet Settings")]
    public int MaxBiomes = 3;
    public float MaxRadius = 20f;
    [Range(1, 8)]
    public int NumberNoiseLayerCount = 4;

    [Header("Static Noise Setting Scope")]
    public static float MinStrength = 0.1f;
    public static float MaxStrength = 1f;
    public static int MinNumLayers = 1;
    public static int MaxNumLayers = 8;
    public static float MinBaseRoughness = 0.1f;
    public static float MaxBaseRoughness = 1f;
    public static float MinRoughness = 0.1f;
    public static float MaxRoughness = 1f;
    public static float MinPersistence = 0.1f;
    public static float MaxPersistence = 1f;
    public static float MinWeightMultiplier = 0.1f;
    public static float MaxWeightMultiplier = 1f;

    [Header("Other Misc Values")]
    public int RandomSeedStringLength = 10;    

    // Prefabs
    public GameObject PlanetPrefab;
    
    // Private Values
    string[] Characters = new string[] {
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
    };
    int HashSeed;
    float CurrentDistance = 0;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        GeneratePlanets();

        for (int i = 0; i < Planets.Length; i++)
        {
            Planets[i].gameObject.GetComponent<Gravity>().enabled = false;
        }
        Debug.Log("Fully Setup!");
    }

    private void Init()
    {
        // Generate a random string seed if input is null or empty
        if (Seed == null || Seed.Length == 0) Seed = CreateRandomString(RandomSeedStringLength);
        HashSeed = Seed.GetHashCode();
        Random.InitState(HashSeed);

        // Generate Number of Planets
        NumberOfPlanets = Random.Range(MinNumberOfPlanets, MaxNumberOfPlanets);
        NumberOfSuns = Random.Range(MinNumberOfSuns, MaxNumberOfSuns);
    }

    private void GeneratePlanets()
    {
        Planets = new Planet[NumberOfPlanets];           

        for (int i = 0; i < Planets.Length; i++)
        {
            float dist = Random.Range(MinPlanetDistance, MaxPlanetDistance);
            CurrentDistance += dist;
            GameObject planetObj = Instantiate(PlanetPrefab, new Vector3(0, 0, 0), Quaternion.identity);            
            planetObj.name = "Planet: " + (i + 1);
            planetObj.transform.parent = GameObject.Find("[Planets]").transform;
            planetObj.transform.localPosition = new Vector3(CurrentDistance, 0, 0);            
            float RotationSpeed = Random.Range(-MaxRotationSpeed, MaxRotationSpeed);
            float OrbitSpeed = Random.Range(-MaxOrbitSpeed, MaxOrbitSpeed);
            float TiltAngle = Random.Range(-MaxTiltAngle, MaxTiltAngle);
            int BiomeCount = Random.Range(1, MaxBiomes);
            float Radius = Random.Range(1, MaxRadius);
            planetObj.GetComponent<Planet>().SetRotationSpeed(RotationSpeed);
            planetObj.GetComponent<Planet>().SetOrbitSpeed(OrbitSpeed);
            planetObj.GetComponent<Planet>().SetTiltAngle(TiltAngle);
            planetObj.GetComponent<Planet>().SetBiomeCount(BiomeCount);
            planetObj.GetComponent<Planet>().SetNoiseLayerCount(NumberNoiseLayerCount);
            planetObj.GetComponent<Planet>().SetPlanetRadius(Radius);
            planetObj.GetComponent<Planet>().Generate();                        
            Planets[i] = planetObj.GetComponent<Planet>();            
        }
    }

    private string CreateRandomString(int RandomSeedStringLength)
    {
        int StringLength = RandomSeedStringLength - 1;
        string RandomString = "";

        for (int i = 0; i <= StringLength; i++)
        {
            RandomString = RandomString + Characters[Random.Range(0, Characters.Length)];
        }
        return RandomString;
    }   
}
