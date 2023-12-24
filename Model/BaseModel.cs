using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace LearningManagement.Model;

public class BaseModel
{
    [Key, Column("id")]
    public int Id { get; set; }

    [Column("created_by")]
    public int CreatedBy { get; set; }

    [Column("created_at", TypeName = "TIMESTAMP")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("updated_at", TypeName = "TIMESTAMP")]
    public DateTime? UpdatedAt { get; set; }

    [Timestamp]
    public uint Ver { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }
}
