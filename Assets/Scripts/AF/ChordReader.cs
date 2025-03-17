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

    [Header("Finger Dots")]
    public Transform dot1, dot2, dot3;
    public float xRange = 1.0f;
    public float yPos = 0.0f;
    public float zOffsetDot1 = 0f;
    public float zOffsetDot2 = 0.1f;
    public float zOffsetDot3 = -0.1f;

    void Start()
    {
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

            UpdateDot(dot1, s1, 0f);
            UpdateDot(dot2, s2, zOffset: 0.1f);
            UpdateDot(dot3, s3, zOffset: -0.1f);
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
