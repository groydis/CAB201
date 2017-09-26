using System;
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
        private Opponent[] noPlayers;
        private Opponent[] noRounds;

        private Map newMap;

        private int curr_round;
        private int start_player;
        private int curr_player;
        
        private int wind;

        private Random rng = new Random();

        public Gameplay(int numPlayers, int numRounds)
        {
            if (numPlayers >= 2 && numPlayers <= 8)
            {
                noPlayers = new Opponent[numPlayers];
            }
            if (numRounds >= 1 && numRounds <= 100)
            {
                noRounds = new Opponent[numRounds];
            }

            List<Effect> effects = new List<Effect>();
        }

        public int NumPlayers()
        {
            return noPlayers.Length;
        }

        public int GetRoundNumber()
        {
            throw new NotImplementedException();
        }

        public int GetMaxRounds()
        {
            return noRounds.Length;
        }

        public void CreatePlayer(int playerNum, Opponent player)
        {
            noPlayers[playerNum - 1] = player;
        }

        public Opponent GetPlayer(int playerNum)
        {
            return noPlayers[playerNum - 1];
        }

        public BattleTank GetGameplayTank(int playerNum)
        {
            throw new NotImplementedException();
        }

        public static Color GetColour(int playerNum)
        {
            Color[] colours = {Color.Aqua, Color.AntiqueWhite,
                  Color.Black, Color.DarkGreen,
                  Color.Gold, Color.IndianRed,
                  Color.LightGray, Color.Maroon};

            return colours[playerNum - 1];
        }

        public static int[] GetPlayerLocations(int numPlayers)
        {
            int[] locations = new int[numPlayers];
            int screenWidth = 160;
            int loc = 0;
            for (int i = 0; i < numPlayers; i++)
            {
                if (i == 0)
                {
                    loc = screenWidth / numPlayers;
                }
                else
                {
                    loc = locations[i - 1] + (screenWidth / numPlayers);
                }
                locations[i] = loc;
            }
            return locations;
        }

        public static void Shuffle(int[] array)
        {

            Random rng = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i + 1;
            }

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
            curr_round = 1;

            start_player = 0;

            CommenceRound();
        }

        public void CommenceRound()
        {
            //curr_player to starting Opponent FIELD (See Begin Game)
            //curr_player = ;

            newMap = new Map();

            //Dot Point 3 Array of opponent Positions
            //int [] positions = {0, 1 };

            for (int i =0; i < noPlayers.Length; i++)
            {
                noPlayers[i].CommenceRound();
                
            }

            //Shuffle that array of positions
            //Shuffle(positions);

            //Create BattleTanks array of Private field that is the length of Opponent noPlayers

            GetWindSpeed();

            //Create New Gameplay Form and Show() it (Show() is exact wording from question

        }

        public Map GetArena()
        {
            throw new NotImplementedException();
        }

        public void DrawPlayers(Graphics graphics, Size displaySize)
        {
            // THIS SHOULD SIMPLY BE AS FOLLOWS!
            // FOR EACH BATTLE TANK tank in BATTLE TANKS ARRAY
            /// ^^ or could be standard for loop
            // IF tank.EXISTS()
            // tank.DISPLAY(graphics, displaysize) 
            throw new NotImplementedException();
        }

        public BattleTank GetCurrentPlayerTank()
        {
            throw new NotImplementedException();
        }

        public void AddWeaponEffect(Effect weaponEffect)
        {
            throw new NotImplementedException();
        }

        public bool ProcessWeaponEffects()
        {
            throw new NotImplementedException();
        }

        public void DrawAttacks(Graphics graphics, Size displaySize)
        {
            throw new NotImplementedException();
        }

        public void RemoveEffect(Effect weaponEffect)
        {
            throw new NotImplementedException();
        }

        public bool CheckHitTank(float projectileX, float projectileY)
        {
            throw new NotImplementedException();
        }

        public void InflictDamage(float damageX, float damageY, float explosionDamage, float radius)
        {
            throw new NotImplementedException();
        }

        public bool GravityStep()
        {
            throw new NotImplementedException();
        }

        public bool FinishTurn()
        {
            throw new NotImplementedException();
        }

        public void FindWinner()
        {
            throw new NotImplementedException();
        }

        public void NextRound()
        {
            throw new NotImplementedException();
        }
        
        public int GetWindSpeed()
        {
            wind = rng.Next(-100, 100);
            return wind;
        }
    }
}
