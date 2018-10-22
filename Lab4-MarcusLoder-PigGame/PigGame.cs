using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Lab4_PigGame_MarcusLoder
{
    class PigGame
    {
        //create a player
        public class player
        {
            public string name { get; set; }
            public int score { get; set; }
            public int turnScore { get; set; }
            public bool win { get; set; }

            public player()
            {
                name = "";
                score = 0;
                turnScore = 0;
                win = false;
            }
        }

        //winning score
        const int WINNINGSCORE = 100;

        //Create and instantiate new players
        public player Player1 = new player();
        public player Player2 = new player();

        //Keeps track of turns
        public bool Player1Trn { get; set; } = true;

        //Random for the dice roll
        public Random random = new Random();

        //Ending bool
        public bool MatchPoint { get; set; } = false;

        //Random Dice Roller
        public int rolldice()
        {
            return random.Next(6) + 1;
        }

        //updates the players scores
        public int RefreshPlayersScore()
        {
            if (Player1Trn)
            {
                Player1.score += Player1.turnScore;
                Player1.turnScore = 0;

                if (Player1.score >= WINNINGSCORE)
                {
                    MatchPoint = true;
                }
                return Player1.score;
            }
            else
            {
                Player2.score += Player2.turnScore;

                if (Player2.score >= WINNINGSCORE)
                {
                    Player2.turnScore = 0;
                }
                return Player2.score;
            }
        }

        public int refreshTurnPts(int score)
        {
            if (Player1Trn)
            {
                if (score == 1)
                {
                    Player1.turnScore = 0;
                }
                else
                {
                    Player1.turnScore += score;
                }
                return Player1.turnScore;
            }
            else
            {
                if (score == 1)
                {
                    Player2.turnScore = 0;
                }
                else
                {
                    Player2.turnScore += score;
                }
                return Player2.turnScore;
            }
        }

        public bool checkWin()
        {
            if (Player1Trn)
            {
                Player1.score += Player1.turnScore;
                Player1.turnScore = 0;

                if (Player1.score >= WINNINGSCORE)
                {
                    Player1.win = true;
                }
                return Player1.win;
            }
            else
            {
                Player2.score += Player2.turnScore;
                Player2.turnScore = 0;

                if (Player2.score >= WINNINGSCORE)
                {
                    Player2.win = true;
                }
                return Player2.win;
            }
        }

        public bool player1Won()
        {
            if (Player1.score > Player2.score)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void reset()
        {
            Player1.score = 0;
            Player1.turnScore = 0;
            Player1.win = false;
            Player2.score = 0;
            Player2.turnScore = 0;
            Player2.win = false;
            Player1Trn = true;
            MatchPoint = false;
        }
    }
}