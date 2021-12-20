namespace AdventOfCode2021.Seventeen;

public class ProbeShot
{
    private readonly TargetRange targetRange;

    public ProbeShot(int xVelocity, int yVelocity, TargetRange target)
    {
        XVelocity = xVelocity;
        YVelocity = yVelocity;
        targetRange = target;

        CurrentX = 0;
        CurrentY = 0;
    }

    public int XVelocity { get; set; }

    public int YVelocity { get; set; }

    public int CurrentX { get; set; }

    public int CurrentY { get; set; }

    public int MaxY { get; set; }

    public bool DidItHit()
    {
        do
        {
            CurrentX += XVelocity;
            CurrentY += YVelocity;

            MaxY = Math.Max(MaxY, CurrentY);

            XVelocity += XVelocity > 0 ? -1 : XVelocity == 0 ? 0 : 1;
            YVelocity--;

            if (targetRange.InTargetRange(CurrentX, CurrentY))
                return true;
        } while (!targetRange.PastTargetRange(CurrentX, CurrentY));

        return false;
    }
}