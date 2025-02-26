using FinalDemo.Models.POCOs;
using ServiceStack.DataAnnotations;

public class YMU01
{
    /// <summary>
    /// Primary Key - Unique Identifier for the user
    /// </summary>
    [PrimaryKey]
    public int U01F01 { get; set; }

    /// <summary>
    /// Username of the user
    /// </summary>
    public string U01F02 { get; set; }

    /// <summary>
    /// Hashed password storage
    /// </summary>
    public string U01F03 { get; set; }

    /// <summary>
    /// Role of the user: Admin, Developer, Tester
    /// </summary>
    public string U01F04 { get; set; }

    /// <summary>
    /// Navigation property - List of assigned bugs
    /// </summary>
    public List<YMB01> U01F05 { get; set; }
}