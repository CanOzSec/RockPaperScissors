using RockPaperScissors;

Console.WriteLine("Welcome to the Rock Paper Scissors");
Console.WriteLine("You should enter the players now...");
Console.WriteLine("After you finish entering the players simply type 'END' in player name.");

List<Player> playerList = new List<Player>();

while (true)
{
    string tempName = new string("");
    char tempOption = new char();
    List<char> validChars = new List<char> { 'R', 'P', 'S', 'r', 'p', 's' };

    Console.WriteLine("Enter player name: ");
    string Input1 = Console.ReadLine();
    if (Input1 == null || Input1 == "END")
    {
        if (Input1 == "END"){
            Console.WriteLine("Finished adding players.");
            break;
        }
        Console.WriteLine("Invalid Name, please enter again.");
        continue;
    }
    else
        tempName = Input1;
    Console.WriteLine("Enter player option (R, P, S): ");
    char Input2 = Console.ReadLine()[0];
    if (!validChars.Contains(Input2))
    {
        Console.WriteLine("Invalid option, please enter again.");
        continue;
    }
    else
        tempOption = Input2;

    playerList.Add(new Player(tempName, tempOption));
}
RPS game = new RPS(playerList);
foreach (Player winner in game.WinnersOfTournament)
{
    Console.Write(winner.name);
    Console.WriteLine(" is a Winner!!!!");
    Console.ReadLine();
}