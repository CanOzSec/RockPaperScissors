using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    public class RPS
    {
        private Guid result { get; set; }
        public Guid Result { get 
            {
                return this.result;
            }
        }
        public List<Player>? Players { get; set; }
        public List<Player>? OriginalPlayers { get; set; }
        public List<Player>? WinnersOfTournament { get; set; }

        public RPS(List<Player>? players)
        {
            this.Players = new List<Player>(players);
            this.OriginalPlayers = new List<Player>(players);
            WinnersOfTournament = new List<Player>();
            if (this.Players == null)
            {
                throw new ArgumentNullException(nameof(this.Players));
            }
            else if(this.Players.Count % 2 != 0)
            {
                Random random = new Random();
                List<char> choices = new List<char> { 'R', 'P', 'S' };
                char random_choice = choices[random.Next(choices.Count)];
                this.Players.Add(new Player("Computer", random_choice));
                this.OriginalPlayers.Add(new Player("Computer", random_choice));
            }
            if (this.Players.Count % 2 == 0)
            {
                List<Player> winners = new List<Player>();
                int draws = 0;

                while (this.Players.Count >= 2){
                    bool LastRound = false;
                    if (this.Players.Count == 2)
                    {
                        LastRound = true;
                    }
                    Random random = new Random();
                    int Index1 = random.Next(this.Players.Count);
                    Player random_choice_a = this.Players[Index1];
                    this.Players.RemoveAt(Index1);
                    Random random1 = new Random();
                    int Index2 = random1.Next(this.Players.Count);
                    Player random_choice_b = this.Players[Index2];
                    this.Players.RemoveAt(Index2);
                    GameLogic game = new GameLogic(random_choice_a, random_choice_b);
                    if (game.Result == Guid.Empty)
                    {
                        if (draws >= 100)
                        {
                            this.WinnersOfTournament.Add(random_choice_a);
                            this.WinnersOfTournament.Add(random_choice_b);
                            break;
                        }
                        this.Players.Add(random_choice_a);
                        this.Players.Add(random_choice_b);
                        draws += 1;
                    }
                    else
                    {
                        Guid winnerGuid = game.Result;
                        IEnumerable<Player> winner =
                            from player in OriginalPlayers
                            where player.id == winnerGuid
                            select player;
                        foreach (Player player in winner)
                            winners.Add(player);
                    }
                    if (LastRound)
                    {
                        if (winners.Count == 1) 
                        {
                            this.WinnersOfTournament.Add(winners.First());
                            break;
                        }
                        this.Players.AddRange(winners);
                        winners.Clear();
                    }
                }
            }
        }
    }
    public class Player
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public char option { get; set; }
        public Player(string? name, char option)
        {
            this.id = Guid.NewGuid();
            this.name = name;
            this.option = Char.ToUpper(option);
        }
    }
    internal class GameLogic
    {
        public Guid Result { get; set; }
        public GameLogic(Player player1, Player player2)
        {
            int res = Battle(player1.option, player2.option);
            if (res == 0)
                this.Result = Guid.Empty;
            else if (res == 1)
                this.Result = player1.id;
            else if (res == 2)
                this.Result = player2.id;
        }
        private int Battle(char option_a, char option_b)
        {
            if (option_a == option_b)
                return 0;
            else if (option_a == 'R' && option_b == 'S')
                return 1;
            else if (option_a == 'R' && option_b == 'P')
                return 2;
            else if (option_a == 'S' && option_b == 'R')
                return 2;
            else if (option_a == 'S' && option_b == 'P')
                return 1;
            else if (option_a == 'P' && option_b == 'S')
                return 2;
            else if (option_a == 'P' && option_b == 'R')
                return 1;
            else
                return -127;
        }
    }
}
