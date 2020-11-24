public class ChartJs
{
    public string Type { get; set; }

    public bool Responsive { get; set; }

    public Data Data { get; set; }

    public Options Options { get; set; }
}

public class Data
{
    public string[] labels { get; set; }

    public Dataset[] datasets { get; set; }
}

public class Dataset
{
    public string Label { get; set; }

    public int[] Data { get; set; }

    public string[] backgroundColor { get; set; }

    public string[] borderColor { get; set; }

    public int borderWidth { get; set; }
}

public class Options
{
    public Scales scales { get; set; }
}

public class Scales
{
    public Yax[] yAxes { get; set; }
}

public class Yax
{
    public Ticks ticks { get; set; }
}

public class Ticks
{
    public bool beginAtZero { get; set; }
}
