using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoTest.Models
{
    public class UseModel
    {
    }
    public class UploadFile
    {
        public int ErrorCode { get; set; }
        public string Info { get; set; }
    }
    public class PostPhoto
    {
        public long panzhangid { get; set; }
        public string EmployeeId { get; set; }
    }
}