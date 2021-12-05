namespace AdventOfCode2021.Four;

public class Square
{
    public Square(int number)
    {
        Number = number;
        IsSelected = false;
    }

    public int Number { get; set; }

    public bool IsSelected { get; set; }
}