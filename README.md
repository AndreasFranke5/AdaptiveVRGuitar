# Adaptive VR Guitar

**Slogan:** Rock Out — One Hand Is Enough!

![Adaptive VR Guitar Logo](./images/AdaptiveVrGuitar.png)  

---

## Introduction

**Adaptive VR Guitar** is a university group project developed in **Unity 6** that reimagines guitar learning for people with limited hand mobility. Our goal is to create an immersive, accessible educational experience. Using a physical prototype—a wooden plank equipped with three SoftPot membrane potentiometer sensors (50 mm)—an Arduino board reads sensor data and sends it via serial communication to Unity. In our current prototype, the Unity scene displays a canvas with three dots that represent the sensor (finger) positions on a virtual fretboard.

At this stage, the app functions much like a virtual synthesizer: it simply shows the sensor data (the dots) without any lesson content, menus, or advanced interaction. We have many exciting ideas for improvement (such as implementing interactive lessons, tab-based chord learning, refined chord detection logic, haptic feedback, and a full song/lesson mode), but these features are not implemented yet.

---

## Design Process

### Group Discussion & Ideation

- **Brainstorming:** We held a group session (using tools like Google Draw) to discuss how to best adapt guitar learning for one-handed users.  
- **Initial Prototype:** Our first working prototype uses a wooden plank with three SoftPot sensors that transmit raw sensor values to Unity via Arduino.
- **Key Insight:** The current state displays three moving dots on a canvas that represent the sensor values. This serves as a foundation for future features such as lessons and interactive chord learning.

### Storyline & User Journey

- **Storyline:**  
  - **Beginning:** Introduce the challenge of learning guitar with limited hand mobility.
  - **Middle:** In future iterations, guide the user through interactive lessons (displaying chord shapes like A, D, and E in a tab-like format similar to Ultimate Guitar) where they play along with the lesson.
  - **End:** Provide feedback (audio, visual, and haptic) to help users master the correct chord shapes.
- **User Journey:**  
  1. The user starts in the VR scene and sees the virtual fretboard with three sensor dots.
  2. (Planned) In lesson mode, the app would scroll through chord shapes slowly, prompting the user to place their fingers correctly.
  3. (Planned) When a correct chord is detected, the app will play the corresponding chord sound and provide positive feedback; if incorrect, it will signal errors via visual cues and vibration.

### Transition & Interaction

- **Physical-to-Virtual Transition:**  
  Initially, we considered using a pocket fretboard, but we refined the concept to a wooden plank with sensors mounted on it. Touch interactions on the plank will eventually trigger the transition into lesson modes and interactive menus.
- **Interaction:**  
  Currently, the app behaves like a virtual synth by showing the sensor dots only. Future improvements will allow the user to interact with an evolving lesson system, where they simply place their fingers in the correct chord shape.

---

## System Description & Planned Features

### Current State

- **Hardware:**  
  - A wooden plank with **three SoftPot membrane potentiometers (50 mm)**.
  - An Arduino board that reads sensor values from analog pins A0, A1, and A2 and sends them via serial communication.
- **Software:**  
  - A **Unity 6** project that reads these sensor values and displays a canvas with **three dots** representing the finger positions on a virtual fretboard.
  - Basic chord detection logic (using a simple 3×3 grid system) is in place, but it currently only supports three chords (A, D, and E) and does not yet include lessons or menus.

### Planned Features (Not Yet Implemented)

- **Tab-Based Chord Learning System:**  
  - Display a static chord reference ("legend") showing valid chord shapes.
  - Implement a lesson mode where chords scroll slowly—similar to playing along with Ultimate Guitar tabs.
  - Provide positive feedback (audio, visual, and haptic) for correct chord shapes and negative feedback for errors.

- **Enhanced Chord Recognition:**  
  - Shift the chord detection logic fully to Unity so that Arduino only sends raw sensor values.
  - Map sensor values to a 3×3 grid with each sensor interpreted as being in one of three positions (Left, Middle, Right).
  - Support multiple valid variations for each chord (A, D, and E) as defined in our planned grid mappings.

- **Haptic Feedback Integration:**  
  - Use vibration motors (via Arduino) to give tactile feedback when a wrong chord is played.

- **Sound Integration:**  
  - Play corresponding chord audio clips (for A, D, and E) when the correct chord is detected.
  - Future expansions include smooth chord transitions and multiple difficulty levels.

- **Performance Tracking:**  
  - Implement a basic score system and display simple statistics (accuracy, streaks) on-screen.

- **Usability Enhancements & Simulation Mode:**  
  - Add mock sensor data functionality so that sensor input and smooth movement of the dots can be simulated without the physical hardware.
  - Optimize the UI by moving sensor dots above the fretboard, resizing them, and positioning the chord text and sensor values for clarity.

*Note: While we have many ideas for future improvements, the current prototype only functions as a basic virtual synth that displays sensor data.*

---

## Installation

### Prerequisites

- **Unity Hub** with **Unity Editor LTS 2022.3 or higher** (for Unity 6 projects)
- (Optional) An **Arduino** board with three SoftPot sensors for hardware testing

### Steps

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/yourusername/AdaptiveVRGuitar.git
   cd AdaptiveVRGuitar
   ```

2. **Open the Project in Unity:**
   - Launch **Unity Hub** and open the cloned project.

3. **(Optional) Switch Platform:**
   - For Android/Meta Quest: Go to **File > Build Settings**, select **Android**, and click **Switch Platform**.

4. **Configure XR Settings (if using VR):**
   - Navigate to **Project Settings > XR Plug-in Management** and enable your target XR platform (e.g., Oculus).

5. **Build & Run:**
   - In **Build Settings**, add your scene (e.g., `ChordTestScene.unity`).
   - Click **Build and Run** to deploy to your device.

6. **Arduino Setup (if using hardware):**
   - Connect the three SoftPot sensors to analog pins A0, A1, and A2.
   - Upload the `sensors.ino` sketch to your Arduino.
   - Adjust the serial port settings in the Unity scripts if needed.

---

## Usage

When you launch the app:

- **Navigation & Interaction:**
  - The app shows a virtual fretboard with three dots representing the sensor inputs.
  - Currently, the app functions as a virtual synth; it reads sensor values (or simulated mock data) and moves the dots accordingly.
  
- **Demo/Showcase Mode:**
  - To record a showcase video, enable the **mock sensor input mode** so that you can simulate smooth and fluid movement of the dots even when the physical hardware is unavailable.

- **Planned Future Features:**
  - A lesson mode that scrolls through chord shapes in a tab-like format.
  - Interactive menus, performance tracking, and refined chord detection with multiple valid variations.
  - Haptic feedback and advanced sound integration for an enhanced user experience.

---

## References

This project draws inspiration from several sources:
- **Guitar Hero, OSU, and Yousician** for interactive and engaging musical gameplay.
- **Ultimate Guitar tabs** for the static tab format used in lessons.
- Documentation and community projects on **Unity VR development** and **Arduino sensor integration**.

---

## Contributors

- **Sakib Ahsan Dipto**
- **Andreas Franke**
- **Anum Faisal**

*For more details, please contact our team or check out our individual portfolios.*

---

## License

This project is licensed under the **MIT License**. See the [LICENSE](./LICENSE) file for details.

---

*Note: This README reflects the current state of the project, which is a basic prototype acting as a virtual synth. Many planned improvements (interactive lessons, menus, refined chord detection, and additional feedback systems) remain to be implemented in future iterations.*
