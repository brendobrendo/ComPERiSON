using System.Collections.Generic;

namespace Features.Models
{
    public class ComparisonSession
    {
        public List<Candidate> Candidates { get; set; }
        public int WinningBoxNumber { get; set; }
    }
}