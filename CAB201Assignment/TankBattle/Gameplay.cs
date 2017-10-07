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
        List<Effect> effects;

        private Opponent[] noPlayers;
        private Opponent[] noRounds;

        private Map arena;

        private BattleTank[] battleTanks;

        private int curr_round = 1;
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

            effects = new List<Effect>();
        }

        public int NumPlayers()
        {
            return noPlayers.Length;
        }

        public int GetRoundNumber()
        {
            return curr_round;
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
            return battleTanks[playerNum];
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
            curr_player = start_player;

            arena = new Map();
            
            int [] positions = GetPlayerLocations(noPlayers.Length);
            
            for (int i =0; i < noPlayers.Length; i++)
            {
                noPlayers[i].CommenceRound();                
            }
            
            Shuffle(positions);
            
            battleTanks = new BattleTank[noPlayers.Length];
            
            for (int i = 0; i < noPlayers.Length - 1;i++)
            {
                int X_pos = positions[i];
                int Y_pos = arena.TankYPosition(X_pos);

                battleTanks[i] = new BattleTank(noPlayers[i], X_pos, Y_pos, this);
 
            }
            
            wind = GetWindSpeed();

            GameplayForm gameplayForm = new GameplayForm(this);
            gameplayForm.Show(); 

        }

        public Map GetArena()
        {
            return arena;
        }

        public void DrawPlayers(Graphics graphics, Size displaySize)
        {
            for (int i = 0; i < battleTanks.Length - 1; i++)
            {
                if (battleTanks[i].Exists())
                {
                    battleTanks[i].Display(graphics, displaySize);
                }
            }


            /*Console.WriteLine("Draw Players in Gameplay");
            foreach (BattleTank tank in battleTanks)
            {
                Console.WriteLine("For Loop");
                if (tank.Exists() == true)
                {
                    Console.WriteLine("Tank Display");
                    tank.Display(graphics, displaySize);
                    Console.WriteLine("Success");
                } else
                {
                    Console.WriteLine("Tank does not exist");
                }
            }
            */
        }

        public BattleTank GetCurrentPlayerTank()
        {
            return battleTanks[curr_player];
        }

        public void AddWeaponEffect(Effect weaponEffect)
        {
            effects.Add(weaponEffect);
        }

        public bool ProcessWeaponEffects()
        {
            bool ans = false;
            foreach (Effect effect in effects)
            {
                effect.Tick();
                ans = true;                
            }
            return ans;
        }

        public void DrawAttacks(Graphics graphics, Size displaySize)
        {
            foreach(Effect effect in effects)
            {
                effect.Display(graphics, displaySize);
            }
        }

        public void RemoveEffect(Effect weaponEffect)
        {
            effects.Remove(weaponEffect);
        }

        public bool CheckHitTank(float projectileX, float projectileY)
        {
            if (projectileX < 0 || projectileX > Map.WIDTH)
            {
                if (projectileY < 0 || projectileY > Map.HEIGHT)
                {
                    return false;
                }
            }

            if (arena.Get((int)projectileX,(int)projectileY) == true)
            {
                return true;
            }

            for (int i = 0; i < battleTanks.Length; i++)
            {
                int x_pos = battleTanks[i].GetX();
                int y_pos = battleTanks[i].Y();
                int right = x_pos + TankModel.WIDTH;
                int bottom = y_pos + TankModel.HEIGHT;

                if (projectileX >= x_pos || projectileX <= right)
                {
                    if(projectileY >= y_pos || projectileY <= bottom)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        public void InflictDamage(float damageX, float damageY, float explosionDamage, float radius)
        {
            float overallDamage = explosionDamage;
            for (int i = 0; i < battleTanks.Length; i++)
            {
                if (battleTanks[i].Exists() == true)
                {
                    float[] pos = { battleTanks[i].GetX() + (TankModel.HEIGHT / 2), battleTanks[i].Y() + (TankModel.HEIGHT / 2)  };

                    float dist = (float)Math.Sqrt(Math.Pow(pos[0] - damageX, 2) + Math.Pow(pos[1] - damageY, 2));

                    if (dist > radius/2 && dist < radius)
                    {
                        float diff = dist - radius;

                        overallDamage = explosionDamage * diff / radius;
                    }
                    if (dist < radius / 2)
                    {
                        overallDamage = explosionDamage;
                    }
                    battleTanks[i].InflictDamage((int)overallDamage);
                }
            }
        }

        public bool GravityStep()
        {
            bool moved = false;

            if (arena.GravityStep() == true)
            {
                moved = true;
            }

            for(int i = 0; i < battleTanks.Length - 1; i++)
            {
                if(battleTanks[i].GravityStep() == true)
                {
                    moved = true;
                }
            }

            return moved;
        }

        public bool FinishTurn()
        {
            int playersLeft = 0;
            for (int i = 0; i< battleTanks.Length - 1; i++)
            {
                if (battleTanks[i].Exists() == true)
                {
                    playersLeft++;
                }
                if (playersLeft >= 2)
                {
                    curr_player++;
                    if (battleTanks[curr_player].Exists() == true)
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
                    } else
                    {
                        curr_player--;
                        if (i == noPlayers.Length)
                        {
                            i = 0;
                        }
                    }

                } else if(playersLeft <= 1)
                {
                    FindWinner();
                    return false;
                }
            }
            return false;
        }

        public void FindWinner()
        {
            for (int i = 0; i < noPlayers.Length- 1; i++)
            {
                if (battleTanks[i].Exists() == true)
                {
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
            if (curr_round > noPlayers.Length)
            {
                MainMenuForm newMainMenu = new MainMenuForm();
                newMainMenu.Show();
            }
        }
        
        public int GetWindSpeed()
        {
            wind = rng.Next(-100, 100);
            return wind;
        }
    }
}
