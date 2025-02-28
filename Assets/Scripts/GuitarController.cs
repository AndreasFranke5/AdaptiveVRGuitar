using UnityEngine;
using UnityEngine.XR;

public class GuitarController : MonoBehaviour
{
    public Transform fretboard;
    public XRNode controllerNode = XRNode.RightHand;
    private InputDevice controller;

    public AudioSource chordAudioSource;
    public AudioClip[] chordClips; // Assign different chord sounds in Unity Inspector
    void Start()
    {
        controller = InputDevices.GetDeviceAtXRNode(controllerNode);
    }

    // Update is called once per frame
    void Update()
    {
         if (controller.isValid)
        {
            bool triggerPressed;
            if (controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed) && triggerPressed)
            {
                PlayChord();
            }
        }
    }
    void PlayChord()
    {
        int randomChordIndex = Random.Range(0, chordClips.Length);
        chordAudioSource.clip = chordClips[randomChordIndex];
        chordAudioSource.Play();
    }
}
