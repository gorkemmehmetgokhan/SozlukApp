//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SozlukApp.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Header
    {
        public int HeaderId { get; set; }
        public Nullable<int> TopicId { get; set; }
        public string Name { get; set; }
        public Nullable<int> EntryCount { get; set; }
    }
}