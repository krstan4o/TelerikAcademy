using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using CGWeb;
using CGWeb.Games;

namespace SinkBreaker
{
    /// <summary>
    /// A basic structure layout of a CGWeb game. 
    /// Remember to register your game in the global.asax if you change this class`s name.
    /// </summary>
    public class SinkBreakerGame : Game
    {
        Dictionary<User, Dictionary<string, Cell>> playerGrids;

        string[] figureTypes = new string[] { "ninetile", "plus", "hline", "vline", "angle-ur", "angle-dr", "angle-dl", "angle-ul" };
        public Figure[] Figures { get; protected set; }
        short gridWidth;
        short gridHeight;
        byte cellSize;

        #region Hide for now
        /// <summary>
        /// Array to hold all the players in the game.
        /// </summary>
        private User[] players;

        /// <summary>
        /// Every constructor your game has should call the base constructor of <see cref="CGWeb.Games.Game"/> information
        /// about the its name and the required number of players.
        /// </summary>
        public SinkBreakerGame() :
            base(2) // number of players needed for the game to function
        {

        }


        /// <summary>
        /// This method checks whether the specified game is in the same mode as the current one. Used by the matchmaking system.
        /// </summary>
        public override bool IsSameModeAs(Game game)
        {
            return
                this.Name == game.Name &&
                this.RequiredPlayers == game.RequiredPlayers;
        }

        /// <summary>
        /// This method should return true if the specified user can act in the current moment and false otherwise.
        /// </summary>
        public override bool IsUserOnTheMove(CGWeb.User user)
        {
            return true;
        }

        /// <summary>
        /// The gameroom hosting this game will call the following method whenever a user disconnects. It`s up to you 
        /// to decide whether the game can continue without the specified user or it should end.
        /// </summary>
        protected override void OnUserDisconnect(CGWeb.User user)
        {
            OnGameEnded(EventArgs.Empty);
        }
        #endregion

        /// <summary>
        /// The gameroom hosting this game will call followin method to start the game. Use 
        /// </summary>
        /// <param name="players"></param>
        protected override void Start(User[] players)
        {
            playerGrids = new Dictionary<User, Dictionary<string, Cell>>();
            cellSize = 20;
            gridWidth = 500;
            gridHeight = 360;
            this.players = players;
            Dictionary<string, Cell> gridPlayerOne = new Dictionary<string, Cell>();
            Dictionary<string, Cell> gridPlayerTwo = new Dictionary<string, Cell>();
            Figures = GenerateFigures();

            InitGameField(gridPlayerOne);
            InitGameField(gridPlayerTwo);

            ConnectCells(gridPlayerOne);
            ConnectCells(gridPlayerTwo);

            playerGrids.Add(players[0], gridPlayerOne);
            playerGrids.Add(players[1], gridPlayerTwo);
            PopulateFigures();
        }

        public string OtherPlayerName(User player)
        {
            string name = player.Name;
            if (players[0].Name != name)
            {
                return players[0].Name;
            }

            return players[1].Name;
        }

        public string OtherPlayerID(User player)
        {
            string cID = player.ConnectionId;
            if (players[0].ConnectionId != cID)
            {
                return players[0].ConnectionId;
            }

            return players[1].ConnectionId;
        }

        public MoveData PlaceFigure(User currentPlayer, string figureType, int x, int y)
        {
            sbyte[] overlapsChart = new sbyte[0];
            bool success = false;
            bool levelCompleted = false;
            string msg = string.Empty;

            try
            {
                HandleFigure(figureType, x, y, playerGrids[currentPlayer], false);
                Dictionary<string, Cell> vals = playerGrids[currentPlayer];
                List<Cell> currentUserCells = new List<Cell>();

                foreach (var item in vals)
                {
                    currentUserCells.Add(item.Value);
                }


                KeyValuePair<sbyte[], bool> levelState = CheckOverlapsState(currentUserCells.ToArray());

                overlapsChart = levelState.Key;
                levelCompleted = levelState.Value;
                success = true;
            }
            catch (Exception e)
            {
                msg = e.ToString();
            }

            return new MoveData { Success = success, Overlaps = overlapsChart, Message = msg, GameEnded= levelCompleted };
        }

        public MoveData TakeFigure(User currentPlayer, string figureType, int x, int y)
        {
            bool success = false;
            string msg = string.Empty;

            try
            {
                HandleFigure(figureType, x, y, playerGrids[currentPlayer], true);
                success = true;
            }
            catch (Exception e)
            {
                msg = e.ToString();
            }

            return new MoveData { Success = success, Message = msg };
        }

        private KeyValuePair<sbyte[], bool> CheckOverlapsState(Cell[] allCells)
        {
            bool levelCompleted = true;

            List<sbyte> overlapsChart = new List<sbyte>();
            foreach (Cell cell in allCells)
            {
                sbyte currentCellTilesCount = cell.ResidentTilesCount;
                if (currentCellTilesCount > 1)
                {
                    levelCompleted = false;
                }

                overlapsChart.Add(currentCellTilesCount);
            }

            return new KeyValuePair<sbyte[],bool>(overlapsChart.ToArray(), levelCompleted);
        }

        private void PopulateFigures()
        {
            int figuresLen = Figures.Length;
            for (int i = 0; i < figuresLen; i++)
            {
                Figure currentFigure = Figures[i];
                HandleFigure(currentFigure.type, currentFigure.x, currentFigure.y, playerGrids[players[0]], false);
                HandleFigure(currentFigure.type, currentFigure.x, currentFigure.y, playerGrids[players[1]], false);
            }
        }

