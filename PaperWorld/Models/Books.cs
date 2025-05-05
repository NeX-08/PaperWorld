using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Books
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string ISBN { get; set; } = string.Empty;

    [Required]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public string Author { get; set; } = string.Empty;

    public string? Publisher { get; set; }

    public string? Language { get; set; }

    public string? ImageUrl { get; set; }

    public string? Genre { get; set; }

    public DateTime? PublicationDate { get; set; } 

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public int Stock { get; set; } = 0;

    public bool IsOnSale { get; set; } = false;

    [Column(TypeName = "decimal(18,2)")]
    public decimal? DiscountPrice { get; set; } 

    public DateTime? SaleStart { get; set; } 
    public DateTime? SaleEnd { get; set; }   
}