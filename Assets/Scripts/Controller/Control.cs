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
    public float PlanetMaxDistance = 200f;
    public float PlanetMinDistance = 10f;

    [Header("Solar System Details")]
    [ReadOnly]
    public Planet[] Planets;
    public GameObject[] Guides;

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

    // Start is called before the first frame update
    void Start()
    {
        Init();
        GeneratePlanets();
        Debug.Log("Fully Setup!");
    }

    private void Update()
    {
        UpdatePlanets();
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
        Guides = new GameObject[Planets.Length];

        for (int i = 0; i < Planets.Length; i++)
        {
            GameObject planetGuide = new GameObject("Planet: " + (i + 1));            
            planetGuide.transform.parent = GameObject.Find("[Planets]").transform;
            GameObject planetObj = Instantiate(PlanetPrefab, new Vector3(Random.Range(-PlanetMaxDistance, PlanetMaxDistance), 0, 0), Quaternion.identity);            
            planetObj.name = "Model";
            planetObj.transform.parent = planetGuide.transform;
            planetObj.GetComponent<Planet>().Generate();
            float RotationSpeed = Random.Range(-MaxRotationSpeed, MaxRotationSpeed);
            float OrbitSpeed = Random.Range(-MaxOrbitSpeed, MaxOrbitSpeed);
            float TiltAngle = Random.Range(-MaxTiltAngle, MaxTiltAngle);
            planetObj.GetComponent<Planet>().SetRotationSpeed(RotationSpeed);
            planetObj.GetComponent<Planet>().SetOrbitSpeed(OrbitSpeed);
            planetObj.GetComponent<Planet>().SetTiltAngle(TiltAngle);            
            Planets[i] = planetObj.GetComponent<Planet>();
            Guides[i] = planetGuide;
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
    
    private void UpdatePlanets()
    {
        for (int i = 0; i < Planets.Length; i++)
        {
            Planets[i].transform.Rotate(0, Planets[i].RotationSpeed * Time.deltaTime, 0);
            Guides[i].transform.Rotate(0, Planets[i].OrbitSpeed * Time.deltaTime, 0);            
        }
    }
}
