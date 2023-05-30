using System.ComponentModel;

namespace Vojna; 

partial class leaderBoardForm {
    private Label WinPlayerOne;
    private Label WinPlayerTwo;
    
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
        WinPlayerOne = new Label();
        WinPlayerTwo = new Label();
        
        WinPlayerOne.Location = new Point(100, 50);
        WinPlayerOne.Text = Vars.PlayerOne.Wins.ToString();
        WinPlayerTwo.Text = Vars.PlayerTwo.Wins.ToString();
        WinPlayerTwo.Location = new Point(700, 50);
        
        this.Controls.Add(WinPlayerOne);
        this.Controls.Add(WinPlayerTwo);
        
    }
    
    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "leaderBoardForm";
    }

    #endregion
}