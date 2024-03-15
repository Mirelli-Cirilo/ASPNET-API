namespace GameStoreApi;

public record class UpdateGameDTO
(
    [Required][StringLength(50)]string Name, 
    [Required][StringLength(50)]string Genre,
    [Range(1, 100)]decimal Price,
    DateOnly ReleaseDate);  