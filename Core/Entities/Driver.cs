using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Driver : BaseEntity
{

    public string Age { get; set; }

    public ICollection<Team> Teams { get; set; }  = new HashSet<Team>();
    public ICollection<Teamdriver> Teamdrivers { get; set; } 

}
