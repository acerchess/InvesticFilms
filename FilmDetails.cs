using System.Collections.Generic;

namespace InvesticFilms
{
    public class FilmDetails
    {
        public string Film { get; set; }
        public List<string> Characters { get; set; } = new List<string>();

        public int TotalCharacters
        {
            get { return Characters.Count; }
        }
    }


}
