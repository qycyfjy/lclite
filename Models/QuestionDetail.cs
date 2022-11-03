namespace LeetcodeLite.Models;

public class QuestionDetail
{
    public int ID { get; set; }
    public int QuestionID { get; set; }
    public float Frequency { get; set; }
    public float AcRate { get; set; }

    public Question Question { get; set; }
}