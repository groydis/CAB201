using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class BattleTank
    {
        private float angle;
        private int power;
        private int curr_weapon;

        private int tankX;
        private int tankY;
        private int currDurability;

        private Opponent player;

        private Gameplay game;
        private TankModel tankModel;
        private Bitmap tankBmp;
        private Color plColour;

        /// <summary>
        /// Creates a BattleTank with the passed information
        /// Setting the colour with GetColour()
        /// Durability with GetTankArmour();
        /// TankModel with GetTank();
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="player">The player associated with the Battle Tanks</param> 
        /// <param name="tankX">The X Position</param> 
        /// <param name="tankY">The Y position</param> 
        /// <param name="game">Current games</param> 
        /// <returns>explanation of return value</returns>
        public BattleTank(Opponent player, int tankX, int tankY, Gameplay game)
        {
            this.player = player;
            this.tankX = tankX;
            this.tankY = tankY;

            tankModel = player.GetTank();
            currDurability = tankModel.GetTankArmour();

            angle = 0;
            power = 25;
            curr_weapon = 0;

            plColour = player.GetColour();
            
            tankBmp = tankModel.CreateBMP(plColour, angle);

            this.game = game;
        }

        /// <summary>
        /// Returns the player assocaited with the BattleTank
        /// Written, edited and tested by both team members
        /// </summary>
        /// <returns>Returns the Opponent type of the BattleTank</returns>
        public Opponent GetPlayer()
        {
            return player;
        }

        /// <summary>
        /// Returns the TankModel assocaited with the BattleTank
        /// Written, edited and tested by both team members
        /// </summary>
        /// <returns>Returns the TankModel of the BattleTank</returns>
        public TankModel GetTank()
        {
            return player.GetTank();
        }

        /// <summary>
        /// Returns the current angle of the BattleTank
        /// Written, edited and tested by both team members
        /// </summary>
        /// <returns>Returns the Angle as a Float of the BattleTank</returns>
        public float GetTankAngle()
        {
            return angle;              
        }

        /// <summary>
        /// Modifies the sprite bitmap with teh angle
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="angle">The angle to passed</param> 
        public void SetAngle(float angle)
        {
            this.angle = angle;
            tankBmp = tankModel.CreateBMP(plColour, angle);
        }

        /// <summary>
        /// Returns the power of the BattleTank
        /// Written, edited and tested by both team members
        /// </summary>
        /// <returns>Returns an Int equal to the power of the BattleTank</returns>
        public int GetCurrentPower()
        {
            return power;
        }

        /// <summary>
        /// Sets the power of the BattleTank
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="power">The power to be set</param> 
        public void SetPower(int power)
        {
            this.power = power;

        }

        /// <summary>
        /// Returns the current index of the current weapon being used by the BattleTank
        /// Written, edited and tested by both team members
        /// </summary>
        /// <returns>Returns an Int equal to that of the current weapon</returns>
        public int GetWeaponIndex()
        {
            return curr_weapon;
        }

        /// <summary>
        /// Sets the weapon to the passed Int
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="newWeapon">Int representing the new weapon</param> 
        public void SetWeapon(int newWeapon)
        {
            curr_weapon = newWeapon;
        }

        /// <summary>
        /// This method draws the BattleTank to graphics, scaled to the provided displaySize. 
        /// The BattleTank's durability will also be shown as a percentage.
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="graphics">Graphics</param> 
        /// <param name="displaySize">Display Size</param> 
        public void Display(Graphics graphics, Size displaySize)
        {
            int x = tankX;
            int y = tankY;
            int startAmrour = 100;

            int drawX1 = displaySize.Width * x / Map.WIDTH;
            int drawY1 = displaySize.Height * y / Map.HEIGHT;
            int drawX2 = displaySize.Width * (x + TankModel.WIDTH) / Map.WIDTH;
            int drawY2 = displaySize.Height * (y + TankModel.HEIGHT) / Map.HEIGHT;

            graphics.DrawImage(tankBmp, new Rectangle(drawX1, drawY1, drawX2 - drawX1, drawY2 - drawY1));

            int drawY3 = displaySize.Height * (y - TankModel.HEIGHT) / Map.HEIGHT;
            Font font = new Font("Arial", 8);
            Brush brush = new SolidBrush(Color.White);

            int pct = currDurability * 100 / startAmrour;
            if (pct < 100)
            {
                graphics.DrawString(pct + "%", font, brush, new Point(drawX1, drawY3));
            }

        }

        /// <summary>
        /// Returns the X Position of the Battle Tank
        /// Written, edited and tested by both team members
        /// </summary>
        /// <returns> Returns an Int equal to the X position of the BattleTank</returns>
        public int GetX()
        {
            return tankX;
        }

        /// <summary>
        /// Returns the Y Position of the Battle Tank
        /// Written, edited and tested by both team members
        /// </summary>
        /// <returns> Returns an Int equal to the Y position of the BattleTank</returns>
        public int Y()
        {
            return tankY;
        }

        /// <summary>
        /// Gets the current Tank and Activates the weapon
        /// Written, edited and tested by both team members
        /// </summary>
        public void Fire()
        {
            tankModel = GetTank();

            tankModel.ActivateWeapon(curr_weapon ,this ,game);
        }

        /// <summary>
        /// Reduces the current durability of teh BattleTank by the damage passed
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="damageAmount">Amount of damage to inflict on the Player</param> 
        public void InflictDamage(int damageAmount)
        {
            currDurability -= damageAmount;
        }

        /// <summary>
        /// Bool, checks the BattleTanks current durability and returns true if greater than ZERO
        /// Written, edited and tested by both team members
        /// </summary>
        /// <returns> Returns true if current duability greater than ZERO, returns false if less than or equal to ZERO</returns> 
        public bool Exists()
        {
            if (currDurability > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Moves thank BattleTank down on the Y axis if there is no ground underneath it
        /// If the tank doesn't exist will return false
        /// If the tank falls off the screen, will destroy the tank and return true
        /// </summary>
        public bool GravityStep()
        {
            if (Exists() == false)
            {
                return false;
            }
            Map map = this.game.GetArena();
            int x = GetX();
            int y = Y();
            if (map.CheckTankCollide(x, y + 1))
            {
                return false;
            }
            else
            {
                tankY++;
                currDurability--;
                if (tankY == Map.HEIGHT - TankModel.HEIGHT)
                {
                    currDurability = 0;
                    return true;
                }
            }
            return false;
        }
    }
}
