using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
// TEMP
using System.Diagnostics;

namespace TankBattle
{
    public class Gameplay
    {
        //List of Effects used to store and effect the weapons of the game
        List<Effect> effects;

        //Opponent arrays used to get the Round Data and Player Data
        private Opponent[] noPlayers;
        private Opponent[] noRounds;

        //Map 
        private Map arena;

        //Array of BattleTanks
        private BattleTank[] battleTanks;

        //Game Values that work to show current round and current player
        private int curr_round = 1;
        private int start_player;
        private int curr_player;
        
        //wind value that will be used to calculate game effect values 
        private int wind;

        //Random calculator
        private Random rng = new Random();

        public Gameplay(int numPlayers, int numRounds)
        {
            //Check that the numPlayers is within 2 and 8
            //Minimum number of Players is 2 and Maximum number is 8
            if (numPlayers >= 2 && numPlayers <= 8)
            {
                //Make the array noPlayers to the length of the input Numplayers
                noPlayers = new Opponent[numPlayers];
            }

            //Check that the numRounds is within 1 and 100
            //Minimum number of Rounds is 1 and Maximum number is 100
            if (numRounds >= 1 && numRounds <= 100)
            {
                //Set the array length of noRounds to NumRounds
                noRounds = new Opponent[numRounds];
            }

            //Initiliaze the LIst of Effects to be used later in the 
            effects = new List<Effect>();
        }

        public int NumPlayers()
        {
            //Return the length of the array 
            return noPlayers.Length;
        }

        public int GetRoundNumber()
        {
            //Return the value of current round set in BeginGame
            return curr_round;
        }

        public int GetMaxRounds()
        {
            //Returns the number of rounds that are set in the Gameplay function
            return noRounds.Length;
        }

        public void CreatePlayer(int playerNum, Opponent player)
        {
            //Creates the opponent in the array noPlayers witht the input value of player
            noPlayers[playerNum - 1] = player;
        }

        public Opponent GetPlayer(int playerNum)
        {
            //Returns the specific Opponent type from noPlayers at the position of playerNum
            //The array noPlayers is zero indexed and the input value is from 1 to noPlayers.Length
            return noPlayers[playerNum - 1];
        }

        public BattleTank GetGameplayTank(int playerNum)
        {
            //Returns the specific BattleTank type from battleTanks at the position of playerNum
            //The array battleTanks is zero indexed and the input value is from 1 to battleTanks.Length
            return battleTanks[playerNum - 1];
        }

        public static Color GetColour(int playerNum)
        {
            //Creates an array of Colors named colours
            //There are only 8 colours as their is only a max of 8 players
            //each player has only one colour
            Color[] colours = {Color.Aqua, Color.AntiqueWhite,
                  Color.Magenta, Color.DarkGreen,
                  Color.Gold, Color.IndianRed,
                  Color.LightGray, Color.Maroon};

            //Returns the specific Color type from colours at the position of playerNum
            //The array colours is zero indexed and the input value is from 1 to colours.Length
            return colours[playerNum - 1];
        }

        public static int[] GetPlayerLocations(int numPlayers)
        {

            // Create an array of to store the X position of player locations
            int[] locations = new int[numPlayers];
            // Stored variable to ensure positions don't exceed screen width
            int screenWidth = 160;
            // interget to store the x value temporarily
            int loc = 0;

            // Cycles through number of positions in the locations araray
            for (int i = 0; i < locations.Length; i++)
            {
                // does a check to see if it is the first position
                if (i == 0)
                {
                    // Calculates first position by deviding the screen width by the number of players, then dividing that value by the number of players.
                    loc = (screenWidth / numPlayers) / numPlayers;
                }
                // Otherwises if it is not the first position of the array.
                else
                {

                    // Get the first position, and add the screnWidth / number of players.
                    loc = locations[i - 1] + (screenWidth / numPlayers);
                }
                // Add the loc value to the locations array.
                locations[i] = loc;
            }
            // Return the array.
            return locations;
        }

