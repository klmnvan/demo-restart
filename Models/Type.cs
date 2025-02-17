using System;
using System.Collections.Generic;

namespace DemoEkzZachet.Models;

public partial class Type
{
    public int Id { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<ToursType> ToursTypes { get; set; } = new List<ToursType>();
}
