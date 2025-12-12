using System;
using System.Collections.Generic;

namespace FilmsLibrary.Models;

public partial class Film
{
    public int FilmId { get; set; }

    public string Name { get; set; } = null!;

    public short Duration { get; set; }

    public short ReleaseYear { get; set; }

    public string? Description { get; set; }

    public byte[]? Poster { get; set; }

    public string? AgeRating { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Frame> Frames { get; set; } = new List<Frame>();
}
