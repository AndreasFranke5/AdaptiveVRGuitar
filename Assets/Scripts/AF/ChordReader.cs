using UnityEngine;
using System.IO.Ports;   // For serial
using UnityEngine.UI;    // For UI Text (optional)
using TMPro;             // For TextMeshProUI (optional)

public class ChordReader : MonoBehaviour
{
    [Header("Serial Port Settings")]
    public string portName = "COM7"; // e.g. "COM3" on Windows
    public int baudRate = 9600;

    private SerialPort serialPort;

    [Header("UI Elements")]
    public TextMeshProUGUI chordText;   // Show chord name
    public TextMeshProUGUI sensorText;  // Show raw sensor values

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
    public float zOffsetDot2 = 0.1f;  // so Dot2 isn't exactly on top of Dot1
    public float zOffsetDot3 = -0.1f; // just an example offset

    void Start()
    {  
        minX = -xRange * 0.5f;
        maxX = xRange * 0.5f;

        try
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.ReadTimeout = 50;
            serialPort.Open();
            Debug.Log("Serial Port opened successfully!");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to open port: " + e.Message);
        }
    }

    void Update()
    {
        if (serialPort == null) return;
        if (!serialPort.IsOpen) return;

        // Attempt to read a line: e.g. "A|80,60,40"
        try
        {
            string line = serialPort.ReadLine().Trim();
            ProcessData(line);
        }
        catch (System.Exception)
        {
            // timeouts / partial reads ignored
        }
    }

    void ProcessData(string dataLine)
    {
        if (!dataLine.Contains("|")) return;

        string[] parts = dataLine.Split('|');
        if (parts.Length < 2) return;

        string chordName = parts[0];
        string sensorPart = parts[1];

        // Crucial debug log to confirm chordName received:
        Debug.Log($"ChordName Received: {chordName}");

        // Set Chord text clearly:
        if (chordText != null)
            chordText.text = chordName;
        else
            Debug.LogWarning("ChordText is NOT assigned!");

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


    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
            serialPort.Close();
    }
}
