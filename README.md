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

1. **Clone the repository**
   ```bash
   git clone https://github.com/ZixiaoZhou/2300-Assign3.git
   cd 2300-Assign3
