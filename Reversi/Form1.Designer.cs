namespace Reversi {
    partial class frm_Reversi {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btn_NieuwSpel = new System.Windows.Forms.Button();
            this.btn_Help = new System.Windows.Forms.Button();
            this.pnl_Game = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btn_NieuwSpel
            // 
            this.btn_NieuwSpel.Location = new System.Drawing.Point(12, 12);
            this.btn_NieuwSpel.Name = "btn_NieuwSpel";
            this.btn_NieuwSpel.Size = new System.Drawing.Size(75, 23);
            this.btn_NieuwSpel.TabIndex = 0;
            this.btn_NieuwSpel.Text = "Nieuw spel";
            this.btn_NieuwSpel.UseVisualStyleBackColor = true;
            // 
            // btn_Help
            // 
            this.btn_Help.Location = new System.Drawing.Point(347, 12);
            this.btn_Help.Name = "btn_Help";
            this.btn_Help.Size = new System.Drawing.Size(75, 23);
            this.btn_Help.TabIndex = 1;
            this.btn_Help.Text = "Help";
            this.btn_Help.UseVisualStyleBackColor = true;
            // 
            // pnl_Game
            // 
            this.pnl_Game.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Game.Location = new System.Drawing.Point(12, 41);
            this.pnl_Game.Name = "pnl_Game";
            this.pnl_Game.Size = new System.Drawing.Size(800, 800);
            this.pnl_Game.TabIndex = 2;
            this.pnl_Game.Paint += new System.Windows.Forms.PaintEventHandler(this.drawBoard);
            this.pnl_Game.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnl_Game_MouseClick);
            // 
            // frm_Reversi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 858);
            this.Controls.Add(this.pnl_Game);
            this.Controls.Add(this.btn_Help);
            this.Controls.Add(this.btn_NieuwSpel);
            this.Name = "frm_Reversi";
            this.Text = "Reversi";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_NieuwSpel;
        private System.Windows.Forms.Button btn_Help;
        private System.Windows.Forms.Panel pnl_Game;
    }
}

