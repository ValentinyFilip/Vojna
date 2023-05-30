using System.ComponentModel;
using System.Media;

namespace Vojna;

partial class Form1 {
    private Label WinPlayerOne;
    private Label WinPlayerTwo;
    private cardForm _cardForm;
    private WinForm _winForm;
    private Button _playbutton;

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

    private void help_click(object sender, System.EventArgs e) {
        MessageBox.Show("Klikej dokud nevyhraješ \n Adam Hanusek, Filip Valentíny");
    }

    private void playButton_Click(object sender, System.EventArgs e) {
        if (Vars.end == false) {
            List<CarDLogic.WarCard> cards = Vars.Game.DrawCard();
                    
            int roundResults = Vars.Game.EvaluateResults(cards[0], cards[1]);
            if (roundResults == 1) {
                Vars.PlayerOne.Wins += 1;
            }
            else if (roundResults == 2)
            {
                Vars.PlayerTwo.Wins += 1;
            }
            else if (roundResults == 0) {
                Vars.end = true;
            }
            else {
                roundResults = Vars.Game.War();
                if (roundResults == 1) {
                    Vars.PlayerOne.Wins += 1;
                }
                else if (roundResults == 0) {
                    Vars.end = true;
                }
                else {
                    Vars.PlayerTwo.Wins += 1;
                }
            }
            if (_cardForm != null) {
                _cardForm.Close();
            }
            _cardForm = new cardForm();
            _cardForm.Show();
        }
        else {
            _cardForm.Close();
            if (_winForm == null) {
                _winForm = new WinForm();
                _winForm.Show();
            }
        }
    }
    
    public void InitiliazeViewPort() {
        _playbutton = new Button();
        WinPlayerOne = new Label();
        WinPlayerTwo = new Label();
        Button help = new Button();

        _playbutton.Click += new EventHandler(playButton_Click);
        _playbutton.Text = "Draw a card";
        _playbutton.Location = new Point(25, 100);
        _playbutton.Size = new Size(200, 50);
        
        help.Click += new EventHandler(help_click);
        help.Text = "Shoe help";
        help.Location = new Point(75, 50);
        help.Size = new Size(100, 25);

        WinPlayerOne.Location = new Point(100, 50);
        WinPlayerOne.Text = "0";
        WinPlayerTwo.Text = "0";
        WinPlayerTwo.Location = new Point(700, 50);
        
        this.Controls.Add(_playbutton);
        this.Controls.Add(help);
        
        Vars.Game = new CarDLogic();
        Vars.Game.Initialize();
        string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string sFile = Path.Combine(sCurrentDirectory, @"..\..\..\bangr.wav");
        string sFilePath = Path.GetFullPath(sFile);
        System.Media.SoundPlayer sp = new SoundPlayer(sFilePath);
        sp.PlayLooping();
    }

    private void DrawCardForm(Boolean end) {
        if (end == false) {
            if (_cardForm != null) {
                _cardForm.Close();
            }
            _cardForm = new cardForm();
            _cardForm.Show();
        }
        else {
            _cardForm.Close();
            MessageBox.Show("bla");
            _winForm = new WinForm();
            _winForm.Show();
        }
    }
    
    
    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(250, 250);
        this.Text = "Vojna";
    }

    #endregion
}