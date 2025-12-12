using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmsLibrary.Models;

public partial class Frame
{
    public int FrameId { get; set; }
    [Display(Name = "Film")]
    public int FilmId { get; set; }

    public string? FileName { get; set; } = null!;

    public virtual Film? Film { get; set; } = null!;
}
