using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System.IO;

public class StarListGenerator : MonoBehaviour
{
    public TextAsset StarList;
    public string[] dataLines;
    public List<Star> Stars = new List<Star>();
    [Range(1, 100000)]
    [SerializeField] private int NumberOfStars;
    [Range(1, 100000)]
    [SerializeField] private float Size;
    public GameObject StarPrefab;
    public VisualEffect StarSystem;
    public Texture2D texture;
    // Start is called before the first frame update
    void Start()
    {
        dataLines = StarList.text.Split('\n');
        int lineNum = dataLines.Length;
        Debug.Log(lineNum + " Lines Processed");
        for (int count = 0; count < NumberOfStars - 1; count++)
        {
            string line = dataLines[count];
            //Debug.Log(line);
            string[] splitValues = line.Split(';');
            int HB_fromString = int.Parse(splitValues[0]);
            float Mag_fromString = float.Parse(splitValues[1]);
            string[] Right_A = splitValues[2].Split(' ');
            float RA_1 = float.Parse(Right_A[0]);
            float RA_2 = float.Parse(Right_A[1]);
            float RA_3 = float.Parse(Right_A[2]);
            string[] Dec_A = splitValues[3].Split(' ');
            float DE_1 = float.Parse(Dec_A[0]);
            float DE_2 = float.Parse(Dec_A[1]);
            float DE_3 = float.Parse(Dec_A[2]);
            Star newStar = new Star(HB_fromString, Mag_fromString, RA_1, RA_2, RA_3, DE_1, DE_2, DE_3);
            Stars.Add(newStar);

            // End do this on every frame
        }
            // Create texture
            texture = new Texture2D(1024,1024);
            // Set all of your particle positions in the texture
            var positions = new Color[Stars.Count];

            // Begin do this on every frame
            for (int i = 0; i < Stars.Count; i++)
            {
                Star current = Stars[i];
                float A = (float)((current.RA.x * 15) + (current.RA.y * 0.25) + (current.RA.z * 0.004166));
                float B = (Mathf.Abs(current.DEC.x) + (current.DEC.y / 60) + (current.DEC.z / 3600)) * Mathf.Sin(current.DEC.x);
                float X = (Size * Mathf.Cos(B)) * Mathf.Cos(A);
                float Y = (Size * Mathf.Cos(B)) * Mathf.Sin(A);
                float Z = Size * Mathf.Sin(B);
            Instantiate(StarPrefab, new Vector3(X, Y, Z), Quaternion.identity, transform);
                positions[i] = new Color(X,Y,Z);
                texture.SetPixel(i % texture.width, (int)i/texture.width, positions[i]);
                Debug.Log(positions[i].ToString());
            }
            texture.Apply();
            SaveTexture(texture);
            StarSystem.SetTexture("StarPosTexture", texture);
            StarSystem.Play();

    }
        private void SaveTexture(Texture2D texture)
        {
            byte[] bytes = texture.EncodeToPNG();
            var dirPath = Application.dataPath + "/RenderOutput";
            if (!System.IO.Directory.Exists(dirPath))
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }
            System.IO.File.WriteAllBytes(dirPath + "/R_" + Random.Range(0, 100000) + ".png", bytes);
            Debug.Log(bytes.Length / 1024 + "Kb was saved as: " + dirPath);
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
    }