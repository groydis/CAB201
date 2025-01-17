﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


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

        /// <summary>
        /// Initiates numPlayers and numRounds private variables and initialises list of effects
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="numPlayers">Number of Players</param> 
        /// <param name="numRounds">Number of Rounds</param> 
        /// <returns>explanation of return value</returns>
        public Gameplay(int numPlayers, int numRounds)
        {
            //number of Players must be between 2 and 8
            if (numPlayers >= 2 && numPlayers <= 8)
            {
                noPlayers = new Opponent[numPlayers];
            }

            //Number of Rounds is between 1 and 100
            if (numRounds >= 1 && numRounds <= 100)
            {
                noRounds = new Opponent[numRounds];
            }

            effects = new List<Effect>();
        }

        /// <summary>
        /// Return the length of the array
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary> 
        /// <returns> Returns an int equal to the number of players</returns>
        public int NumPlayers()
        {
            return noPlayers.Length;
        }

        /// <summary>
        /// Return the value of current round set in BeginGame
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary> 
        /// <returns>Returns an int equal to teh current round</returns>
        public int GetRoundNumber()
        {
            return curr_round;
        }

        /// <summary>
        /// Returns the number of rounds that are set in the Gameplay function
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <returns>Retruns an int equal to the number of rounds</returns>
        public int GetMaxRounds()
        {
            return noRounds.Length;
        }

        /// <summary>
        /// Creates the opponent in the array noPlayers witht the input value of player
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="playerNum">Player Number for the player to be created</param> 
        /// <param name="player">The assocated player to be stored in the array</param> 
        public void CreatePlayer(int playerNum, Opponent player)
        {
            noPlayers[playerNum - 1] = player;
        }

        /// <summary>
        /// Returns the specific Opponent type from noPlayers at the position of playerNum
        /// The array noPlayers is zero indexed and the input value is from 1 to noPlayers.Length
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="playerNum">Index of the player</param> 
        /// <returns>Returns the Player</returns>
        public Opponent GetPlayer(int playerNum)
        {
            return noPlayers[playerNum - 1];
        }

        /// <summary>
        /// Returns the specific BattleTank type from battleTanks at the position of playerNum
        /// The array battleTanks is zero indexed and the input value is from 1 to battleTanks.Length
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="playerNum">The player number for the tank</</param> 
        /// <returns>Returns the assoicated Battle Tank for that player</returns>
        public BattleTank GetGameplayTank(int playerNum)
        {
            return battleTanks[playerNum - 1];
        }

        /// <summary>
        /// Returns the colour of player which is stored in a static array
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="playerNum">The player number fot the associate colour to be returned</param> 
        /// <returns>Returns a colour</returns>
        public static Color GetColour(int playerNum)
        {
            Color[] colours = {Color.Aqua, Color.AntiqueWhite,
                  Color.Magenta, Color.DarkGreen,
                  Color.Gold, Color.IndianRed,
                  Color.LightGray, Color.Maroon};

            return colours[playerNum - 1];
        }

        /// <summary>
        /// Returns a list of locations for the player tanks to spawn on the x axis
        /// Does this by calculating the screen width and dividing it by the number of players and then the number of players again
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="numPlayers">Number of players playing</param> 
        /// <returns>Returns an array of X locations for the number of players</returns>
        public static int[] GetPlayerLocations(int numPlayers) 
        {

            int[] locations = new int[numPlayers];

            int screenWidth = 160;

            int loc = 0;

            //Loop through number of players and setup their X positions
            for (int i = 0; i < locations.Length; i++)
            {

                if (i == 0)
                {
                    loc = (screenWidth / numPlayers) / numPlayers;
                }
                else
                {
                    loc = locations[i - 1] + (screenWidth / numPlayers);
                }

                locations[i] = loc;
            }

            return locations;
        }

        /// <summary>
        /// Shuffles the list of positions randomly
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="array">An array of positionsparam> 
        public static void Shuffle(int[] array)
        {
            Random rng = new Random();

            //Loop through the given array
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i] + 1;
            }

            //Loop through the given array
            for (int j = 0; j < array.Length; j++)
            {
                int z = rng.Next(j);
                int k = array[z];
                array[z] = array[j];
                array[j] = k;
            }
        }

        /// <summary>
        /// Sets the current round to 1
        /// the starting player to 0 
        /// and calls commenceRound()
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        public void BeginGame()
        {
            curr_round = 1;
            start_player = 0;

            CommenceRound();
        }

        /// <summary>
        /// Sets the current player to the starting player
        /// Creates a new map
        /// populates a list of player positions and calls commence round on each player
        /// Shuffles the positions
        /// Createsa a list of battle tanks and stores the battle tanks in the list
        /// Sets the windspeed for the game
        /// Calsl teh gameplayForm and shows it
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        public void CommenceRound()
        {
            curr_player = start_player;

            arena = new Map();
            
            //Setup players positions and change them about
            int [] positions = GetPlayerLocations(noPlayers.Length);

            for (int i = 0; i < noPlayers.Length; i++)
            {
                noPlayers[i].CommenceRound();
            }
            
            Shuffle(positions);

            //Setup BattleTanks in their given positions
            battleTanks = new BattleTank[noPlayers.Length];
            
            for (int i = 0; i < noPlayers.Length ; i++)
            {

                int X_pos = positions[i];

                int Y_pos = arena.TankYPosition(X_pos);


                battleTanks[i] = new BattleTank(noPlayers[i], X_pos, Y_pos, this);
 
            }
            
            wind = GetWindSpeed();

            //Show Form
            GameplayForm gameplayForm = new GameplayForm(this);
            gameplayForm.Show(); 

        }

        /// <summary>
        /// Returns the map of this gameplay
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <returns>Returns the Map of this gameplay</returns>
        public Map GetArena()
        {
            return arena;
        }

        /// <summary>
        /// Returns the battletank of the current player
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <returns>Returns the BattleTank of the playere</returns>
        public BattleTank GetCurrentPlayerTank()
        {
            
            return battleTanks[curr_player];
        }

        /// <summary>
        /// Loops through the existing players and displays them
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="graphics">explanation of paramName</param> 
        /// <param name="displaySize">explanation of paramName</param> 
        public void DrawPlayers(Graphics graphics, Size displaySize)
        {
            for (int i = 0; i < battleTanks.Length; i++)
            {

                if (battleTanks[i].Exists())
                {

                    battleTanks[i].Display(graphics, displaySize);
                }
            }
        }

        /// <summary>
        /// Adds an effect to the effects list
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="weaponEffect">Weapon Effect to be added to the effects liste</param> 
        public void AddWeaponEffect(Effect weaponEffect)
        {

            effects.Add(weaponEffect);

            weaponEffect.ConnectGame(this);
        }

        /// <summary>
        /// Loops through the list of effects and runs the tick function for the effect
        /// Returning true once complete
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <returns>Returns true once effects are finishing being processed</returns>
        public bool ProcessWeaponEffects()
        {
            bool ans = false;
            for (int i = 0; i < effects.Count(); i++)
            {
                effects[i].Tick();
                ans = true;                
            }
            return ans;
        }

        /// <summary>
        /// Loops through the list of effects and displays them
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="graphics">????</param> 
        /// <param name="displaySize">????</param>
        public void DrawAttacks(Graphics graphics, Size displaySize)
        {

            for (int i = 0; i < effects.Count(); i++)
            {
                effects[i].Display(graphics, displaySize);
            }
        }

        /// <summary>
        /// removes and effect from the effects list
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="weaponEffect">Effect to be removede</param> 

        public void RemoveEffect(Effect weaponEffect)
        {
            effects.Remove(weaponEffect);
        }

        /// <summary>
        /// Checks if the projectile has hit anything
        /// First it checks if the projectile is within the game bounds
        /// Then chicks if it has hit terrain
        /// Finally checks if it has hit a tank
        /// If nothing was hit it returns false
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="projectileX">The x position value of the projectilee</param> 
        /// <param name="projectileY">The y position value of the projectilee</param> 
        /// <returns>Returns true if the pojective hits the ground or a tank</returns>
        public bool CheckHitTank(float projectileX, float projectileY)
        {
 
            if (projectileX < 0 || projectileX > Map.WIDTH || projectileY < 0 || projectileY > Map.HEIGHT)
            {
                return false;
            }

            if (arena.Get((int)projectileX, (int)projectileY)) {
                return true;
            }
 
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

        /// <summary>
        /// Calculates the tanks posted based on where the shell hit
        /// If within range does damage
        /// else the damage will be equal to 0
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="damageX">The X position of the explosion</param> 
        /// <param name="damageY">The Y position of the explosion</param>
        /// <param name="explosionDamage">The ammmount of damage the explsion doese</param>
        /// <param name="radius">Radius of the explosion</param>
        public void InflictDamage(float damageX, float damageY, float explosionDamage, float radius)
        {
            float overall_dmg = 0;
            for (int i = 0; i < battleTanks.Length; i++)
            {
                if (battleTanks[i].Exists())
                {
                    //Check the distance between tank and the explosion
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

        /// <summary>
        /// Boolean check to see if the tanks need to move due to gravity
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <returns>Returns true if tanks require moving, otherwise false</returns>
        public bool GravityStep()
        {
            bool moved = false;

            if (arena.GravityStep())
            {
                moved = true;
            }

            for(int i = 0; i < battleTanks.Length; i++)
            {
                if(battleTanks[i].GravityStep())
                {
                    moved = true;
                }
            }
            return moved;
        }

        /// <summary>
        /// Boolean check to see if the current players turn is over, if so changes the current player
        /// to the next player, picks a new value for wind -10 or +10 the existing speed
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <returns>Returns true if players exist, otherwise false</returns>
        public bool FinishTurn()
        {
            int playersLeft = 0;
            for (int i = 0; i < battleTanks.Length; i++)
            {
                if (battleTanks[i].Exists())
                {
                    playersLeft++;
                }
             
            }
            if (playersLeft > 1)
            {
                for (int i = 0; i < playersLeft; i++)
                {
                    curr_player++;
                    if (curr_player >= battleTanks.Length)
                    {
                        curr_player = 0;
                    }
                    if (battleTanks[curr_player].Exists())
                    {
                        //Make sure values are always in between -100 and 100
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
                    }
                }
            }
            if (playersLeft == 1)
            {
                FindWinner();
                return false;
            }
            return false;
        }

        /// <summary>
        /// Finds the winner of a round by checking what tanks remain
        /// Increase the winning tanks score
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        public void FindWinner()
        {            
            for (int i = 0; i < battleTanks.Length; i++)
            {
                
                if (battleTanks[i].Exists())
                {                    
                    battleTanks[i].GetPlayer().AddScore();
                }
            }
        }

        /// <summary>
        /// Initiates the next round, or if maximum rounds ends the game and shows the rankings window
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
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
                //Maxrounds reached show ranking form
                Rankings rankingsWindow = new Rankings(this);
                rankingsWindow.Show();
            }
        }
        /// <summary>
        /// Randomly generats a value for the wind betwene -100 and 100 then returns it
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary> 
        /// <returns>Returns an integer between -100 and 100returns>
        public int GetWindSpeed()
        {
            
            wind = rng.Next(-100, 100);
            return wind;
        }
    }
}
