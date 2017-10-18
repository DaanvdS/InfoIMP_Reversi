using System;
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
            revBoard = new Board(Rows, Columns, this);
            
            revBoard.arrPlayers[0] = new Player(Color.Blue);
            revBoard.arrPlayers[1] = new Player(Color.Red);

            if (revBoard.IsRowsEven) {
                //goedzo
            } else {
                MessageBox.Show("Hey, ho, be careful, the rows and columns in the board should be even!", "Wow wow wow hey!");
            }
        }

        private void pnl_Game_MouseClick(object sender, MouseEventArgs e) {
            int clicked_i, clicked_j;
            clicked_i = e.Y / revBoard.squareSize;
            clicked_j = e.X / revBoard.squareSize;

            revBoard.arrSquares[clicked_i, clicked_j].PieceColor = revBoard.playerAtTurn.PlayerColor;
            Invalidate();
        }

        public void drawBoard(object sender, PaintEventArgs e) {
            //teken bord iets met een forloop en drawSquares
            Graphics g = e.Graphics;
            Brush b = new SolidBrush(Color.White);
            g.FillRectangle(b, 0, 0, revBoard.rows * (revBoard.squareSize + revBoard.borderWidth), revBoard.rows * (revBoard.squareSize + revBoard.borderWidth));
            for (int i = 0; i < revBoard.rows; i++) {
                drawVerticalBorder(i, g);
                for (int j = 0; j < revBoard.rows; j++) {
                    drawSquare(i, j, g);
                    if (i == 0) { 
                        drawHorizontalBorder(j, g);
                    }
                }
            }
            drawVerticalBorder(revBoard.rows, g);
            drawHorizontalBorder(revBoard.rows, g);
        }

        private void drawVerticalBorder(int i, Graphics g) {
            Brush b = new SolidBrush(Color.Black);
            int offsetX = i * revBoard.borderWidth;
            g.FillRectangle(b, i * revBoard.squareSize + offsetX, 0, 2, revBoard.boardSize[0]);
        }

        private void drawHorizontalBorder(int i, Graphics g) {
            Brush b = new SolidBrush(Color.Black);
            int offsetX = i * revBoard.borderWidth;
            g.FillRectangle(b, 0, i * revBoard.squareSize + offsetX, revBoard.boardSize[1], 2);
        }

        private void drawSquare(int i, int j, Graphics g) {
            Square currSquare = revBoard.arrSquares[i, j];
            Brush b = new SolidBrush(currSquare.PieceColor);
            //Brush b = new SolidBrush(Color.Purple);
            int offsetX = (i + 1) * revBoard.borderWidth - 1;
            int offsetY = (j + 1) * revBoard.borderWidth - 1;
            g.FillEllipse(b, revBoard.squareSize * i + offsetX, revBoard.squareSize * j + offsetY, revBoard.squareSize, revBoard.squareSize);
        }

        private void btn_NieuwSpel_Click(object sender, EventArgs e) {
            if ((nud_Rows.Value % 2 == 0) && (nud_Columns.Value % 2 == 0)) {
                revBoard = new Board((int)nud_Rows.Value, (int)nud_Rows.Value, this);
                revBoard.arrPlayers[0] = new Player(Color.Blue);
                revBoard.arrPlayers[1] = new Player(Color.Red);
            } else {
                MessageBox.Show("Hey, ho, be careful, the rows and columns in the board should be even!", "Wow wow wow hey!");
            }
        }
    }

    public class Board {
        private frm_Reversi parent_Form;
        public int rows, columns;               //these variables are set by constructor
        public Player playerAtTurn;
        public int[] boardSize = new int[] { 0, 0 };
        public int squareSize = 100;
        public int borderWidth = 4;
        private bool GameState;          //indicates if game is running or not
        private string Winner;          //indicates which player has won

        public bool IsRowsEven;         //these variables are created in instance of class            
        public Square[,] arrSquares;
        public Player[] arrPlayers = new Player[2];

        public Board(int t_rows, int t_columns, frm_Reversi t_Form) {
            this.parent_Form = t_Form;
            this.rows = t_rows;
            this.columns = t_columns;
            this.boardSize[0] = (borderWidth + squareSize) * rows;
            this.boardSize[1] = (borderWidth + squareSize) * columns;
            IsRowsEven = (rows % 2 == 0);
            arrSquares = new Square[rows, rows];


            for (int i = 0; i < rows; i++) {     //these nested forloops are used to initiate an empty board.
                int x = i * squareSize;
                for (int j = 0; j < rows; j++) {
                    int y = j * squareSize;
                    arrSquares[i, j] = new Square(false, Color.White, false);
                }
            }

            arrSquares[(rows / 2) - 1, (columns / 2) - 1] = new Square(true, Color.Blue, false);
            arrSquares[(rows / 2) - 1, (columns / 2)] = new Square(true, Color.Red, false);
            arrSquares[(rows / 2), (columns / 2) - 1] = new Square(true, Color.Red, false);
            arrSquares[(rows / 2), (columns / 2)] = new Square(true, Color.Blue, false);
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
        public bool myTurn;             //indicator if this player is currently able to do a move

        public Player(Color PlayerColor) {
            this.PlayerColor = PlayerColor;
            myTurn = false;
        }

        public void SetIfItIsMyTurn(bool myTurn) {
            this.myTurn = myTurn;
        }

    }
}




