using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_BaseballGameCreator
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BaseballGameScreen : ContentPage
    {
        public static ArrayList awayTeamBattersX = new ArrayList();
        public static ArrayList homeTeamBattersX = new ArrayList();
        public static ArrayList awayTeamPBEX = new ArrayList();
        public static ArrayList homeTeamPBEX = new ArrayList();

        public static int botSX, topSX, leftSX, rightSX, midSX, ballRR, aPRandom, hPRandom, aBRandom, hBRandom;
        public static int inn, awayBatterOrder, homeBatterOrder, strikeCount, ballCount, outCount, atScore, htScore, fbCol, sbCol, tbCol;
        public static int awayPitchesThrown, homePitchesThrown;
        public static double inn2;

        public static BoxView firstB, secondB, thirdB;
        public static BoxView s1, s2, s3, b1, b2, b3, b4, o1, o2, o3;

        public static object important;

        public static Label actionBarX, ATScoreTX, HTScoreTX, TBInd, InnTB;

        public static string currentAwayPitcher, currentHomePitcher;

        /*public static string[] XY = new string[1];
        public static List<string> XZ = new List<string>();
        public static List<string> XYZ = new List<string>();*/

        public static string FINALGAMELOG;

        public static string ATNameXYZ, HTNameXYZ;

        //public static string path1, pathA, pathB, pathX, gameLog;

        public BaseballGameScreen()
        {
            InitializeComponent();
            gameStart();
        }

        public static string getFINALGAMELOG()
        { return FINALGAMELOG; }
        public static string getAwayTeamNameForGL()
        { return ATNameXYZ; }
        public static string getHomeTeamNameForGL()
        { return HTNameXYZ; }

        public void gameStart()
        {
            //gameLog = await storageFolder.CreateFileAsync("GameLog.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
            //gameLog = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");
            //File.Create(gameLog, 4, FileOptions.None);

            /*pathA = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            pathB = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            path1 = System.IO.Path.Combine(pathB, "GameLog.txt");
            pathX = @"C:\Users\ahm53\AppData\Local\Packages\ae6043be-cfa4-4db1-9a89-328d3abf3fde_js1exkfqt36te\LocalState\GameLog.txt";*/

            awayTeamBattersX = SetRosters.getATBatters(); homeTeamBattersX = SetRosters.getHTBatters(); awayTeamPBEX = SetRosters.getATPBE(); homeTeamPBEX = SetRosters.getHTPBE();

            ATNameT.Text = SetRosters.getATName().Substring(0, 3);
            HTNameT.Text = SetRosters.getHTName().Substring(0, 3);
            ATNameXYZ = SetRosters.getATName().Substring(0, 3);
            HTNameXYZ = SetRosters.getHTName().Substring(0, 3);

            awayPitcherA.Text = SetRosters.getATName().Substring(0, 3);
            homePitcherA.Text = SetRosters.getHTName().Substring(0, 3);
            awayBenchA.Text = SetRosters.getATName().Substring(0, 3);
            homeBenchA.Text = SetRosters.getHTName().Substring(0, 3);

            inn = 1; inn2 = inn;

            actionBarX = ActionBar; TBInd = topbotIndicator; InnTB = currentGameInning;

            firstB = firstBase; secondB = secondBase; thirdB = thirdBase;

            s1 = strike1; s2 = strike2; b1 = ball1; b2 = ball2; b3 = ball3; o1 = out1; o2 = out2;

            fbCol = 0; sbCol = 0; tbCol = 0; //black = 0, green = 1, red = 2; blue = 3, orange = 4

            awayBatterOrder = 0; homeBatterOrder = 0; strikeCount = 0; ballCount = 0; outCount = 0; atScore = 0; htScore = 0;

            ATScoreTX = ATScoreT; HTScoreTX = HTScoreT;

            ATScoreTX.Text = atScore.ToString(); HTScoreTX.Text = htScore.ToString();

            currentlyPText.Text = homeTeamPBEX[0].ToString();
            currentAwayPitcher = awayTeamPBEX[0].ToString();
            currentHomePitcher = homeTeamPBEX[0].ToString();
            atBatText.Text = awayTeamBattersX[GetABO()].ToString();
            onDeckText.Text = awayTeamBattersX[GetABO1()].ToString();
            inTheHoleText.Text = awayTeamBattersX[GetABO2()].ToString();
        }

        public static void clearShapes()
        {
            fbCol = 0; sbCol = 0; tbCol = 0;
            outCount = 0; strikeCount = 0; ballCount = 0;
            clearBases();
            s1.Color = Xamarin.Forms.Color.Black; s2.Color = Xamarin.Forms.Color.Black;
            b1.Color = Xamarin.Forms.Color.Black; b2.Color = Xamarin.Forms.Color.Black; b3.Color = Xamarin.Forms.Color.Black;
            o1.Color = Xamarin.Forms.Color.Black; o2.Color = Xamarin.Forms.Color.Black;
        }

        public static void clearBases()
        { firstB.Color = Xamarin.Forms.Color.Black; secondB.Color = Xamarin.Forms.Color.Black; thirdB.Color = Xamarin.Forms.Color.Black; fbCol = 0; sbCol = 0; tbCol = 0; }

        public static void Green_FirstBase()
        { firstB.Color = Xamarin.Forms.Color.Green; }
        public static void Green_SecondBase()
        { secondB.Color = Xamarin.Forms.Color.Green; }
        public static void Green_ThirdBase()
        { thirdB.Color = Xamarin.Forms.Color.Green; }
        public static void Black_FirstBase()
        { firstB.Color = Xamarin.Forms.Color.Black; }
        public static void Black_SecondBase()
        { secondB.Color = Xamarin.Forms.Color.Black; }
        public static void Black_ThirdBase()
        { thirdB.Color = Xamarin.Forms.Color.Black; }

        public static void clearStrikesBalls()
        {
            strikeCount = 0; ballCount = 0;
            s1.Color = Xamarin.Forms.Color.Black; s2.Color = Xamarin.Forms.Color.Black;
            b1.Color = Xamarin.Forms.Color.Black; b2.Color = Xamarin.Forms.Color.Black; b3.Color = Xamarin.Forms.Color.Black;
        }

        private async void GameOver_Click(object sender, EventArgs e)
        {
            FINALGAMELOG += "Game ended at inning: " + TBInd.Text + " " + inn2.ToString() + "\n";
            FINALGAMELOG += SetRosters.getATName() + " - Score: " + atScore.ToString() + "\n";
            FINALGAMELOG += SetRosters.getHTName() + " - Score: " + htScore.ToString() + "\n";
            FINALGAMELOG += currentAwayPitcher + " - Pitchcount: " + awayPitchesThrown.ToString() + "\n";
            FINALGAMELOG += currentHomePitcher + " - Pitchcount: " + homePitchesThrown.ToString() + "\n";

            await Navigation.PushAsync(new GameLogScreen());
        }

        public static void addActionToGameLog()
        {
            FINALGAMELOG += actionBarX.Text + "\n";
        }

        private void ChangeAwayPitcher_Click(object sender, EventArgs e)
        {
            Random rnd = new Random(); aPRandom = rnd.Next(1, 6);
            if (awayTeamPBEX[aPRandom] != null)
            {
                FINALGAMELOG += currentAwayPitcher + " has been replaced by " + awayTeamPBEX[aPRandom].ToString() + ". " + currentAwayPitcher + " threw: " + awayPitchesThrown.ToString() + " pitches." + "\n";
                currentAwayPitcher = awayTeamPBEX[aPRandom].ToString();
                awayPitchesThrown = 0;
                awayTeamPBEX[aPRandom] = null;
            }
            else if (awayTeamPBEX[1] != null)
            {
                FINALGAMELOG += currentAwayPitcher + " has been replaced by " + awayTeamPBEX[1].ToString() + ". " + currentAwayPitcher + " threw: " + awayPitchesThrown.ToString() + " pitches." + "\n";
                currentAwayPitcher = awayTeamPBEX[1].ToString();
                awayPitchesThrown = 0;
                awayTeamPBEX[1] = null;
            }
            else if (awayTeamPBEX[2] != null)
            {
                FINALGAMELOG += currentAwayPitcher + " has been replaced by " + awayTeamPBEX[2].ToString() + ". " + currentAwayPitcher + " threw: " + awayPitchesThrown.ToString() + " pitches." + "\n";
                currentAwayPitcher = awayTeamPBEX[2].ToString(); 
                awayPitchesThrown = 0;
                awayTeamPBEX[2] = null;
            }
            else if (awayTeamPBEX[3] != null)
            {
                FINALGAMELOG += currentAwayPitcher + " has been replaced by " + awayTeamPBEX[3].ToString() + ". " + currentAwayPitcher + " threw: " + awayPitchesThrown.ToString() + " pitches." + "\n";
                currentAwayPitcher = awayTeamPBEX[3].ToString();
                awayPitchesThrown = 0;
                awayTeamPBEX[3] = null;
            }
            else if (awayTeamPBEX[4] != null)
            {
                FINALGAMELOG += currentAwayPitcher + " has been replaced by " + awayTeamPBEX[4].ToString() + ". " + currentAwayPitcher + " threw: " + awayPitchesThrown.ToString() + " pitches." + "\n";
                currentAwayPitcher = awayTeamPBEX[4].ToString();
                awayPitchesThrown = 0;
                awayTeamPBEX[4] = null;
            }
            else if (awayTeamPBEX[5] != null)
            {
                FINALGAMELOG += currentAwayPitcher + " has been replaced by " + awayTeamPBEX[5].ToString() + ". " + currentAwayPitcher + " threw: " + awayPitchesThrown.ToString() + " pitches." + "\n";
                currentAwayPitcher = awayTeamPBEX[5].ToString();
                awayPitchesThrown = 0;
                awayTeamPBEX[5] = null;
            }
        }

        private void ChangeHomePitcher_Click(object sender, EventArgs e)
        {
            Random rnd = new Random(); hPRandom = rnd.Next(1, 6);
            if (homeTeamPBEX[hPRandom] != null)
            {
                FINALGAMELOG += currentHomePitcher + " has been replaced by " + homeTeamPBEX[hPRandom].ToString() + ". " + currentHomePitcher + " threw: " + homePitchesThrown.ToString() + " pitches." + "\n";
                currentHomePitcher = homeTeamPBEX[hPRandom].ToString();
                homePitchesThrown = 0;
                homeTeamPBEX[hPRandom] = null;
            }
            else if (homeTeamPBEX[1] != null)
            {
                FINALGAMELOG += currentHomePitcher + " has been replaced by " + homeTeamPBEX[1].ToString() + ". " + currentHomePitcher + " threw: " + homePitchesThrown.ToString() + " pitches." + "\n";
                currentHomePitcher = homeTeamPBEX[1].ToString();
                homePitchesThrown = 0;
                homeTeamPBEX[1] = null;
            }
            else if (homeTeamPBEX[2] != null)
            {
                FINALGAMELOG += currentHomePitcher + " has been replaced by " + homeTeamPBEX[2].ToString() + ". " + currentHomePitcher + " threw: " + homePitchesThrown.ToString() + " pitches." + "\n";
                currentHomePitcher = homeTeamPBEX[2].ToString();
                homePitchesThrown = 0;
                homeTeamPBEX[2] = null;
            }
            else if (homeTeamPBEX[3] != null)
            {
                FINALGAMELOG += currentHomePitcher + " has been replaced by " + homeTeamPBEX[3].ToString() + ". " + currentHomePitcher + " threw: " + homePitchesThrown.ToString() + " pitches." + "\n";
                currentHomePitcher = homeTeamPBEX[3].ToString();
                homePitchesThrown = 0;
                homeTeamPBEX[3] = null;
            }
            else if (homeTeamPBEX[4] != null)
            {
                FINALGAMELOG += currentHomePitcher + " has been replaced by " + homeTeamPBEX[4].ToString() + ". " + currentHomePitcher + " threw: " + homePitchesThrown.ToString() + " pitches." + "\n";
                currentHomePitcher = homeTeamPBEX[4].ToString();
                homePitchesThrown = 0;
                homeTeamPBEX[4] = null;
            }
            else if (homeTeamPBEX[5] != null)
            {
                FINALGAMELOG += currentHomePitcher + " has been replaced by " + homeTeamPBEX[5].ToString() + ". " + currentHomePitcher + " threw: " + homePitchesThrown.ToString() + " pitches." + "\n";
                currentHomePitcher = homeTeamPBEX[5].ToString();
                homePitchesThrown = 0;
                homeTeamPBEX[5] = null;
            }
        }

        private void ChangeAwayPlayer_Click(object sender, EventArgs e)
        {
            Random rnd = new Random(); aBRandom = rnd.Next(6, 9);
            if (awayTeamPBEX[aBRandom] != null)
            {
                FINALGAMELOG += awayTeamBattersX[GetABO()].ToString() + " has been replaced by " + awayTeamPBEX[aBRandom].ToString() + "\n";
                awayTeamBattersX[GetABO()] = awayTeamPBEX[aBRandom];
                awayTeamPBEX[aBRandom] = null;
            }
            else if (awayTeamPBEX[6] != null)
            {
                FINALGAMELOG += awayTeamBattersX[GetABO()].ToString() + " has been replaced by " + awayTeamPBEX[aBRandom].ToString() + "\n";
                awayTeamBattersX[GetABO()] = awayTeamPBEX[6];
                awayTeamPBEX[6] = null;
            }
            else if (awayTeamPBEX[7] != null)
            {
                FINALGAMELOG += awayTeamBattersX[GetABO()].ToString() + " has been replaced by " + awayTeamPBEX[aBRandom].ToString() + "\n";
                awayTeamBattersX[GetABO()] = awayTeamPBEX[7];
                awayTeamPBEX[7] = null;
            }
            else if (awayTeamPBEX[8] != null)
            {
                FINALGAMELOG += awayTeamBattersX[GetABO()].ToString() + " has been replaced by " + awayTeamPBEX[aBRandom].ToString() + "\n";
                awayTeamBattersX[GetABO()] = awayTeamPBEX[8];
                awayTeamPBEX[8] = null;
            }
        }

        private void ChangeHomePlayer_Click(object sender, EventArgs e)
        {
            Random rnd = new Random(); hBRandom = rnd.Next(6, 9);
            if (homeTeamPBEX[hBRandom] != null)
            {
                FINALGAMELOG += homeTeamBattersX[GetHBO()].ToString() + " has been replaced by " + homeTeamPBEX[hBRandom].ToString() + "\n";
                homeTeamBattersX[GetHBO()] = homeTeamPBEX[hBRandom];
                homeTeamPBEX[hBRandom] = null;
            }
            else if (homeTeamPBEX[6] != null)
            {
                FINALGAMELOG += homeTeamBattersX[GetHBO()].ToString() + " has been replaced by " + homeTeamPBEX[hBRandom].ToString() + "\n";
                homeTeamBattersX[GetHBO()] = homeTeamPBEX[6];
                homeTeamPBEX[6] = null;
            }
            else if (homeTeamPBEX[7] != null)
            {
                FINALGAMELOG += homeTeamBattersX[GetHBO()].ToString() + " has been replaced by " + homeTeamPBEX[hBRandom].ToString() + "\n";
                homeTeamBattersX[GetHBO()] = homeTeamPBEX[7];
                homeTeamPBEX[7] = null;
            }
            else if (homeTeamPBEX[8] != null)
            {
                FINALGAMELOG += homeTeamBattersX[GetHBO()].ToString() + " has been replaced by " + homeTeamPBEX[hBRandom].ToString() + "\n";
                homeTeamBattersX[GetHBO()] = homeTeamPBEX[8];
                homeTeamPBEX[8] = null;
            }
        }

        public static int midSRandom()
        { Random rnd = new Random(); midSX = rnd.Next(1, 51); return midSX; }
        public static int botSRandom()
        { Random rnd = new Random(); botSX = rnd.Next(1, 51); return botSX; }
        public static int topSRandom()
        { Random rnd = new Random(); topSX = rnd.Next(1, 51); return topSX; }

        public static int leftSRandom()
        { Random rnd = new Random(); leftSX = rnd.Next(1, 51); return leftSX; }
        public static int rightSRandom()
        { Random rnd = new Random(); rightSX = rnd.Next(1, 51); return rightSX; }
        public static int BallButtonRandom()
        { Random rnd = new Random(); ballRR = rnd.Next(1, 51); return ballRR; }

        public static bool scenario_NoOneOn()
        {
            if (firstB.Color == Xamarin.Forms.Color.Black)
            {
                if (secondB.Color == Xamarin.Forms.Color.Black)
                {
                    if (thirdB.Color == Xamarin.Forms.Color.Black)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        public static bool scenario_OnFirst()
        {
            if (firstB.Color == Xamarin.Forms.Color.Green)
            {
                if (secondB.Color == Xamarin.Forms.Color.Black)
                {
                    if (thirdB.Color == Xamarin.Forms.Color.Black)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        public static bool scenario_OnFirstSecond()
        {
            if (firstB.Color == Xamarin.Forms.Color.Green)
            {
                if (secondB.Color == Xamarin.Forms.Color.Green)
                {
                    if (thirdB.Color == Xamarin.Forms.Color.Black)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        public static bool scenario_OnFirstThird()
        {
            if (firstB.Color == Xamarin.Forms.Color.Green)
            {
                if (secondB.Color == Xamarin.Forms.Color.Black)
                {
                    if (thirdB.Color == Xamarin.Forms.Color.Green)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        public static bool scenario_BasesLoaded()
        {
            if (firstB.Color == Xamarin.Forms.Color.Green)
            {
                if (secondB.Color == Xamarin.Forms.Color.Green)
                {
                    if (thirdB.Color == Xamarin.Forms.Color.Green)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        public static bool scenario_OnSecond()
        {
            if (firstB.Color == Xamarin.Forms.Color.Black)
            {
                if (secondB.Color == Xamarin.Forms.Color.Green)
                {
                    if (thirdB.Color == Xamarin.Forms.Color.Black)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        public static bool scenario_OnSecondThird()
        {
            if (firstB.Color == Xamarin.Forms.Color.Black)
            {
                if (secondB.Color == Xamarin.Forms.Color.Green)
                {
                    if (thirdB.Color == Xamarin.Forms.Color.Green)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        public static bool scenario_OnThird()
        {
            if (firstB.Color == Xamarin.Forms.Color.Black)
            {
                if (secondB.Color == Xamarin.Forms.Color.Black)
                {
                    if (thirdB.Color == Xamarin.Forms.Color.Green)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public static string generateAwayHitType()
        {
            Random rnd = new Random();
            int x = rnd.Next(0, 50);
            if (x < 26)
            {
                awaySingleOccurred();
                return "single.";
            }
            else if (x > 25 && x < 41)
            {
                awayDoubleOccurred();
                return "double.";
            }
            else if (x > 40 && x < 46)
            {
                awayTripleOccurred();
                return "triple.";
            }
            else
            {
                awayHROccurred();
                return "homerun.";
            }
        }

        public static void awaySingleOccurred()
        {
            if (scenario_NoOneOn()) //0
            {
                Green_FirstBase(); Black_SecondBase(); Black_ThirdBase();
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnFirst()) //1
            {
                Green_FirstBase(); Green_SecondBase(); Black_ThirdBase();
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnSecond()) //2
            {
                Green_FirstBase(); Black_SecondBase(); Green_ThirdBase(); 
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnThird()) //3
            {
                Green_FirstBase(); Black_SecondBase(); Black_ThirdBase(); atScore++; 
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnFirstSecond()) //4
            {
                Green_FirstBase(); Green_SecondBase(); Green_ThirdBase();
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnFirstThird()) //5
            {
                Green_FirstBase(); Green_SecondBase(); Black_ThirdBase();  atScore++;
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnSecondThird()) //6
            {
                Green_FirstBase(); Black_SecondBase(); Green_ThirdBase();  atScore++; 
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_BasesLoaded()) //7
            {
                Green_FirstBase(); Green_SecondBase(); Green_ThirdBase(); atScore++; 
                ATScoreTX.Text = atScore.ToString();
            }
        }

        public static void awayDoubleOccurred()
        {
            if (scenario_NoOneOn()) //0
            {
                Black_FirstBase(); Green_SecondBase(); Black_ThirdBase();
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnFirst()) //1
            {
                Black_FirstBase(); Green_SecondBase(); Green_ThirdBase();
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnSecond()) //2
            {
                Black_FirstBase(); Green_SecondBase(); Black_ThirdBase(); atScore++;
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnThird()) //3
            {
                Black_FirstBase(); Green_SecondBase(); Black_ThirdBase(); atScore++;
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnFirstSecond()) //4
            {
                Black_FirstBase(); Green_SecondBase(); Green_ThirdBase(); atScore++;
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnFirstThird()) //5
            {
                Black_FirstBase(); Green_SecondBase(); Green_ThirdBase(); atScore++;
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnSecondThird()) //6
            {
                Black_FirstBase(); Green_SecondBase(); Black_ThirdBase(); atScore+=2;
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_BasesLoaded()) //7
            {
                Black_FirstBase(); Green_SecondBase(); Green_ThirdBase(); atScore+=2;
                ATScoreTX.Text = atScore.ToString();
            }
        }

        public static void awayTripleOccurred()
        {
            if (scenario_NoOneOn()) //0
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase();
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnFirst()) //1
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); atScore++;
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnSecond()) //2
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); atScore++;
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnThird()) //3
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); atScore++;
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnFirstSecond()) //4
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); atScore += 2;
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnFirstThird()) //5
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); atScore += 2;
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnSecondThird()) //6
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); atScore += 2;
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_BasesLoaded()) //7
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); atScore += 3;
                ATScoreTX.Text = atScore.ToString();
            }
        }

        public static void awayHROccurred()
        {
            if (scenario_NoOneOn()) //0
            {
                atScore++; ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnFirst()) //1
            {
                atScore += 2; ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnSecond()) //2
            {
                atScore += 2; ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnThird()) //3
            {
                atScore += 2; ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnFirstSecond()) //4
            {
                atScore += 3; ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnFirstThird()) //5
            {
                atScore += 3; ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnSecondThird()) //6
            {
                atScore += 3; ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_BasesLoaded()) //7
            {
                atScore += 4; ATScoreTX.Text = atScore.ToString();
            }
            clearBases();
        }

        public static void awayGroundOut()
        {
            if (scenario_OnFirst())
            {
                Black_FirstBase(); Black_SecondBase(); Black_ThirdBase(); outCount += 2;
            }
            else if (scenario_OnFirstSecond())
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); outCount += 2;
            }
            else if (scenario_OnFirstThird())
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); outCount += 2;
            }
            else if (scenario_BasesLoaded())
            {
                Black_FirstBase(); Green_SecondBase(); Green_ThirdBase(); outCount += 2;
            }
            else
            {
                outCount++;
            }
        }

        public static void awayWalk()
        {
            if (scenario_NoOneOn()) //0
            {
                Green_FirstBase(); Black_SecondBase(); Black_ThirdBase();
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnFirst()) //1
            {
                Green_FirstBase(); Green_SecondBase(); Black_ThirdBase();
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnSecond()) //2
            {
                Green_FirstBase(); Green_SecondBase(); Black_ThirdBase();
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnThird()) //3
            {
                Green_FirstBase(); Black_SecondBase(); Green_ThirdBase();
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnFirstSecond()) //4
            {
                Green_FirstBase(); Green_SecondBase(); Green_ThirdBase();
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnFirstThird()) //5
            {
                Green_FirstBase(); Green_SecondBase(); Green_ThirdBase();
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_OnSecondThird()) //6
            {
                Green_FirstBase(); Green_SecondBase(); Green_ThirdBase();
                ATScoreTX.Text = atScore.ToString();
            }
            else if (scenario_BasesLoaded()) //7
            {
                Green_FirstBase(); Green_SecondBase(); Green_ThirdBase(); atScore++;
                ATScoreTX.Text = atScore.ToString();
            }
        }

        public static string generateHomeHitType()
        {
            Random rnd = new Random();
            int x = rnd.Next(0, 50);
            if (x < 26)
            {
                homeSingleOccurred();
                return "single.";
            }
            else if (x > 25 && x < 41)
            {
                homeDoubleOccurred();
                return "double.";
            }
            else if (x > 40 && x < 46)
            {
                homeTripleOccurred();
                return "triple.";
            }
            else
            {
                homeHROccurred();
                return "homerun.";
            }
        }

        public static void homeSingleOccurred()
        {
            if (scenario_NoOneOn()) //0
            {
                Green_FirstBase(); Black_SecondBase(); Black_ThirdBase();
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnFirst()) //1
            {
                Green_FirstBase(); Green_SecondBase(); Black_ThirdBase();
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnSecond()) //2
            {
                Green_FirstBase(); Black_SecondBase(); Green_ThirdBase();
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnThird()) //3
            {
                Green_FirstBase(); Black_SecondBase(); Black_ThirdBase(); htScore++;
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnFirstSecond()) //4
            {
                Green_FirstBase(); Green_SecondBase(); Green_ThirdBase();
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnFirstThird()) //5
            {
                Green_FirstBase(); Green_SecondBase(); Black_ThirdBase();  htScore++;
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnSecondThird()) //6
            {
                Green_FirstBase(); Black_SecondBase(); Green_ThirdBase();  htScore++;
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_BasesLoaded()) //7
            {
                Green_FirstBase(); Green_SecondBase(); Green_ThirdBase(); htScore++;
                HTScoreTX.Text = htScore.ToString();
            }
        }

        public static void homeDoubleOccurred()
        {
            if (scenario_NoOneOn()) //0
            {
                Black_FirstBase(); Green_SecondBase(); Black_ThirdBase();
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnFirst()) //1
            {
                Black_FirstBase(); Green_SecondBase(); Green_ThirdBase();
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnSecond()) //2
            {
                Black_FirstBase(); Green_SecondBase(); Black_ThirdBase(); htScore++;
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnThird()) //3
            {
                Black_FirstBase(); Green_SecondBase(); Black_ThirdBase(); htScore++;
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnFirstSecond()) //4
            {
                Black_FirstBase(); Green_SecondBase(); Green_ThirdBase(); htScore++;
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnFirstThird()) //5
            {
                Black_FirstBase(); Green_SecondBase(); Green_ThirdBase(); htScore++;
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnSecondThird()) //6
            {
                Black_FirstBase(); Green_SecondBase(); Black_ThirdBase(); htScore += 2;
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_BasesLoaded()) //7
            {
                Black_FirstBase(); Green_SecondBase(); Green_ThirdBase(); htScore += 2;
                HTScoreTX.Text = htScore.ToString();
            }
        }

        public static void homeTripleOccurred()
        {
            if (scenario_NoOneOn()) //0
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase();
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnFirst()) //1
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); htScore++;
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnSecond()) //2
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); htScore++;
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnThird()) //3
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); htScore++;
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnFirstSecond()) //4
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); htScore+=2;
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnFirstThird()) //5
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); htScore+=2;
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnSecondThird()) //6
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); htScore+=2;
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_BasesLoaded()) //7
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); htScore+=3;
                HTScoreTX.Text = htScore.ToString();
            }
        }

        public static void homeHROccurred()
        {
            if (scenario_NoOneOn())
            {
                htScore++; HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnFirst()) //1
            {
                htScore+=2; HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnSecond()) //2
            {
                htScore += 2; HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnThird()) //3
            {
                htScore += 2; HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnFirstSecond()) //4
            {
                htScore += 3; HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnFirstThird()) //5
            {
                htScore += 3; HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnSecondThird()) //6
            {
                htScore += 3; HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_BasesLoaded()) //7
            {
                htScore += 4; HTScoreTX.Text = htScore.ToString();
            }
            clearBases();
        }

        public static void homeGroundOut()
        {
            if (scenario_OnFirst())
            {
                Black_FirstBase(); Black_SecondBase(); Black_ThirdBase(); outCount += 2;
            }
            else if (scenario_OnFirstSecond())
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); outCount += 2;
            }
            else if (scenario_OnFirstThird())
            {
                Black_FirstBase(); Black_SecondBase(); Green_ThirdBase(); outCount += 2;
            }
            else if (scenario_BasesLoaded())
            {
                Black_FirstBase(); Green_SecondBase(); Green_ThirdBase(); outCount += 2;
            }
            else
            {
                outCount++;
            }
        }

        public static void homeWalk()
        {
            if (scenario_NoOneOn()) //0
            {
                Green_FirstBase(); Black_SecondBase(); Black_ThirdBase();
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnFirst()) //1
            {
                Green_FirstBase(); Green_SecondBase(); Black_ThirdBase();
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnSecond()) //2
            {
                Green_FirstBase(); Green_SecondBase(); Black_ThirdBase();
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnThird()) //3
            {
                Green_FirstBase(); Black_SecondBase(); Green_ThirdBase();
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnFirstSecond()) //4
            {
                Green_FirstBase(); Green_SecondBase(); Green_ThirdBase();
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnFirstThird()) //5
            {
                Green_FirstBase(); Green_SecondBase(); Green_ThirdBase();
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_OnSecondThird()) //6
            {
                Green_FirstBase(); Green_SecondBase(); Green_ThirdBase();
                HTScoreTX.Text = htScore.ToString();
            }
            else if (scenario_BasesLoaded()) //7
            {
                Green_FirstBase(); Green_SecondBase(); Green_ThirdBase(); htScore++;
                HTScoreTX.Text = htScore.ToString();
            }
        }

        public static void whenFoulOccurs()
        {
            strikeCount++;
            if (strikeCount == 1)
                s1.Color = Xamarin.Forms.Color.Orange;
            else if (strikeCount == 2)
            {
                s1.Color = Xamarin.Forms.Color.Orange;
                s2.Color = Xamarin.Forms.Color.Orange;
            }
            else if (strikeCount == 3)
            {
                strikeCount--;
                s1.Color = Xamarin.Forms.Color.Orange;
                s2.Color = Xamarin.Forms.Color.Orange;
            }
        }

        public static void whenOutOccurs()
        {
            if (outCount == 1)
                o1.Color = Xamarin.Forms.Color.Red;
            else if (outCount == 2)
            {
                o1.Color = Xamarin.Forms.Color.Red;
                o2.Color = Xamarin.Forms.Color.Red;
            }
            else if (outCount == 3)
            {
                o1.Color = Xamarin.Forms.Color.Black;
                o2.Color = Xamarin.Forms.Color.Black;
            }
        }

        public static void whenStrikeOccurs()
        {
            if (strikeCount == 1)
                s1.Color = Xamarin.Forms.Color.Orange;
            else if (strikeCount == 2)
            {
                s1.Color = Xamarin.Forms.Color.Orange;
                s2.Color = Xamarin.Forms.Color.Orange;
            }
            else if (strikeCount == 3)
            {
                clearStrikesBalls();
            }
        }

        public static void whenBallOccurs()
        {
            if (ballCount == 1)
                b1.Color = Xamarin.Forms.Color.Blue;
            else if (ballCount == 2)
            {
                b1.Color = Xamarin.Forms.Color.Blue;
                b2.Color = Xamarin.Forms.Color.Blue;
            }
            else if (ballCount == 3)
            {
                b1.Color = Xamarin.Forms.Color.Blue;
                b2.Color = Xamarin.Forms.Color.Blue;
                b3.Color = Xamarin.Forms.Color.Blue;
            }
            else if (ballCount == 4)
            {
                clearStrikesBalls();
            }
        }

        public void MStrike_Click(object sender, EventArgs e)
        {
            important = 0;
            if (inn % 2 != 0)
            {
                TheAwayAlgorithm();
                currentlyPText.Text = currentHomePitcher;
                homePitchesThrown++;
                pitchesThrownText.Text = homePitchesThrown.ToString();
                atBatText.Text = awayTeamBattersX[GetABO()].ToString();
                onDeckText.Text = awayTeamBattersX[GetABO1()].ToString();
                inTheHoleText.Text = awayTeamBattersX[GetABO2()].ToString();
                TBInd.Text = "TOP";
                inn2 = inn;
                inn2 = Math.Ceiling(inn2 / 2);
                InnTB.Text = inn2.ToString();
            }
            else
            {
                TheHomeAlgorithm();
                currentlyPText.Text = currentAwayPitcher;
                awayPitchesThrown++;
                pitchesThrownText.Text = awayPitchesThrown.ToString();
                atBatText.Text = homeTeamBattersX[GetHBO()].ToString();
                onDeckText.Text = homeTeamBattersX[GetHBO1()].ToString();
                inTheHoleText.Text = homeTeamBattersX[GetHBO2()].ToString();
                TBInd.Text = "BOT";
                inn2 = inn;
                inn2 = Math.Ceiling(inn2 / 2);
                InnTB.Text = inn2.ToString();
            }
        }
        public void BStrike_Click(object sender, EventArgs e)
        {
            important = 1;
            if (inn % 2 != 0)
            {
                TheAwayAlgorithm();
                currentlyPText.Text = currentHomePitcher;
                homePitchesThrown++;
                pitchesThrownText.Text = homePitchesThrown.ToString();
                atBatText.Text = awayTeamBattersX[GetABO()].ToString();
                onDeckText.Text = awayTeamBattersX[GetABO1()].ToString();
                inTheHoleText.Text = awayTeamBattersX[GetABO2()].ToString();
                TBInd.Text = "TOP";
                inn2 = inn;
                inn2 = Math.Ceiling(inn2 / 2);
                InnTB.Text = inn2.ToString();
            }
            else
            {
                TheHomeAlgorithm();
                currentlyPText.Text = currentAwayPitcher;
                awayPitchesThrown++;
                pitchesThrownText.Text = awayPitchesThrown.ToString();
                atBatText.Text = homeTeamBattersX[GetHBO()].ToString();
                onDeckText.Text = homeTeamBattersX[GetHBO1()].ToString();
                inTheHoleText.Text = homeTeamBattersX[GetHBO2()].ToString();
                TBInd.Text = "BOT";
                inn2 = inn;
                inn2 = Math.Ceiling(inn2 / 2);
                InnTB.Text = inn2.ToString();
            }
        }
        public void TStrike_Click(object sender, EventArgs e)
        {
            important = 2;
            if (inn % 2 != 0)
            {
                TheAwayAlgorithm();
                currentlyPText.Text = currentHomePitcher;
                homePitchesThrown++;
                pitchesThrownText.Text = homePitchesThrown.ToString();
                atBatText.Text = awayTeamBattersX[GetABO()].ToString();
                onDeckText.Text = awayTeamBattersX[GetABO1()].ToString();
                inTheHoleText.Text = awayTeamBattersX[GetABO2()].ToString();
                TBInd.Text = "TOP";
                inn2 = inn;
                inn2 = Math.Ceiling(inn2 / 2);
                InnTB.Text = inn2.ToString();
            }
            else
            {
                TheHomeAlgorithm();
                currentlyPText.Text = currentAwayPitcher;
                awayPitchesThrown++;
                pitchesThrownText.Text = awayPitchesThrown.ToString();
                atBatText.Text = homeTeamBattersX[GetHBO()].ToString();
                onDeckText.Text = homeTeamBattersX[GetHBO1()].ToString();
                inTheHoleText.Text = homeTeamBattersX[GetHBO2()].ToString();
                TBInd.Text = "BOT";
                inn2 = inn;
                inn2 = Math.Ceiling(inn2 / 2);
                InnTB.Text = inn2.ToString();
            }
        }
        public void LStrike_Click(object sender, EventArgs e)
        {
            important = 3;
            if (inn % 2 != 0)
            {
                TheAwayAlgorithm();
                currentlyPText.Text = currentHomePitcher;
                homePitchesThrown++;
                pitchesThrownText.Text = homePitchesThrown.ToString();
                atBatText.Text = awayTeamBattersX[GetABO()].ToString();
                onDeckText.Text = awayTeamBattersX[GetABO1()].ToString();
                inTheHoleText.Text = awayTeamBattersX[GetABO2()].ToString();
                TBInd.Text = "TOP";
                inn2 = inn;
                inn2 = Math.Ceiling(inn2 / 2);
                InnTB.Text = inn2.ToString();
            }
            else
            {
                TheHomeAlgorithm();
                currentlyPText.Text = currentAwayPitcher;
                awayPitchesThrown++;
                pitchesThrownText.Text = awayPitchesThrown.ToString();
                atBatText.Text = homeTeamBattersX[GetHBO()].ToString();
                onDeckText.Text = homeTeamBattersX[GetHBO1()].ToString();
                inTheHoleText.Text = homeTeamBattersX[GetHBO2()].ToString();
                TBInd.Text = "BOT";
                inn2 = inn;
                inn2 = Math.Ceiling(inn2 / 2);
                InnTB.Text = inn2.ToString();
            }
        }
        public void RStrike_Click(object sender, EventArgs e)
        {
            important = 4;
            if (inn % 2 != 0)
            {
                TheAwayAlgorithm();
                currentlyPText.Text = currentHomePitcher;
                homePitchesThrown++;
                pitchesThrownText.Text = homePitchesThrown.ToString();
                atBatText.Text = awayTeamBattersX[GetABO()].ToString();
                onDeckText.Text = awayTeamBattersX[GetABO1()].ToString();
                inTheHoleText.Text = awayTeamBattersX[GetABO2()].ToString();
                TBInd.Text = "TOP";
                inn2 = inn;
                inn2 = Math.Ceiling(inn2 / 2);
                InnTB.Text = inn2.ToString();
            }
            else
            {
                TheHomeAlgorithm();
                currentlyPText.Text = currentAwayPitcher;
                awayPitchesThrown++;
                pitchesThrownText.Text = awayPitchesThrown.ToString();
                atBatText.Text = homeTeamBattersX[GetHBO()].ToString();
                onDeckText.Text = homeTeamBattersX[GetHBO1()].ToString();
                inTheHoleText.Text = homeTeamBattersX[GetHBO2()].ToString();
                TBInd.Text = "BOT";
                inn2 = inn;
                inn2 = Math.Ceiling(inn2 / 2);
                InnTB.Text = inn2.ToString();
            }
        }

        public void Ball_Click(object sender, EventArgs e)
        {
            important = 5;
            if (inn % 2 != 0)
            {
                TheAwayAlgorithm();
                currentlyPText.Text = currentHomePitcher;
                homePitchesThrown++;
                pitchesThrownText.Text = homePitchesThrown.ToString();
                atBatText.Text = awayTeamBattersX[GetABO()].ToString();
                onDeckText.Text = awayTeamBattersX[GetABO1()].ToString();
                inTheHoleText.Text = awayTeamBattersX[GetABO2()].ToString();
                TBInd.Text = "TOP";
                inn2 = inn;
                inn2 = Math.Ceiling(inn2 / 2);
                InnTB.Text = inn2.ToString();
            }
            else
            {
                TheHomeAlgorithm();
                currentlyPText.Text = currentAwayPitcher;
                awayPitchesThrown++;
                pitchesThrownText.Text = awayPitchesThrown.ToString();
                atBatText.Text = homeTeamBattersX[GetHBO()].ToString();
                onDeckText.Text = homeTeamBattersX[GetHBO1()].ToString();
                inTheHoleText.Text = homeTeamBattersX[GetHBO2()].ToString();
                TBInd.Text = "BOT";
                inn2 = inn;
                inn2 = Math.Ceiling(inn2 / 2);
                InnTB.Text = inn2.ToString();
            }
        }

        public static int GetABO()
        {
            if (awayBatterOrder < 9)
                return awayBatterOrder;
            else
                awayBatterOrder = 0;
            return awayBatterOrder;
        }
        public static int GetHBO()
        {
            if (homeBatterOrder < 9)
                return homeBatterOrder;
            else
                homeBatterOrder = 0;
            return homeBatterOrder;
        }
        public static int GetABO1()
        {
            if (awayBatterOrder + 1 < 9)
                return awayBatterOrder + 1;
            else
                return 0;
        }
        public static int GetHBO1()
        {
            if (homeBatterOrder + 1 < 9)
                return homeBatterOrder + 1;
            else
                return 0;
        }
        public static int GetABO2()
        {
            if (awayBatterOrder + 2 < 9)
                return awayBatterOrder + 2;
            else if (awayBatterOrder + 2 < 10)
                return 0;
            else
                return 1;
        }
        public static int GetHBO2()
        {
            if (homeBatterOrder + 2 < 9)
                return homeBatterOrder + 2;
            else if (homeBatterOrder + 2 < 10)
                return 0;
            else
                return 1;
        }

        public static void TheAwayAlgorithm()
        {
            if (outCount < 3)
            {
                if (important.Equals(0))
                {
                    int midS1 = midSRandom();
                    if (midS1 < 29)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a " + generateAwayHitType();
                        addActionToGameLog();
                        clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (midS1 > 28 && midS1 < 34)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a groundout.";
                        addActionToGameLog();
                        awayGroundOut(); whenOutOccurs(); clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (midS1 > 33 && midS1 < 39)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a flyout.";
                        addActionToGameLog();
                        outCount++; whenOutOccurs(); clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (midS1 > 38 && midS1 < 40)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a foul ball.";
                        addActionToGameLog();
                        whenFoulOccurs();
                    }
                    else if (midS1 > 39 && midS1 < 50)
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            awayBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " swings and misses.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                    else
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            awayBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " looks at a strike.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                }
                else if (important.Equals(1))
                {
                    int botS1 = botSRandom();
                    if (botS1 < 16)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a " + generateAwayHitType();
                        addActionToGameLog();
                        clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (botS1 > 15 && botS1 < 19)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a groundout.";
                        addActionToGameLog();
                        awayGroundOut(); whenOutOccurs(); clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (botS1 > 18 && botS1 < 34)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a flyout.";
                        addActionToGameLog();
                        outCount++; whenOutOccurs(); clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (botS1 > 33 && botS1 < 39)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a foul ball.";
                        addActionToGameLog();
                        whenFoulOccurs();
                    }
                    else if (botS1 > 38 && botS1 < 47)
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            awayBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " swings and misses.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                    else
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            awayBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " looks at a strike.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                }
                else if (important.Equals(2))
                {
                    int topS1 = topSRandom();
                    if (topS1 < 13)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a " + generateAwayHitType();
                        addActionToGameLog();
                        clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (topS1 > 12 && topS1 < 23)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a groundout.";
                        addActionToGameLog();
                        awayGroundOut(); whenOutOccurs(); clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (topS1 > 22 && topS1 < 31)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a flyout.";
                        addActionToGameLog();
                        outCount++; whenOutOccurs(); clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (topS1 > 30 && topS1 < 43)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a foul ball.";
                        addActionToGameLog();
                        whenFoulOccurs();
                    }
                    else if (topS1 > 42 && topS1 < 49)
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            awayBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " swings and misses.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                    else
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            awayBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " looks at a strike.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                }
                else if (important.Equals(3))
                {
                    int leftS1 = leftSRandom();
                    if (leftS1 < 11)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a " + generateAwayHitType();
                        addActionToGameLog();
                        clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (leftS1 > 10 && leftS1 < 21)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a groundout.";
                        addActionToGameLog();
                        awayGroundOut(); whenOutOccurs(); clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (leftS1 > 20 && leftS1 < 26)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a flyout.";
                        addActionToGameLog();
                        outCount++; whenOutOccurs(); clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (leftS1 > 25 && leftS1 < 36)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a foul ball.";
                        addActionToGameLog();
                        whenFoulOccurs();
                    }
                    else if (leftS1 > 35 && leftS1 < 46)
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            awayBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " swings and misses.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                    else
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            awayBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " looks at a strike.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                }
                else if (important.Equals(4))
                {
                    int rightS1 = rightSRandom();
                    if (rightS1 < 11)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a " + generateAwayHitType();
                        addActionToGameLog();
                        clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (rightS1 > 10 && rightS1 < 21)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a groundout.";
                        addActionToGameLog();
                        awayGroundOut(); whenOutOccurs(); clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (rightS1 > 20 && rightS1 < 26)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a flyout.";
                        addActionToGameLog();
                        outCount++; whenOutOccurs(); clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (rightS1 > 25 && rightS1 < 36)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a foul ball.";
                        addActionToGameLog();
                        whenFoulOccurs();
                    }
                    else if (rightS1 > 35 && rightS1 < 46)
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            awayBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " swings and misses.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                    else
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            awayBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " looks at a strike.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                }
                else if (important.Equals(5))
                {
                    int ballQ = BallButtonRandom();
                    if (ballQ < 4)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a " + generateAwayHitType();
                        addActionToGameLog();
                        clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (ballQ > 3 && ballQ < 8)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a groundout.";
                        addActionToGameLog();
                        awayGroundOut(); whenOutOccurs(); clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (ballQ > 7 && ballQ < 12)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a flyout.";
                        addActionToGameLog();
                        outCount++; whenOutOccurs(); clearStrikesBalls();
                        awayBatterOrder++;
                    }
                    else if (ballQ > 11 && ballQ < 17)
                    {
                        actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " hits a foul ball.";
                        addActionToGameLog();
                        whenFoulOccurs();
                    }
                    else if (ballQ > 16 && ballQ < 38)
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            awayBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " swings and misses.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                    else
                    {
                        ballCount++;
                        if (ballCount == 4)
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " walked";
                            addActionToGameLog();
                            awayWalk(); clearStrikesBalls();
                            awayBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = awayTeamBattersX[GetABO()].ToString() + " looks at a ball.";
                            addActionToGameLog();
                            whenBallOccurs();
                        }
                    }
                }
            }
            else
            {
                inn++;
                FINALGAMELOG += "UPDATE- INN: " + TBInd.Text + " " + inn2.ToString() + ", " + SetRosters.getATName().Substring(0, 3) + ": " + atScore.ToString() + " " + SetRosters.getHTName().Substring(0, 3) + ": " + htScore.ToString() + "\n";
                homePitchesThrown--;
                clearShapes();
            }
        }

        public static void TheHomeAlgorithm()
        {
            if (outCount < 3)
            {
                if (important.Equals(0))
                {
                    int midS1 = midSRandom();
                    if (midS1 < 29)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a " + generateHomeHitType();
                        addActionToGameLog();
                        clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (midS1 > 28 && midS1 < 34)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a groundout.";
                        addActionToGameLog();
                        homeGroundOut(); whenOutOccurs(); clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (midS1 > 33 && midS1 < 39)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a flyout.";
                        addActionToGameLog();
                        outCount++; whenOutOccurs(); clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (midS1 > 38 && midS1 < 40)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a foul ball.";
                        addActionToGameLog();
                        whenFoulOccurs();
                    }
                    else if (midS1 > 39 && midS1 < 50)
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            homeBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " swings and misses.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                    else
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            homeBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " looks at a strike.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                }
                else if (important.Equals(1))
                {
                    int botS1 = botSRandom();
                    if (botS1 < 16)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a " + generateHomeHitType();
                        addActionToGameLog();
                        clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (botS1 > 15 && botS1 < 19)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a groundout.";
                        addActionToGameLog();
                        homeGroundOut(); whenOutOccurs(); clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (botS1 > 18 && botS1 < 34)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a flyout.";
                        addActionToGameLog();
                        outCount++; whenOutOccurs(); clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (botS1 > 33 && botS1 < 39)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a foul ball.";
                        addActionToGameLog();
                        whenFoulOccurs();
                    }
                    else if (botS1 > 38 && botS1 < 47)
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            homeBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " swings and misses.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                    else
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            homeBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " looks at a strike.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                }
                else if (important.Equals(2))
                {
                    int topS1 = topSRandom();
                    if (topS1 < 13)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a " + generateHomeHitType();
                        addActionToGameLog();
                        clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (topS1 > 12 && topS1 < 23)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a groundout.";
                        addActionToGameLog();
                        homeGroundOut(); whenOutOccurs(); clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (topS1 > 22 && topS1 < 31)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a flyout.";
                        addActionToGameLog();
                        outCount++; whenOutOccurs(); clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (topS1 > 30 && topS1 < 43)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a foul ball.";
                        addActionToGameLog();
                        whenFoulOccurs();
                    }
                    else if (topS1 > 42 && topS1 < 49)
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            homeBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " swings and misses.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                    else
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            homeBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " looks at a strike.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                }
                else if (important.Equals(3))
                {
                    int leftS1 = leftSRandom();
                    if (leftS1 < 11)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a " + generateHomeHitType();
                        addActionToGameLog();
                        clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (leftS1 > 10 && leftS1 < 21)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a groundout.";
                        addActionToGameLog();
                        homeGroundOut(); whenOutOccurs(); clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (leftS1 > 20 && leftS1 < 26)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a flyout.";
                        addActionToGameLog();
                        outCount++; whenOutOccurs(); clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (leftS1 > 25 && leftS1 < 36)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a foul ball.";
                        addActionToGameLog();
                        whenFoulOccurs();
                    }
                    else if (leftS1 > 35 && leftS1 < 46)
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            homeBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " swings and misses.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                    else
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            homeBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " looks at a strike.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                }
                else if (important.Equals(4))
                {
                    int rightS1 = rightSRandom();
                    if (rightS1 < 11)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a " + generateHomeHitType();
                        addActionToGameLog();
                        clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (rightS1 > 10 && rightS1 < 21)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a groundout.";
                        addActionToGameLog();
                        homeGroundOut(); whenOutOccurs(); clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (rightS1 > 20 && rightS1 < 26)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a flyout.";
                        addActionToGameLog();
                        outCount++; whenOutOccurs(); clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (rightS1 > 25 && rightS1 < 36)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a foul ball.";
                        addActionToGameLog();
                        whenFoulOccurs();
                    }
                    else if (rightS1 > 35 && rightS1 < 46)
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            homeBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " swings and misses.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                    else
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            homeBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " looks at a strike.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                }
                else if (important.Equals(5))
                {
                    int ballQ = BallButtonRandom();
                    if (ballQ < 4)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a " + generateHomeHitType();
                        addActionToGameLog();
                        clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (ballQ > 3 && ballQ < 8)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a groundout.";
                        addActionToGameLog();
                        homeGroundOut(); whenOutOccurs(); clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (ballQ > 7 && ballQ < 12)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a flyout.";
                        addActionToGameLog();
                        outCount++; whenOutOccurs(); clearStrikesBalls();
                        homeBatterOrder++;
                    }
                    else if (ballQ > 11 && ballQ < 17)
                    {
                        actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " hits a foul ball.";
                        addActionToGameLog();
                        whenFoulOccurs();
                    }
                    else if (ballQ > 16 && ballQ < 38)
                    {
                        strikeCount++;
                        if (strikeCount == 3)
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " struck out.";
                            addActionToGameLog();
                            outCount++; whenOutOccurs(); clearStrikesBalls();
                            homeBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " swings and misses.";
                            addActionToGameLog();
                            whenStrikeOccurs();
                        }
                    }
                    else
                    {
                        ballCount++;
                        if (ballCount == 4)
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " walked.";
                            addActionToGameLog();
                            homeWalk(); clearStrikesBalls();
                            homeBatterOrder++;
                        }
                        else
                        {
                            actionBarX.Text = homeTeamBattersX[GetHBO()].ToString() + " looks at a ball.";
                            addActionToGameLog();
                            whenBallOccurs();
                        }
                    }
                }
            }
            else
            {
                inn++;
                FINALGAMELOG += "UPDATE- INN: " + TBInd.Text + " " + inn2.ToString() + ", " + SetRosters.getATName().Substring(0, 3) + ": " + atScore.ToString() + " " + SetRosters.getHTName().Substring(0, 3) + ": " + htScore.ToString() + "\n";
                awayPitchesThrown--;
                clearShapes();
            }
        }
    }
}