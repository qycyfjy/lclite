using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LeetcodeLite;

public readonly struct Topic
{
    [JsonPropertyName("name")]
    public string Name { get; }
    [JsonPropertyName("slug")]
    public string SlugName { get; }
    [JsonPropertyName("translatedName")]
    public string TranslatedName { get; }

    [JsonConstructor]
    public Topic(string name, string slugName, string translatedName) =>
        (Name, SlugName, TranslatedName) = (name, slugName, translatedName);
}

public readonly struct Question
{
    [JsonPropertyName("questionId")]
    public string QuestionID { get; }
    [JsonPropertyName("title")]
    public string Title { get; }
    [JsonPropertyName("titleSlug")]
    public string SlugTitle { get; }
    [JsonPropertyName("translatedTitle")]
    public string TranslatedTitle { get; }
    [JsonPropertyName("difficulty")]
    public string Difficulty { get; }
    [JsonPropertyName("topicTags")]
    public List<Topic> Topics { get; }

    [JsonConstructor]
    public Question(string questionID, string title, string slugTitle, string translatedTitle, string difficulty, List<Topic> topics)
    {
        // var questionIDI = int.Parse(questionID);
        (QuestionID, Title, SlugTitle, TranslatedTitle, Difficulty, Topics) = (questionID, title, slugTitle, translatedTitle, difficulty, topics);
    }
}

public readonly struct QuestionDetail
{
    [JsonPropertyName("questionId")]
    public string QuestionID { get; }
    [JsonPropertyName("frequency")]
    public float Frequency { get; }
    [JsonPropertyName("acRate")]
    public string AcRate { get; }

    [JsonConstructor]
    public QuestionDetail(string questionID, float frequency, string acRate)
    {
        // var questionIDI = int.Parse(questionID);
        // var acRateS = acRate.AsSpan().Slice(0, acRate.Length - 1);
        // var acRateF = 0f;
        // float.TryParse(acRateS, out acRateF);
        (QuestionID, Frequency, AcRate) = (questionID, frequency, acRate);
    }
}

public readonly struct QuestionDetailLevel1
{
    [JsonPropertyName("problemsetQuestionsDynamicInfos")]
    public List<QuestionDetail> QuestionDetails { get; }

    [JsonConstructor]
    public QuestionDetailLevel1(List<QuestionDetail> questionDetails) =>
        QuestionDetails = questionDetails;
}

public readonly struct QuestionDetailLevel0
{
    [JsonPropertyName("data")]
    public QuestionDetailLevel1 Data { get; }

    [JsonConstructor]
    public QuestionDetailLevel0(QuestionDetailLevel1 data) => Data = data;
}

public readonly struct CompanyQuestion
{
    [JsonPropertyName("questionId")]
    public string QuestionId { get; }

    [JsonConstructor]
    public CompanyQuestion(string questionId) => QuestionId = questionId;
}

public readonly struct CompanyQuestions
{
    [JsonPropertyName("questions")]
    public List<CompanyQuestion> Questions { get; }

    [JsonConstructor]
    public CompanyQuestions(List<CompanyQuestion> questions) => Questions = questions;
}

public readonly struct CompanyLevel1
{
    [JsonPropertyName("companyTag")]
    public CompanyQuestions Data { get; }

    [JsonConstructor]
    public CompanyLevel1(CompanyQuestions data) => Data = data;
}

public readonly struct CompanyLevel0
{
    [JsonPropertyName("data")]
    public CompanyLevel1 Data { get; }

    [JsonConstructor]
    public CompanyLevel0(CompanyLevel1 data) => Data = data;
}

public class Dump
{
    public static List<Question> GetQuestions(string fpath)
    {
        using var fs = File.Open(fpath, FileMode.Open);
        return JsonSerializer.Deserialize<List<Question>>(fs);
    }

    public static List<QuestionDetail> GetQuestionDetails(string fpath)
    {
        using var fs = File.Open(fpath, FileMode.Open);
        var raw = JsonSerializer.Deserialize<QuestionDetailLevel0>(fs);
        return raw.Data.QuestionDetails;
    }

    public static List<CompanyQuestion> GetCompanyQuestions(string fpath)
    {
        using var fs = File.Open(fpath, FileMode.Open);
        var raw = JsonSerializer.Deserialize<CompanyLevel0>(fs);
        return raw.Data.Data.Questions;
    }
}