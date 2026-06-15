using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class CoffeeUsage
    {
        [Key]
        public int Id { get; set; }

        // 🔥 المستخدم المرتبط
        public int UserId { get; set; }

        // ☕ عدد الأكواب المستخدمة في الشهر الحالي
        public int UsedCups { get; set; } = 0;

        // 📦 الحد الشهري (يأتي من الباقة)
        public int MonthlyLimit { get; set; }

        // 📅 بداية الشهر (لتجميع الاستخدام شهرياً)
        public DateTime Month { get; set; }

        // 🔗 العلاقة مع المستخدم
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
    }
}