namespace LeetcodeLite.Models;

public class CompanyQuestion
{
    public int CompanyID { get; set; }
    public int QuestionID { get; set; }

    public Question Question { get; set; }
    public Company Company { get; set; }
}