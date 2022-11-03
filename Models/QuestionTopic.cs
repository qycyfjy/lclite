namespace LeetcodeLite.Models;

public class QuestionTopic {
    public int QuestionID {get; set;}
    public int TopicID {get; set;}

    public Question Question {get; set;}
    public Topic Topic {get; set;}
}