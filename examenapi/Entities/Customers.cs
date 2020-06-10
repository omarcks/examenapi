using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace examenapi.Entities
{
    [Table("Customer", Schema = "SalesLT")]
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public bool NameStyle { get; set; }

        [MaxLength(8)]
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string CompanyName { get; set; }
        public string SalesPerson { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
