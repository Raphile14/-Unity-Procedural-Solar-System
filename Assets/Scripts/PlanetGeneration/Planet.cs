using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // Public Values
    [Header("Planet Settings")]
    public float RotationSpeed;
    public float OrbitSpeed;
    public float TiltAngle;
    public int BiomeCount;
    public int NoiseLayerCount;
    public float PlanetRadius;

    [Range(2, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back};
    public FaceRenderMask faceRenderMask;

    [Header("Requirements")]
    public Shader shader;
    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colorSettingsFoldout;

    // Private Values
    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    MeshCollider[] meshColliders;
    TerrainFace[] terrainFaces;
    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColorGenerator colorGenerator = new ColorGenerator();
    string[] FaceNames = { "Top", "Bottom", "Left", "Right", "Front", "Back" };

    public void Generate()
    {
        Initialize();
        GeneratePlanet();
    }    

    void Initialize()
    {        
        colorSettings = (ColorSettings) ScriptableObject.CreateInstance("ColorSettings");
        colorSettings.InitColorSettings(new Material(shader), BiomeCount);        
        shapeSettings = (ShapeSettings) ScriptableObject.CreateInstance("ShapeSettings");
        shapeSettings.InitShapeSettings(NoiseLayerCount, PlanetRadius);
        shapeGenerator.UpdateSettings(shapeSettings);
        colorGenerator.UpdateSettings(colorSettings);

        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        if (meshColliders == null || meshColliders.Length == 0)
        {
            meshColliders = new MeshCollider[6];
        }
        terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        if (transform != null)
        {
            for (int i = 0; i < 6; i++)
            {
                if (meshFilters[i] == null)
                {
                    GameObject meshObj = new GameObject("Face " + FaceNames[i]);
                    meshObj.transform.parent = transform;
                    meshObj.transform.localPosition = Vector3.zero;
                    meshObj.AddComponent<MeshRenderer>();                    
                    meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                    meshColliders[i] = meshObj.AddComponent<MeshCollider>();
                    meshColliders[i].convex = true;
                }
                if (meshFilters[i].sharedMesh == null)
                {
                    meshFilters[i].sharedMesh = new Mesh();
                }
                meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;

                terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, meshColliders[i], resolution, directions[i]);
                bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
                meshFilters[i].gameObject.SetActive(renderFace);                
            }
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColors();
    }

    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }

    public void OnColorSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColors();
        }
    }

    void GenerateMesh()
    {
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].ConstructMesh();
            }
        }

        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColors()
    {
        colorGenerator.UpdateColors();
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].UpdateUVs(colorGenerator);
            }
        }
    }

    public void SetRotationSpeed(float value)
    {
        RotationSpeed = value;
    }

    public void SetOrbitSpeed(float value)
    {
        OrbitSpeed = value;
    }

    public void SetTiltAngle(float value)
    {
        TiltAngle = value;
    }

    public void SetBiomeCount(int value)
    {
        BiomeCount = value;
    }

    public void SetNoiseLayerCount(int value)
    {
        NoiseLayerCount = value;
    }    

    public void SetPlanetRadius(float value)
    {
        PlanetRadius = value;
    }
}
