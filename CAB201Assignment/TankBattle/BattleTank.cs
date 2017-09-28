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
            this.currDurability = tankModel.GetTankArmour();

            this.angle = 0;
            this.power = 25;
            this.curr_weapon = 0;
            
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
            return this.angle;
                
        }

        public void SetAngle(float angle)
        {
            this.angle = angle;
        }

        public int GetCurrentPower()
        {
            return this.power;
        }

        public void SetPower(int power)
        {
            this.power = power;
        }

        public int GetWeaponIndex()
        {
            return this.curr_weapon;
        }
        public void SetWeapon(int newWeapon)
        {
            this.curr_weapon = newWeapon;
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
            throw new NotImplementedException();
        }
        public int Y()
        {
            throw new NotImplementedException();
        }

        public void Fire()
        {
            throw new NotImplementedException();
        }

        public void InflictDamage(int damageAmount)
        {
            throw new NotImplementedException();
        }

        public bool Exists()
        {
            throw new NotImplementedException();
        }

        public bool GravityStep()
        {
            throw new NotImplementedException();
        }
    }
}
