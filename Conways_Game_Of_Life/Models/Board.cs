namespace Conways_Game_Of_Life.Models
{
    public class Board
    {
        public Guid Id { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public bool[,]? Grid { get; set; }
    }
}
