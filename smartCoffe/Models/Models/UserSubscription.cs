using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class UserSubscription
    {
        [Key]
        public int SubscriptionID { get; set; }

        // ===================== USER =====================
        public int UserID { get; set; }

        // ===================== PLAN =====================
        public int PlanID { get; set; }

        // ===================== DATES =====================
        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; }

        // ===================== USAGE =====================
        public int RemainingCups { get; set; }

        // ===================== STATUS =====================
        public bool IsActive { get; set; } = true;

        // ===================== NAVIGATION PROPERTIES =====================

        [ForeignKey(nameof(UserID))]
        public User User { get; set; } = null!;

        [ForeignKey(nameof(PlanID))]
        public Plan Plan { get; set; } = null!;
    }
}