# TicTacToe

An implementation of TicTacToe with a command line runner and automated computer AI players, who play moves at random.

The code has been written to demonstate loosely coupled, SOLID code and good unit testing practices.

There are different components for 

  * stopping and starting games see [GameInstanceManager](https://github.com/mcintyre321/TicTacToe/blob/master/TicTacToe/GameManagement/GameInstanceManager.cs)
    This means the Console app could be used to run other games implementing IGameInstance 
  * exposing possible moves to Player objects see [IMoveBasedGame](https://github.com/mcintyre321/TicTacToe/blob/master/TicTacToe/MoveBasedGames/IMoveBasedGame.cs) and [Player.cs](https://github.com/mcintyre321/TicTacToe/blob/master/TicTacToe/MoveBasedGames/Player.cs). 
    Player objects take a Strategy function for deciding which move to take, and take moves at their own discretion (triggered by a game loop in the Console application for automated players). This should make it easy to plug in other types of player, e.g. human players.

Rendering has been extracted but not yet decoupled. The game shouldn't really have responsibility for rendering itself rendering to text, as that ties it to a Console implementation. It's on the list of things to do!

PS This has been a great task as I previously wrote a [Chess rules engine](https://github.com/mcintyre321/ChessEngine) but never got around to writing a proper runtime for it :) I'm looking forwards to glueing them together.

