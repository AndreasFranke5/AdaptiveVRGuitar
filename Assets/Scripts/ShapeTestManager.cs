using UnityEngine;
using UnityEngine.UI;
public class ShapeTestManager : MonoBehaviour
{
    public Text chordText;
    public Transform dot1, dot2, dot3; // small spheres
    public Transform fretboard; 

    public float length = 2f; // map sensor fraction to local X
    public float offsetX = -1f;
    private SensorInput sensorInput;

    void Start()
    {
        sensorInput = GetComponent<SensorInput>();
        if(chordText) chordText.text = "None";
    }

    // Update is called once per frame
    void Update()
    {
        string chord = sensorInput.currentChord;
        if(chordText) chordText.text = chord;
// 2) Map sensor values to [0..1]
        float f1 = Mathf.Clamp01(sensorInput.sensor1 / 1023f);
        float f2 = Mathf.Clamp01(sensorInput.sensor2 / 1023f);
        float f3 = Mathf.Clamp01(sensorInput.sensor3 / 1023f);
// 3) Place each dot
        UpdateDot(dot1, f1);
        UpdateDot(dot2, f2);
        UpdateDot(dot3, f3);
    }

    void UpdateDot(Transform dot, float fraction)
    {
      if (!dot || !fretboard) return;
// local X from fraction
      float localX = offsetX + fraction * length;
      float localZ = 0f; // keep them in a line, or vary for clarity
// place dot in fretboard local space
      Vector3 localPos = new Vector3(localX, 0.02f, localZ);
// transform to world
    dot.position = fretboard.TransformPoint(localPos);
}

}
