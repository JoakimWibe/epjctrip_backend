using System.ComponentModel.DataAnnotations;

namespace epjctrip_backend.Models;

public class PlanRequest
{
    [Required]
    public string Name { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public string Destionation { get; set; }
    
    public string? Departure { get; set; }
    
    public List<Activity>? Activities { get; set; }
    
    public int Participants { get; set; }
    
    public int? Cost { get; set; } 
}