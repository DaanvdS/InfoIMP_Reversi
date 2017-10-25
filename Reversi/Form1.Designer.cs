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
            this.btn_Help = new System.Windows.Forms.Button();
            this.pnl_Game = new System.Windows.Forms.Panel();
            this.nud_Rows = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nud_Columns = new System.Windows.Forms.NumericUpDown();
            this.lbl_PlayerAtTurn = new System.Windows.Forms.Label();
            this.lbl_AmountRed = new System.Windows.Forms.Label();
            this.lbl_AmountBlue = new System.Windows.Forms.Label();
            this.btn_NewGame = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Rows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Columns)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Help
            // 
            this.btn_Help.Location = new System.Drawing.Point(157, 13);
            this.btn_Help.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Help.Name = "btn_Help";
            this.btn_Help.Size = new System.Drawing.Size(136, 35);
            this.btn_Help.TabIndex = 1;
            this.btn_Help.Text = "Help";
            this.btn_Help.UseVisualStyleBackColor = true;
            this.btn_Help.Click += new System.EventHandler(this.btn_Help_Click);
            // 
            // pnl_Game
            // 
            this.pnl_Game.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Game.Location = new System.Drawing.Point(13, 115);
            this.pnl_Game.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_Game.Name = "pnl_Game";
            this.pnl_Game.Size = new System.Drawing.Size(974, 896);
            this.pnl_Game.TabIndex = 2;
            this.pnl_Game.Paint += new System.Windows.Forms.PaintEventHandler(this.drawBoard);
            this.pnl_Game.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnl_Game_MouseClick);
            // 
            // nud_Rows
            // 
            this.nud_Rows.Location = new System.Drawing.Point(84, 56);
            this.nud_Rows.Margin = new System.Windows.Forms.Padding(4);
            this.nud_Rows.Name = "nud_Rows";
            this.nud_Rows.Size = new System.Drawing.Size(65, 22);
            this.nud_Rows.TabIndex = 4;
            this.nud_Rows.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 61);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Rows";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 87);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Columns";
            // 
            // nud_Columns
            // 
            this.nud_Columns.Location = new System.Drawing.Point(84, 85);
            this.nud_Columns.Margin = new System.Windows.Forms.Padding(4);
            this.nud_Columns.Name = "nud_Columns";
            this.nud_Columns.Size = new System.Drawing.Size(65, 22);
            this.nud_Columns.TabIndex = 7;
            this.nud_Columns.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // lbl_PlayerAtTurn
            // 
            this.lbl_PlayerAtTurn.AutoSize = true;
            this.lbl_PlayerAtTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PlayerAtTurn.ForeColor = System.Drawing.Color.Blue;
            this.lbl_PlayerAtTurn.Location = new System.Drawing.Point(413, 17);
            this.lbl_PlayerAtTurn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_PlayerAtTurn.Name = "lbl_PlayerAtTurn";
            this.lbl_PlayerAtTurn.Size = new System.Drawing.Size(29, 31);
            this.lbl_PlayerAtTurn.TabIndex = 0;
            this.lbl_PlayerAtTurn.Text = "1";
            // 
            // lbl_AmountRed
            // 
            this.lbl_AmountRed.AutoSize = true;
            this.lbl_AmountRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AmountRed.ForeColor = System.Drawing.Color.Red;
            this.lbl_AmountRed.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_AmountRed.Location = new System.Drawing.Point(859, 53);
            this.lbl_AmountRed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_AmountRed.Name = "lbl_AmountRed";
            this.lbl_AmountRed.Size = new System.Drawing.Size(94, 31);
            this.lbl_AmountRed.TabIndex = 9;
            this.lbl_AmountRed.Text = "Red: 2";
            this.lbl_AmountRed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_AmountBlue
            // 
            this.lbl_AmountBlue.AutoSize = true;
            this.lbl_AmountBlue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AmountBlue.ForeColor = System.Drawing.Color.Blue;
            this.lbl_AmountBlue.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbl_AmountBlue.Location = new System.Drawing.Point(753, 53);
            this.lbl_AmountBlue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_AmountBlue.Name = "lbl_AmountBlue";
            this.lbl_AmountBlue.Size = new System.Drawing.Size(98, 31);
            this.lbl_AmountBlue.TabIndex = 10;
            this.lbl_AmountBlue.Text = "Blue: 2";
            this.lbl_AmountBlue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_NewGame
            // 
            this.btn_NewGame.Location = new System.Drawing.Point(13, 13);
            this.btn_NewGame.Margin = new System.Windows.Forms.Padding(4);
            this.btn_NewGame.Name = "btn_NewGame";
            this.btn_NewGame.Size = new System.Drawing.Size(136, 35);
            this.btn_NewGame.TabIndex = 11;
            this.btn_NewGame.Text = "New game";
            this.btn_NewGame.UseVisualStyleBackColor = true;
            this.btn_NewGame.Click += new System.EventHandler(this.btn_NewGame_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(806, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 31);
            this.label3.TabIndex = 12;
            this.label3.Text = "Score";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(321, 17);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 31);
            this.label4.TabIndex = 13;
            this.label4.Text = "Player      at turn";
            // 
            // frm_Reversi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 1023);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_NewGame);
            this.Controls.Add(this.lbl_AmountRed);
            this.Controls.Add(this.lbl_AmountBlue);
            this.Controls.Add(this.lbl_PlayerAtTurn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nud_Columns);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nud_Rows);
            this.Controls.Add(this.pnl_Game);
            this.Controls.Add(this.btn_Help);
            this.Controls.Add(this.label4);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frm_Reversi";
            this.Text = "Reversi";
            ((System.ComponentModel.ISupportInitialize)(this.nud_Rows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Columns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Help;
        private System.Windows.Forms.Panel pnl_Game;
        private System.Windows.Forms.NumericUpDown nud_Rows;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nud_Columns;
        private System.Windows.Forms.Label lbl_PlayerAtTurn;
        private System.Windows.Forms.Label lbl_AmountRed;
        private System.Windows.Forms.Label lbl_AmountBlue;
        private System.Windows.Forms.Button btn_NewGame;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

