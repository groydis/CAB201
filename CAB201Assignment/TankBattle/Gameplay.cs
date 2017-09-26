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

        public static int[] CalcPlayerLocations(int numPlayers)
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
            throw new NotImplementedException();
        }

        public void BeginGame()
        {
            throw new NotImplementedException();
        }

        public void CommenceRound()
        {
            throw new NotImplementedException();
        }

        public Map GetArena()
        {
            throw new NotImplementedException();
        }

        public void DrawPlayers(Graphics graphics, Size displaySize)
        {
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
