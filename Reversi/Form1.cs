﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Heb voornamelijk alleen nog de structuur van classes aangelegd
// belangrijkste methodes die nog moeten: het tekenen, het handelen van mouseclick events, 
// Als je code wilt verwijderen/aanpassen kan je het dan in een comment zetten? dan kunnen we makkelijker in één bestand werken

namespace Reversi {
        //En nu weer terug?
    public partial class frm_Reversi : Form {
        private Board revBoard;
        public frm_Reversi() {
            InitializeComponent();
            int Rows = 6;               //values in design??!?
            int Columns = 6;

            revBoard = new Board(Rows, Columns, this);

            if (revBoard.IsRowsEven) {
                //goedzo
            } else {
                MessageBox.Show("Hey, ho, be careful, the rows and columns in the board should be even!", "Wow wow wow hey!");
            }
        }

        private void pnl_Game_MouseClick(object sender, MouseEventArgs e) {
            int clicked_i, clicked_j;
            if (e.X > (revBoard.columns * revBoard.squareSize) || e.X < 0) {
                return;
            }
            if (e.Y > (revBoard.rows * revBoard.squareSize) || e.Y < 0) {
                return;
            }


            clicked_i = e.X / revBoard.squareSize; //er lijkt iets niet te kloppen qua positie e.X (een verschuiving)
            clicked_j = e.Y / revBoard.squareSize;

            revBoard.ChangeSquareColor(clicked_i, clicked_j);

            Invalidate();

            revBoard.playerAtTurn = revBoard.arrPlayers[1 - revBoard.playerAtTurn.myId];
        }

        public void drawBoard(object sender, PaintEventArgs e) {
            //Draw a rectangle with the size of the board
            Graphics g = e.Graphics;            
            g.FillRectangle(Brushes.White, 0, 0, revBoard.boardSize[0], revBoard.boardSize[1]);

            //Draw borders and circles on the board
            for (int i = 0; i < revBoard.rows; i++) {
                drawVerticalBorder(i, g);
                for (int j = 0; j < revBoard.rows; j++) {
                    drawSquare(i, j, g);
                    if (i == 0) { 
                        drawHorizontalBorder(j, g);
                    }
                }
            }

            //Draw the last two borders.
            drawVerticalBorder(revBoard.rows, g);
            drawHorizontalBorder(revBoard.rows, g);
        }

        private void drawVerticalBorder(int i, Graphics g) {
            g.FillRectangle(Brushes.Black, i * (revBoard.squareSize + revBoard.borderWidth), 0, 2, revBoard.boardSize[0]);
        }

        private void drawHorizontalBorder(int i, Graphics g) {
            g.FillRectangle(Brushes.Black, 0, i * (revBoard.squareSize + revBoard.borderWidth), revBoard.boardSize[1], 2);
        }

        private void drawSquare(int i, int j, Graphics g) {
            Square currSquare = revBoard.arrSquares[i, j];
            Brush b = new SolidBrush(currSquare.PieceColor);
            int offsetX = (i + 1) * revBoard.borderWidth - 1;
            int offsetY = (j + 1) * revBoard.borderWidth - 1;
            g.FillEllipse(b, revBoard.squareSize * i + offsetX, revBoard.squareSize * j + offsetY, revBoard.squareSize, revBoard.squareSize);
        }
    }

    public class Board {
        public int rows, columns;
        public int squareSize = 100;
        public int borderWidth = 4;
        public int[] boardSize = new int[] { 0, 0 };
        
        public Player[] arrPlayers = new Player[2];
        public Player playerAtTurn;
        private Player Winner;          //indicates which player has won

        public bool IsRowsEven;         //these variables are created in instance of class            

        public Square[,] arrSquares;

        private bool GameState;          //indicates if game is running or not

