//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LanguageSchool.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class ServiceHistoryAttachment
    {
        public int IdAttachment { get; set; }
        public int IdServiceHistory { get; set; }
        public string Attachment { get; set; }
    
        public virtual ServiceHistory ServiceHistory { get; set; }
    }
}