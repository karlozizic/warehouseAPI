﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebApplication1.Database.Entities;

public class Warehouse
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string Name { get; set; }
    /*[Required]*/
    public Location? Location { get; set; } 
    
    public string PhoneNumber { get; set; }
    public string Code {get; set; }
    public DateTime DateTimeCreatedUtc { get; set; }
    public bool Deleted { get; set; }
    public string DefaultLanguage { get; set; }
    public DateTime? DateOpenUtc { get; set; }
    public DateTime? DateClosedUtc { get; set; }
    public bool IsPayoutLockedForOtherCostCenter { get; set; }
    public FranchiseUser? OperatorUser { get; set; }
    //promijeni List u IQueryable - kako bi se optimizirali upiti prema bazi podataka 
    public List<Item>? Items { get; set; }
    
    [Required]
    public Guid TenantId { get; set; }

    public Warehouse(Guid id, string name, string phoneNumber, string code, DateTime dateTimeCreatedUtc, bool deleted,
        string defaultLanguage, DateTime? dateOpenUtc, DateTime? dateClosedUtc, bool isPayoutLockedForOtherCostCenter)

    {
        this.Id = id;
        this.Name = name;
        //this.location = location;
        this.Location = null; 
        this.PhoneNumber = phoneNumber;
        this.Code = code;
        this.DateTimeCreatedUtc = dateTimeCreatedUtc;
        this.Deleted = deleted;
        this.DefaultLanguage = defaultLanguage;
        this.DateOpenUtc = dateOpenUtc;
        this.DateClosedUtc = dateClosedUtc;
        this.IsPayoutLockedForOtherCostCenter = isPayoutLockedForOtherCostCenter;
        this.OperatorUser = null;
        this.Items = null;
    }
    
    public Warehouse(string name, string phoneNumber, string code, DateTime dateTimeCreatedUtc, bool deleted,
        string defaultLanguage, DateTime? dateOpenUtc, DateTime? dateClosedUtc, bool isPayoutLockedForOtherCostCenter)
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        //this.location = location;
        this.Location = null; 
        this.PhoneNumber = phoneNumber;
        this.Code = code;
        this.DateTimeCreatedUtc = dateTimeCreatedUtc;
        this.Deleted = deleted;
        this.DefaultLanguage = defaultLanguage;
        this.DateOpenUtc = dateOpenUtc;
        this.DateClosedUtc = dateClosedUtc;
        this.IsPayoutLockedForOtherCostCenter = isPayoutLockedForOtherCostCenter;
        this.OperatorUser = null;
        this.Items = null;
    }

    public Warehouse()
    {
    }

}