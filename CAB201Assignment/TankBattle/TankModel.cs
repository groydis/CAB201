using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TankBattle
{
    public abstract class TankModel
    {
        public const int WIDTH = 4;
        public const int HEIGHT = 3;
        public const int NUM_TANKS = 1;

        public abstract int[,] DisplayTankSprite(float angle);

        public static void LineDraw(int[,] graphic, int X1, int Y1, int X2, int Y2)
        {
            int dx = X2 - X1;
            int dy = Y2 - Y1;
            
            if (X1 > X2)
            {
                for (int x = X1; x != X2 - 1; x--)
                {
                    int y = Y1 + dy * (x - X1) / dx;
                    graphic[x, y] = 1;
                }
            } else if (X2 > X1)
            {
                for (int x = X1; x != X2 - 1; x++)
                {
                    int y = Y1 + dy * (x - X1) / dx;
                    graphic[x, y] = 1;
                }
            }
        }

        public Bitmap CreateBMP(Color tankColour, float angle)
        {
            int[,] tankGraphic = DisplayTankSprite(angle);
            int height = tankGraphic.GetLength(0);
            int width = tankGraphic.GetLength(1);

            Bitmap bmp = new Bitmap(width, height);
            Color transparent = Color.FromArgb(0, 0, 0, 0);
            Color tankOutline = Color.FromArgb(255, 0, 0, 0);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (tankGraphic[y, x] == 0)
                    {
                        bmp.SetPixel(x, y, transparent);
                    }
                    else
                    {
                        bmp.SetPixel(x, y, tankColour);
                    }
                }
            }

            // Outline each pixel
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    if (tankGraphic[y, x] != 0)
                    {
                        if (tankGraphic[y - 1, x] == 0)
                            bmp.SetPixel(x, y - 1, tankOutline);
                        if (tankGraphic[y + 1, x] == 0)
                            bmp.SetPixel(x, y + 1, tankOutline);
                        if (tankGraphic[y, x - 1] == 0)
                            bmp.SetPixel(x - 1, y, tankOutline);
                        if (tankGraphic[y, x + 1] == 0)
                            bmp.SetPixel(x + 1, y, tankOutline);
                    }
                }
            }

            return bmp;
        }

        public abstract int GetTankArmour();

        public abstract string[] WeaponList();

        public abstract void ActivateWeapon(int weapon, BattleTank playerTank, Gameplay currentGame);

        public static TankModel GetTank(int tankNumber)
        {
            return new NormTank();
        }
    }

    public class NormTank : TankModel
    {
        public override int GetTankArmour()
        {
            return 100;
        }
        public override string[] WeaponList()
        {
            return new string[] { "Standard Shell", "Nuke" };
        }

        public override int[,] DisplayTankSprite(float angle)
        {
            double length = 7;
            double end_Y = 0;
            double end_X = 0;
            float turret_angle = 0;
            int[,] norm = { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
                            { 0, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 0 },
                            { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
          
            // Checks to ensure line does not draw past given positions;
            if (angle >= 65)
            {
                turret_angle = 65;
            }
            else if (angle <= -65)
            {
                turret_angle = -65;
            }
            else
            {
                turret_angle = angle;
            }
            float angleRadians = (180 - turret_angle) * (float) Math.PI / 180;
            
            end_X = Math.Round(7 + (length * Math.Cos(angleRadians)));
            end_Y = Math.Round(7 + (length * Math.Sin(angleRadians)));

            LineDraw(norm, 7, 7, (int)end_X, (int)end_Y);
            return norm;
        }

        public override void ActivateWeapon(int weapon, BattleTank playerTank, Gameplay currentGame)
        {
            float gravity = 0;
            float x_pos = (float)playerTank.GetX() + (TankModel.WIDTH / 2);
            float y_pos = (float)playerTank.Y() + (TankModel.HEIGHT / 2);

            Opponent player = playerTank.GetPlayer();
            int explosionDmg = 0;
            int explosionRad = 0;
            int explosionDes = 0;


            if (weapon == 0)
            {
                gravity = 0.01f;
                explosionDmg = 100;
                explosionRad = 4;
                explosionDes = 4;
            }
            else if (weapon == 1)
            {
                gravity = 0.05f;
                explosionDmg = 200;
                explosionRad = 20;
                explosionDes = 10;
            }
            
            Blast blast = new Blast(explosionDmg, explosionRad, explosionDes);
            Shell shell = new Shell(x_pos, y_pos, playerTank.GetTankAngle(), playerTank.GetCurrentPower(), gravity, blast, player);
            currentGame.AddWeaponEffect(shell);       

        }
    }
}
