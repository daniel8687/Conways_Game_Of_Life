using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Conways_Game_Of_Life.Models
{
    public class BoardIn
    {
        [DefaultValue(3)]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a Rows value bigger than {1}")]
        public int Rows { get; set; }
        [DefaultValue(3)]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a Columns value bigger than {1}")]
        public int Columns { get; set; }
        public List<List<bool>>? Grid { get; set; }
    }
}
