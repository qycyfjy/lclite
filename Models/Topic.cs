using System.Collections.Generic;

namespace LeetcodeLite.Models;

public class Topic
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SlugName { get; set; }
    public string TranslatedName { get; set; }

    public ICollection<QuestionTopic> Questions {get; set;}
}