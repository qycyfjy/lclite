using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using LeetcodeLite;
using LeetcodeLite.Data;

var db = new LeetcodeLiteContext();

var questions = Dump.GetQuestions("raw/all_detail.json");
var details = Dump.GetQuestionDetails("raw/all.json");
var detailsMap = details.ToDictionary(detail => detail.QuestionID);

foreach (var question in questions)
{
    var id = question.QuestionID;
    LeetcodeLite.Models.Question questionEntity = new();

    LeetcodeLite.Models.QuestionDetail detail = new();
    if (detailsMap.ContainsKey(id))
    {
        var rawDetail = detailsMap[id];
        var acRate = rawDetail.AcRate;
        var acRateS = acRate.AsSpan().Slice(0, acRate.Length - 1);
        var acRateF = 0f;
        float.TryParse(acRateS, out acRateF);
        detail.AcRate = acRateF;
        detail.Frequency = rawDetail.Frequency;
    }
    questionEntity.QuestionDetail = detail;
    questionEntity.ID = int.Parse(question.QuestionID);
    questionEntity.Title = question.Title;
    questionEntity.SlugTitle = question.SlugTitle;
    questionEntity.TranslatedTitle = question.TranslatedTitle;
    questionEntity.Difficulty = question.Difficulty;
    db.Add(questionEntity);

    foreach (var rawTopic in question.Topics)
    {
        LeetcodeLite.Models.Topic topic;
        try
        {
            topic = db.Topics.First(t => t.SlugName == rawTopic.SlugName); ;
        }
        catch (Exception)
        {
            topic = new LeetcodeLite.Models.Topic();
            topic.Name = rawTopic.Name;
            topic.SlugName = rawTopic.SlugName;
            topic.TranslatedName = rawTopic.TranslatedName;
            db.Add(topic);
        }
        var questionTopic = new LeetcodeLite.Models.QuestionTopic
        {
            Question = questionEntity,
            Topic = topic,
        };
        db.Add(questionTopic);
    }
}

db.SaveChanges();

foreach (var fname in Directory.GetFiles("raw/companies/"))
{
    var companyQuestions = Dump.GetCompanyQuestions(fname);
    var company = new LeetcodeLite.Models.Company { Name = Path.GetFileNameWithoutExtension(fname) };
    db.Add(company);
    foreach (var q in companyQuestions)
    {
        var qid = int.Parse(q.QuestionId);
        try
        {
            var entry = db.Questions.First(e => e.ID == qid);
            var companyQuestion = new LeetcodeLite.Models.CompanyQuestion
            {
                Question = entry,
                Company = company,
            };
            db.Add(companyQuestion);
        }
        catch (Exception) { }
    }
}

db.SaveChanges();


LeetcodeLite.Models.Difficulty convert(string difficulty)
{
    switch (difficulty)
    {
        case "Easy":
            return LeetcodeLite.Models.Difficulty.Easy;
        case "Medium":
            return LeetcodeLite.Models.Difficulty.Medium;
        case "Hard":
            return LeetcodeLite.Models.Difficulty.Hard;
        default:
            return LeetcodeLite.Models.Difficulty.Medium;
    }
}