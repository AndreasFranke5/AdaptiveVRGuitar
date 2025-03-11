using UnityEngine;
using System.IO.Ports;
public class SensorInput : MonoBehaviour
{
   [Header("Serial Settings")]
   public string portName = "COM3";
   public int baudRate = 9600;
   private SerialPort serialPort;
   [Header("Output Data")]
   public string currentChord = "None";
   public int sensor1, sensor2, sensor3;
    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open();
        serialPort.ReadTimeout = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (serialPort.IsOpen)
         {
            try
                {
                    string line = serialPort.ReadLine().Trim();
                    ProcessLine(line);
                }
        catch (System.Exception ex) 
        {
            Debug.LogError("Error reading from serial port: " + ex.Message);
        }
}

    }
    void ProcessLine(string line)
{
// Expecting "ChordName|val1,val2,val3"
if (!line.Contains("|")) return;
var parts = line.Split('|');
if (parts.Length < 2) return;
currentChord = parts[0]; // e.g. "A" or "None"
string sensorPart = parts[1]; // e.g. "60,50,55"
var vals = sensorPart.Split(',');
if (vals.Length != 3) return;
sensor1 = int.Parse(vals[0]);
sensor2 = int.Parse(vals[1]);
sensor3 = int.Parse(vals[2]);
}
void OnApplicationQuit()
{
if(serialPort!=null && serialPort.IsOpen)
serialPort.Close();
}

    
}
