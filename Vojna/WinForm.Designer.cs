using System.ComponentModel;

namespace Vojna; 

partial class WinForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }

        base.Dispose(disposing);
    }
    
    public void InitiliazeViewPort() {
        Label WinPlayerOne = new Label();
        Label WinPlayerTwo = new Label();
        Label PlayerOneCard = new Label();
        Label PlayerTwoCard = new Label();
        Label playerWin = new Label();

        WinPlayerOne.Text = Vars.PlayerOne.Wins.ToString();
        WinPlayerTwo.Text = Vars.PlayerTwo.Wins.ToString();
        PlayerOneCard.Text = Vars.PlayerOneCard.suite + "/" + Vars.PlayerOneCard.face;
        PlayerTwoCard.Text = Vars.PlayerTwoCard.suite + "/" + Vars.PlayerTwoCard.face;
        playerWin.Text = (Vars.PlayerOne.Wins > Vars.PlayerTwo.Wins) ? "Player one won" : "Player two won";
        
        WinPlayerOne.Location = new Point(50, 50);
        WinPlayerTwo.Location = new Point(200, 50);
        PlayerOneCard.Location = new Point(50, 250);
        PlayerTwoCard.Location = new Point(200, 250);
        playerWin.Location = new Point(125, 150);
        
        this.Controls.AddRange(new Control[]{
            WinPlayerOne, WinPlayerTwo, PlayerOneCard, PlayerTwoCard, playerWin
        });
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(300, 400);
        this.Text = "WinForm";
    }

    #endregion
}