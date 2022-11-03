using System;
using System.Collections.Generic;

namespace LeetcodeLite.Models;

[Flags]
public enum Difficulty
{
    Easy = 0x0001,
    Medium = 0x0002,
    Hard = 0x0004,
}

public class Question
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string SlugTitle { get; set; }
    public string TranslatedTitle { get; set; }
    public string Difficulty { get; set; }

    public QuestionDetail QuestionDetail { get; set; }
    public ICollection<QuestionTopic> Topics { get; set; }
    public ICollection<CompanyQuestion> Companies { get; set; }
}