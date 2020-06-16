using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PayCompute.Entity;

namespace PayCompute.Models
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("tag-name")]
    public class EmployeeDetailViewModel : TagHelper
    {
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
    
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DateJoined { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string NationalInsuranceNo { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public StudentLoan StudentLoan { get; set; }
        public UnionMember UnionMember { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
    }
}
