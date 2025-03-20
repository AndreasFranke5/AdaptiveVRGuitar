# Adaptive Guitar

**Slogan:** Rock Out — One Hand Is Enough!

![Adaptive VR Guitar Logo](./images/AdaptiveGuitar.png)  

---

## Introduction

Adaptive Guitar is an innovative educational experience designed for individuals with one hand to learn and play the guitar. Our application uses a physical wooden fretboard equipped with three SoftPot membrane potentiometer sensors. An Arduino transmits real-time sensor data to our Unity 6 desktop app, which maps these values to accurately detect finger positions and play the corresponding chord sound. This design empowers users who face traditional accessibility challenges by allowing them to practice guitar with only one hand.

---

## Design Process

Our design process involved several key stages:

- **Group Discussion:**  
  We started by brainstorming and collaborating on ideas using tools like Google Draw. This session helped us capture diverse perspectives and lay the foundation for our project.

- **Storyline Development:**  
  We developed a clear narrative addressing the challenges faced by musicians with limited hand mobility and how our solution can overcome those barriers.

- **Hardware & Software Integration:**  
  Our prototype uses a wooden fretboard with three SoftPot sensors. The Arduino reads sensor values and sends them via serial communication to our Unity 6 desktop app, where chord detection is performed.

- **Idea Development & Future Vision:**  
  Although the current state of the app functions as a basic chord detector (acting like a virtual synth that plays chord sounds based on finger positions), we have numerous ideas for improvement. These include:
  - Implementing a complete lesson system with interactive menus (inspired by Ultimate Guitar tabs).
  - Expanding the chord detection logic with more advanced algorithms.
  - Adding wireless sensor integration (using ESP32) and haptic feedback for enhanced interactivity.

---

## System Description

### Features

- **One-Handed Guitar Practice:**  
  Users can form chord shapes on a physical fretboard and instantly hear the corresponding chord sound (A, D, or E).

- **Sensor-Driven Chord Detection:**  
  The app maps sensor values from the fretboard (using a 3x3 grid system) to detect valid chord shapes.

- **Interactive Feedback:**  
  Visual feedback is provided through moving dots on the screen that represent finger positions, along with audio feedback when a chord is recognized.

- **Future Expansion:**  
  We plan to add a complete song/lesson system, interactive menus, and expand the system to include additional chords, customization options, and even a wireless sensor solution with haptic feedback.

---

## Installation

### Prerequisites

- **Hardware:**  
  - Wooden fretboard with three SoftPot membrane potentiometers  
  - Arduino microcontroller and connection cables

- **Software:**  
  - Unity 6  
  - Arduino IDE

### Steps

1. **Clone the Repository:**  
   ```bash
   git clone https://github.com/yourusername/AdaptiveGuitar.git
   cd AdaptiveGuitar
   ```

2. **Open in Unity 6:**  
   - Launch Unity Hub and open the project.  
   - Make sure Unity 6 is used to maintain compatibility with our project.

3. **Upload the Arduino Sketch:**  
   - Open the Arduino folder and load the `sensors.ino` sketch in the Arduino IDE.  
   - Connect your Arduino and upload the sketch.

4. **Run the Project:**  
   - In Unity, press Play. The app will display a canvas with three dots representing finger positions on the fretboard.  
   - When you form a chord shape on the fretboard, the corresponding chord sound (A, D, or E) is played.

5. **(Optional) Use Mock Sensor Input:**  
   - The project includes mock sensor input functionality for testing if the hardware is unavailable.

---

## Usage

- **Playing the Guitar:**  
  Place your left-hand fingers on the physical fretboard to form a chord shape. The app detects the chord and plays the corresponding sound immediately.

- **Interactive Feedback:**  
  Visual feedback via moving dots helps you see the detected sensor values, ensuring your finger positions are correctly registered.

- **Planned Features:**  
  Future updates will include a complete lesson system with interactive menus, customizable settings, and more advanced analytics.

---

## References

- [Unity Documentation](https://docs.unity3d.com/)
- [Arduino Serial Communication](https://www.arduino.cc/)
- [Ultimate Guitar Tabs](https://www.ultimate-guitar.com/)

---

## Contributors

- **Sakib Ahsan Dipto** – [Contact/Portfolio Link]  
- **Anum Faisal** – [Contact/Portfolio Link]  
- **Andreas Franke** – [Contact/Portfolio Link]

---

## License

This project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for details.
