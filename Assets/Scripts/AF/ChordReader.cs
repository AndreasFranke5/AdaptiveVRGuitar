using UnityEngine;
using TMPro;
using System.IO.Ports;

public class ChordReader : MonoBehaviour
{
    [Header("Serial Settings")]
    public string portName = "COM7";
    public int baudRate = 9600;
    private SerialPort serialPort;

    [Header("UI Elements")]
    public TextMeshProUGUI chordText;
    public TextMeshProUGUI sensorText;

    [Header("Finger Dot Transforms")]
    // Assign these in the Inspector, e.g. Spheres named Dot1, Dot2, Dot3
    public Transform dot1;
    public Transform dot2;
    public Transform dot3;

    [Header("Mapping Settings")]
    // We'll map sensor values 0..1023 -> fraction 0..1 -> localX from -0.5..+0.5
    public float xRange = 1.0f;  // total width for mapping
    private float minX;
    private float maxX;

    public float yPos = 0.0f;  // keep them on the same y-level
    public float zOffsetDot1 = 0f;
    public float zOffsetDot2 = 0.1f;
    public float zOffsetDot3 = -0.1f;

    void Start()
    {  
        minX = -xRange * 0.5f;
        maxX = xRange * 0.5f;

        try
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.ReadTimeout = 50;
            serialPort.Open();
            Debug.Log("Serial port successfully opened.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed opening port: " + e.Message);
        }
    }

    void Update()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string line = serialPort.ReadLine().Trim();
                ProcessData(line);
            }
            catch { }
        }
    }

    void ProcessData(string dataLine)
    {
        if (!dataLine.Contains("|")) return;

        string[] parts = dataLine.Split('|');
        if (parts.Length < 2) return;

        string sensorPart = parts[0];
        string chordName = parts[1];

        if (chordText != null)
            chordText.text = chordName;

        string[] tokens = sensorPart.Split(',');
        if (tokens.Length == 3)
        {
            int s1 = int.Parse(tokens[0]);
            int s2 = int.Parse(tokens[1]);
            int s3 = int.Parse(tokens[2]);

            if (sensorText != null)
                sensorText.text = $"S1={s1}, S2={s2}, S3={s3}";

            // Sensor to fraction
            float f1 = Mathf.Clamp01(s1 / 1023f);
            float f2 = Mathf.Clamp01(s2 / 1023f);
            float f3 = Mathf.Clamp01(s3 / 1023f);

            Debug.Log($"Fractions calculated: {f1}, {f2}, {f3}");

            // Positions
            if (dot1) dot1.localPosition = new Vector3(Mathf.Lerp(minX, maxX, f1), yPos, zOffsetDot1);
            if (dot2) dot2.localPosition = new Vector3(Mathf.Lerp(minX, maxX, f2), yPos, zOffsetDot2);
            if (dot3) dot3.localPosition = new Vector3(Mathf.Lerp(minX, maxX, f3), yPos, zOffsetDot3);
        }
    }

    void UpdateDot(Transform dot, int sensorValue, float zOffset)
    {
        float fraction = Mathf.Clamp01(sensorValue / 1023f);
        float localX = Mathf.Lerp(-xRange / 2, xRange / 2, fraction);
        dot.localPosition = new Vector3(localX, yPos, zOffset);
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
            serialPort.Close();
    }
}
