using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vimachem.Models.Domain
{
    public class AuditLog
    {
        
        public int AuditLogId { get; set; }
        public string? ActionType { get; set; } 
        public string? EntityType { get; set; } 
        public string? EntityId { get; set; }   
        public DateTime Timestamp { get; set; }
        public string? Changes { get; set; }    
        public string? UserId { get; set; }
        public string? ErrorMessage { get; set;}

    }
}
