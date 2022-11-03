using System.Collections.Generic;

namespace LeetcodeLite.Models;

public class Company
{
    public int ID { get; set; }
    public string Name { get; set; }

    public ICollection<CompanyQuestion> Questions { get; set; }
}