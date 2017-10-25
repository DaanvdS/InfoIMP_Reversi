using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi {
    public partial class frm_Reversi : Form {
        private Board revBoard;
        private bool helpEnabled = false;
        public frm_Reversi() {
            InitializeComponent();

            //Bij het beginnen van het programma een spel initieren door een click van NewGame te simuleren.
            btn_NewGame_Click(null, null);
        }

        private void pnl_Game_MouseClick(object sender, MouseEventArgs e) {
            if (revBoard.gameState) {
                //Controleren of de muiscursor niet buiten het bord staat.
                if (e.X > (revBoard.columns * revBoard.squareSize) || e.X < 0) {
                    return;
                }

                if (e.Y > (revBoard.rows * revBoard.squareSize) || e.Y < 0) {
                    return;
                }

                //De muiscursor omrekenen naar vakjes op het bord.
                int clicked_i = e.X / (revBoard.squareSize+revBoard.borderWidth);
                int clicked_j = e.Y / (revBoard.squareSize + revBoard.borderWidth);

                //Als de speler op een wit vakje heeft geklikt, controleren of de move valid is (checkMove wisselt de speler vanzelf om als dat gelukt is).
                if (revBoard.arrSquares[clicked_i, clicked_j].PieceColor == Color.White) {
                    revBoard.checkMoveIterations=0;
                    revBoard.checkMove(clicked_i, clicked_j, true);
                } else {
                    MessageBox.Show("No possible moves from this square. ");
                }

                //Help-rondjes uitrekenen
                if (helpEnabled) {
                    helpCalculate();
                }

                //Tijd om te tekenen
                Invalidate();

                //Winnaar bepalen
                for (int i = 0; i < 2; i++) {
                    if (revBoard.isWinner(revBoard.arrPlayers[i])) {
                        MessageBox.Show("Player " + (i + 1).ToString() + " has won!");
                        revBoard.gameState = false;
                        return;
                    }

                    if (revBoard.isBoardFull()) {
                        if (revBoard.AmountBlue > revBoard.AmountRed) {
                            MessageBox.Show("Player 1 has won!");
                        } else if (revBoard.AmountBlue == revBoard.AmountRed) {
                            MessageBox.Show("Tie!");
                        } else {
                            MessageBox.Show("Player 2 has won!");
                        }

                        revBoard.gameState = false;
                        return;
                    }
                }

                //Kijken of er een beurt overgeslagen moet worden
                if (!checkForPossibleMove()) {
                    MessageBox.Show("There are no available moves, player " + (revBoard.playerAtTurn.myId + 1) + " skips a turn.");
                    revBoard.playerAtTurn = revBoard.arrPlayers[1 - revBoard.playerAtTurn.myId];
                }

                //Labels vullen
                lbl_PlayerAtTurn.Text = ("Player " + (revBoard.playerAtTurn.myId + 1) + " at turn");
                lbl_PlayerAtTurn.ForeColor = revBoard.playerAtTurn.PlayerColor;
                revBoard.countPieces();
                lbl_AmountBlue.Text = (revBoard.AmountBlue + " Blue Pieces");
                lbl_AmountRed.Text = (revBoard.AmountRed + " Red Pieces");
            }
        }

        public void drawBoard(object sender, PaintEventArgs e) {
            //Een vierkant tekenen in de maat van het bord.
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.White, 0, 0, revBoard.boardSize[1], revBoard.boardSize[0]);

            //Even randjes tekenen en de vierkantjes vullen met gekleurde rondjes
            for (int i = 0; i < revBoard.columns; i++) {
                drawVerticalBorder(i, g);
                for (int j = 0; j < revBoard.rows; j++) {
                    drawSquare(i, j, g);
                    if (i == 0) {
                        drawHorizontalBorder(j, g);
                    }
                }
            }

            //De laatste twee randen tekenen
            drawVerticalBorder(revBoard.columns, g);
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

            if (currSquare.PieceColor == Color.White && currSquare.movePossible) {
                //Als het een helprondje is, een klein circeltje tekenen
                int diameter = revBoard.squareSize / 2;
                int offsetX = (i + 1) * revBoard.borderWidth - 1 + diameter / 2;
                int offsetY = (j + 1) * revBoard.borderWidth - 1 + diameter / 2;
                g.DrawEllipse(Pens.Black, revBoard.squareSize * i + offsetX, revBoard.squareSize * j + offsetY, diameter, diameter);
            } else {
                //Gevulde cirkel tekenen
                Brush b = new SolidBrush(currSquare.PieceColor);
                int diameter = revBoard.squareSize;
                int offsetX = (i + 1) * revBoard.borderWidth - 1;
                int offsetY = (j + 1) * revBoard.borderWidth - 1;
                g.FillEllipse(b, revBoard.squareSize * i + offsetX, revBoard.squareSize * j + offsetY, diameter, diameter);
            }
        }

        private void btn_Help_Click(object sender, EventArgs e) {
            //Togglen tussen help aan en uit
            if (helpEnabled) {
                helpClear();
                helpEnabled = false;
            } else {
                helpCalculate();
                helpEnabled = true;
            }
        }

        private bool checkForPossibleMove() {
            //Kijken of de huidige speler nog beschikbare zetten heeft aan hand van de helper.
            helpCalculate();
            bool movePossible = false; 
            for (int i = 0; i < revBoard.columns; i++) {
                for (int j = 0; j < revBoard.rows; j++) {
                    if (revBoard.arrSquares[i, j].movePossible) {
                        
                        movePossible = true;
                    }
                }
            }

            //Als de helper niet aanstond, gauw die helper-rondjes weer van het bord vegen.
            if (!helpEnabled) {
                helpClear();
            }
            Invalidate();

            return movePossible;
        }

        private void helpCalculate() {
            helpClear();
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

        private void btn_NewGame_Click(object sender, EventArgs e) {
            //Getallen uit form halen en spel initializeren
            if ((nud_Rows.Value % 2 == 0) && (nud_Columns.Value % 2 == 0)) {
                revBoard = new Board((int)nud_Rows.Value, (int)nud_Columns.Value, this);
                revBoard.arrPlayers[0] = new Player(Color.Blue, 0);
                revBoard.arrPlayers[1] = new Player(Color.Red, 1);
                if (helpEnabled) helpCalculate();
                Invalidate();
            } else {
                MessageBox.Show("Hey, ho, be careful, the rows and columns in the board should be even!", "Wow wow wow hey!");
            }
        }
    }

    public class Board {
        public int AmountBlue, AmountRed;
        public int rows, columns;
        public int squareSize = 25;
        public int borderWidth = 4;
        public int[] boardSize = new int[] { 0, 0 };
        int[,][] directionCoeffs = new int[3, 3][];
        public int checkMoveIterations = 0;

        public Player[] arrPlayers = new Player[2];
        public Player playerAtTurn;
        public bool IsRowsEven; 

        public Square[,] arrSquares;

        public bool gameState = true; //true==running, false==ended

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
            for (int i = 0; i < columns; i++) {
                for (int j = 0; j < rows; j++) {
                    arrSquares[i, j] = new Square(Color.White);
                }
            }

            //Starting squares.
            arrSquares[(columns / 2) - 1, (rows / 2) - 1].PieceColor = arrPlayers[0].PlayerColor;
            arrSquares[(columns / 2), (rows / 2)].PieceColor = arrPlayers[0].PlayerColor;
            arrSquares[(columns / 2) - 1, (rows / 2)].PieceColor = arrPlayers[1].PlayerColor;
            arrSquares[(columns / 2), (rows / 2) - 1].PieceColor = arrPlayers[1].PlayerColor;
        }

        public Boolean isOpponentColor(int i, int j) {
            //Kijk of een square de kleur van de tegenstander heeft EN NIET WIT IS!
            Color squareColor = getPieceColorFromBoard(i, j);
            Color playerColor = this.playerAtTurn.PlayerColor;
            return (squareColor != playerColor) && (squareColor != Color.White);
        }

        public Color getPieceColorFromBoard(int i, int j) {
            //Als het vakje bestaat, geef de kleur terug
            if ((!(i < 0 || i >= columns)) && (!(j < 0 || j >= rows))) {
                return arrSquares[i, j].PieceColor;
            } else {
                return Color.White;
            }
        }

        public bool checkDirection(int i, int j, int k, int l) {
            //Kijken of deze richting de juiste combinatie van kleurtjes bevat
            int[] directionCoeff = this.directionCoeffs[k, l];
            if (isOpponentColor(i + directionCoeff[0], j + directionCoeff[1])) {
                //Recursie
                return checkDirection(i + directionCoeff[0], j + directionCoeff[1], k, l);
            } else if (getPieceColorFromBoard(i + directionCoeff[0], j + directionCoeff[1]) == Color.White) {
                //Witte steen gevonden voordat de spelerkleur gevonden is.
                return false;
            } else {
                //We hebben de speler kleur weer gevonden, dus de move is goed.
                return true;
            }
        }

        public void changeSquareColor(int i, int j, int k, int l) {
            //Vakjeskleur in een pad aanpassen
            int[] directionCoeff = this.directionCoeffs[k, l];
            if (isOpponentColor(i + directionCoeff[0], j + directionCoeff[1])) {
                this.arrSquares[i + directionCoeff[0], j + directionCoeff[1]].PieceColor = this.playerAtTurn.PlayerColor;
                //recursie
                changeSquareColor(i + directionCoeff[0], j + directionCoeff[1], k, l);
            } else {
                //We hebben de speler kleur weer gevonden, dus het veranderen is ten einde.
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

        public void checkMove(int i, int j, bool changeGame) {
            for (int k = 0; k < 3; k++) {
                for (int l = 0; l < 3; l++) {
                    int[] directionCoeff = this.directionCoeffs[k, l];
                    if (isOpponentColor(i + directionCoeff[0], j + directionCoeff[1])) {
                        //Oke, aangrenzend aan de plek die gevuld gaat worden is een opponent-vakje
                        if (checkDirection(i + directionCoeff[0], j + directionCoeff[1], k, l)) {
                            //Oke, het pad bevat oppontent color en eindigt met de players color, de move is valid.
                            //Nu alle stenen in het pad omdraaien.
                            if (isOpponentColor(i + directionCoeff[0], j + directionCoeff[1])) {
                                //changeGame=false als de functie gebruikt wordt om de helpervakjes te bepalen.
                                if (changeGame) {
                                    this.arrSquares[i, j].PieceColor = this.playerAtTurn.PlayerColor;
                                    changeSquareColor(i, j, k, l);
                                    checkMove(i, j, changeGame);

                                    //Als we voor de eerste keer een richting goedgekeurd hebben, dan is de zet in ieder geval gelukt, en moet dus de speler gewisseld worden!
                                    if (this.checkMoveIterations == 0) {
                                        this.playerAtTurn = this.arrPlayers[1 - this.playerAtTurn.myId];
                                    }

                                    this.checkMoveIterations++;
                                    return;
                                } else {
                                    this.arrSquares[i, j].movePossible = true;
                                    setMovePossible(i, j, k, l);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void countPieces() {
            this.AmountBlue = this.AmountRed = 0;

            for (int p = 0; p < this.columns; p++) {
                for (int q = 0; q < this.rows; q++) {
                    if (this.arrSquares[p, q].PieceColor == this.arrPlayers[0].PlayerColor)
                        this.AmountBlue++;
                    if (this.arrSquares[p, q].PieceColor == this.arrPlayers[1].PlayerColor)
                        this.AmountRed++;
                }
            }
        }

        public bool isWinner(Player p) {
            bool playerWon = true;
            for (int i = 0; i < this.columns; i++) {
                for (int j = 0; j < this.rows; j++) {
                    if (this.arrSquares[i, j].PieceColor == this.arrPlayers[1 - p.myId].PlayerColor)
                        playerWon = false;
                }
            }
            return playerWon;
        }

        public bool isBoardFull() {
            bool boardFull = true;
            for (int i = 0; i < this.columns; i++) {
                for (int j = 0; j < this.rows; j++) {
                    if (this.arrSquares[i, j].PieceColor == Color.White)
                        boardFull = false;
                }
            }
            return boardFull;
        }
    }

    public class Square {
        public Color PieceColor;
        public bool movePossible;

        public Square(Color PieceColor) {
            this.PieceColor = PieceColor;
            this.movePossible = false;
        }
    }

    public class Player {
        public Color PlayerColor;
        public int myId;

        public Player(Color PlayerColor, int t_Id) {
            this.PlayerColor = PlayerColor;
            myId = t_Id;
        }
    }
}




