using System.Text.Json.Serialization;

namespace Conways_Game_Of_Life.Models
{
    public class Board
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        [JsonPropertyName("Grid")]
        public List<List<bool>> GridEnumerable { get; set; }
        [JsonIgnore]
        public bool[,] Grid { get; set; }
    }
}