        private void HandleFigure(string figureType, int x, int y, Dictionary<string, Cell> grid, bool take)
        {
            Cell root = grid[string.Format("{0} {1}", x, y)];
            sbyte value = (sbyte)(take ? -1 : 1);

            switch (figureType)
            {
                case "ninetile":
                    HandleNinetile(root, value);
                    break;
                case "plus":
                    HandlePlus(root, value);
                    break;
                case "vline":
                    HandleVline(root, value);
                    break;
                case "hline":
                    HandleHline(root, value);
                    break;
                case "angle-ul":
                    HandleAngleUL(root, value);
                    break;
                case "angle-ur":
                    HandleAngleUR(root, value);
                    break;
                case "angle-dl":
                    HandleAngleDL(root, value);
                    break;
                case "angle-dr":
                    HandleAngleDR(root, value);
                    break;
                default:
                    break;
            }
        }

        private void HandleNinetile(Cell root, sbyte value)
        {
            root.AdjustResidents(value);
            root.TopNeighbour.AdjustResidents(value);
            root.BottomNeighbour.AdjustResidents(value);
            root.LeftNeighbour.AdjustResidents(value);
            root.RightNeighbour.AdjustResidents(value);
            root.TopLeftNeighbour.AdjustResidents(value);
            root.TopRightNeighbour.AdjustResidents(value);
            root.BottomLeftNeighbour.AdjustResidents(value);
            root.BottomRightNeighbour.AdjustResidents(value);
        }

        private void HandlePlus(Cell root, sbyte value)
        {
            root.AdjustResidents(value);
            root.TopNeighbour.AdjustResidents(value);
            root.BottomNeighbour.AdjustResidents(value);
            root.LeftNeighbour.AdjustResidents(value);
            root.RightNeighbour.AdjustResidents(value);
        }

        private void HandleVline(Cell root, sbyte value)
        {
            root.AdjustResidents(value);
            root.TopNeighbour.AdjustResidents(value);
            root.BottomNeighbour.AdjustResidents(value);
        }

        private void HandleHline(Cell root, sbyte value)
        {
            root.AdjustResidents(value);
            root.LeftNeighbour.AdjustResidents(value);
            root.RightNeighbour.AdjustResidents(value);
        }

        private void HandleAngleUL(Cell root, sbyte value)
        {
            root.AdjustResidents(value);
            root.TopNeighbour.AdjustResidents(value);
            root.LeftNeighbour.AdjustResidents(value);
        }

        private void HandleAngleUR(Cell root, sbyte value)
        {
            root.AdjustResidents(value);
            root.TopNeighbour.AdjustResidents(value);
            root.RightNeighbour.AdjustResidents(value);
        }

        private void HandleAngleDL(Cell root, sbyte value)
        {
            root.AdjustResidents(value);
            root.BottomNeighbour.AdjustResidents(value);
            root.LeftNeighbour.AdjustResidents(value);
        }

        private void HandleAngleDR(Cell root, sbyte value)
        {
            root.AdjustResidents(value);
            root.BottomNeighbour.AdjustResidents(value);
            root.RightNeighbour.AdjustResidents(value);
        }

        private void InitGameField(Dictionary<string, Cell> grid)
        {
            for (int r = 0; r < gridHeight; r += cellSize)
            {
                for (int c = 0; c < gridWidth; c += cellSize)
                {
                    Cell newCell = new Cell(c, r);
                    grid.Add(string.Format("{0} {1}", c, r), newCell);
                }
            }
        }

        private void ConnectCells(Dictionary<string, Cell> grid)
        {
            for (int r = 0; r < gridHeight; r += cellSize)
            {
                for (int c = 0; c < gridWidth; c += cellSize)
                {
                    Cell currentCell = grid[string.Format("{0} {1}", c, r)];
                    bool up = false;
                    bool down = false;
                    bool left = false;
                    bool right = false;

                    if (r > 0)
                    {
                        up = true;
                        currentCell.TopNeighbour = grid[string.Format("{0} {1}", c, r - cellSize)];
                    }
                    if (c > 0)
                    {
                        left = true;
                        currentCell.LeftNeighbour = grid[string.Format("{0} {1}", c - cellSize, r)];
                    }
                    if (r < gridHeight - cellSize)
                    {
                        down = true;
                        currentCell.BottomNeighbour = grid[string.Format("{0} {1}", c, r + cellSize)];
                    }
                    if (c < gridWidth - cellSize)
                    {
                        right = true;
                        currentCell.RightNeighbour = grid[string.Format("{0} {1}", c + cellSize, r)];
                    }
                    if (up)
                    {
                        if (left)
                        {
                            currentCell.TopLeftNeighbour = grid[string.Format("{0} {1}", c - cellSize, r - cellSize)];
                        }
                        if (right)
                        {
                            currentCell.TopRightNeighbour = grid[string.Format("{0} {1}", c + cellSize, r - cellSize)];
                        }
                    }
                    if (down)
                    {
                        if (left)
                        {
                            currentCell.BottomLeftNeighbour = grid[string.Format("{0} {1}", c - cellSize, r + cellSize)];
                        }
                        if (right)
                        {
                            currentCell.BottomRightNeighbour = grid[string.Format("{0} {1}", c + cellSize, r + cellSize)];
                        }
                    }
                }
            }
        }

        private Figure[] GenerateFigures()
        {
            List<Figure> arr = new List<Figure>();
            Random rand = new Random();
            int count = 45;
            int doubleCellSize = cellSize << 1;
            
            for (var i = 0; i < count; i++)
            {
                int randomX = rand.Next(cellSize, gridWidth - doubleCellSize).RoundOff();
                int randomY = rand.Next(cellSize, gridHeight - doubleCellSize).RoundOff();

                arr.Add(new Figure { type = figureTypes[rand.Next(0, figureTypes.Length)], x = randomX, y = randomY });
            }

            return arr.ToArray();
        }
    }
}