using System;
using System.Collections.Generic;

namespace DemoEkzZachet.Models;

public partial class Tour
{
    public int Id { get; set; }

    public int TicketCount { get; set; } = 0;

    public string Name { get; set; } = "";

    public string? Description { get; set; } = "";

    public int Price { get; set; } = 0;

    public int IsActual { get; set; } = 1;

    public string? ImagePreview { get; set; } = "";

    public virtual ICollection<ToursType> ToursTypes { get; set; } = new List<ToursType>();
}
