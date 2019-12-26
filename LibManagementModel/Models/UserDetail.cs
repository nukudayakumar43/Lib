using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibManagementModel
{
    public class UserDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserDetailId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter UserName")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter Password")]        
        public string Password { get; set; }

        public string EmailID { get; set; }

        public DateTime CreatedDateTime { get; set; }

    }
}
