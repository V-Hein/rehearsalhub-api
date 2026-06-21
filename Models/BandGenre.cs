public class BandGenre
{
    public int BandId { get; set; }
    public Band Band { get; set; } = null!;
    
    public int GenreId { get; set; } 
    public Genre Genre { get; set; } = null!;
}