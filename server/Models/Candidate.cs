using System.ComponentModel.DataAnnotations;
using Features.Models;

namespace Features.Models
{
    public class Candidate
    {
        
        [Key]
        public int CandidateId { get; set; }
        public int UserId { get; set; }
        public User CandidateUser { get; set; }
        public int Score { get; set; }
    }
}