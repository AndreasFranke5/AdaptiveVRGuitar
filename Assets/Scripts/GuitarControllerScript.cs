using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;

public class GuitarControllerScript : MonoBehaviour
{
    SerialPort serialPort;
    private string lastChord = "None";  // Prevents duplicate triggering
    public bool useSimulation = true;  // Toggle between real hardware and simulation

    public AudioSource chordA;
    public AudioSource chordD;
    public AudioSource chordE;
    public Text debugText;

    private float simulationTimer = 0f;
    private string simulatedData = "50,50,50"; // Default simulated data (Chord A)

    void Start()
    {
         if (!useSimulation)
        {
            serialPort = new SerialPort("COM3", 9600); // Update COM port if needed
            serialPort.Open();
            serialPort.ReadTimeout = 50;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (useSimulation)
        {
            SimulateSensorData();
        }
        else if (serialPort.IsOpen)
        {
            try
            {
                string receivedData = serialPort.ReadLine().Trim();
                if (!string.IsNullOrEmpty(receivedData))
                {
                    ProcessSensorData(receivedData);
                }
            }
            catch (System.Exception) { }
        }

    }
    void SimulateSensorData()
    {
        // Simulate different chords every 3 seconds
        simulationTimer += Time.deltaTime;
        if (simulationTimer > 3f)
        {
            simulationTimer = 0f;
            int randomChord = Random.Range(0, 3);

            switch (randomChord)
            {
                case 0: simulatedData = "50,50,50"; break; // Simulate A Chord
                case 1: simulatedData = "100,10,100"; break; // Simulate D Chord
                case 2: simulatedData = "10,100,10"; break; // Simulate E Chord
            }
        }

        ProcessSensorData(simulatedData);
    }
    void ProcessSensorData(string data)
    {
        string[] values = data.Split(',');
        if (values.Length != 3) return;  // Ensure 3 sensor values are received

        int sensor1 = int.Parse(values[0]);
        int sensor2 = int.Parse(values[1]);
        int sensor3 = int.Parse(values[2]);

        if (debugText != null)
        {
            debugText.text = $"S1: {sensor1} | S2: {sensor2} | S3: {sensor3}";
        }

        string detectedChord = DetectChord(sensor1, sensor2, sensor3);

        if (detectedChord != lastChord && detectedChord != "None")
        {
            PlayChord(detectedChord);
            lastChord = detectedChord;
        }
    }
     string DetectChord(int s1, int s2, int s3)
    {
        if (s1 == 100 && s2 == 10 && s3 == 100)
            return "D";  // D Chord (high-low-high)
        if (s1 == 50 && s2 == 50 && s3 == 50)
            return "A";  // A Chord (all equal)
        if (s1 == 10 && s2 == 100 && s3 == 10)
            return "E";  // E Chord (low-high-low)

        return "None";
    }
    void PlayChord(string chord)
    {
        if (chord == "A" && !chordA.isPlaying) chordA.Play();
        else if (chord == "D" && !chordD.isPlaying) chordD.Play();
        else if (chord == "E" && !chordE.isPlaying) chordE.Play();
    }
     void OnApplicationQuit()
    {
        if (!useSimulation && serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }






}