        public static void Shuffle(int[] array)
        {
            Random rng = new Random();
            // I don't know why this is here?
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i] + 1;
            }
            // Loop through and shuffle the positions randomly.
            for (int j = 0; j < array.Length; j++)
            {
                int z = rng.Next(j);
                int k = array[z];
                array[z] = array[j];
                array[j] = k;
            }
        }

        public void BeginGame()
        {
            //Setup the game and initiliaze game values 
            curr_round = 1;
            start_player = 0;

            //Calls the CommenceRound function to setup players, battletanks and maps
            CommenceRound();
        }

        public void CommenceRound()
        {
            //Sets the current player to the starting player
            curr_player = start_player;

            //Create a new map in the arena value
            arena = new Map();
            
            //Create an array to hold the player locations retrieved through GetPlayerLocations()
            int [] positions = GetPlayerLocations(noPlayers.Length);

            //Loop through the array of players
            for (int i = 0; i < noPlayers.Length; i++)
            {
                //Call CommenceRound on each player
                noPlayers[i].CommenceRound();
            }
            
            //Shuffle the positions of each player
            //So that player 1 can be at pos 1 or pos 2 depending on the game
            Shuffle(positions);
            
            //Initiliaze the array of battleTanks to the length of noPlayers. 
            //As they ahve to be the same length cause there can't seperate null values
            battleTanks = new BattleTank[noPlayers.Length];
            

            //Loop through the array again with length of noPlayers.Length
            for (int i = 0; i < noPlayers.Length ; i++)
            {
                //Get the X position of the i player
                int X_pos = positions[i];
                //Get the Y position of the i player using the X position
                int Y_pos = arena.TankYPosition(X_pos);

                //Create the battleTanks at i position using Opponent iin No players,
                //the X and Y Positions and the Gameplay of this Game
                battleTanks[i] = new BattleTank(noPlayers[i], X_pos, Y_pos, this);
 
            }
            
            //Get a random wind speed between -100 and 100
            wind = GetWindSpeed();

            //Create and Show a new GamePlayForm
            GameplayForm gameplayForm = new GameplayForm(this);
            gameplayForm.Show(); 

        }

        public Map GetArena()
        {
            //Return the map value of this GamePlay
            return arena;
        }

        public BattleTank GetCurrentPlayerTank()
        {
            //Return the BattleTank of the current player
            return battleTanks[curr_player];
        }

        public void DrawPlayers(Graphics graphics, Size displaySize)
        {
            //Loop through the battleTanks array
            for (int i = 0; i < battleTanks.Length; i++)
            {
                //Check to see if this battleTank Exists
                if (battleTanks[i].Exists())
                {
                    //Display the battleTanks BMP so that the tank can be drawn
                    battleTanks[i].Display(graphics, displaySize);
                }
            }
        }

        public void AddWeaponEffect(Effect weaponEffect)
        {
            //Add the WeaponEffect
            effects.Add(weaponEffect);
            //Connect the WeaponEffect to the game so that it can be accessed
            weaponEffect.ConnectGame(this);
        }

        public bool ProcessWeaponEffects()
        {
            //Setup a bool to be returned with either true or false
            bool ans = false;
            for (int i = 0; i < effects.Count(); i++)
            {
                //Call Tick on Each effect in the List
                effects[i].Tick();
                //bool becomes true once one is done
                ans = true;                
            }
            //Return the true or false
            return ans;
        }

        public void DrawAttacks(Graphics graphics, Size displaySize)
        {
            //Loops through the effects list
            for (int i = 0; i < effects.Count(); i++)
            {
                //Draw each effect using the display functions
                effects[i].Display(graphics, displaySize);
            }
        }

        public void RemoveEffect(Effect weaponEffect)
        {
            //Remove this weaponEffect from the List
            effects.Remove(weaponEffect);
        }

        public bool CheckHitTank(float projectileX, float projectileY)
        {
            //Is the projectile within the bounds
            if (projectileX < 0 || projectileX > Map.WIDTH || projectileY < 0 || projectileY > Map.HEIGHT)
            {
                return false;
            }
            //Is the projectile hitting anything
            if (arena.Get((int)projectileX, (int)projectileY)) {
                return true;
            }
            //Loop through battleTanks array
            for (int i = 0; i < battleTanks.Length; i++)
            {
                if (i == curr_player)
                {
                    return false;
                }
                int x_pos = battleTanks[i].GetX();
                int y_pos = battleTanks[i].Y();
                int right = x_pos + TankModel.WIDTH;
                int bottom = y_pos + TankModel.HEIGHT;
                if (projectileX >= x_pos && projectileX <= right)
                {
                    if (projectileY >= y_pos && projectileY <= bottom)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public void InflictDamage(float damageX, float damageY, float explosionDamage, float radius)
        {
            // Calculates tank position based on where the shell hit.
            // If within range, does damage
            // Else the damage will be equal to 0
            float overall_dmg = 0;
            for (int i = 0; i < battleTanks.Length; i++)
            {
                if (battleTanks[i].Exists())
                {
                    float tank_centre_x = battleTanks[i].GetX() + (TankModel.WIDTH / 2);
                    float tank_centre_y = battleTanks[i].Y() + (TankModel.HEIGHT / 2);

                    float dist = (float)Math.Sqrt(Math.Pow(tank_centre_x - damageX, 2) + Math.Pow(tank_centre_y - damageY, 2));

                    if (dist < radius && dist > radius / 2)
                    {
                        float diff = dist - radius;
                        overall_dmg = (explosionDamage * diff) / radius;
                    }
                    else if (dist < radius / 2)
                    {
                        overall_dmg = explosionDamage;
                    }
                    battleTanks[i].InflictDamage((int)overall_dmg);
                }
            }
        }

        public bool GravityStep()
        {
            //Have a bool value to determine returned True or False
            bool moved = false;

            //Return True if Map's GravityStep occurs
            if (arena.GravityStep())
            {
                moved = true;
            }

            //Loop through each BattleTank
            for(int i = 0; i < battleTanks.Length; i++)
            {
                //Return True if BattleTank's GravityStep occurs
                if(battleTanks[i].GravityStep())
                {
                    moved = true;
                }
            }
            return moved;
        }

        public bool FinishTurn()
        {
            int playersLeft = 0;
            for (int i = 0; i< battleTanks.Length; i++)
            {
                if (battleTanks[i].Exists())
                {
                    playersLeft++;
                }
                if (playersLeft >= 2)
                {
                    curr_player++;             
                    if (curr_player >= NumPlayers())
                    {
                        curr_player = 0;
                    }
                    if (battleTanks[curr_player].Exists())
                    {
                        wind += rng.Next(-10, 10);

                        if (wind < -100)
                        {
                            wind = -100;
                        }
                        if (wind > 100)
                        {
                            wind = 100;
                        }
                        return true;
                    } else if (i == battleTanks.Length - 1)
                    {
                        i = 0;
                    }
                }
            }

            if (playersLeft <= 1)
            {
                FindWinner();
                return false;
            }
            return false;
        }

        public void FindWinner()
        {
            //Loop through the battleTanks Array
            for (int i = 0; i < battleTanks.Length; i++)
            {
                //Check that the battleTanks exists
                if (battleTanks[i].Exists())
                {
                    //Add the score to this player that wins
                    battleTanks[i].GetPlayer().AddScore();
                }
            }
        }

        public void NextRound()
        {
            curr_round++;
            if (curr_round <= noRounds.Length)
            {
                start_player++;
                if (start_player == noPlayers.Length)
                {
                    start_player = 0;
                }
                CommenceRound();
            }
            if (curr_round > noRounds.Length)
            {
                Rankings rankingsWindow = new Rankings(this);
                rankingsWindow.Show();
            }
        }
        
        public int GetWindSpeed()
        {
            //Randomly generate a value between -100 and 100 then return it
            wind = rng.Next(-100, 100);
            return wind;
        }
    }
}