        public Board(int t_rows, int t_columns, frm_Reversi t_Form) {
            //Calculate board dimensions
            this.rows = t_rows;
            this.columns = t_columns;
            this.boardSize[0] = (borderWidth + squareSize) * rows;
            this.boardSize[1] = (borderWidth + squareSize) * columns;
            this.IsRowsEven = (rows % 2 == 0);
            this.arrSquares = new Square[rows, rows];

            //Initiate players
            this.arrPlayers[0] = new Player(Color.Blue, 0);
            this.arrPlayers[1] = new Player(Color.Red, 1);
            this.playerAtTurn = arrPlayers[0];
            
            //Initiating the squares on the empty board
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < rows; j++) {
                    arrSquares[i, j] = new Square(false, Color.White, false);
                }
            }

            //Starting squares.
            arrSquares[(rows / 2) - 1, (columns / 2) - 1].PieceColor = arrPlayers[0].PlayerColor;
            arrSquares[(rows / 2), (columns / 2)].PieceColor = arrPlayers[0].PlayerColor;
            arrSquares[(rows / 2) - 1, (columns / 2)].PieceColor = arrPlayers[1].PlayerColor;
            arrSquares[(rows / 2), (columns / 2) - 1].PieceColor = arrPlayers[1].PlayerColor;
        }
		
		//public bool CheckMove(int i, int j) {
        //    int c_i, c_j;
        //    c_i = i;c_j = j;
        //    for (int t = 0; t < (this.rows - j); t++) {
        //        c_j++;
        //        if ((arrSquares[c_i, c_j].PieceColor != Color.White) && (arrSquares[c_i, c_j].PieceColor != this.playerAtTurn.PlayerColor)) {

        //        }
        //    }
        //    return false;
        //}

        //public bool CheckMove(int i, int j) { //versie 1(klopt niks van)
        //    //this method should check the 8 directions from the square (i,j) 
        //    //square, and check if one ends in a piece of same color as the player at turn
        //    // The 8 directions:
        //    //  1   2   3
        //    //  8  i,j  4
        //    //  7   6   5

        //    bool[] dir_Possible = new bool[8];

        //    dir_Possible[0] = (this.playerAtTurn.PlayerColor == arrSquares[i - 1, j - 1].PieceColor);
        //    dir_Possible[1] = (this.playerAtTurn.PlayerColor == arrSquares[i    , j - 1].PieceColor);
        //    dir_Possible[2] = (this.playerAtTurn.PlayerColor == arrSquares[i + 1, j - 1].PieceColor);
        //    dir_Possible[3] = (this.playerAtTurn.PlayerColor == arrSquares[i + 1, j    ].PieceColor);
        //    dir_Possible[4] = (this.playerAtTurn.PlayerColor == arrSquares[i + 1, j + 1].PieceColor);
        //    dir_Possible[5] = (this.playerAtTurn.PlayerColor == arrSquares[i    , j + 1].PieceColor);
        //    dir_Possible[6] = (this.playerAtTurn.PlayerColor == arrSquares[i - 1, j + 1].PieceColor);
        //    dir_Possible[7] = (this.playerAtTurn.PlayerColor == arrSquares[i - 1, j    ].PieceColor);

        //    for (int t = 0; t < 8; t++) {
        //        if (dir_Possible[t] == true)
        //            return true;
        //    }
        //    return false;
        //}
        public void ChangeSquareColor(int i, int j) {
            this.arrSquares[i,j].PieceColor = this.playerAtTurn.PlayerColor;
        }
    }

    public class Square {
        public bool Occupied;           //indicator if the square is empty or not
        public Color PieceColor;        //indicator of piece color, not always neccesary (White if not necesarry)
        public bool movePossible;       //indicator if the square can be clicked as valid move (small black circle)

        public Square(bool Occupied, Color PieceColor, bool movePossible) {
            this.Occupied = Occupied;
            this.PieceColor = PieceColor;
            this.movePossible = movePossible;
        }
    }

    public class Player {
        public Color PlayerColor;       //indicator of playercolor        
        public int myId;

        public Player(Color PlayerColor, int t_Id) {
            this.PlayerColor = PlayerColor;            
            myId = t_Id;
        }        
    }
}




