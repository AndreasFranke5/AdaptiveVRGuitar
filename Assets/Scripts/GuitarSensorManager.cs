using UnityEngine;

public class GuitarSensorManager : MonoBehaviour
{
    public AudioSource chordA;
    public AudioSource chordD;
    public AudioSource chordE;
    private SensorInput sensorScript;
    private string lastChord = "None";
    void Start()
    {
        sensorScript = GetComponent<SensorInput>();
    }

    // Update is called once per frame
    void Update()
    {
        string chordNow = sensorScript.currentChord;
       if (chordNow != lastChord && chordNow != "None")
           {
             PlayChordAudio(chordNow);
           }
            lastChord = chordNow;
         }
         void PlayChordAudio(string chordName)
     { 
        switch(chordName)
        {
            case "A": if(!chordA.isPlaying) chordA.Play(); break;
            case "D": if(!chordD.isPlaying) chordD.Play(); break;
            case "E": if(!chordE.isPlaying) chordE.Play(); break;
        
        }
 }    

}
