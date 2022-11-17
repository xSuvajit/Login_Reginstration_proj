//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Login_Reginstration_proj.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Can't be blank"), DisplayName("First Name")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Can't be blank"), DisplayName("Last Name")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Can't be blank"), DisplayName("User Name")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Can't be blank"), DisplayName("Password"), DataType(DataType.Password)]
        public string SecretId { get; set; }

        [Required(ErrorMessage = "Can't be blank"), DisplayName("Contact Number")]
        public long contact { get; set; }
        public string createdBy { get; set; }
        public System.DateTime created { get; set; }
        public string modifiedBy { get; set; }
        public System.DateTime modified { get; set; }
        public Nullable<System.DateTime> LastLoginTime { get; set; }
    }
}
