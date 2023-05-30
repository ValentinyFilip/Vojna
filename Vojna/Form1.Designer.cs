using System.ComponentModel;
using System.Media;

namespace Vojna;

partial class Form1 {
    private Label WinPlayerOne;
    private Label WinPlayerTwo;
    private Thread _thread;

    public delegate void Changetext(String playerOne, String playerTwo);

    public Changetext myDelegate;

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

    private void saveButton_Click(object sender, System.EventArgs e) {
        InitiliazeLeaderBoard();
    }

    private void playButton_Click(object sender, System.EventArgs e) {
        _thread = new Thread(new ThreadStart(ThreadFunction));
        _thread.Start();
    }

    public void InitiliazeLeaderBoard() {
        var leaderBoardForm = new leaderBoardForm();
        leaderBoardForm.Show();
    }

    public void InitiliazeViewPort() {
        Button playButton = new Button();
        Button saveButton = new Button();
        WinPlayerOne = new Label();
        WinPlayerTwo = new Label();

        playButton.Click += new EventHandler(playButton_Click);
        playButton.Text = "Draw a card";
        playButton.Location = new Point(350, 300);
        playButton.Size = new Size(200, 50);
        
        saveButton.Click += new EventHandler(saveButton_Click);
        saveButton.Text = "Save score";
        saveButton.Location = new Point(50, 300);
        saveButton.Size = new Size(200, 50);

        WinPlayerOne.Location = new Point(100, 50);
        WinPlayerOne.Text = "0";
        WinPlayerTwo.Text = "0";
        WinPlayerTwo.Location = new Point(700, 50);
        
        this.Controls.Add(playButton);
        this.Controls.Add(saveButton);
        
        Vars.Game = new CarDLogic();
        Vars.Game.Initialize();
        myDelegate = new Changetext(ChangeTextFunc);
        string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string sFile = Path.Combine(sCurrentDirectory, @"..\..\..\bangr.wav");
        string sFilePath = Path.GetFullPath(sFile);
        System.Media.SoundPlayer sp = new SoundPlayer(sFilePath);
        sp.PlayLooping();
    }

    private void ChangeTextFunc(String playerOne, String playerTwo) {
        WinPlayerOne.Text = playerOne;
        WinPlayerTwo.Text = playerTwo;
    }

    private void ThreadFunction() {
        MyThreadClass myThreadObject = new MyThreadClass(this);
        myThreadObject.Run();
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

class MyThreadClass {
    private Form1 myForm1;

    public MyThreadClass(Form1 myForm) {
        myForm1 = myForm;
    }

    private void Round() {
        List<CarDLogic.WarCard> cards = Vars.Game.DrawCard();
        int roundResults = Vars.Game.EvaluateResults(cards[0], cards[1]);
        if (roundResults == 1) {
            Vars.PlayerOne.Wins += 1;
        }
        else if (roundResults == 2)
        {
            Vars.PlayerTwo.Wins += 1;
        }
        else {
            roundResults = Vars.Game.War();
            if (roundResults == 1) {
                Vars.PlayerOne.Wins += 1;
            }
            else if (roundResults == 0) {
                MessageBox.Show("one of the players run out of cards. Save your score"); 
            }
            else {
                Vars.PlayerTwo.Wins += 1;
            }
        }
    }
    
    public void Run() {
        Round();
        myForm1.BeginInvoke(myForm1.myDelegate,new Object[] {Vars.PlayerOne.Wins.ToString(), Vars.PlayerTwo.Wins.ToString()});
    }
}
