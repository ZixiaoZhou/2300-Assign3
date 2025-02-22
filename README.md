# 🎈 Bubble Clicker - MonoGame Edition

Bubble Clicker is a **MonoGame-based bubble-clicking game** where players must click as many bubbles as possible within **1 minute** to score points. The game has **two levels**:
- **Level 1**: Only bubbles appear. Clicking them gives points.
- **Level 2**: Both bubbles and **bombs** appear. Clicking bombs **deducts points**.

At the end of the game, the **leaderboard ranks players** based on **score & number of clicks**, testing their reflexes! 🚀

---

## 🎮 Game Flow

1. **Enter Level 1**, with a **60-second countdown**.
2. **Click bubbles to earn points**.
3. **After Level 1 ends**, the game **automatically transitions to Level 2**.
4. **Level 2 introduces bombs**:
   - **Clicking a bubble ➜ + points**
   - **Clicking a bomb ➜ - points**
5. **When the timer runs out, the game ends**, and the leaderboard is displayed.

---

## 🛠 Tech Stack

- **Game Engine**: MonoGame (C#)
- **Physics System**: Custom collision detection in MonoGame
- **Timer & UI**: MonoGame built-in timer and sprite rendering
- **Leaderboard Storage**: Local file (JSON/XML) or database (SQLite)
- **Sound & Animations**: MonoGame SoundEffect & Sprite Animation

---

## 🚀 Installation & Run Instructions

### **Step 1: Download the Game**
1. Go to the **[Releases Page](https://github.com/ZixiaoZhou/2300-Assign3/releases)**
2. Download the latest version: **`BubbleClicker_vX.X.zip`**
3. Extract the ZIP file to a folder on your computer.

### **Step 2: Run the Game**
1. Open the extracted folder.
2. Double-click **`final.exe`** to start the game.
3. Enjoy playing!

---

## 🎯 Key Features

- ✅ **Two-Level Challenge**: Standard mode & bomb mode
- 🎈 **Smooth Click Interaction**: Precise input detection using MonoGame
- ⏳ **60-Second Countdown**: Adds tension and excitement
- 🔥 **Real-Time Leaderboard**: Tracks score & click count
- 🎶 **Sound Effects & Animations**: Enhances immersion
- 🎨 **Custom UI Rendering**: Sprite-based UI system

---

## 📝 Future Enhancements (TODO)

- 🌍 **More Levels**: Increased difficulty, special effects
- 🎭 **Customizable Skins & Themes**: Unique bubble & bomb designs
- 🏆 **Online Leaderboard**: Store player scores in a remote database
- 📱 **Mobile Support**: Touchscreen gameplay support


