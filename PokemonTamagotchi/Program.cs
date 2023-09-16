using PokemonTamagotchi.Models;
using PokemonTamagotchi.Services;
using PokemonTamagotchi.Views;

PokemonSpeciesResult pokemonSpecies = PokemonService.GetPokemonSpecies();
List<PokePet> pets = new List<PokePet>();

var gameInterface = new GameInterface();
gameInterface.ShowWelcomingMessage();

do
{
    gameInterface.ShowMainMenu();
    gameInterface.GetUserOption();

    switch (gameInterface.UserOption)
    {
        case 1:
            Console.Clear();
            gameInterface.ShowSpeciesMenu(pokemonSpecies);

            var pressedKey = Console.ReadKey(true).Key;

            while (pressedKey != ConsoleKey.D2 && pressedKey != ConsoleKey.NumPad2)
            {
                if (pressedKey == ConsoleKey.DownArrow && !string.IsNullOrEmpty(pokemonSpecies.Next))
                {
                    var offset = gameInterface.GetOffset(pokemonSpecies.Next);
                    pokemonSpecies = PokemonService.GetPokemonSpecies(pokemonSpecies.Next!);
                    pokemonSpecies.OffSet = offset;
                }
                else if (pressedKey == ConsoleKey.UpArrow && !string.IsNullOrEmpty(pokemonSpecies.Previous))
                {
                    int offset = gameInterface.GetOffset(pokemonSpecies.Previous);
                    pokemonSpecies = PokemonService.GetPokemonSpecies(pokemonSpecies.Previous!);
                    pokemonSpecies.OffSet = offset;
                }
                else if (pressedKey == ConsoleKey.D1 || pressedKey == ConsoleKey.NumPad1)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.Write("Type in the number of the pokémon to be your companion: ");
                    gameInterface.GetUserInput();

                    while (gameInterface.UserOption > pokemonSpecies.Count)
                    {
                        Console.Clear();
                        gameInterface.PrintPokemonSpecies(pokemonSpecies);
                        Console.WriteLine();
                        Console.Write("Type in the number of the pokémon to be your companion: ");

                        gameInterface.GetUserInput();
                    }

                    Console.Clear();
                    var petPokemon = PokemonService.CreatePokePet(gameInterface.UserOption);
                    gameInterface.PrintPokePetInfo(petPokemon);
                    Console.WriteLine();
                    Console.WriteLine($"[1] Adopt {petPokemon.Name}");
                    Console.WriteLine("[2] Go back");

                    gameInterface.GetUserOption();

                    switch (gameInterface.UserOption)
                    {
                        case 1:
                            Console.Clear();
                            pets.Add(petPokemon);
                            Console.WriteLine($"Congratulations! Now {petPokemon.Name} is your companion!");
                            Console.WriteLine();
                            Console.WriteLine("[1] Go back to main menu");
                            gameInterface.GetUserOption();
                            while (gameInterface.UserOption != 1)
                            {
                                Console.ReadKey(true);
                                gameInterface.GetUserOption();
                            }
                            pressedKey = ConsoleKey.D2;
                            break;
                        case 2:
                            break;
                        default:
                            Console.ReadKey(true);
                            break;
                    }
                }

                Console.Clear();
                if (pressedKey != ConsoleKey.D2)
                {
                    gameInterface.ShowSpeciesMenu(pokemonSpecies);
                    pressedKey = Console.ReadKey(true).Key;
                }
            }
            Console.Clear();
            break;
        case 2:
            gameInterface.PrintCompanions(pets);
            Console.WriteLine();
            Console.WriteLine("[1] Go back to main menu");
            gameInterface.GetUserOption();
            while (gameInterface.UserOption != 1)
            {
                Console.ReadKey(true);
                gameInterface.GetUserOption();
            }
            Console.Clear();
            break;
        case 3:
            Console.WriteLine();
            Console.WriteLine("Thanks for playing! See you later!");
            return;
        default:
            Console.Clear();
            Console.WriteLine("Invalid option");
            break;
    }
} while (gameInterface.UserOption != 0);
