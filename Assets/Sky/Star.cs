
using UnityEngine;
public class Star
{
    int HDnumber;
    public Vector3 RA;
    public Vector3 DEC;
    public float Magnitude;

    public Star(int HDnum, float magnitude, float A1, float A2, float A3, float D1, float D2, float D3)
    {
        RA = new Vector3(A1, A2, A3);
        DEC = new Vector3(D1, D2, D3);
        HDnumber = HDnum;
        Magnitude = magnitude;
    }
    public override string ToString()
    {
        return $"Star {HDnumber} with Magnitude {Magnitude} is at {RA.x}, {RA.y}, {RA.z} and {DEC.x}, {DEC.y}, {DEC.z}";
    }
}