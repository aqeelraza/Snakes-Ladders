A very simple game made in Unity3d version 2018.3.1f.

A basic descripition of the script is following. 

UIHandler.cs
This handles all the UI events and updates the game states.This basically sends events to GameManger to run the game.


GameStateManager.cs
This keeps the record of the game states.


GameManager.cs
This is the main class to handle the game logic. This handles all the managers in the game.You cannot call this class and rather fire events in the class.

PlayerManager.cs
This class controlls the behaviour of players. Right now only two players can play the game and players are being handled here.

Player.cs
This class is for the functionality of the player. 


