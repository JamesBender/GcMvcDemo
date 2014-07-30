using System;

namespace FnhDemo.Data.Entities
{
    public class Track : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual int Length { get; set; }
        public virtual string Artist { get; set; }        
        public virtual CD CD { get; set; }
    }
}