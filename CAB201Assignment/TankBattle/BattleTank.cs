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

        public BattleTank(Opponent player, int tankX, int tankY, Gameplay game)
        {
            this.player = player;
            this.tankX = tankX;
            this.tankY = tankY;

            this.tankModel = player.GetTank();
            currDurability = tankModel.GetTankArmour();

            angle = 0;
            power = 25;
            curr_weapon = 0;
            
            this.tankBmp = tankModel.CreateBMP(player.GetColour(), angle);

            this.game = game;
        }

        public Opponent GetPlayer()
        {
            return this.player;
        }

        public TankModel GetTank()
        {
            return this.player.GetTank();
        }

        public float GetTankAngle()
        {
            return angle;
                
        }

        public void SetAngle(float angle)
        {
            this.angle = angle;
        }

        public int GetCurrentPower()
        {
            return power;
        }

        public void SetPower(int power)
        {
            this.power = power;
        }

        public int GetWeaponIndex()
        {
            return curr_weapon;
        }
        public void SetWeapon(int newWeapon)
        {
            curr_weapon = newWeapon;
        }

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

        public int GetX()
        {
            return tankX;
        }
        public int Y()
        {
            return tankY;
        }

        public void Fire()
        {
            tankModel = GetTank();

            tankModel.ActivateWeapon(curr_weapon ,this ,this.game);
        }

        public void InflictDamage(int damageAmount)
        {
            currDurability -= damageAmount;
        }

        public bool Exists()
        {
            if (currDurability <= 0)
            {
                return false;
            }
            return true;
        }

        public bool GravityStep()
        {
            throw new NotImplementedException();
        }
    }
}
