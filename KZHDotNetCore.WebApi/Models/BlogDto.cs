using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KZHDotNetCore.WebApi.Models;

[Table("Tbl_Blog")]
public class BlogDto
{
    [Key]
    public int BlogID { get; set; }
    public string? BlogTitle { get; set; }
    public string? BlogAuthor { get; set; }
    public string? BlogContent { get; set; }

}