using System.ComponentModel.DataAnnotations;

namespace Features.Models
{
    public class Spotify
    {
        [Key]
        public int SpotifyId { get; set; }

        public string Artist { get; set; }
        public string ArtistImage { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}