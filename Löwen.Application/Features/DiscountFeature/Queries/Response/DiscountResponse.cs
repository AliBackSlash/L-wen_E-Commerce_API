using Löwen.Domain.Layer_Dtos.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Löwen.Application.Features.DiscountFeature.Queries.Response
{
    public class DiscountResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; } 
        public DiscountType DiscountType { get; set; }
        public decimal? DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

    }
}
