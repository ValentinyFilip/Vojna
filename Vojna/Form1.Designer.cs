namespace Vojna;


partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void newButtoN_Click(object sender, System.EventArgs e)
    {
        List<carD_Logic.WarCard> cards = vars.game.drawCard();
        List<string> roundResults = vars.game.evaluateResults(cards[0], cards[1]);
        MessageBox.Show($"{roundResults[0]}, won by {roundResults[1]}, \n with p1 card {cards[0].face} and p2 card {cards[1].face}");
        int num;
        string notNum;
        if (roundResults[1] == "1")
        {
            int.TryParse(vars.winPlayerOne.Text, out num);
            num += 1;
            notNum = num.ToString();
            vars.winPlayerOne.Text = notNum;
        }
        else if (roundResults[1] == "2")
        {
            int.TryParse(vars.winPlayerTwo.Text, out num);
            num += 1;
            notNum = num.ToString();
            vars.winPlayerTwo.Text = notNum;
        }
        else
        {
            int result = vars.game.war();
            if (result == 1)
            {
                int.TryParse(vars.winPlayerOne.Text, out num);
                num += 1;
                notNum = num.ToString();
                vars.winPlayerOne.Text = notNum;
            }
            else if (result == 2)
            {
                int.TryParse(vars.winPlayerTwo.Text, out num);
                num += 1;
                notNum = num.ToString();
                vars.winPlayerTwo.Text = notNum;
            }
            else if (result == 10)
            {
                MessageBox.Show("Player 1 won");
            }
            else
            {
                MessageBox.Show("Player 2 won");
            }
        }

    }

    public void InitiliazeViewPort() {
        Button newButton = new Button();
        vars.winPlayerOne = new Label();
        vars.winPlayerTwo = new Label();

        newButton.Click += new EventHandler(newButtoN_Click);
        newButton.Text = "Draw a card";
        newButton.Location = new Point(350, 300);
        newButton.Size = new Size(200, 50);

        vars.winPlayerOne.Location = new Point(100, 50);
        vars.winPlayerOne.Text = "0";
        vars.winPlayerTwo.Text = "0";
        vars.winPlayerTwo.Location = new Point(700, 50);
        
        this.Controls.Add(newButton);
        this.Controls.Add(vars.winPlayerTwo);
        this.Controls.Add(vars.winPlayerOne);

        vars.game = new carD_Logic();
        vars.game.initialize();
        
        
        System.Media.SoundPlayer sp = new System.Media.SoundPlayer("D:/Vojna/Vojna/bangr.wav");
        sp.PlayLooping();
    }
    
    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "Vojna";
    }

    #endregion
}
