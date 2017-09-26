using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TankBattle;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace TankBattleTestSuite
{
    class RequirementException : Exception
    {
        public RequirementException()
        {
        }

        public RequirementException(string message) : base(message)
        {
        }

        public RequirementException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    class Test
    {
        #region Testing Code

        private delegate bool TestCase();

        private static string ErrorDescription = null;

        private static void SetErrorDescription(string desc)
        {
            ErrorDescription = desc;
        }

        private static bool FloatEquals(float a, float b)
        {
            if (Math.Abs(a - b) < 0.01) return true;
            return false;
        }

        private static Dictionary<string, string> unitTestResults = new Dictionary<string, string>();

        private static void Passed(string name, string comment)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[passed] ");
            Console.ResetColor();
            Console.Write("{0}", name);
            if (comment != "")
            {
                Console.Write(": {0}", comment);
            }
            if (ErrorDescription != null)
            {
                throw new Exception("ErrorDescription found for passing test case");
            }
            Console.WriteLine();
        }
        private static void Failed(string name, string comment)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[failed] ");
            Console.ResetColor();
            Console.Write("{0}", name);
            if (comment != "")
            {
                Console.Write(": {0}", comment);
            }
            if (ErrorDescription != null)
            {
                Console.Write("\n{0}", ErrorDescription);
                ErrorDescription = null;
            }
            Console.WriteLine();
        }
        private static void FailedToMeetRequirement(string name, string comment)
        {
            Console.Write("[      ] ");
            Console.Write("{0}", name);
            if (comment != "")
            {
                Console.Write(": ");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("{0}", comment);
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        private static void DoTest(TestCase test)
        {
            // Have we already completed this test?
            if (unitTestResults.ContainsKey(test.Method.ToString()))
            {
                return;
            }

            bool passed = false;
            bool metRequirement = true;
            string exception = "";
            try
            {
                passed = test();
            }
            catch (RequirementException e)
            {
                metRequirement = false;
                exception = e.Message;
            }
            catch (Exception e)
            {
                exception = e.GetType().ToString();
            }

            string className = test.Method.ToString().Replace("Boolean Test", "").Split('0')[0];
            string fnName = test.Method.ToString().Split('0')[1];

            if (metRequirement)
            {
                if (passed)
                {
                    unitTestResults[test.Method.ToString()] = "Passed";
                    Passed(string.Format("{0}.{1}", className, fnName), exception);
                }
                else
                {
                    unitTestResults[test.Method.ToString()] = "Failed";
                    Failed(string.Format("{0}.{1}", className, fnName), exception);
                }
            }
            else
            {
                unitTestResults[test.Method.ToString()] = "Failed";
                FailedToMeetRequirement(string.Format("{0}.{1}", className, fnName), exception);
            }
            Cleanup();
        }

        private static Stack<string> errorDescriptionStack = new Stack<string>();


        private static void Requires(TestCase test)
        {
            string result;
            bool wasTested = unitTestResults.TryGetValue(test.Method.ToString(), out result);

            if (!wasTested)
            {
                // Push the error description onto the stack (only thing that can change, not that it should)
                errorDescriptionStack.Push(ErrorDescription);

                // Do the test
                DoTest(test);

                // Pop the description off
                ErrorDescription = errorDescriptionStack.Pop();

                // Get the proper result for out
                wasTested = unitTestResults.TryGetValue(test.Method.ToString(), out result);

                if (!wasTested)
                {
                    throw new Exception("This should never happen");
                }
            }

            if (result == "Failed")
            {
                string className = test.Method.ToString().Replace("Boolean Test", "").Split('0')[0];
                string fnName = test.Method.ToString().Split('0')[1];

                throw new RequirementException(string.Format("-> {0}.{1}", className, fnName));
            }
            else if (result == "Passed")
            {
                return;
            }
            else
            {
                throw new Exception("This should never happen");
            }

        }

        #endregion

        #region Test Cases
        private static Gameplay InitialiseGame()
        {
            Requires(TestGameplay0Gameplay);
            Requires(TestTankModel0GetTank);
            Requires(TestOpponent0PlayerController);
            Requires(TestGameplay0CreatePlayer);

            Gameplay game = new Gameplay(2, 1);
            TankModel tank = TankModel.GetTank(1);
            Opponent player1 = new PlayerController("player1", tank, Color.Orange);
            Opponent player2 = new PlayerController("player2", tank, Color.Purple);
            game.CreatePlayer(1, player1);
            game.CreatePlayer(2, player2);
            return game;
        }
        private static void Cleanup()
        {
            while (Application.OpenForms.Count > 0)
            {
                Application.OpenForms[0].Dispose();
            }
        }
        private static bool TestGameplay0Gameplay()
        {
            Gameplay game = new Gameplay(2, 1);
            return true;
        }
        private static bool TestGameplay0NumPlayers()
        {
            Requires(TestGameplay0Gameplay);

            Gameplay game = new Gameplay(2, 1);
            return game.NumPlayers() == 2;
        }
        private static bool TestGameplay0GetMaxRounds()
        {
            Requires(TestGameplay0Gameplay);

            Gameplay game = new Gameplay(3, 5);
            return game.GetMaxRounds() == 5;
        }
        private static bool TestGameplay0CreatePlayer()
        {
            Requires(TestGameplay0Gameplay);
            Requires(TestTankModel0GetTank);

            Gameplay game = new Gameplay(2, 1);
            TankModel tank = TankModel.GetTank(1);
            Opponent player = new PlayerController("playerName", tank, Color.Orange);
            game.CreatePlayer(1, player);
            return true;
        }
        private static bool TestGameplay0GetPlayer()
        {
            Requires(TestGameplay0Gameplay);
            Requires(TestTankModel0GetTank);
            Requires(TestOpponent0PlayerController);

            Gameplay game = new Gameplay(2, 1);
            TankModel tank = TankModel.GetTank(1);
            Opponent player = new PlayerController("playerName", tank, Color.Orange);
            game.CreatePlayer(1, player);
            return game.GetPlayer(1) == player;
        }
        private static bool TestGameplay0GetColour()
        {
            Color[] arrayOfColours = new Color[8];
            for (int i = 0; i < 8; i++)
            {
                arrayOfColours[i] = Gameplay.GetColour(i + 1);
                for (int j = 0; j < i; j++)
                {
                    if (arrayOfColours[j] == arrayOfColours[i]) return false;
                }
            }
            return true;
        }
        private static bool TestGameplay0GetPlayerLocations()
        {
            int[] positions = Gameplay.GetPlayerLocations(8);
            for (int i = 0; i < 8; i++)
            {
                if (positions[i] < 0) return false;
                if (positions[i] > 160) return false;
                for (int j = 0; j < i; j++)
                {
                    if (positions[j] == positions[i]) return false;
                }
            }
            return true;
        }
        private static bool TestGameplay0Shuffle()
        {
            int[] ar = new int[100];
            for (int i = 0; i < 100; i++)
            {
                ar[i] = i;
            }
            Gameplay.Shuffle(ar);
            for (int i = 0; i < 100; i++)
            {
                if (ar[i] != i)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool TestGameplay0BeginGame()
        {
            Gameplay game = InitialiseGame();
            game.BeginGame();

            foreach (Form f in Application.OpenForms)
            {
                if (f is GameplayForm)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool TestGameplay0GetArena()
        {
            Requires(TestMap0Map);
            Gameplay game = InitialiseGame();
            game.BeginGame();
            Map battlefield = game.GetArena();
            if (battlefield != null) return true;

            return false;
        }
        private static bool TestGameplay0GetCurrentPlayerTank()
        {
            Requires(TestGameplay0Gameplay);
            Requires(TestTankModel0GetTank);
            Requires(TestOpponent0PlayerController);
            Requires(TestGameplay0CreatePlayer);
            Requires(TestBattleTank0GetPlayer);

            Gameplay game = new Gameplay(2, 1);
            TankModel tank = TankModel.GetTank(1);
            Opponent player1 = new PlayerController("player1", tank, Color.Orange);
            Opponent player2 = new PlayerController("player2", tank, Color.Purple);
            game.CreatePlayer(1, player1);
            game.CreatePlayer(2, player2);

            game.BeginGame();
            BattleTank ptank = game.GetCurrentPlayerTank();
            if (ptank.GetPlayer() != player1 && ptank.GetPlayer() != player2)
            {
                return false;
            }
            if (ptank.GetTank() != tank)
            {
                return false;
            }

            return true;
        }

        private static bool TestTankModel0GetTank()
        {
            TankModel tank = TankModel.GetTank(1);
            if (tank != null) return true;
            else return false;
        }
        private static bool TestTankModel0DisplayTankSprite()
        {
            Requires(TestTankModel0GetTank);
            TankModel tank = TankModel.GetTank(1);

            int[,] tankGraphic = tank.DisplayTankSprite(45);
            if (tankGraphic.GetLength(0) != 12) return false;
            if (tankGraphic.GetLength(1) != 16) return false;
            // We don't really care what the tank looks like, but the 45 degree tank
            // should at least look different to the -45 degree tank
            int[,] tankGraphic2 = tank.DisplayTankSprite(-45);
            for (int y = 0; y < 12; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    if (tankGraphic2[y, x] != tankGraphic[y, x])
                    {
                        return true;
                    }
                }
            }

            SetErrorDescription("Tank with turret at -45 degrees looks the same as tank with turret at 45 degrees");

            return false;
        }
        private static void DisplayLine(int[,] array)
        {
            string report = "";
            report += "A line drawn from 3,0 to 0,3 on a 4x4 array should look like this:\n";
            report += "0001\n";
            report += "0010\n";
            report += "0100\n";
            report += "1000\n";
            report += "The one produced by TankModel.LineDraw() looks like this:\n";
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    report += array[y, x] == 1 ? "1" : "0";
                }
                report += "\n";
            }
            SetErrorDescription(report);
        }
        private static bool TestTankModel0LineDraw()
        {
            int[,] ar = new int[,] { { 0, 0, 0, 0 },
                                     { 0, 0, 0, 0 },
                                     { 0, 0, 0, 0 },
                                     { 0, 0, 0, 0 } };
            TankModel.LineDraw(ar, 3, 0, 0, 3);

            // Ideally, the line we want to see here is:
            // 0001
            // 0010
            // 0100
            // 1000

            // However, as we aren't that picky, as long as they have a 1 in every row and column
            // and nothing in the top-left and bottom-right corners

            int[] rows = new int[4];
            int[] cols = new int[4];
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (ar[y, x] == 1)
                    {
                        rows[y] = 1;
                        cols[x] = 1;
                    }
                    else if (ar[y, x] > 1 || ar[y, x] < 0)
                    {
                        // Only values 0 and 1 are permitted
                        SetErrorDescription(string.Format("Somehow the number {0} got into the array.", ar[y, x]));
                        return false;
                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (rows[i] == 0)
                {
                    DisplayLine(ar);
                    return false;
                }
                if (cols[i] == 0)
                {
                    DisplayLine(ar);
                    return false;
                }
            }
            if (ar[0, 0] == 1)
            {
                DisplayLine(ar);
                return false;
            }
            if (ar[3, 3] == 1)
            {
                DisplayLine(ar);
                return false;
            }

            return true;
        }
        private static bool TestTankModel0GetTankArmour()
        {
            Requires(TestTankModel0GetTank);
            // As long as it's > 0 we're happy
            TankModel tank = TankModel.GetTank(1);
            if (tank.GetTankArmour() > 0) return true;
            return false;
        }
        private static bool TestTankModel0WeaponList()
        {
            Requires(TestTankModel0GetTank);
            // As long as there's at least one result and it's not null / a blank string, we're happy
            TankModel tank = TankModel.GetTank(1);
            if (tank.WeaponList().Length == 0) return false;
            if (tank.WeaponList()[0] == null) return false;
            if (tank.WeaponList()[0] == "") return false;
            return true;
        }

        private static Opponent CreateTestingPlayer()
        {
            Requires(TestTankModel0GetTank);
            Requires(TestOpponent0PlayerController);

            TankModel tank = TankModel.GetTank(1);
            Opponent player = new PlayerController("player1", tank, Color.Aquamarine);
            return player;
        }

        private static bool TestOpponent0PlayerController()
        {
            Requires(TestTankModel0GetTank);

            TankModel tank = TankModel.GetTank(1);
            Opponent player = new PlayerController("player1", tank, Color.Aquamarine);
            if (player != null) return true;
            return false;
        }
        private static bool TestOpponent0GetTank()
        {
            Requires(TestTankModel0GetTank);
            Requires(TestOpponent0PlayerController);

            TankModel tank = TankModel.GetTank(1);
            Opponent p = new PlayerController("player1", tank, Color.Aquamarine);
            if (p.GetTank() == tank) return true;
            return false;
        }
        private static bool TestOpponent0Identifier()
        {
            Requires(TestTankModel0GetTank);
            Requires(TestOpponent0PlayerController);

            const string PLAYER_NAME = "kfdsahskfdajh";
            TankModel tank = TankModel.GetTank(1);
            Opponent p = new PlayerController(PLAYER_NAME, tank, Color.Aquamarine);
            if (p.Identifier() == PLAYER_NAME) return true;
            return false;
        }
        private static bool TestOpponent0GetColour()
        {
            Requires(TestTankModel0GetTank);
            Requires(TestOpponent0PlayerController);

            Color playerColour = Color.Chartreuse;
            TankModel tank = TankModel.GetTank(1);
            Opponent p = new PlayerController("player1", tank, playerColour);
            if (p.GetColour() == playerColour) return true;
            return false;
        }
        private static bool TestOpponent0AddScore()
        {
            Opponent p = CreateTestingPlayer();
            p.AddScore();
            return true;
        }
        private static bool TestOpponent0GetScore()
        {
            Requires(TestOpponent0AddScore);

            Opponent p = CreateTestingPlayer();
            int wins = p.GetScore();
            p.AddScore();
            if (p.GetScore() == wins + 1) return true;
            return false;
        }
        private static bool TestPlayerController0CommenceRound()
        {
            Opponent p = CreateTestingPlayer();
            p.CommenceRound();
            return true;
        }
        private static bool TestPlayerController0NewTurn()
        {
            Requires(TestGameplay0BeginGame);
            Requires(TestGameplay0GetPlayer);
            Gameplay game = InitialiseGame();

            game.BeginGame();

            // Find the gameplay form
            GameplayForm gameplayForm = null;
            foreach (Form f in Application.OpenForms)
            {
                if (f is GameplayForm)
                {
                    gameplayForm = f as GameplayForm;
                }
            }
            if (gameplayForm == null)
            {
                SetErrorDescription("Gameplay form was not created by Gameplay.BeginGame()");
                return false;
            }

            // Find the control panel
            Panel controlPanel = null;
            foreach (Control c in gameplayForm.Controls)
            {
                if (c is Panel)
                {
                    foreach (Control cc in c.Controls)
                    {
                        if (cc is NumericUpDown || cc is Label || cc is TrackBar)
                        {
                            controlPanel = c as Panel;
                        }
                    }
                }
            }

            if (controlPanel == null)
            {
                SetErrorDescription("Control panel was not found in GameplayForm");
                return false;
            }

            // Disable the control panel to check that NewTurn enables it
            controlPanel.Enabled = false;

            game.GetPlayer(1).NewTurn(gameplayForm, game);

            if (!controlPanel.Enabled)
            {
                SetErrorDescription("Control panel is still disabled after HumanPlayer.NewTurn()");
                return false;
            }
            return true;

        }
        private static bool TestPlayerController0HitPos()
        {
            Opponent p = CreateTestingPlayer();
            p.HitPos(0, 0);
            return true;
        }

        private static bool TestBattleTank0BattleTank()
        {
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            BattleTank playerTank = new BattleTank(p, 32, 32, game);
            return true;
        }
        private static bool TestBattleTank0GetPlayer()
        {
            Requires(TestBattleTank0BattleTank);
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            BattleTank playerTank = new BattleTank(p, 32, 32, game);
            if (playerTank.GetPlayer() == p) return true;
            return false;
        }
        private static bool TestBattleTank0GetTank()
        {
            Requires(TestBattleTank0BattleTank);
            Requires(TestOpponent0GetTank);
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            BattleTank playerTank = new BattleTank(p, 32, 32, game);
            if (playerTank.GetTank() == playerTank.GetPlayer().GetTank()) return true;
            return false;
        }
        private static bool TestBattleTank0GetTankAngle()
        {
            Requires(TestBattleTank0BattleTank);
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            BattleTank playerTank = new BattleTank(p, 32, 32, game);
            float angle = playerTank.GetTankAngle();
            if (angle >= -90 && angle <= 90) return true;
            return false;
        }
        private static bool TestBattleTank0SetAngle()
        {
            Requires(TestBattleTank0BattleTank);
            Requires(TestBattleTank0GetTankAngle);
            float angle = 75;
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            BattleTank playerTank = new BattleTank(p, 32, 32, game);
            playerTank.SetAngle(angle);
            if (FloatEquals(playerTank.GetTankAngle(), angle)) return true;
            return false;
        }
        private static bool TestBattleTank0GetCurrentPower()
        {
            Requires(TestBattleTank0BattleTank);
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            BattleTank playerTank = new BattleTank(p, 32, 32, game);

            playerTank.GetCurrentPower();
            return true;
        }
        private static bool TestBattleTank0SetPower()
        {
            Requires(TestBattleTank0BattleTank);
            Requires(TestBattleTank0GetCurrentPower);
            int power = 65;
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            BattleTank playerTank = new BattleTank(p, 32, 32, game);
            playerTank.SetPower(power);
            if (playerTank.GetCurrentPower() == power) return true;
            return false;
        }
        private static bool TestBattleTank0GetWeaponIndex()
        {
            Requires(TestBattleTank0BattleTank);

            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            BattleTank playerTank = new BattleTank(p, 32, 32, game);

            playerTank.GetWeaponIndex();
            return true;
        }
        private static bool TestBattleTank0SetWeapon()
        {
            Requires(TestBattleTank0BattleTank);
            Requires(TestBattleTank0GetWeaponIndex);
            int weapon = 3;
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            BattleTank playerTank = new BattleTank(p, 32, 32, game);
            playerTank.SetWeapon(weapon);
            if (playerTank.GetWeaponIndex() == weapon) return true;
            return false;
        }
        private static bool TestBattleTank0Display()
        {
            Requires(TestBattleTank0BattleTank);
            Size bitmapSize = new Size(640, 480);
            Bitmap image = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            Graphics graphics = Graphics.FromImage(image);
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            BattleTank playerTank = new BattleTank(p, 32, 32, game);
            playerTank.Display(graphics, bitmapSize);
            graphics.Dispose();

            for (int y = 0; y < bitmapSize.Height; y++)
            {
                for (int x = 0; x < bitmapSize.Width; x++)
                {
                    if (image.GetPixel(x, y) != image.GetPixel(0, 0))
                    {
                        // Something changed in the image, and that's good enough for me
                        return true;
                    }
                }
            }
            SetErrorDescription("Nothing was drawn.");
            return false;
        }
        private static bool TestBattleTank0GetX()
        {
            Requires(TestBattleTank0BattleTank);

            Opponent p = CreateTestingPlayer();
            int x = 73;
            int y = 28;
            Gameplay game = InitialiseGame();
            BattleTank playerTank = new BattleTank(p, x, y, game);
            if (playerTank.GetX() == x) return true;
            return false;
        }
        private static bool TestBattleTank0Y()
        {
            Requires(TestBattleTank0BattleTank);

            Opponent p = CreateTestingPlayer();
            int x = 73;
            int y = 28;
            Gameplay game = InitialiseGame();
            BattleTank playerTank = new BattleTank(p, x, y, game);
            if (playerTank.Y() == y) return true;
            return false;
        }
        private static bool TestBattleTank0Fire()
        {
            Requires(TestBattleTank0BattleTank);

            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            BattleTank playerTank = new BattleTank(p, 32, 32, game);
            playerTank.Fire();
            return true;
        }
        private static bool TestBattleTank0InflictDamage()
        {
            Requires(TestBattleTank0BattleTank);
            Opponent p = CreateTestingPlayer();

            Gameplay game = InitialiseGame();
            BattleTank playerTank = new BattleTank(p, 32, 32, game);
            playerTank.InflictDamage(10);
            return true;
        }
        private static bool TestBattleTank0Exists()
        {
            Requires(TestBattleTank0BattleTank);
            Requires(TestBattleTank0InflictDamage);

            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            BattleTank playerTank = new BattleTank(p, 32, 32, game);
            if (!playerTank.Exists()) return false;
            playerTank.InflictDamage(playerTank.GetTank().GetTankArmour());
            if (playerTank.Exists()) return false;
            return true;
        }
        private static bool TestBattleTank0GravityStep()
        {
            Requires(TestGameplay0GetArena);
            Requires(TestMap0DestroyTerrain);
            Requires(TestBattleTank0BattleTank);
            Requires(TestBattleTank0InflictDamage);
            Requires(TestBattleTank0Exists);
            Requires(TestBattleTank0GetTank);
            Requires(TestTankModel0GetTankArmour);

            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            game.BeginGame();
            // Unfortunately we need to rely on DestroyTerrain() to get rid of any terrain that may be in the way
            game.GetArena().DestroyTerrain(Map.WIDTH / 2.0f, Map.HEIGHT / 2.0f, 20);
            BattleTank playerTank = new BattleTank(p, Map.WIDTH / 2, Map.HEIGHT / 2, game);
            int oldX = playerTank.GetX();
            int oldY = playerTank.Y();

            playerTank.GravityStep();

            if (playerTank.GetX() != oldX)
            {
                SetErrorDescription("Caused X coordinate to change.");
                return false;
            }
            if (playerTank.Y() != oldY + 1)
            {
                SetErrorDescription("Did not cause Y coordinate to increase by 1.");
                return false;
            }

            int initialArmour = playerTank.GetTank().GetTankArmour();
            // The tank should have lost 1 armour from falling 1 tile already, so do
            // (initialArmour - 2) damage to the tank then drop it again. That should kill it.

            if (!playerTank.Exists())
            {
                SetErrorDescription("Tank died before we could check that fall damage worked properly");
                return false;
            }
            playerTank.InflictDamage(initialArmour - 2);
            if (!playerTank.Exists())
            {
                SetErrorDescription("Tank died before we could check that fall damage worked properly");
                return false;
            }
            playerTank.GravityStep();
            if (playerTank.Exists())
            {
                SetErrorDescription("Tank survived despite taking enough falling damage to destroy it");
                return false;
            }

            return true;
        }
        private static bool TestMap0Map()
        {
            Map battlefield = new Map();
            return true;
        }
        private static bool TestMap0Get()
        {
            Requires(TestMap0Map);

            bool foundTrue = false;
            bool foundFalse = false;
            Map battlefield = new Map();
            for (int y = 0; y < Map.HEIGHT; y++)
            {
                for (int x = 0; x < Map.WIDTH; x++)
                {
                    if (battlefield.Get(x, y))
                    {
                        foundTrue = true;
                    }
                    else
                    {
                        foundFalse = true;
                    }
                }
            }

            if (!foundTrue)
            {
                SetErrorDescription("IsTileAt() did not return true for any tile.");
                return false;
            }

            if (!foundFalse)
            {
                SetErrorDescription("IsTileAt() did not return false for any tile.");
                return false;
            }

            return true;
        }
        private static bool TestMap0CheckTankCollide()
        {
            Requires(TestMap0Map);
            Requires(TestMap0Get);

            Map battlefield = new Map();
            for (int y = 0; y <= Map.HEIGHT - TankModel.HEIGHT; y++)
            {
                for (int x = 0; x <= Map.WIDTH - TankModel.WIDTH; x++)
                {
                    int colTiles = 0;
                    for (int iy = 0; iy < TankModel.HEIGHT; iy++)
                    {
                        for (int ix = 0; ix < TankModel.WIDTH; ix++)
                        {

                            if (battlefield.Get(x + ix, y + iy))
                            {
                                colTiles++;
                            }
                        }
                    }
                    if (colTiles == 0)
                    {
                        if (battlefield.CheckTankCollide(x, y))
                        {
                            SetErrorDescription("Found collision where there shouldn't be one");
                            return false;
                        }
                    }
                    else
                    {
                        if (!battlefield.CheckTankCollide(x, y))
                        {
                            SetErrorDescription("Didn't find collision where there should be one");
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        private static bool TestMap0TankYPosition()
        {
            Requires(TestMap0Map);
            Requires(TestMap0Get);

            Map battlefield = new Map();
            for (int x = 0; x <= Map.WIDTH - TankModel.WIDTH; x++)
            {
                int lowestValid = 0;
                for (int y = 0; y <= Map.HEIGHT - TankModel.HEIGHT; y++)
                {
                    int colTiles = 0;
                    for (int iy = 0; iy < TankModel.HEIGHT; iy++)
                    {
                        for (int ix = 0; ix < TankModel.WIDTH; ix++)
                        {

                            if (battlefield.Get(x + ix, y + iy))
                            {
                                colTiles++;
                            }
                        }
                    }
                    if (colTiles == 0)
                    {
                        lowestValid = y;
                    }
                }

                int placedY = battlefield.TankYPosition(x);
                if (placedY != lowestValid)
                {
                    SetErrorDescription(string.Format("Tank was placed at {0},{1} when it should have been placed at {0},{2}", x, placedY, lowestValid));
                    return false;
                }
            }
            return true;
        }
        private static bool TestMap0DestroyTerrain()
        {
            Requires(TestMap0Map);
            Requires(TestMap0Get);

            Map battlefield = new Map();
            for (int y = 0; y < Map.HEIGHT; y++)
            {
                for (int x = 0; x < Map.WIDTH; x++)
                {
                    if (battlefield.Get(x, y))
                    {
                        battlefield.DestroyTerrain(x, y, 0.5f);
                        if (battlefield.Get(x, y))
                        {
                            SetErrorDescription("Attempted to destroy terrain but it still exists");
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            SetErrorDescription("Did not find any terrain to destroy");
            return false;
        }
        private static bool TestMap0GravityStep()
        {
            Requires(TestMap0Map);
            Requires(TestMap0Get);
            Requires(TestMap0DestroyTerrain);

            Map battlefield = new Map();
            for (int x = 0; x < Map.WIDTH; x++)
            {
                if (battlefield.Get(x, Map.HEIGHT - 1))
                {
                    if (battlefield.Get(x, Map.HEIGHT - 2))
                    {
                        // Seek up and find the first non-set tile
                        for (int y = Map.HEIGHT - 2; y >= 0; y--)
                        {
                            if (!battlefield.Get(x, y))
                            {
                                // Do a gravity step and make sure it doesn't slip down
                                battlefield.GravityStep();
                                if (!battlefield.Get(x, y + 1))
                                {
                                    SetErrorDescription("Moved down terrain even though there was no room");
                                    return false;
                                }

                                // Destroy the bottom-most tile
                                battlefield.DestroyTerrain(x, Map.HEIGHT - 1, 0.5f);

                                // Do a gravity step and make sure it does slip down
                                battlefield.GravityStep();

                                if (battlefield.Get(x, y + 1))
                                {
                                    SetErrorDescription("Terrain didn't fall");
                                    return false;
                                }

                                // Otherwise this seems to have worked
                                return true;
                            }
                        }


                    }
                }
            }
            SetErrorDescription("Did not find any appropriate terrain to test");
            return false;
        }
        private static bool TestEffect0ConnectGame()
        {
            Requires(TestBlast0Blast);
            Requires(TestGameplay0Gameplay);

            Effect weaponEffect = new Blast(1, 1, 1);
            Gameplay game = new Gameplay(2, 1);
            weaponEffect.ConnectGame(game);
            return true;
        }
        private static bool TestShell0Shell()
        {
            Requires(TestBlast0Blast);
            Opponent player = CreateTestingPlayer();
            Blast explosion = new Blast(1, 1, 1);
            Shell projectile = new Shell(25, 25, 45, 30, 0.02f, explosion, player);
            return true;
        }
        private static bool TestShell0Tick()
        {
            Requires(TestGameplay0BeginGame);
            Requires(TestBlast0Blast);
            Requires(TestShell0Shell);
            Requires(TestEffect0ConnectGame);
            Gameplay game = InitialiseGame();
            game.BeginGame();
            Opponent player = game.GetPlayer(1);
            Blast explosion = new Blast(1, 1, 1);

            Shell projectile = new Shell(25, 25, 45, 100, 0.01f, explosion, player);
            projectile.ConnectGame(game);
            projectile.Tick();

            // We can't really test this one without a substantial framework,
            // so we just call it and hope that everything works out

            return true;
        }
        private static bool TestShell0Display()
        {
            Requires(TestGameplay0BeginGame);
            Requires(TestGameplay0GetPlayer);
            Requires(TestBlast0Blast);
            Requires(TestShell0Shell);
            Requires(TestEffect0ConnectGame);

            Size bitmapSize = new Size(640, 480);
            Bitmap image = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(Color.Black); // Blacken out the image so we can see the projectile
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            game.BeginGame();
            Opponent player = game.GetPlayer(1);
            Blast explosion = new Blast(1, 1, 1);

            Shell projectile = new Shell(25, 25, 45, 100, 0.01f, explosion, player);
            projectile.ConnectGame(game);
            projectile.Display(graphics, bitmapSize);
            graphics.Dispose();

            for (int y = 0; y < bitmapSize.Height; y++)
            {
                for (int x = 0; x < bitmapSize.Width; x++)
                {
                    if (image.GetPixel(x, y) != image.GetPixel(0, 0))
                    {
                        // Something changed in the image, and that's good enough for me
                        return true;
                    }
                }
            }
            SetErrorDescription("Nothing was drawn.");
            return false;
        }
        private static bool TestBlast0Blast()
        {
            Opponent player = CreateTestingPlayer();
            Blast explosion = new Blast(1, 1, 1);

            return true;
        }
        private static bool TestBlast0Ignite()
        {
            Requires(TestBlast0Blast);
            Requires(TestEffect0ConnectGame);
            Requires(TestGameplay0GetPlayer);
            Requires(TestGameplay0BeginGame);

            Gameplay game = InitialiseGame();
            game.BeginGame();
            Opponent player = game.GetPlayer(1);
            Blast explosion = new Blast(1, 1, 1);
            explosion.ConnectGame(game);
            explosion.Ignite(25, 25);

            return true;
        }
        private static bool TestBlast0Tick()
        {
            Requires(TestBlast0Blast);
            Requires(TestEffect0ConnectGame);
            Requires(TestGameplay0GetPlayer);
            Requires(TestGameplay0BeginGame);
            Requires(TestBlast0Ignite);

            Gameplay game = InitialiseGame();
            game.BeginGame();
            Opponent player = game.GetPlayer(1);
            Blast explosion = new Blast(1, 1, 1);
            explosion.ConnectGame(game);
            explosion.Ignite(25, 25);
            explosion.Tick();

            // Again, we can't really test this one without a full framework

            return true;
        }
        private static bool TestBlast0Display()
        {
            Requires(TestBlast0Blast);
            Requires(TestEffect0ConnectGame);
            Requires(TestGameplay0GetPlayer);
            Requires(TestGameplay0BeginGame);
            Requires(TestBlast0Ignite);
            Requires(TestBlast0Tick);

            Size bitmapSize = new Size(640, 480);
            Bitmap image = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            Graphics graphics = Graphics.FromImage(image);
            graphics.Clear(Color.Black); // Blacken out the image so we can see the explosion
            Opponent p = CreateTestingPlayer();
            Gameplay game = InitialiseGame();
            game.BeginGame();
            Opponent player = game.GetPlayer(1);
            Blast explosion = new Blast(10, 10, 10);
            explosion.ConnectGame(game);
            explosion.Ignite(25, 25);
            // Step it for a bit so we can be sure the explosion is visible
            for (int i = 0; i < 10; i++)
            {
                explosion.Tick();
            }
            explosion.Display(graphics, bitmapSize);

            for (int y = 0; y < bitmapSize.Height; y++)
            {
                for (int x = 0; x < bitmapSize.Width; x++)
                {
                    if (image.GetPixel(x, y) != image.GetPixel(0, 0))
                    {
                        // Something changed in the image, and that's good enough for me
                        return true;
                    }
                }
            }
            SetErrorDescription("Nothing was drawn.");
            return false;
        }

        private static GameplayForm InitialiseGameplayForm(out NumericUpDown angleCtrl, out TrackBar powerCtrl, out Button fireCtrl, out Panel controlPanel, out ListBox weaponSelect)
        {
            Requires(TestGameplay0BeginGame);

            Gameplay game = InitialiseGame();

            angleCtrl = null;
            powerCtrl = null;
            fireCtrl = null;
            controlPanel = null;
            weaponSelect = null;

            game.BeginGame();
            GameplayForm gameplayForm = null;
            foreach (Form f in Application.OpenForms)
            {
                if (f is GameplayForm)
                {
                    gameplayForm = f as GameplayForm;
                }
            }
            if (gameplayForm == null)
            {
                SetErrorDescription("Gameplay.BeginGame() did not create a GameplayForm and that is the only way GameplayForm can be tested");
                return null;
            }

            bool foundDisplayPanel = false;
            bool foundControlPanel = false;

            foreach (Control c in gameplayForm.Controls)
            {
                // The only controls should be 2 panels
                if (c is Panel)
                {
                    // Is this the control panel or the display panel?
                    Panel p = c as Panel;

                    // The display panel will have 0 controls.
                    // The control panel will have separate, of which only a few are mandatory
                    int controlsFound = 0;
                    bool foundFire = false;
                    bool foundAngle = false;
                    bool foundAngleLabel = false;
                    bool foundPower = false;
                    bool foundPowerLabel = false;


                    foreach (Control pc in p.Controls)
                    {
                        controlsFound++;

                        // Mandatory controls for the control panel are:
                        // A 'Fire!' button
                        // A NumericUpDown for controlling the angle
                        // A TrackBar for controlling the power
                        // "Power:" and "Angle:" labels

                        if (pc is Label)
                        {
                            Label lbl = pc as Label;
                            if (lbl.Text.ToLower().Contains("angle"))
                            {
                                foundAngleLabel = true;
                            }
                            else
                            if (lbl.Text.ToLower().Contains("power"))
                            {
                                foundPowerLabel = true;
                            }
                        }
                        else
                        if (pc is Button)
                        {
                            Button btn = pc as Button;
                            if (btn.Text.ToLower().Contains("fire"))
                            {
                                foundFire = true;
                                fireCtrl = btn;
                            }
                        }
                        else
                        if (pc is TrackBar)
                        {
                            foundPower = true;
                            powerCtrl = pc as TrackBar;
                        }
                        else
                        if (pc is NumericUpDown)
                        {
                            foundAngle = true;
                            angleCtrl = pc as NumericUpDown;
                        }
                        else
                        if (pc is ListBox)
                        {
                            weaponSelect = pc as ListBox;
                        }
                    }

                    if (controlsFound == 0)
                    {
                        foundDisplayPanel = true;
                    }
                    else
                    {
                        if (!foundFire)
                        {
                            SetErrorDescription("Control panel lacks a \"Fire!\" button OR the display panel incorrectly contains controls");
                            return null;
                        }
                        else
                        if (!foundAngle)
                        {
                            SetErrorDescription("Control panel lacks an angle NumericUpDown OR the display panel incorrectly contains controls");
                            return null;
                        }
                        else
                        if (!foundPower)
                        {
                            SetErrorDescription("Control panel lacks a power TrackBar OR the display panel incorrectly contains controls");
                            return null;
                        }
                        else
                        if (!foundAngleLabel)
                        {
                            SetErrorDescription("Control panel lacks an \"Angle:\" label OR the display panel incorrectly contains controls");
                            return null;
                        }
                        else
                        if (!foundPowerLabel)
                        {
                            SetErrorDescription("Control panel lacks a \"Power:\" label OR the display panel incorrectly contains controls");
                            return null;
                        }

                        foundControlPanel = true;
                        controlPanel = p;
                    }

                }
                else
                {
                    SetErrorDescription(string.Format("Unexpected control ({0}) named \"{1}\" found in GameplayForm", c.GetType().FullName, c.Name));
                    return null;
                }
            }

            if (!foundDisplayPanel)
            {
                SetErrorDescription("No display panel found");
                return null;
            }
            if (!foundControlPanel)
            {
                SetErrorDescription("No control panel found");
                return null;
            }
            return gameplayForm;
        }

        private static bool TestGameplayForm0GameplayForm()
        {
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            GameplayForm gameplayForm = InitialiseGameplayForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            return true;
        }
        private static bool TestGameplayForm0EnableControlPanel()
        {
            Requires(TestGameplayForm0GameplayForm);
            Gameplay game = InitialiseGame();
            game.BeginGame();

            // Find the gameplay form
            GameplayForm gameplayForm = null;
            foreach (Form f in Application.OpenForms)
            {
                if (f is GameplayForm)
                {
                    gameplayForm = f as GameplayForm;
                }
            }
            if (gameplayForm == null)
            {
                SetErrorDescription("Gameplay form was not created by Gameplay.BeginGame()");
                return false;
            }

            // Find the control panel
            Panel controlPanel = null;
            foreach (Control c in gameplayForm.Controls)
            {
                if (c is Panel)
                {
                    foreach (Control cc in c.Controls)
                    {
                        if (cc is NumericUpDown || cc is Label || cc is TrackBar)
                        {
                            controlPanel = c as Panel;
                        }
                    }
                }
            }

            if (controlPanel == null)
            {
                SetErrorDescription("Control panel was not found in GameplayForm");
                return false;
            }

            // Disable the control panel to check that EnableControlPanel enables it
            controlPanel.Enabled = false;

            gameplayForm.EnableControlPanel();

            if (!controlPanel.Enabled)
            {
                SetErrorDescription("Control panel is still disabled after GameplayForm.EnableControlPanel()");
                return false;
            }
            return true;

        }
        private static bool TestGameplayForm0SetAngle()
        {
            Requires(TestGameplayForm0GameplayForm);
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            GameplayForm gameplayForm = InitialiseGameplayForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            float testAngle = 27;

            gameplayForm.SetAngle(testAngle);
            if (FloatEquals((float)angle.Value, testAngle)) return true;

            else
            {
                SetErrorDescription(string.Format("Attempted to set angle to {0} but angle is {1}", testAngle, (float)angle.Value));
                return false;
            }
        }
        private static bool TestGameplayForm0SetPower()
        {
            Requires(TestGameplayForm0GameplayForm);
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            GameplayForm gameplayForm = InitialiseGameplayForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            int testPower = 71;

            gameplayForm.SetPower(testPower);
            if (power.Value == testPower) return true;

            else
            {
                SetErrorDescription(string.Format("Attempted to set power to {0} but power is {1}", testPower, power.Value));
                return false;
            }
        }
        private static bool TestGameplayForm0SetWeapon()
        {
            Requires(TestGameplayForm0GameplayForm);
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            GameplayForm gameplayForm = InitialiseGameplayForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            if (gameplayForm == null) return false;

            gameplayForm.SetWeapon(0);

            // WeaponSelect is optional behaviour, so it's okay if it's not implemented here, as long as the method works.
            return true;
        }
        private static bool TestGameplayForm0Fire()
        {
            Requires(TestGameplayForm0GameplayForm);
            // This is something we can't really test properly without a proper framework, so for now we'll just click
            // the button and make sure it disables the control panel
            NumericUpDown angle;
            TrackBar power;
            Button fire;
            Panel controlPanel;
            ListBox weaponSelect;
            GameplayForm gameplayForm = InitialiseGameplayForm(out angle, out power, out fire, out controlPanel, out weaponSelect);

            controlPanel.Enabled = true;
            fire.PerformClick();
            if (controlPanel.Enabled)
            {
                SetErrorDescription("Control panel still enabled immediately after clicking fire button");
                return false;
            }

            return true;
        }
        private static void UnitTests()
        {
            DoTest(TestGameplay0Gameplay);
            DoTest(TestGameplay0NumPlayers);
            DoTest(TestGameplay0GetMaxRounds);
            DoTest(TestGameplay0CreatePlayer);
            DoTest(TestGameplay0GetPlayer);
            DoTest(TestGameplay0GetColour);
            DoTest(TestGameplay0GetPlayerLocations);
            DoTest(TestGameplay0Shuffle);
            DoTest(TestGameplay0BeginGame);
            DoTest(TestGameplay0GetArena);
            DoTest(TestGameplay0GetCurrentPlayerTank);
            DoTest(TestTankModel0GetTank);
            DoTest(TestTankModel0DisplayTankSprite);
            DoTest(TestTankModel0LineDraw);
            DoTest(TestTankModel0GetTankArmour);
            DoTest(TestTankModel0WeaponList);
            DoTest(TestOpponent0PlayerController);
            DoTest(TestOpponent0GetTank);
            DoTest(TestOpponent0Identifier);
            DoTest(TestOpponent0GetColour);
            DoTest(TestOpponent0AddScore);
            DoTest(TestOpponent0GetScore);
            DoTest(TestPlayerController0CommenceRound);
            DoTest(TestPlayerController0NewTurn);
            DoTest(TestPlayerController0HitPos);
            DoTest(TestBattleTank0BattleTank);
            DoTest(TestBattleTank0GetPlayer);
            DoTest(TestBattleTank0GetTank);
            DoTest(TestBattleTank0GetTankAngle);
            DoTest(TestBattleTank0SetAngle);
            DoTest(TestBattleTank0GetCurrentPower);
            DoTest(TestBattleTank0SetPower);
            DoTest(TestBattleTank0GetWeaponIndex);
            DoTest(TestBattleTank0SetWeapon);
            DoTest(TestBattleTank0Display);
            DoTest(TestBattleTank0GetX);
            DoTest(TestBattleTank0Y);
            DoTest(TestBattleTank0Fire);
            DoTest(TestBattleTank0InflictDamage);
            DoTest(TestBattleTank0Exists);
            DoTest(TestBattleTank0GravityStep);
            DoTest(TestMap0Map);
            DoTest(TestMap0Get);
            DoTest(TestMap0CheckTankCollide);
            DoTest(TestMap0TankYPosition);
            DoTest(TestMap0DestroyTerrain);
            DoTest(TestMap0GravityStep);
            DoTest(TestEffect0ConnectGame);
            DoTest(TestShell0Shell);
            DoTest(TestShell0Tick);
            DoTest(TestShell0Display);
            DoTest(TestBlast0Blast);
            DoTest(TestBlast0Ignite);
            DoTest(TestBlast0Tick);
            DoTest(TestBlast0Display);
            DoTest(TestGameplayForm0GameplayForm);
            DoTest(TestGameplayForm0EnableControlPanel);
            DoTest(TestGameplayForm0SetAngle);
            DoTest(TestGameplayForm0SetPower);
            DoTest(TestGameplayForm0SetWeapon);
            DoTest(TestGameplayForm0Fire);
        }
        
        #endregion
        
        #region CheckClasses

        private static bool CheckClasses()
        {
            string[] classNames = new string[] { "Program", "AIPlayer", "Map", "Blast", "GameplayForm", "Gameplay", "PlayerController", "Shell", "Opponent", "BattleTank", "TankModel", "Effect" };
            string[][] classFields = new string[][] {
                new string[] { "Main" }, // Program
                new string[] { }, // AIPlayer
                new string[] { "Get","CheckTankCollide","TankYPosition","DestroyTerrain","GravityStep","WIDTH","HEIGHT"}, // Map
                new string[] { "Ignite" }, // Blast
                new string[] { "EnableControlPanel","SetAngle","SetPower","SetWeapon","Fire","InitialiseBuffer"}, // GameplayForm
                new string[] { "NumPlayers","GetRoundNumber","GetMaxRounds","CreatePlayer","GetPlayer","GetGameplayTank","GetColour","GetPlayerLocations","Shuffle","BeginGame","CommenceRound","GetArena","DrawPlayers","GetCurrentPlayerTank","AddWeaponEffect","ProcessWeaponEffects","DrawAttacks","RemoveEffect","CheckHitTank","InflictDamage","GravityStep","FinishTurn","FindWinner","NextRound","GetWindSpeed"}, // Gameplay
                new string[] { }, // PlayerController
                new string[] { }, // Shell
                new string[] { "GetTank","Identifier","GetColour","AddScore","GetScore","CommenceRound","NewTurn","HitPos"}, // Opponent
                new string[] { "GetPlayer","GetTank","GetTankAngle","SetAngle","GetCurrentPower","SetPower","GetWeaponIndex","SetWeapon","Display","GetX","Y","Fire","InflictDamage","Exists","GravityStep"}, // BattleTank
                new string[] { "DisplayTankSprite","LineDraw","CreateBMP","GetTankArmour","WeaponList","ActivateWeapon","GetTank","WIDTH","HEIGHT","NUM_TANKS"}, // TankModel
                new string[] { "ConnectGame","Tick","Display"} // Effect
            };

            Assembly assembly = Assembly.GetExecutingAssembly();

            Console.WriteLine("Checking classes for public methods...");
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsPublic)
                {
                    if (type.Namespace != "TankBattle")
                    {
                        Console.WriteLine("Public type {0} is not in the TankBattle namespace.", type.FullName);
                        return false;
                    }
                    else
                    {
                        int typeIdx = -1;
                        for (int i = 0; i < classNames.Length; i++)
                        {
                            if (type.Name == classNames[i])
                            {
                                typeIdx = i;
                                classNames[typeIdx] = null;
                                break;
                            }
                        }
                        foreach (MemberInfo memberInfo in type.GetMembers())
                        {
                            string memberName = memberInfo.Name;
                            bool isInherited = false;
                            foreach (MemberInfo parentMemberInfo in type.BaseType.GetMembers())
                            {
                                if (memberInfo.Name == parentMemberInfo.Name)
                                {
                                    isInherited = true;
                                    break;
                                }
                            }
                            if (!isInherited)
                            {
                                if (typeIdx != -1)
                                {
                                    bool fieldFound = false;
                                    if (memberName[0] != '.')
                                    {
                                        foreach (string allowedFields in classFields[typeIdx])
                                        {
                                            if (memberName == allowedFields)
                                            {
                                                fieldFound = true;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        fieldFound = true;
                                    }
                                    if (!fieldFound)
                                    {
                                        Console.WriteLine("The public field \"{0}\" is not one of the authorised fields for the {1} class.\n", memberName, type.Name);
                                        Console.WriteLine("Remove it or change its access level.");
                                        return false;
                                    }
                                }
                            }
                        }
                    }

                    //Console.WriteLine("{0} passed.", type.FullName);
                }
            }
            for (int i = 0; i < classNames.Length; i++)
            {
                if (classNames[i] != null)
                {
                    Console.WriteLine("The class \"{0}\" is missing.", classNames[i]);
                    return false;
                }
            }
            Console.WriteLine("All public methods okay.");
            return true;
        }
        
        #endregion

        public static void Main()
        {
            if (CheckClasses())
            {
                UnitTests();

                int passed = 0;
                int failed = 0;
                foreach (string key in unitTestResults.Keys)
                {
                    if (unitTestResults[key] == "Passed")
                    {
                        passed++;
                    }
                    else
                    {
                        failed++;
                    }
                }

                Console.WriteLine("\n{0}/{1} unit tests passed", passed, passed + failed);
                if (failed == 0)
                {
                    Console.WriteLine("Starting up TankBattle...");
                    Program.Main();
                    return;
                }
            }

            Console.WriteLine("\nPress enter to exit.");
            Console.ReadLine();
        }
    }
}
