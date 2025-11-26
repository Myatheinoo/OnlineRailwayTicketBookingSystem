using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DTO.Shared
{
    public class Common
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public bool ActiveFlag { get; set; }
    }
    public class MessageModel
    {
        public static readonly string AddSuccess = "Add Successfully";
        public static readonly string UpdateSuccess = "Update Data Successfully";
        public static readonly string DeleteSuccess = "Delete Data Successfully";
        public static readonly string Success = "Successfully";
        public static readonly string UnSuccess = "Fail";
    }

}
