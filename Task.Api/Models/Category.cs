#nullable enable
using System.ComponentModel.DataAnnotations;

namespace Task.Api.Models;

public class Category
{
    public Guid Id { get; set; }

    [StringLength(100)]
    public string? CategoryName { get; set; }
    
    public int RangeFrom { get; set; }
    
    public int RangeTo { get; set; }
    
    public int Percent { get; set; }
    
}