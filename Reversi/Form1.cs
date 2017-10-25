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
        private bool helpEnabled = false;
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
            if (helpEnabled) {
                helpClear();
            }

            int clicked_i, clicked_j;
            if (e.X > (revBoard.columns * revBoard.squareSize) || e.X < 0) {
                return;
            }
            if (e.Y > (revBoard.rows * revBoard.squareSize) || e.Y < 0) {
                return;
            }


            clicked_i = e.X / revBoard.squareSize; //er lijkt iets niet te kloppen qua positie e.X (een verschuiving)
            clicked_j = e.Y / revBoard.squareSize;
            if (revBoard.arrSquares[clicked_i, clicked_j].PieceColor == Color.White) {     //dit voorkomt de zetten waarbij er op een niet witte geklikt wordt!
                revBoard.checkMove(clicked_i, clicked_j,true);
            } else {
                MessageBox.Show("No possible moves from this square. ");        
            }

            if (helpEnabled) {
                helpCalculate();
            }

            Invalidate();


            lbl_PlayerAtTurn.Text = ("Player " + (revBoard.playerAtTurn.myId + 1) + " at turn");
            lbl_PlayerAtTurn.ForeColor = revBoard.playerAtTurn.PlayerColor;
            revBoard.CountPieces();
            lbl_AmountBlue.Text = (revBoard.AmountBlue + " Blue Pieces");
            lbl_AmountRed.Text = (revBoard.AmountRed + " Red Pieces");

            if (!checkForPossibleMove()) {
                MessageBox.Show("There are no available moves, player " + (revBoard.playerAtTurn.myId + 1) + " skips a turn.");
                revBoard.playerAtTurn = revBoard.arrPlayers[1 - revBoard.playerAtTurn.myId];
            }
        }

        public void drawBoard(object sender, PaintEventArgs e) {
            //Draw a rectangle with the size of the board
            Graphics g = e.Graphics;            
            g.FillRectangle(Brushes.White, 0, 0, revBoard.boardSize[0], revBoard.boardSize[1]);

            //Draw borders and circles on the board
            for (int i = 0; i < revBoard.columns; i++) {
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
            
            if (currSquare.PieceColor == Color.White) {
                if (currSquare.movePossible == true) {
                    int diameter = revBoard.squareSize / 2;
                    int offsetX = (i + 1) * revBoard.borderWidth - 1 + diameter / 2;
                    int offsetY = (j + 1) * revBoard.borderWidth - 1 + diameter / 2;
                    g.DrawEllipse(Pens.Black, revBoard.squareSize * i + offsetX, revBoard.squareSize * j + offsetY, diameter, diameter);
                }
            } else { 
                Brush b = new SolidBrush(currSquare.PieceColor);
                int diameter = revBoard.squareSize;
                int offsetX = (i + 1) * revBoard.borderWidth - 1;
                int offsetY = (j + 1) * revBoard.borderWidth - 1;
                g.FillEllipse(b, revBoard.squareSize * i + offsetX, revBoard.squareSize * j + offsetY, diameter, diameter);
            }
        }

        private void btn_Help_Click(object sender, EventArgs e) {
            if (helpEnabled) {
                helpClear();
                helpEnabled = false;
            } else {
                helpCalculate();
                helpEnabled = true;
            }
        }

        private bool checkForPossibleMove() {
            helpCalculate();
            for (int i = 0; i < revBoard.columns; i++) {
                for (int j = 0; j < revBoard.rows; j++) {
                    if (revBoard.arrSquares[i, j].movePossible)
                        return true;
                }
            }
            helpClear();
            return false;
        }

        private void helpCalculate() {
            for (int i = 0; i < revBoard.columns; i++) {
                for (int j = 0; j < revBoard.rows; j++) {
                    if (revBoard.arrSquares[i, j].PieceColor == Color.White)
                        revBoard.checkMove(i, j, false);
                }
            }

            Invalidate();
        }

        private void helpClear() {
            for (int i = 0; i < revBoard.columns; i++) {
                for (int j = 0; j < revBoard.rows; j++) {
                    revBoard.arrSquares[i, j].movePossible = false;
                }
            }

            Invalidate();
        }

    }

    public class Board {
        public int AmountBlue, AmountRed;
        public int rows, columns;
        public int squareSize = 100;
        public int borderWidth = 4;
        public int[] boardSize = new int[] { 0, 0 };
        int[,][] directionCoeffs = new int[3, 3][];

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
            this.arrSquares = new Square[columns, rows];
            this.AmountBlue = 2; this.AmountRed = 2;

            //Initiate players
            this.arrPlayers[0] = new Player(Color.Blue, 0);
            this.arrPlayers[1] = new Player(Color.Red, 1);
            this.playerAtTurn = arrPlayers[0];

            //Helper array for checking validity of move
            directionCoeffs[0, 0] = new int[] { (-1), (-1) };
            directionCoeffs[0, 1] = new int[] { (-1), 0 };
            directionCoeffs[0, 2] = new int[] { (-1), 1 };
            directionCoeffs[1, 0] = new int[] { 0, (-1) };
            directionCoeffs[1, 1] = new int[] { 0, 0 };
            directionCoeffs[1, 2] = new int[] { 0, 1 };
            directionCoeffs[2, 0] = new int[] { 1, (-1) };
            directionCoeffs[2, 1] = new int[] { 1, 0 };
            directionCoeffs[2, 2] = new int[] { 1, 1 };

            //Initiating the squares on the empty board
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < rows; j++) {
                    arrSquares[i, j] = new Square(false, Color.White);
                }
            }

            //Starting squares.
            arrSquares[(rows / 2) - 1, (columns / 2) - 1].PieceColor = arrPlayers[0].PlayerColor;
            arrSquares[(rows / 2), (columns / 2)].PieceColor = arrPlayers[0].PlayerColor;
            arrSquares[(rows / 2) - 1, (columns / 2)].PieceColor = arrPlayers[1].PlayerColor;
            arrSquares[(rows / 2), (columns / 2) - 1].PieceColor = arrPlayers[1].PlayerColor;
        }

        public Boolean isOpponentColor(int i, int j) {
            Color squareColor = getPieceColorFromBoard(i, j);
            Color playerColor = this.playerAtTurn.PlayerColor;
            return (squareColor != playerColor) && (squareColor != Color.White);
        }

        public Color getPieceColorFromBoard(int i, int j) {
            if ((!(i < 0 || i >= columns)) && (!(j < 0 || j >= rows))) {
                return arrSquares[i, j].PieceColor;
            } else {
                return Color.White;
            }
        }

        public bool checkDirection(int i, int j, int k, int l) {
            int[] directionCoeff = this.directionCoeffs[k, l];
            if (isOpponentColor(i + directionCoeff[0], j + directionCoeff[1])) {
                bool done = checkDirection(i + directionCoeff[0], j + directionCoeff[1], k, l);
                return done;
            } else if(getPieceColorFromBoard(i + directionCoeff[0], j + directionCoeff[1]) ==Color.White) {
                //Witte steen gevonden voordat de spelerkleur gevonden is.
                return false;
            } else {
                //We hebben de speler kleur weer gevonden, dus de move is goed.
                return true;
            }            
        }

        public void changeSquareColor(int i, int j, int k, int l) {
            int[] directionCoeff = this.directionCoeffs[k, l];
            if (isOpponentColor(i + directionCoeff[0], j + directionCoeff[1])) {
                this.arrSquares[i + directionCoeff[0], j + directionCoeff[1]].PieceColor = this.playerAtTurn.PlayerColor;
                changeSquareColor(i + directionCoeff[0], j + directionCoeff[1], k, l);
            } else {
                //We hebben de speler kleur weer gevonden, dus de move is goed.
            }
        }

        public void setMovePossible(int i, int j, int k, int l) {
            int[] directionCoeff = this.directionCoeffs[k, l];
            if (isOpponentColor(i + directionCoeff[0], j + directionCoeff[1])) {
                this.arrSquares[i + directionCoeff[0], j + directionCoeff[1]].movePossible = true;
                setMovePossible(i + directionCoeff[0], j + directionCoeff[1], k, l);
            } else {
                //We hebben de speler kleur weer gevonden, dus de move is goed.
            }
        }

        public void checkMove(int i, int j, bool changeColor) {
            //Oke, we staan in i,j. Eerst gaan we kijken in direct aanliggende velden, als daar geen opponentcolor is zijn we klaar. Zowel, dan die kant registreren
            for (int k = 0; k < 3; k++) {
                for (int l = 0; l < 3; l++) {
                    int[] directionCoeff = this.directionCoeffs[k, l];
                    if (isOpponentColor(i + directionCoeff[0], j + directionCoeff[1])) {
                        //MessageBox.Show("Richting " + k.ToString() + ";" + l.ToString() + " kan misschien.");
                        //Oke, er is een mogelijke richting
                        if (checkDirection(i + directionCoeff[0], j + directionCoeff[1], k, l)) {
                            //Oke, het pad bevat oppontent color en eindigt met de players color.
                            //Nu alle stenen omdraaien.

                            if (isOpponentColor(i + directionCoeff[0], j + directionCoeff[1])) {
                                if (changeColor) {
                                    this.arrSquares[i, j].PieceColor = this.playerAtTurn.PlayerColor;
                                    changeSquareColor(i, j, k, l);
                                    checkMove(i, j, changeColor);
                                    this.playerAtTurn = this.arrPlayers[1 - this.playerAtTurn.myId];   //moved to here from the mouseclick
                                    return;
                                } else {
                                    this.arrSquares[i, j].movePossible = true;
                                    setMovePossible(i, j, k, l);
                                    
                                }
                                
                                
                            } else {
                                //Er is iets echt kapot gegaan wow.
                                MessageBox.Show("Dit kan niet fout gaan, eigenlijk.");
                            }
                            
                            //Nu moet de forloop stoppen

                        }
                    } else {
                        if (k == 2 && l == 2) {
                            //MessageBox.Show("No possible moves from this square. ");
                        }
                        
                    }

                }
            }
        }
        public void CountPieces() {
            this.AmountBlue = 0;  //even resetten van de values
            this.AmountRed = 0;
            for(int p = 0; p<this.rows; p++) {
                for(int q = 0; q<this.columns; q++) {
                    if (this.arrSquares[p, q].PieceColor == this.arrPlayers[0].PlayerColor)
                        this.AmountBlue++;
                    if (this.arrSquares[p, q].PieceColor == this.arrPlayers[1].PlayerColor)
                        this.AmountRed++;
                }
            }
        } 
    }

    public class Square {
        public bool Occupied;           //indicator if the square is empty or not
        public Color PieceColor;        //indicator of piece color, not always neccesary (White if not necesarry)
        public bool movePossible;       //indicator if the square can be clicked as valid move (small black circle)

        public Square(bool Occupied, Color PieceColor) {
            this.Occupied = Occupied;
            this.PieceColor = PieceColor;
            this.movePossible = false;
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




