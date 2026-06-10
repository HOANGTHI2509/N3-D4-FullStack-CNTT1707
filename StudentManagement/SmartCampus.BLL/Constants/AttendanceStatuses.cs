namespace SmartCampus.BLL.Constants;

public static class AttendanceStatuses
{
    public const string Present = "Present";
    public const string Absent = "Absent";
    public const string Late = "Late";
    public const string Excused = "Excused";
    public const string PresentVi = "Co mat";
    public const string AbsentVi = "Vang";
    public const string LateVi = "Di muon";
    public const string ExcusedVi = "Co phep";

    public static readonly IReadOnlySet<string> Allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        Present,
        Absent,
        Late,
        Excused,
        PresentVi,
        AbsentVi,
        LateVi,
        ExcusedVi
    };

    public static bool IsAttended(string status)
    {
        var normalizedStatus = status.Trim();

        return string.Equals(normalizedStatus, Present, StringComparison.OrdinalIgnoreCase)
            || string.Equals(normalizedStatus, Late, StringComparison.OrdinalIgnoreCase)
            || string.Equals(normalizedStatus, Excused, StringComparison.OrdinalIgnoreCase)
            || string.Equals(normalizedStatus, PresentVi, StringComparison.OrdinalIgnoreCase)
            || string.Equals(normalizedStatus, LateVi, StringComparison.OrdinalIgnoreCase)
            || string.Equals(normalizedStatus, ExcusedVi, StringComparison.OrdinalIgnoreCase);
    }
}
