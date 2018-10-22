using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;

namespace Lab4_PigGame_MarcusLoder
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //instance variables
        Button rollButton;
        Button endTurnButton;
        Button newGameButton;

        EditText p1NameEditText;
        EditText p2NameEditText;

        TextView p1ScoreTextView;
        TextView p2ScoreTextView;
        TextView pointsThisTurnTextView;
        TextView playersTurnTextView;

        //create game
        PigGame game;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // Define Controls
            rollButton = FindViewById<Button>(Resource.Id.rolldice);
            endTurnButton = FindViewById<Button>(Resource.Id.endTurn);
            newGameButton = FindViewById<Button>(Resource.Id.newGame);

            pointsThisTurnTextView = FindViewById<TextView>(Resource.Id.ptsThisTurn);
            playersTurnTextView = FindViewById<TextView>(Resource.Id.whosTurn);

            p1ScoreTextView = FindViewById<TextView>(Resource.Id.score1);
            p2ScoreTextView = FindViewById<TextView>(Resource.Id.score2);

            var diceImageView = FindViewById<ImageView>(Resource.Id.dice);

            var player1NameEditText = FindViewById<EditText>(Resource.Id.p1Name);
            var player2NameEditText = FindViewById<EditText>(Resource.Id.p2Name);

            //instantiate a new game.
            game = new PigGame();

            // The click EVENT (Who know Will Smith could Switch)
            rollButton.Click += delegate
            {
                //Update Names
                if (p1NameEditText.Text == "")
                {
                    game.Player1.name = "Player 1";
                }
                else game.Player1.name = p1NameEditText.Text;

                if (p2NameEditText.Text == "")
                {
                    game.Player2.name = "Player 2";
                }
                else game.Player2.name = p2NameEditText.Text;

                //Update Whos Turn It Is
                if (game.Player1Trn)
                {
                    playersTurnTextView.Text = game.Player1.name + "'s Turn";//Player 1
                }
                else
                {
                    playersTurnTextView.Text = game.Player2.name + "'s Turn";//Player 2
                }

                int roll = game.rolldice();
                switch (roll)
                {
                    case 1:
                        diceImageView.SetImageResource(Resource.Drawable.Dice - 1);
                        break;
                    case 2:
                        diceImageView.SetImageResource(Resource.Drawable.Dice - 2);
                        break;
                    case 3:
                        diceImageView.SetImageResource(Resource.Drawable.Dice - 3);
                        break;
                    case 4:
                        diceImageView.SetImageResource(Resource.Drawable.Dice - 4);
                        break;
                    case 5:
                        diceImageView.SetImageResource(Resource.Drawable.Dice - 5);
                        break;
                    case 6:
                        diceImageView.SetImageResource(Resource.Drawable.Dice - 6);
                        break;
                    default:
                        break;
                }

                int turnPts = game.refreshTurnPts(roll);

                if (turnPts == 0)
                {
                    rollButton.Enabled = false;
                    Android.Widget.Toast.MakeText(this, "You Lost Your Points!", Android.Widget.ToastLength.Short).Show();
                    pointsThisTurnTextView.Text = "0";
                }
                else
                {
                    pointsThisTurnTextView.Text = turnPts.ToString();//Update turn Pts
                }
            };

            // End Game Click Event Handler
            endTurnButton.Click += delegate
            {
                rollButton.Enabled = true;

                if (game.MatchPoint)
                {

                    if (game.Player1Trn)
                    {
                        p1ScoreTextView.Text = game.RefreshPlayersScore().ToString();
                    }
                    else
                    {
                        p2ScoreTextView.Text = game.RefreshPlayersScore().ToString();
                    }

                    if (game.player1Won())
                    {
                        //P1 won
                        Android.Widget.Toast.MakeText(this, "Player 1 Wins--Reseting Game", Android.Widget.ToastLength.Short).Show();

                    }
                    else
                    {
                        //P2 won
                        Android.Widget.Toast.MakeText(this, "Player 2 Wins--Reseting Game", Android.Widget.ToastLength.Short).Show();
                    }
                    reset();
                }
                else
                {
                    if (game.Player1Trn)
                    {
                        p1ScoreTextView.Text = game.RefreshPlayersScore().ToString();
                        playersTurnTextView.Text = game.Player2.name + "'s Turn";
                    }
                    else
                    {
                        p2ScoreTextView.Text = game.RefreshPlayersScore().ToString();
                        playersTurnTextView.Text = game.Player1.name + "'s Turn";
                    }

                    pointsThisTurnTextView.Text = "0";
                    game.Player1Trn = !game.Player1Trn;
                }

                newGameButton.Click += delegate
                {
                    //reset the ui
                    Android.Widget.Toast.MakeText(this, "Reseting Game", Android.Widget.ToastLength.Short).Show(); p1ScoreTextView.Text = game.refreshPlayersScore().ToString();//update score
                    reset();
                };

            };
        }
        protected void update()
        {

        }
        private void reset()
        {
            game.reset();
            playersTurnTextView.Text = "Player 1's Turn";
            p1ScoreTextView.Text = game.Player1.score.ToString();//Reset Score
            p2ScoreTextView.Text = game.Player2.score.ToString();//Reset Score
            pointsThisTurnTextView.Text = "0"; //Reset the Text View
        }
    }
}