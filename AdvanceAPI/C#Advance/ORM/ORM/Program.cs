using System;
using ORM.DTO;
using ORM.Model.Enum;
using ORM.POCO;
using ORM.ServiceLayer;
using System.Data;
using ORM.Data;

namespace ORM
{
    public class Program
    {
        ///<summary>
        /// Main method to handle user input, model selection, and operation selection
        /// for both Game and Player models.
        ///</summary>
        static void Main(string[] args)
        {
            // Initialize service layer for Player and Game operations
            PLAServiceLayer playerService = new PLAServiceLayer();
            GAMServiceLayer gameService = new GAMServiceLayer();

            ///<summary>
            /// Prompt the user to select the model type: Game or Player.
            ///</summary>
            Console.WriteLine("Select Role: ");
            Console.WriteLine("1. Game");
            Console.WriteLine("2. Player");

            ///<summary>
            /// Validate user input for model selection (1 or 2).
            ///</summary>
            if (!int.TryParse(Console.ReadLine(), out int modelInput))
            {
                Console.WriteLine("Invalid input. Please enter 1 or 2.");
                return;
            }

            ///<summary>
            /// Parse the model input into the ModelType enum and display the selected model type.
            ///</summary>
            if (Enum.TryParse<ModelType>(modelInput.ToString(), out ModelType model))
            {
                Console.WriteLine($"Selected model type: {model}");

                ///<summary>
                /// Prompt the user to select an operation: Add, Update, or Delete.
                ///</summary>
                Console.WriteLine("Select Operation: ");
                Console.WriteLine("1. Add");
                Console.WriteLine("2. Update");
                Console.WriteLine("3. Delete");

                string operationInput = Console.ReadLine()?.ToUpper();

                ///<summary>
                /// Parse the operation input into the OperationType enum.
                ///</summary>
                if (Enum.TryParse<OperationType>(operationInput.ToString(), out OperationType operation))
                {
                    Console.WriteLine($"Selected Operation: {operation}");

                    ///<summary>
                    /// Switch based on the selected model (Game or Player).
                    ///</summary>
                    switch (model)
                    {
                        case ModelType.Game:
                            ///<summary>
                            /// Switch based on the selected operation for Game model.
                            ///</summary>
                            switch (operation)
                            {
                                ///<summary>
                                /// Handle Add operation for Game: Collect input, validate, and save game.
                                ///</summary>
                                case OperationType.Add:
                                    Console.WriteLine("\nAdd Game:");
                                    Console.Write("Enter game name: ");
                                    string gameName = Console.ReadLine();
                                    Console.Write("Enter number of players required for game: ");
                                    int gameNoOfPlayers = int.Parse(Console.ReadLine());

                                    DTOGAM01 gameDto = new DTOGAM01
                                    {
                                        M02102 = gameName,
                                        M03103 = gameNoOfPlayers,
                                    };

                                    GAM01 gameModel = gameService.PreSaveGame(gameDto);

                                    ///<summary>
                                    /// Validate the game data before saving.
                                    ///</summary>
                                    var (isValidGame, gameValidationMessage) = gameService.ValidateOnSaveGame(gameModel);

                                    ///<summary>
                                    /// If valid, save the game and display success message. Otherwise, display validation message.
                                    ///</summary>
                                    if (isValidGame)
                                    {
                                        Response response = gameService.SaveGame(gameModel);
                                        Console.WriteLine(response.Message);
                                    }
                                    else
                                    {
                                        Console.WriteLine(gameValidationMessage);
                                    }
                                    break;

                                ///<summary>
                                /// Handle Update operation for Game: Fetch game, update details, validate, and save.
                                ///</summary>
                                case OperationType.Update:
                                    Console.WriteLine("\nUpdate Game:");
                                    Console.Write("Enter game Id for update process: ");
                                    int updateGameId = int.Parse(Console.ReadLine());

                                    GAM01 editGameModel = gameService.preDeleteGame(updateGameId);

                                    ///<summary>
                                    /// If the game is found, allow user to update details, validate, and save.
                                    ///</summary>
                                    if (editGameModel != null)
                                    {
                                        Console.WriteLine($"Editing Game: {editGameModel.M02F02}");

                                        Console.Write("Enter game new name: ");
                                        editGameModel.M02F02 = Console.ReadLine();

                                        Console.Write("Enter game updated number of players required for game: ");
                                        editGameModel.M03F03 = int.Parse(Console.ReadLine());

                                        var (isValidUpdateGame, updateGameValidationMessage) = gameService.ValidateOnSaveGame(editGameModel);

                                        if (isValidUpdateGame)
                                        {
                                            Response response = gameService.SaveGame(editGameModel);
                                            Console.WriteLine(response.Message);
                                        }
                                        else
                                        {
                                            Console.WriteLine(updateGameValidationMessage);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Game not found.");
                                    }
                                    break;

                                ///<summary>
                                /// Handle Delete operation for Game: Validate and delete game by ID.
                                ///</summary>
                                case OperationType.Delete:
                                    Console.WriteLine("\nDelete game:");
                                    Console.Write("Enter game Id to delete: ");
                                    int deleteGameId = int.Parse(Console.ReadLine());

                                    GAM01 preDeleteGameModel = gameService.preDeleteGame(deleteGameId);

                                    var (isValidDeleteGame, deleteGameValidationMessage) = gameService.ValidateOnDeleteGame(preDeleteGameModel);

                                    if (isValidDeleteGame)
                                    {
                                        Response response = gameService.DeleteGame(deleteGameId);
                                        Console.WriteLine(response.Message);
                                    }
                                    else
                                    {
                                        Console.WriteLine(deleteGameValidationMessage);
                                    }
                                    break;
                            }
                            break;

                        case ModelType.Player:
                            ///<summary>
                            /// Switch based on the selected operation for Player model.
                            ///</summary>
                            switch (operation)
                            {
                                ///<summary>
                                /// Handle Add operation for Player: Collect input, validate, and save player.
                                ///</summary>
                                case OperationType.Add:
                                    Console.WriteLine("\nAdd Player:");
                                    Console.Write("Enter player Name: ");
                                    string playerName = Console.ReadLine();
                                    Console.Write("Enter player Email: ");
                                    string playerEmail = Console.ReadLine();
                                    Console.Write("Enter player Team Name: ");
                                    string playerTeamName = Console.ReadLine();
                                    Console.Write("Enter Game Id: ");
                                    int playerGameId = int.Parse(Console.ReadLine());

                                    DTOPLA01 playerDto = new DTOPLA01
                                    {
                                        A02102 = playerName,
                                        A03103 = playerEmail,
                                        A04104 = playerTeamName,
                                        A05105 = playerGameId,
                                    };

                                    PLA01 playerModel = playerService.PreSavePlayer(playerDto);
                                    var (isValidPlayer, playerValidationMessage) = playerService.ValidateOnSavePlayer(playerModel);

                                    ///<summary>
                                    /// If valid, save player and display success message. Otherwise, display validation message.
                                    ///</summary>
                                    if (isValidPlayer)
                                    {
                                        Response response = playerService.SavePlayer(playerModel);
                                        Console.WriteLine(response.Message);
                                    }
                                    else
                                    {
                                        Console.WriteLine(playerValidationMessage);
                                    }
                                    break;

                                ///<summary>
                                /// Handle Update operation for Player: Fetch player, update details, validate, and save.
                                ///</summary>
                                case OperationType.Update:
                                    Console.WriteLine("\nUpdate Player:");
                                    Console.Write("Enter player Id for update process: ");
                                    int updatePlayerId = int.Parse(Console.ReadLine());

                                    PLA01 editPlayerModel = playerService.PreDeletePlayer(updatePlayerId);

                                    ///<summary>
                                    /// If the player is found, allow user to update details, validate, and save.
                                    ///</summary>
                                    if (editPlayerModel != null)
                                    {
                                        Console.WriteLine($"Editing Player: {editPlayerModel.A02F02}");

                                        Console.Write("Enter player new name: ");
                                        editPlayerModel.A02F02 = Console.ReadLine();
                                        Console.Write("Enter player new email: ");
                                        editPlayerModel.A03F03 = Console.ReadLine();
                                        Console.Write("Enter player new team name: ");
                                        editPlayerModel.A04F04 = Console.ReadLine();
                                        Console.Write("Enter player new game id: ");
                                        editPlayerModel.A05F05 = int.Parse(Console.ReadLine());

                                        var (isValidEditPlayer, editPlayerValidationMessage) = playerService.ValidateOnSavePlayer(editPlayerModel);

                                        if (isValidEditPlayer)
                                        {
                                            Response response = playerService.SavePlayer(editPlayerModel);
                                            Console.WriteLine(response.Message);
                                        }
                                        else
                                        {
                                            Console.WriteLine(editPlayerValidationMessage);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Player not found.");
                                    }
                                    break;

                                ///<summary>
                                /// Handle Delete operation for Player: Validate and delete player by ID.
                                ///</summary>
                                case OperationType.Delete:
                                    Console.WriteLine("\nDelete player: ");
                                    Console.Write("Enter player Id to delete: ");
                                    int deletePlayerID = int.Parse(Console.ReadLine());

                                    PLA01 preDeletePlayerModel = playerService.PreDeletePlayer(deletePlayerID);

                                    var (isValidDeletePlayer, deletePlayerValidationMessage) = playerService.ValidateOnDeletePlayer(preDeletePlayerModel);

                                    if (isValidDeletePlayer)
                                    {
                                        Response response = playerService.DeletePlayer(deletePlayerID);
                                        Console.WriteLine(response.Message);
                                    }
                                    else
                                    {
                                        Console.WriteLine(deletePlayerValidationMessage);
                                    }
                                    break;
                            }
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid operation type. Please enter A, U, or D.");
            }

            ///<summary>
            /// Wait for the user to press any key before closing the application.
            ///</summary>
            Console.ReadKey();
        }
    }
}
