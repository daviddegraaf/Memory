using BusinessLogic;
using BusinessLogic.Exceptions;
using BusinessLogic.Models;
using BusinessLogic.Services;
using ConsoleApp.Handlers;
using ConsoleApp.Helpers;
using DataAccess.Repositories;

public class Program
{
    public static HighscoreManager? HighscoreManager;
    public static CardManager? CardManager;
    public static Game? Game;
    public static IRepository Repository = new SqliteRepository();

    public static void Main(string[] args)
    {
        HighscoreManager = new HighscoreManager(Repository);
        CardManager = new CardManager(Repository);

        Console.Clear();
        Console.WriteLine("Welcome the the Memory game!\n");
        Drawings.DrawHighscores();

        CommandInput();
    }

    public static void CommandInput()
    {
        try
        {
            CommandHandler handler = new CommandHandler();
            handler.HandleInput();
        } catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            if(ex.GetType() == typeof(CommandNotFoundException)) CommandInput();
        }
    }
}


//StartGame:
//Console.Clear();

//game = new Game(_cardManager.GenerateCardList(8));
//game.Start();

//int foundPairs = 0;

//goto SelectCard;

//SelectCard:
//Console.Clear();
//Console.WriteLine("You have found " + foundPairs + " pairs.\n");
//Drawings.DrawGrid(game.Cards);

//Console.WriteLine("\nWhat card would you like to pick?" + (game.Moves == 0 ? "Please specify as \"row,column\", for example 1,3" : ""));
//string? position1 = Console.ReadLine();

//if (position1 == null) goto SelectCard;

//Card? card1 = CardManager.GetCardFromPosition(position1, game.Cards);
//if (card1 == null || card1.IsTurned || card1.IsFound) goto SelectCard;

//goto SelectPair;

//SelectPair:
//Console.Clear();
//Drawings.DrawGrid(game.Cards);

//Console.WriteLine("\nWhat card do you think is (" + position1 + ") it's pair?");
//string? position2 = Console.ReadLine();

//if (position2 == null) goto SelectPair;

//Card? card2 = CardManager.GetCardFromPosition(position2, game.Cards);
//if (card2 == null || card2.IsTurned || card2.IsFound) goto SelectPair;

//game.ValidatePair(card1, card2);