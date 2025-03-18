using UnityEngine;
using TMPro;
using System.IO.Ports;

public class ChordReader : MonoBehaviour
{
    public int baudRate = 9600;
    private SerialPort serialPort;
    private bool serialConnected = false;

    [Header("UI Elements")]
    public TextMeshProUGUI chordText;
    public TextMeshProUGUI sensorText;

    [Header("Finger Dots")]
    public Transform dot1, dot2, dot3;
    public float xRange = 1.0f;
    public float yRange = 1.0f;
    public float yPos = 0.0f;
    public float zOffsetDot1 = 0f;
    public float zOffsetDot2 = 0.1f;
    public float zOffsetDot3 = -0.1f;
     
     [Header("Audio Clips")]
    public AudioClip chordASound;
    public AudioClip chordDSound;
    public AudioClip chordESound;

    private AudioSource audioSource;

    public float yPosDot3 = 0.2f;  // Top dot
    public float yPosDot2 = 0.0f;  // Middle dot
    public float yPosDot1 = -0.2f; // Bottom dot

    private float timer = 0f;
    private int currentChord = 0;
    private float mockInterval = 3f;

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();

        string[] ports = SerialPort.GetPortNames();
        foreach (string port in ports)
        {
            try
            {
                serialPort = new SerialPort(port, baudRate);
                serialPort.ReadTimeout = 50;
                serialPort.Open();
                serialConnected = true;
                Debug.Log("Connected to: " + port);
                return;
            }
            catch { Debug.LogWarning($"Failed to open port: {port}"); }
        }
        if (!serialConnected) Debug.LogWarning("No COM ports detected. Running MOCK mode.");
    }

    void Update()
    {
        if (serialConnected)
        {
            try
            {
                string line = serialPort.ReadLine().Trim();
                ProcessRealData(line);
            }
            catch { }
        }
        // Keyboard input for playing chord sounds
    if (Input.GetKeyDown(KeyCode.A)) // Press 'A' key for chord A
    {
        PlayChordSound("A");
        MoveDotsForChord("A");
    }
    if (Input.GetKeyDown(KeyCode.D)) // Press 'D' key for chord D
    {
        PlayChordSound("D");
        MoveDotsForChord("D");
    }
    if (Input.GetKeyDown(KeyCode.E)) // Press 'E' key for chord E
    {
        PlayChordSound("E");
        MoveDotsForChord("E");
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

            PlayChordSound(chordName);

        string[] tokens = sensorPart.Split(',');
        if (tokens.Length == 3)
        {
            RunMockData();
        }
    }
    void PlayChordSound(string chordName)
    {
        switch (chordName)
        {
            case "A":
                audioSource.PlayOneShot(chordASound);
                break;
                case "D": // Add this case
            audioSource.PlayOneShot(chordDSound);
            break;
        case "E": // Add this case
            audioSource.PlayOneShot(chordESound);
            break;
            // Add more cases for other chords
            default:
                Debug.LogWarning("No sound assigned for chord: " + chordName);
                break;
        }
    }

    void ProcessRealData(string dataLine)
    {
        string[] tokens = dataLine.Split(',');
        if (tokens.Length != 3) return;

        int s1 = int.Parse(tokens[0]);
        int s2 = int.Parse(tokens[1]);
        int s3 = int.Parse(tokens[2]);

        sensorText.text = $"S1={s1}, S2={s2}, S3={s3}";

        string chord = DetectChord(s1, s2, s3);
        UpdateChordUI(chord);

        UpdateDot(dot1, s1, yRange / 2f);
        UpdateDot(dot2, s2, 0f);
        UpdateDot(dot3, s3, -yRange / 2f);
    }

    void RunMockData()
    {
        timer += Time.deltaTime;
        if (timer >= mockInterval)
        {
            timer = 0;
            currentChord = (currentChord + 1) % 3;
        }

        int s1 = 0, s2 = 0, s3 = 0;
        switch (currentChord)
        {
            case 0: s1 = 850; s2 = 650; s3 = 350; break; // A
            case 1: s1 = 750; s2 = 150; s3 = 650; break; // D
            case 2: s1 = 150; s2 = 250; s3 = 850; break; // E
        }

        sensorText.text = $"S1={s1}, S2={s2}, S3={s3}";
        string chord = DetectChord(s1, s2, s3);
        UpdateChordUI(chord);

        UpdateDot(dot1, s1, yRange / 2f);
        UpdateDot(dot2, s2, 0f);
        UpdateDot(dot3, s3, -yRange / 2f);
    }

    string DetectChord(int s1, int s2, int s3)
    {
        int MIN_DIFF = 100;

        if (s1 > s2 && s2 > s3 && (s1 - s3 >= MIN_DIFF))
            return "A";
        if (s1 > s3 && s3 > s2 && (s1 - s2 >= MIN_DIFF))
            return "D";
        if (s3 > s2 && s2 > s1 && (s3 - s1 >= MIN_DIFF))
            return "E";
        return "None";
    }

    void UpdateChordUI(string chord)
    {
        chordText.text = chord;

        switch (chord)
        {
            case "A": chordText.color = Color.green; break;
            case "D": chordText.color = Color.cyan; break;
            case "E": chordText.color = Color.yellow; break;
            default: chordText.color = Color.white; break;
        }
    }

    void UpdateDot(Transform dot, int sensorValue, float yPos)
    {
        float fraction = Mathf.Clamp01(sensorValue / 1023f);
        float localX = Mathf.Lerp(-xRange / 2f, xRange / 2f, fraction);
        dot.localPosition = new Vector3(localX, yPos, -0.1f);
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
            serialPort.Close();
    }
    void MoveDotsForChord(string chordName)
{
    int s1 = 0, s2 = 0, s3 = 0;

    // Define mock sensor values for each chord
    switch (chordName)
    {
        case "A":
            s1 = 850; s2 = 650; s3 = 350; // Example values for chord A
            break;
        case "D":
            s1 = 750; s2 = 150; s3 = 650; // Example values for chord D
            break;
        case "E":
            s1 = 150; s2 = 250; s3 = 850; // Example values for chord E
            break;
        default:
            return; // Do nothing for unrecognized chords
    }

    // Update the positions of the dots
    UpdateDot(dot1, s1, yRange / 2f);
    UpdateDot(dot2, s2, 0f);
    UpdateDot(dot3, s3, -yRange / 2f);
}


}
