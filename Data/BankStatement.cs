//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class BankStatement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BankStatement()
        {
            this.Reconciliation = new HashSet<Reconciliation>();
        }
    
        public System.Guid BankStatementID { get; set; }
        public System.Guid BankAccountID { get; set; }
        public System.Guid TransactionTypeID { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public System.Guid RegistrationUserID { get; set; }
        public System.DateTime RegistrationDate { get; set; }
        public Nullable<System.Guid> DesactivationUserID { get; set; }
        public Nullable<System.DateTime> DesactivationDate { get; set; }
        public bool IsActive { get; set; }
    
        public virtual BankAccount BankAccount { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reconciliation> Reconciliation { get; set; }
    }
}