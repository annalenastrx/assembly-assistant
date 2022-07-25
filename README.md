# Assembly Assistant Unity Project

## Introduction
This project was part of a master's thesis to investigate whether 3D virtual assistants that point into the real world improve the interaction experience. For this purpose, a user study was conducted to compare two display types - 2D flat display and 3D spherical FTVR display - as well as two instruction modes - pointing and showing virtual pieces by holding them up. This repo only contains the flat display project.

In order to conduct the user study, an assembly assistant was implemented in Unity with the ability to explain assembly steps using voice instructions and gestures. Depending on the condition that is selected in the application, the assistant either explains an assembly step by using voice while pointing at a piece and target position or while holding a virtual representation of the piece up in front of its body.

Audio files are not included.

## Setup
To start the Unity project, download and open it using Unity version 2018.1.7f. Due to limitations of the spherical FTVR display, this project was created using an older Unity version. 

### Controls
To avoid recognition errors that influence experiment results, the assistant was implemented in a wizard of oz way. This means that instead of including object recognition, the assistant was controlled remotely using a remote client. For better testing, the control functionalities were also included in the main project.

#### 1. To select conditions:

Rin > AvatarController (script) > enter number between 1 and 4 to determine condition order and models (when pointing is 1, in first round only pointing can be started)

#### 2. To control the avatar:

Pointing:
- Start pointing + pointing target: mouse click on dummy table
- End pointing: down arrow

Holding virtual pieces up:
- Start next instruction: space
- Previous instruction: left arrow


