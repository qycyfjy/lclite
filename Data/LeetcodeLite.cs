using System.IO;
using Microsoft.EntityFrameworkCore;

namespace LeetcodeLite.Data;

public class LeetcodeLiteContext : DbContext
{
    public DbSet<Models.Question> Questions { get; set; }
    public DbSet<Models.Topic> Topics { get; set; }
    public DbSet<Models.Company> Companies { get; set; }
    public DbSet<Models.QuestionTopic> QuestionTopics { get; set; }
    public DbSet<Models.CompanyQuestion> CompanyQuestions { get; set; }

    private string DbPath;
    public LeetcodeLiteContext()
    {
        var folder = Path.GetTempPath();
        DbPath = Path.Join(folder, "0", "leetcode.sqlite");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Models.QuestionTopic>().HasKey(i => new { i.QuestionID, i.TopicID });
        modelBuilder.Entity<Models.CompanyQuestion>().HasKey(i => new { i.QuestionID, i.CompanyID });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}