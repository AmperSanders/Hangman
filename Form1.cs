using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HangmanGame
{
    public partial class Form1 : Form
    {

        //"Global" Variables//
        public static List<string> theWords = Wlist();
        public static string chosenWord = SelectWord(theWords);
        public static int length = chosenWord.Length;
        public static char usrInput;
        public static List<char> noLetter = new List<char>();
        public static FlowLayoutPanel panelGroup = new FlowLayoutPanel();
        public static List<char> guessed = new List<char>();
        public static char[] storedChars = new char[length];
        TextBox[] boxes = new TextBox[length];
        public static int g = 0;

        public Form1 frm;
        public Form1()
        {
            InitializeComponent();
            frm = this;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //showList(theWords);               //Displays List of words
            for (int i = 0; i < length; i++)    //Loop to create boxes for the the boxes array
            {
                boxes[i] = new TextBox();
                this.Controls.Add(boxes[i]);
            }
            editPanel();
            CreateBox();
            textBox2.Text = (chosenWord);
        }

        void editPanel()
        {
            panelGroup.BorderStyle = BorderStyle.FixedSingle;
            panelGroup.AutoSize = true;
            panelGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.Controls.Add(panelGroup);
        }

        static List<string> Wlist() //List
        {
            List<string> words = new List<string>();

            words.Add("Keyboard".ToUpper());  //0
            words.Add("Bottle".ToUpper());    //1  
            words.Add("Cardboard".ToUpper()); //2
            words.Add("Dream".ToUpper());     //3
            words.Add("Fate".ToUpper());      //4
            words.Add("Ripple".ToUpper());    //5
            words.Add("Radio".ToUpper());     //6
            words.Add("Basketball".ToUpper());//7
            words.Add("Notebook".ToUpper());  //8
            words.Add("Instrument".ToUpper());//9
            words.Add("Guitar".ToUpper());    //10
            words.Add("Skunk".ToUpper());     //11
            words.Add("Thunder".ToUpper());   //12
            words.Add("Touchpad".ToUpper());  //13
            words.Add("Igloo".ToUpper());     //14
            words.Add("Leopard".ToUpper());   //15
            words.Add("Sky".ToUpper());       //16
            words.Add("Rhythm".ToUpper());    //17
            words.Add("Fruit".ToUpper());     //18
            words.Add("Computer".ToUpper());  //19
            words.Add("Impossible".ToUpper());//20
            words.Add("Forest".ToUpper());    //21
            words.Add("Mountain".ToUpper());  //22
            words.Add("Tribe".ToUpper());     //23
            words.Add("Insect".ToUpper());    //24
            words.Add("Storm".ToUpper());     //25
            words.Add("Animation".ToUpper()); //26
            words.Add("Cloud".ToUpper());     //27
            words.Add("Coin".ToUpper());      //28
            words.Add("Heaven".ToUpper());    //29

            return words;
        }

        void showList(List<string> theWords)  //List display
        {
            foreach (string item in theWords) //loops through words *TEST ONLY*
            {
                usrGuess.Text = string.Join("\r\n", theWords);
            }
        }

        static string SelectWord(List<string> wList)
        {
            string pick;
            var wR = wList;
            var rand = new Random();
            int rng = rand.Next(wR.Count);
            pick = (wR[rng]);
            return pick;
        }

        public void CreateBox()
        {
            int position = 2;                               //space between boxes
            int spacing = 0;                                //centering
            int c = 0;                                      //box counter
            foreach (char letter in chosenWord)
            {
                boxes[c].Name = letter.ToString();
                boxes[c].Text = letter.ToString();
                boxes[c].ReadOnly = true;
                boxes[c].Multiline = false;
                boxes[c].UseSystemPasswordChar = true;
                boxes[c].Font = new Font(boxes[c].Font.FontFamily, 16);
                boxes[c].TextAlign = HorizontalAlignment.Center;
                this.Controls.Add(boxes[c]);
                boxes[c].Left = 20 * position;
                boxes[c].Top = 20;
                boxes[c].Location = new Point(200 + spacing, 280);
                position += 2;
                spacing += 50;
                boxes[c].Width = 35;
                boxes[c].Height = 30;
                panelGroup.Controls.Add(boxes[c]);
                storedChars[c] = letter;
                c++;
            }
            panelGroup.Location = new Point(frm.Width / 2 - (panelGroup.Width / 2), 270);
        }

        public void CompareChar()
        {
            if (storedChars.Contains(usrInput))
            {
                //EXAMPLE of Index to Character:
                //Keyboard
                //01234567
                bool boo = false;
                foreach (TextBox x in boxes)
                {
                    if (x.Name == usrInput.ToString())
                    {
                        boo = true;
                    }
                    else
                    {
                        //null
                    }
                    if (boo)//Correct guess sequence
                    {
                        x.UseSystemPasswordChar = false;
                        guessed.Add(usrInput);
                        usrGuess.SelectionStart = 0;//automatically highlights your input
                        usrGuess.SelectionLength = usrGuess.Text.Length;
                        boo = false;
                        //Winner: all letters guessed correctly
                        if (boxes[g].UseSystemPasswordChar == false && g == length - 1)
                        {
                            DialogResult replay = MessageBox.Show("                 You Win!" + "\r\n" + "Would you like to play again?", "Play Again?", //space is for centering first line
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (replay == DialogResult.Yes)
                            {
                                Application.Restart();
                            }
                            else
                            {
                                Application.Exit();
                            }
                        }
                        else
                        {
                            g++;
                        }
                    }
                }
            }
            else//Wrong Guess     
            {
                noLetter.Add(usrInput);
                usrGuess.SelectionStart = 0;
                usrGuess.SelectionLength = usrGuess.Text.Length;
                GuessList(noLetter, usrInput);
                Graphics g = frm.CreateGraphics();//not sure how these 3 lines worked; guessing I created instances of graphics and rectangle to satisfy paintarg
                Rectangle r = new Rectangle();
                System.Windows.Forms.PaintEventArgs e = new PaintEventArgs(g, r);
                this.DrawPerson(e);
                if (noLetter.Count != 6)
                {
                    //null
                }
                else//Failure: End Game choice? Yes or No
                {
                    foreach (TextBox x in boxes)
                    {
                        x.UseSystemPasswordChar = false;
                    }
                    DialogResult replay = MessageBox.Show("You sent this poor soul to the next life!" + "\r\n" + " " +
                        "                     Play Again?", "Failed", MessageBoxButtons.YesNo, MessageBoxIcon.Question); //whitespace used to center play again
                    if (replay == DialogResult.Yes)
                    {
                        Application.Restart();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
        }

        //Adds Bad guess to a list and also acts as counter for reached guess limits
        private void GuessList(List<char> x, char i)
        {
            txtBoxFail.Text += i + "\r\n";
        }

        public void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics draw = e.Graphics;
            draw.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen myPen = new Pen(Color.Black);
            Pen myPenBrown = new Pen(Color.SaddleBrown);
            Pen myPenGray = new Pen(Color.DarkSlateGray);
            myPen.Width = 3;
            myPenBrown.Width = 3;
            myPenGray.Width = 2;
            draw.DrawLine(myPenBrown, 230, 200, 370, 200); //horizontal floor
            draw.DrawLine(myPenBrown, 260, 200, 260, 50);  //vertical beam
            draw.DrawLine(myPenBrown, 230, 50, 330, 50); //top horizontal beam
            draw.DrawLine(myPenGray, 330, 75, 330, 49);//rope
            draw.DrawLine(myPenBrown, 260, 75, 275, 49);//top right diagonal beam
            draw.DrawLine(myPenBrown, 260, 74, 235, 49);//top left diagonal beam
            draw.DrawLine(myPenBrown, 260, 180, 280, 200);//bottom right diagonal
            draw.DrawLine(myPenBrown, 260, 180, 235, 200);//bottom left diagonal
        }

        private void DrawPerson(PaintEventArgs e)
        {
            Graphics draw = e.Graphics;
            draw.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen myPen = new Pen(Color.Black);
            Pen myPenBrown = new Pen(Color.SaddleBrown);
            Pen myPenGray = new Pen(Color.DarkSlateGray);
            myPen.Width = 3;
            myPenBrown.Width = 3;
            myPenGray.Width = 2;
            switch (noLetter.Count)
            {
                case 1:
                    draw.DrawEllipse(myPen, 315, 75, 30, 30);//head
                    break;
                case 2:
                    draw.DrawLine(myPen, 330, 145, 330, 105);//body
                    break;
                case 3:
                    draw.DrawLine(myPen, 320, 185, 330, 144);//Left leg
                    break;
                case 4:
                    draw.DrawLine(myPen, 340, 185, 330, 144);//Right leg
                    break;
                case 5:
                    draw.DrawLine(myPen, 320, 150, 330, 110);//Left arm
                    break;
                default:
                    draw.DrawLine(myPen, 340, 150, 330, 110);//Right arm*/
                    break;
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void usrGuess_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEnter_Click_1(object sender, EventArgs e)
        {
            try
            {
                usrInput = Convert.ToChar(usrGuess.Text);
                if (!Char.IsLetter(usrInput))
                {
                    MessageBox.Show("Please enter an Alphabetic letter!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    if (guessed.Contains(usrInput) != true && noLetter.Contains(usrInput) != true)
                    {
                        CompareChar();
                    }
                    else
                    {
                        MessageBox.Show("Letter has already been used.", "Not Available", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        usrGuess.SelectionStart = 0;
                        usrGuess.SelectionLength = usrGuess.Text.Length;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No user input found." + "\r\n" + "Please enter a letter.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a personal project in which I used different data types and structures to acheive a complete visual version of the game Hangman in Windows Forms using C#.", "About Project",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hangman is a classic game in which the player will guess letters that may appear in a given word." + "\r\n" +
                "Guessing the correct letters will give clues to what the word is, thus winning the game. However, guessing the wrong letter will draw" +
                " a limb for each wrong guess." + "\r\n" + "The player loses the game if the character is fully drawn before solving the word.", "How to Play", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
