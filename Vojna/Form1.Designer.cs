﻿namespace Vojna;


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
        
    }

    private void InitiliazeViewPort() {
        Button newButton = new Button();
        Label winPlayer = new Label();
        Label winAI = new Label();
        
        newButton.Click += new EventHandler(newButtoN_Click);
        newButton.Text = "Draw a card";
        newButton.Location = new Point(350, 300);
        newButton.Size = new Size(200, 50);

        winPlayer.Location = new Point(100, 50);
        winPlayer.Text = "0";
        winAI.Text = "0";
        winAI.Location = new Point(700, 50);
        
        this.Controls.Add(newButton);
        this.Controls.Add(winAI);
        this.Controls.Add(winPlayer);

        vars.game = new carD_Logic();
        vars.game.initialize();
        
        
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
