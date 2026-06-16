# 🐾 Purrfect Half

**Purrfect Half** is a 2D cat adoption and shelter management game developed with the Unity Engine. The game focuses on the theme of finding the **“other half”** by matching shelter visitors with the most suitable cats based on their stories, personalities, and needs.

Players take the role of a shelter worker who analyzes each visitor’s request and helps them find their perfect feline companion. The main goal is to make successful adoptions, increase the shelter’s reputation, and keep the shelter running.

---

## 🕹️ Game Flow

The game consists of four main connected stages:

### 1. Shelter Scene / Main Hub

The shelter scene is the main hub of the game. In this scene, the player sees the general shelter environment and the visitors who come to adopt a cat. When the player is ready, they can start the matching process by clicking the **“Match”** button.

### 2. Matching Scene

The matching scene is the decision-making part of the game. The player reads the visitor’s story and compares it with the characteristics of the cats in the shelter. The objective is to choose the cat that best fits the visitor’s personality, lifestyle, and expectations.

### 3. Mini Game Scene

If the player makes the correct match, they are directed to a mini game scene. This mini game represents the bond-building process between the cat and the adopter. It adds an interactive challenge to the adoption process.

### 4. Result / Game Over Screen

If the mini game is completed successfully, the adoption is finalized and the player gains **15 reputation points**. After that, the game continues with a new visitor.

Wrong matches or failed mini games can decrease the reputation score. If the reputation score drops below zero, the game switches to the **Game Over** screen. From there, the player can restart and return to the shelter.

---

## 🚀 Technical Features

* **Game Engine:** Unity 6000.4.6f1
* **Programming Language:** C#
* **Game Type:** 2D cat adoption and shelter management game
* **Scene Management:** Dynamic scene transitions using Unity `SceneManager`
* **Core Gameplay Logic:** Story analysis, cat selection, mini game progression, and reputation system
* **Reputation System:** A score mechanic that represents the prestige and success of the shelter
* **Win/Lose Condition:** Successful adoptions increase the reputation score, while wrong choices and failed mini games can decrease it

---

## 📂 Scene Structure

The project includes the following main scenes:

| Scene Name      | Description                                                                |
| --------------- | -------------------------------------------------------------------------- |
| `GirisSahnesi`  | The intro and welcome screen of the game                                   |
| `purrfect`      | The main shelter area where visitors are accepted                          |
| `MatchingScene` | The interface where the player reads the visitor’s story and selects a cat |
| `MiniGame`      | A short reflex or skill-based mini game scene                              |
| `GameOverScene` | The screen shown when the reputation score drops below zero                |

---

## 🛠️ Installation and Running the Game

To run the project, follow these steps:

1. Download or clone the project from GitHub.

```bash
git clone <repository-link>
```

2. Open Unity Hub.

3. Add the project folder through Unity Hub.

4. Open the project with the correct Unity version.

```text
Unity Version: 6000.4.6f1
```

5. Open the following scene:

```text
Assets/purrfect.unity
```

6. Press the **Play** button in the Unity Editor to start the game.

---

## 🎮 How to Play

1. Start the game from the intro scene.
2. Enter the shelter scene.
3. View the visitor who wants to adopt a cat.
4. Click the **Match** button to go to the matching scene.
5. Read the visitor’s story carefully.
6. Analyze the available cats and their characteristics.
7. Choose the cat that best fits the visitor.
8. If the match is correct, play the mini game.
9. If the mini game is completed successfully, the adoption is finalized.
10. The shelter reputation increases and the next visitor appears.
11. If the reputation score drops below zero, the game ends.


## 🎨 Assets and Credits

The visual and interactive style of the game was created using a combination of original team-made assets and external asset packs.

### 🖼️ Graphics and Pixel Art

**Characters and Cats:**
Some of the character and cat designs were originally created by our team in pixel art style using Pixilart.

* Pixilart: https://www.pixilart.com/draw?ref=home-page

**Shelter and Scene Designs:**
The shelter, interior, and scene designs were supported by pixel art assets that fit the theme of the game.

* Animated Pixel Kittens Cats 32x32: https://last-tick.itch.io/animated-pixel-kittens-cats-32x32
* Cat Pixel Animations: https://toffeecraft.itch.io/pixel-cat-animations
* Cat Pixel Mega Pack: https://toffeecraft.itch.io/cat-pixel-mega-pack
* Modern Interiors: https://limezu.itch.io/moderninteriors

### 🧩 UI and Interface Elements

The game interface uses cat-themed UI elements and speech bubble assets.

* Cat User Interface: https://toffeecraft.itch.io/cat-user-interface?download
* Emote Speech Bubble 32p: https://pooklea.itch.io/emote-speech-bubble-32p


---

## 📌 Project Notes

* The game was developed using Unity.
* The in-game systems were implemented with C# scripts.
* Unity `SceneManager` was used for scene transitions.
* The game is based on story analysis and decision-making mechanics.
* The player’s performance is tracked through the **Reputation Score** system.
* The project includes multiple scenes, interactive UI, matching logic, a mini game, and a game over condition.

---

## 🐱 Short Description

**Purrfect Half** is a story-based 2D cat adoption game where the player helps shelter cats find their perfect owners. Each visitor has a unique story, and the player must analyze that story to make the most suitable cat-human match.
