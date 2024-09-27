namespace Conways_Game_Of_Life.Models
{
    public class BoardOut
    {
        public Guid Id { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public List<List<bool>>? Grid { get; set; }
    }
}
