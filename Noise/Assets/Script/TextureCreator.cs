using UnityEngine;
[UnityEngine.ExecuteInEditMode]

public class TextureCreator : MonoBehaviour
{
    [Range (2,1024)]
    public int resolution = 256;
    [Range (2,1024)]
    public float frequency = 32.0f;
    [Range(1, 3)]
    public int dimensions = 3;

    public NoiseMethodType type;

    public float rotationSpeed = 3.0f;

    private Texture2D texture;

    private void OnEnable()
    {
        if(texture == null)
        {
            texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true);
            texture.name = "Procedural Texture";
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Trilinear;
            texture.anisoLevel = 9;
            //GetComponent<MeshRenderer>().material.mainTexture = texture;
            GetComponent<MeshRenderer>().sharedMaterial.EnableKeyword("_NORMALMAP");
            GetComponent<MeshRenderer>().sharedMaterial.SetTexture("_BumpMap", texture);
            FillTexture();
        }
    
    }

    private void Update()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            FillTexture();
        }
        //transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    public void FillTexture()
    {
        if (texture.width != resolution)
        {
            texture.Resize(resolution, resolution);
        }

        Vector3 point00 = transform.TransformPoint(new Vector3(-0.5f, -0.5f));
        Vector3 point10 = transform.TransformPoint(new Vector3(0.5f, -0.5f));
        Vector3 point01 = transform.TransformPoint(new Vector3(-0.5f, 0.5f));
        Vector3 point11 = transform.TransformPoint(new Vector3(0.5f, 0.5f));

        //Ici nous choisissons quel bruit utilisé et quelles dimensions lui appliquer
        NoiseMethod method = Noise.noiseMethods[(int)type][dimensions - 1];
        float stepSize = 1f / resolution;
        Random.InitState(42);
        for (int y = 0; y < resolution; y++)
        {
            //Linear interpolation (Lerp)
            Vector3 point0 = Vector3.Lerp(point00, point01, (y + 0.5f) * stepSize);
            Vector3 point1 = Vector3.Lerp(point10, point11, (y + 0.5f) * stepSize);
            for (int x = 0; x < resolution; x++)
            {
                Vector3 point = Vector3.Lerp(point0, point1, (x + 0.5f) * stepSize);
                float sample = method(point, frequency);
                if (type != NoiseMethodType.Value)
                {
                    sample = sample * 0.5f + 0.5f;
                }
                texture.SetPixel(x, y, Color.white * sample);
            }
        }
        texture.Apply();
    }

}
