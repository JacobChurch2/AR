# Berry Big Trouble

## Team Members
- Jacob Church 🍇
- Jacorey Rowe 🍎
- Cameron Sadusky 🍓

## Project Description
Berry Big Trouble is an AR-powered Unity game where you play as a little bear collecting and eating fruits while avoiding and fighting off enemies. Inspired by Pac-Man, the game challenges you to survive as long as possible, using power-ups to turn the tables on your foes. The game creates an immersive AR experience where players interact with virtual objects in their real-world environment.

### Use Case
This AR game demonstrates interactive 3D gameplay in augmented reality, showcasing how traditional gaming mechanics can be adapted for AR platforms. It serves as a proof-of-concept for location-based AR gaming experiences.

## Technical Details

### AR Framework/SDK
- **Unity AR Foundation**: Core AR framework providing cross-platform AR functionality
- **ARCore (Android)**: Google's AR platform for Android devices
- **ARKit (iOS)**: Apple's AR platform for iOS devices (when deployed)

### Tracking Techniques Implemented
- **Plane Detection**: Automatically detects horizontal surfaces in the real world for game object placement
- **Image Tracking**: Uses reference images/markers for precise object positioning
- **World Tracking**: Maintains spatial awareness and object persistence in the AR environment
- **Touch Interaction**: Direct touch controls for player movement and interaction

### Development Tools & Platforms
- **Unity 2022.3 LTS**: Primary game engine
- **Universal Render Pipeline (URP)**: Graphics pipeline optimized for mobile AR
- **XR Interaction Toolkit**: Advanced interaction system for AR/VR
- **Visual Studio/Visual Studio Code**: Code editing and debugging
- **GitHub**: Version control and project hosting
- **Target Platforms**: Android (primary), iOS (compatible)

## Gameplay
- **Player:** Control a bear in an AR environment.
- **Objective:** Collect all the fruits (blueberries, strawberries, apples) to win.
- **Enemies:** Bees and bats chase the player. Contact with them is fatal unless they are vulnerable.
- **Power-up:** Collecting a blueberry makes enemies vulnerable for a short time.
- **Game Over:** Touching an enemy (when not vulnerable) ends the game.

## Game Flow
1. **Main Menu:** Start or Quit.
2. **Gameplay:** Collect fruit, avoid/fight enemies.
3. **Game Over (Loss):** Triggered by enemy contact. Options: Return to Menu or Quit.
4. **Game Over (Win):** Triggered by collecting all fruit. Options: Return to Menu or Quit.

## Controls
- **Move:** Touch and drag on screen to move the bear character
- **Collect:** Walk into fruit to automatically collect it
- **Power-up:** Collect blueberries to temporarily make enemies vulnerable

## Instructions to Run the Project

### Prerequisites
- Unity 2022.3 LTS or later
- Android device with ARCore support (Google Pixel 3 or newer, Samsung Galaxy S8 or newer)
- USB cable for device connection
- Android Build Support module installed in Unity

### Setup Instructions
1. **Clone the Repository**
   ```bash
   git clone [GitHub repository URL]
   cd AR
   ```

2. **Open in Unity**
   - Launch Unity Hub
   - Click "Open" and select the AR project folder
   - Wait for Unity to import all assets and packages

3. **Verify Package Dependencies**
   - Go to Window > Package Manager
   - Ensure the following packages are installed:
     - AR Foundation (latest version)
     - XR Interaction Toolkit (latest version)
     - Universal Render Pipeline (latest version)
     - ARCore XR Plugin (for Android)

4. **Configure Build Settings**
   - Go to File > Build Settings
   - Select "Android" as the target platform
   - Click "Switch Platform" if needed
   - In Player Settings (Edit > Project Settings > Player):
     - Set Company Name and Product Name
     - Enable "ARCore" in XR Plug-in Management
     - Set minimum API level to Android 7.0 (API level 24)

5. **Build and Deploy**
   - Connect your Android device via USB
   - Enable Developer Options and USB Debugging on your device
   - In Unity, go to File > Build Settings
   - Click "Build and Run" or "Build" to create an APK
   - Install the APK on your device

6. **Running the Game**
   - Launch the app on your AR-capable device
   - Grant camera permissions when prompted
   - Point your camera at a flat surface
   - Tap "Start Game" to begin playing
   - Use touch controls to move the bear and collect fruits

### Troubleshooting
- **AR not working**: Ensure your device supports ARCore and has a clear, well-lit environment
- **Build errors**: Verify all required packages are installed and Android SDK is properly configured
- **Performance issues**: Close other apps and ensure adequate lighting for better AR tracking

## Screenshots and Demo

### Screenshots
[Add screenshots of the game running on device here]

### Demo GIFs
[Add GIFs showing gameplay in action here]

### Video Demo
[Add link to video demonstration here]

## Project Structure
- `Assets/AR/Scripts/` - Core game and AR scripts
  - `GameManager.cs` - Main game state management
  - `ARPlaceObject.cs` - AR object placement logic
  - `UI/` - User interface scripts
- `Assets/AR/Scenes/` - Main scenes (menus, gameplay)
- `Assets/AR/Prefabs/` - Game objects and UI prefabs
- `Assets/AR/Models/` - 3D models for fruits and characters
- `Assets/AR _Marker/` - AR tracking markers and reference images

## Features Implemented
- ✅ AR plane detection and object placement
- ✅ Touch-based player movement
- ✅ Enemy AI with chase behavior
- ✅ Power-up system (blueberry vulnerability)
- ✅ Score tracking and lives system
- ✅ Game state management
- ✅ Cross-scene navigation
- ✅ Mobile-optimized UI

## Future Enhancements
- Multiplayer AR support
- More complex enemy AI patterns
- Additional power-ups and game modes
- Sound effects and background music
- Particle effects and visual polish

## License
This project is created for educational purposes as part of a university course. All assets and code are property of the development team.

---

**Note**: This AR experience requires a device with ARCore support and adequate lighting conditions for optimal performance.
