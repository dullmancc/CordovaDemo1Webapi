//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SoTest.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class t_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_User()
        {
            this.t_Task = new HashSet<t_Task>();
        }
    
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserBirthday { get; set; }
        public string UserGender { get; set; }
        public string UserCompany { get; set; }
        public string UserdeDartment { get; set; }
        public string UserTelephone { get; set; }
        public string UserEmail { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_Task> t_Task { get; set; }
    }
}