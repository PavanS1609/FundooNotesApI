using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Repository_Layer.Entity
{
    public class CollaboratorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollaboratorId { get; set; }
        public string CollaboratorEmail { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        [ForeignKey("Notes")]
        public long NoteId { get; set; }
        [JsonIgnore]
        public virtual UserEntity Users { get; set; }
        [JsonIgnore]
        public virtual NoteEntity Notes { get; set; }
    }
}
